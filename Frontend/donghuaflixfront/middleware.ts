// middleware.ts - COMPATÍVEL COM SEU BACKEND
import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:7025';

// Interface baseada no seu ValidationResult
interface ValidationResponse {
    isValid: boolean;
    user?: {
        id: string;
        email: string;
        name: string;
        role: number; // UserRole enum do C#
    };
    error?: string;
}

async function validateTokenWithBackend(token: string): Promise<ValidationResponse> {
    try {
        const response = await fetch(`${API_BASE_URL}/api/user/validate-token`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        });

        if (response.ok) {
            return await response.json() as ValidationResponse;
        }
        
        return { isValid: false, error: `HTTP ${response.status}` };
    } catch (error) {
        console.error('Token validation error:', error);
        return { isValid: false, error: 'Network error' };
    }
}

export async function middleware(request: NextRequest) {
    const { pathname } = request.nextUrl;

    // Rotas estáticas - sem verificação
    if (pathname.startsWith('/_next') || 
        pathname.startsWith('/static') || 
        /\.(ico|png|jpg|jpeg|gif|webp|svg)$/i.test(pathname)) {
        return NextResponse.next();
    }

    const authToken = request.cookies.get('auth_token')?.value;

    // Definir rotas protegidas baseadas no seu sistema de roles
    const protectedRoutes = [
        '/dashboard',
        '/profile',
        '/admin' // Requer role Admin no backend
    ];

    const isProtectedRoute = protectedRoutes.some(route => 
        pathname.startsWith(route)
    );

    const publicRoutes = ['/login', '/register'];
    const isPublicRoute = publicRoutes.includes(pathname);

    // 🔐 VALIDAÇÃO PARA ROTAS PROTEGIDAS
    if (isProtectedRoute) {
        if (!authToken) {
            return redirectToLogin(request, pathname, 'no_token');
        }

        // ✅ VALIDAÇÃO COM SEU BACKEND ASP.NET
        const validation = await validateTokenWithBackend(authToken);
        
        if (!validation.isValid) {
            const response = redirectToLogin(request, pathname, 'invalid_token');
            response.cookies.delete('auth_token');
            return response;
        }

        // ✅ VERIFICAÇÃO DE ROLE PARA ROTAS ADMIN
        if (pathname.startsWith('/admin') && validation.user?.role !== 1) { // 1 = Admin
            return new NextResponse('Acesso negado', { status: 403 });
        }

        return NextResponse.next();
    }

    // 🔄 REDIRECIONAR SE AUTENTICADO EM ROTAS PÚBLICAS
    if (isPublicRoute && authToken) {
        const validation = await validateTokenWithBackend(authToken);
        if (validation.isValid) {
            return NextResponse.redirect(new URL('/dashboard', request.url));
        }
        
        // Token inválido - limpar e manter na rota pública
        const response = NextResponse.next();
        response.cookies.delete('auth_token');
        return response;
    }

    return NextResponse.next();
}

function redirectToLogin(request: NextRequest, fromPath: string, reason: string) {
    const loginUrl = new URL('/login', request.url);
    loginUrl.searchParams.set('redirect', fromPath);
    loginUrl.searchParams.set('reason', reason);
    return NextResponse.redirect(loginUrl);
}

export const config = {
    matcher: [
        '/((?!_next/static|_next/image|favicon.ico).*)',
    ],
};