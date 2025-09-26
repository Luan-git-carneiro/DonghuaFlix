export interface User {
    id: string
    name: string
    email: string
    createdAt: string  // ✅ Agora inclui
  }
  
  export interface LoginCredentials {
    email: string
    password: string
  }
  
  export interface RegisterData {
    email: string
    password: string
    name: string
  }
  
  export interface LoginResponse {
    isSuccess: boolean    // ✅ Corrigido para isSuccess
    message: string
    errorCode?: string | null   // ✅ Adicionado
    timestamp: string     // ✅ Adicionado
    data?: {              // ✅ Estrutura aninhada correta
      token: string
      role: number        // ✅ Adicionado
      expiresAt: string   // ✅ Adicionado
      user: User
    } | null
  }
  
  export interface AuthState {
    user: User | null
    token: string | null
    role: number | null   // ✅ Novo campo
    expiresAt: string | null // ✅ Novo campo
    isLoading: boolean
    error: string | null
  }

  export interface  AuthHook
  {
    user: User | null
    isLoading: boolean
    error: string | null

    isAuthenticated: boolean,
    hasRole: (requiredRole: number) => boolean

    login: (credentials: LoginCredentials) => Promise<{success: boolean ; message?: string | null}>
    register: (userData: RegisterData) => Promise<{success: boolean ; message?: string | null}>
    logout: () => void
    authenticatedFetch: (url: string, options?: RequestInit) => Promise<Response>
  }