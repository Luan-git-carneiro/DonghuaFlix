'use client'

import { useState, useEffect, useCallback } from 'react';
import { secureStorage } from '@/lib/secure-storage';
import {
  LoginCredentials,
  RegisterData,
  AuthState,
  LoginResponse,
  AuthHook
} from '../types/auth.types';
import { authApi } from '../api/auth.api';



export function useAuth(): AuthHook  {
  const [state, setState] = useState<AuthState>({
    user: null,
    token: null,
    role: null,
    expiresAt: null,
    isLoading: true,
    error: null
  }
  )

  // Verificar se token está expirado
  const isTokenExpired = useCallback((expiresAt: string | null): boolean => {
    if (!expiresAt) return true;
    return new Date() > new Date(expiresAt);
  }, []);


  // Carregar sessão ao inicializar
  useEffect(() => {
    const initialAuth = async () => {
      try {
        const authData = secureStorage.getAuthData();

        if (authData && !isTokenExpired(authData.expiresAt)) {
          setState(prev => (
            {
              ...prev,
              user: authData.user,
              token: authData.token,
              role: authData.role,
              expiresAt: authData.expiresAt,
              isLoading: false,
            }
          ))

        } else {
          // Sessão expirada ou inválida
          secureStorage.clear();
          setState(prev => ({ ...prev, isLoading: false }))
        }
      } catch (error) {
        console.error('Error initializing auth:', error);
        secureStorage.clear();
        setState(prev => ({ ...prev, isLoading: false }));
      }

    };

    initialAuth();

  }, [])


  const login = async (credentials: LoginCredentials): Promise<{ success: boolean; message?: string }> => {

    setState(prev => ({ ...prev, isLoading: true, error: null }))

    try {
      const result: LoginResponse = await authApi.login(credentials);

      if (result.isSuccess && result.data) {
        // Salvar dados de forma segura
        secureStorage.setAuthData(result.data);

        setState(prev => ({
          ...prev,
          user: result.data ? result.data.user : null,
          token: result.data ? result.data.token : null,
          role: result.data ? result.data.role : null,
          expiresAt: result.data ? result.data.expiresAt : null,
          isLoading: false,
          error: null,
        }));

        return { success: true };
      } else {
        const errorMessage = result.message || 'Login failed';
        setState(prev => ({
          ...prev,
          isLoading: false,
          error: errorMessage,
        }));

        return { success: false, message: errorMessage };
      }
    } catch (error) {
      const errorMessage = error instanceof Error ? error.message : 'Network error';
      setState(prev => ({
        ...prev,
        isLoading: false,
        error: errorMessage,
      }));

      return { success: false, message: errorMessage };
    }

  }


  // Register seguro
  const register = async (userData: RegisterData): Promise<{ success: boolean; message?: string }> => {
    setState(prev => ({ ...prev, isLoading: true, error: null }));

    try {
      const result: LoginResponse = await authApi.register(userData);

      if (result.isSuccess && result.data) {
        secureStorage.setAuthData(result.data);

        setState(prev => (
          {
            ...prev,
            user: result.data ? result.data.user : null,
            token: result.data ? result.data.token : null,
            role: result.data ? result.data.role : null,
            expiresAt: result.data ? result.data.expiresAt : null,
            isLoading: false,
            error: null,
          }
        )
        );

        return { success: true };
      } else {
        const errorMessage = result.message || 'Registration failed';
        setState(prev => ({
          ...prev,
          isLoading: false,
          error: errorMessage,
        }));

        return { success: false, message: errorMessage };
      }
    } catch (error) {
      const errorMessage = error instanceof Error ? error.message : 'Network error';
      setState(prev => ({
        ...prev,
        isLoading: false,
        error: errorMessage,
      }));

      return { success: false, message: errorMessage };
    }

    
  }

    // Logout

    const logout = useCallback((): void => {
      secureStorage.clear();
      setState({
        user: null,
        token: null,
        role: null,
        expiresAt: null,
        isLoading: false,
        error: null,
      });
    }, []);


      // Verificar autenticação
    const isAuthenticated = useCallback((): boolean => {
      return !!state.token && !isTokenExpired(state.expiresAt);
    }, [state.token, state.expiresAt, isTokenExpired]);

      // Verificar role
    const hasRole = useCallback((requiredRole: number): boolean => {
      return state.role === requiredRole && isAuthenticated();
    }, [state.role, isAuthenticated]);
    

    //Metodo para fazer chamadas autenticadas
    const authenticatedFetch = useCallback(async (
      url: string, 
      options: RequestInit = {}
    ): Promise<Response> => {
      if (!isAuthenticated()) {
        logout();
        throw new Error('Authentication required');
      }
  
      const authData = secureStorage.getAuthData();
      if (!authData) {
        logout();
        throw new Error('Authentication data not found');
      }
  
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}${url}`, {
        ...options,
        headers: {
          'Authorization': `Bearer ${authData.token}`,
          'Content-Type': 'application/json',
          ...options.headers,
        },
      });
  
      if (response.status === 401) {
        // Token inválido ou expirado
        logout();
        throw new Error('Session expired');
      }
  
      return response;
    }, [isAuthenticated, logout]);

    return {
      // Dados públicos seguros
      user: state.user,
      isLoading: state.isLoading,
      error: state.error,

      // Status derivados
      isAuthenticated: isAuthenticated(),
      hasRole,

      
      // ✅ Expõe as funções
      login,
      register,
      logout,
      authenticatedFetch

    };
}
