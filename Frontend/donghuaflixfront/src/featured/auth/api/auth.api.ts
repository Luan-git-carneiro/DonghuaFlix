import { LoginCredentials, RegisterData, LoginResponse } from '../types/auth.types'

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL

export const authApi = {

  async request(endpoint: string, options: RequestInit = {}) {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      headers: {
        'Content-Type': 'application/json',
        ...options.headers,
      },
      ...options,
    });

    return response.json();
  },

   async login(credentials: LoginCredentials): Promise<LoginResponse> {
    return this.request('/api/user/login', {
      method: 'POST',
      body: JSON.stringify(credentials),
    });
  },

  async register(userData: RegisterData): Promise<LoginResponse> {
    return this.request('/api/user/register', {
      method: 'POST',
      body: JSON.stringify(userData),
    });
  },

  
};
