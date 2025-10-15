'use client'

import Cookies from 'universal-cookie';

const cookies = new Cookies();

// 1. Defina a constante para o ambiente *aqui*
// O linter deve aceitar isso melhor, pois o valor se torna uma constante simples
// O Next.js garante que este valor será o correto durante o build.
// @ts-ignore
const IS_PRODUCTION = process.env.NODE_ENV === 'production';

interface AuthData {
    token: string;
    role: number;
    expiresAt: string;
    user: any;
}

export const secureStorage = {
     /**
   * Armazena dados de autenticação de forma segura
   * @param data - Dados de autenticação retornados pelo backend
   * 
   * SEGURANÇA:
   * - Token vai para cookie (mais seguro contra XSS)
   * - Dados não-sensíveis vão para sessionStorage (limpa ao fechar aba)
   */


    // Armazenar com expiração (mais seguro que localStorage)
    setAuthData: (data: AuthData) => {
        // Criar data de expiração do cookie
    const expires = new Date(data.expiresAt);
    
    // Armazenar token em cookie
    // secure: true = só HTTPS em produção
    // sameSite: 'strict' = proteção contra CSRF
    cookies.set('auth_token', data.token, { 
      path: '/',  // Cookie disponível em todo o site
      expires,    // Data de expiração
      secure: IS_PRODUCTION, // HTTPS em produção
      sameSite: 'strict' // Proteção contra CSRF
    });
    
    // Armazenar dados não-sensíveis no sessionStorage
    // SessionStorage é melhor que localStorage pois limpa ao fechar a aba
    sessionStorage.setItem('auth_user', JSON.stringify({
      user: data.user,
      role: data.role,
      expiresAt: data.expiresAt
    }));
  },

    /**
   * Recupera dados de autenticação armazenados
   * @returns Dados de auth ou null se não existir/expirado
   */

    getAuthData: () => {
    // Buscar token do cookie
    const token = cookies.get('auth_token');
    
    // Buscar dados do sessionStorage
    const sessionData = sessionStorage.getItem('auth_user');
    
    // Se não tiver token ou dados da sessão, retorna null
    if (!token || !sessionData) {
      return null;
    }
    
    try {
      // Parse dos dados do sessionStorage
      const { user, role, expiresAt } = JSON.parse(sessionData);
      
      // Retornar todos os dados
      return { 
        token, 
        user, 
        role, 
        expiresAt 
      };
    } catch (error) {
      // Se der erro no parse, dados corrompidos
      console.error('Erro ao recuperar dados de auth:', error);
      return null;
    }
  },
    
  /**
   * Limpa todos os dados de autenticação (logout)
   */

  
  clear: () => {
    // Remover cookie do token
    cookies.remove('auth_token', { path: '/' });
    
    // Limpar sessionStorage
    sessionStorage.removeItem('auth_user');
  }

}
