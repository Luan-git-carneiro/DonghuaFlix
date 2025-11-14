"use client"

import type React from "react"

import { useAuth } from "@/featured/auth/hooks/use-auth"
import { useRouter, usePathname } from "next/navigation"
import { useEffect } from "react"
import Link from "next/link"
import { LayoutDashboard, Film, Users, Plus, Settings, LogOut } from "lucide-react"
import { Button } from "@/ui/button"
import { UserRole } from "@/featured/auth/types/auth.types"
import PainelAdmin from "@/featured/admin/painel"

const navItems = [
  { href: "/admin", label: "Dashboard", icon: LayoutDashboard },
  { href: "/admin/donghua", label: "Gerenciar Donghua", icon: Film },
  { href: "/admin/adicionar", label: "Adicionar Donghua", icon: Plus },
  { href: "/admin/usuarios", label: "Gerenciar Usuários", icon: Users },
  { href: "/admin/configuracoes", label: "Configurações", icon: Settings },
]

export default function AdminLayout({ children, }: {  children: React.ReactNode }) {
  const { user, isLoading, logout , hasRole } = useAuth()
  const router = useRouter()


  useEffect(() => {
    if (!isLoading && (!user || !hasRole(UserRole.ADMIN))) {
      router.push("/")
    }
  }, [user, isLoading, router])

  if (isLoading) {
    return (
      <div className="flex min-h-screen items-center justify-center">
        <div className="text-center">
          <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary border-t-transparent mx-auto mb-4" />
          <p className="text-muted-foreground">Carregando...</p>
        </div>
      </div>
    )
  }

  if (!user || !hasRole(UserRole.ADMIN)) {
    return null
  }

  const handleLogout = () => {
    logout()
    router.push("/")
  }

  return (
    <div className="flex min-h-screen bg-muted/30">
      {/* Sidebar */}
      <PainelAdmin 
        items={navItems} 
        onLogout={handleLogout}
      />

      {/* Main Content */}
      <main className="flex-1">
        <div className="border-b bg-card">
          <div className="flex h-16 items-center px-8">
            <div className="flex items-center gap-4">
              <div>
                <p className="text-sm font-medium">Bem-vindo, {user.name}</p>
                <p className="text-xs text-muted-foreground">Administrador</p>
              </div>
            </div>
          </div>
        </div>
        <div className="p-8">{children}</div>
      </main>
    </div>
  )
}
