// src/features/auth/contexts/auth-context.tsx
'use client';

import { createContext, useContext, ReactNode } from 'react';
import { useAuth } from '@/featured/auth/hooks/use-auth';
import { AuthHook } from '@/featured/auth/types/auth.types';



const AuthContext = createContext<AuthHook | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const auth = useAuth();

  return (
    <AuthContext.Provider value={auth}>
      {children}
    </AuthContext.Provider>
  );
}

// Hook personalizado para usar o contexto
export function useAuthContext(): AuthHook {
  const context = useContext(AuthContext);
  
  if (context === undefined) {
    throw new Error('useAuthContext must be used within an AuthProvider');
  }
  
  return context;
}

export { useAuth };
