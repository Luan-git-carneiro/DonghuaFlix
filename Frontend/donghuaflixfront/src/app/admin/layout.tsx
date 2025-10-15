"use client"

import type React from "react"

import { useAuth } from "@/featured/auth/hooks/use-auth"
import { useRouter, usePathname } from "next/navigation"
import { useEffect } from "react"
import Link from "next/link"
import { LayoutDashboard, Film, Users, Plus, Settings, LogOut } from "lucide-react"
import { Button } from "@/ui/button"
import { UserRole } from "@/featured/auth/types/auth.types"

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
  const pathname = usePathname()

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

  return (
    <div className="flex min-h-screen bg-muted/30">
      {/* Sidebar */}
      <aside className="w-64 border-r bg-card">
        <div className="flex h-16 items-center border-b px-6">
          <h1 className="text-xl font-bold text-primary">Painel Admin</h1>
        </div>
        <nav className="space-y-1 p-4">
          {navItems.map((item) => {
            const Icon = item.icon
            const isActive = pathname === item.href
            return (
              <Link
                key={item.href}
                href={item.href}
                className={`flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors ${
                  isActive
                    ? "bg-primary text-primary-foreground"
                    : "text-muted-foreground hover:bg-muted hover:text-foreground"
                }`}
              >
                <Icon className="h-5 w-5" />
                {item.label}
              </Link>
            )
          })}
        </nav>
        <div className="absolute bottom-4 left-4 right-4">
          <Button variant="outline" className="w-full justify-start gap-3 bg-transparent" onClick={logout}>
            <LogOut className="h-5 w-5" />
            Sair
          </Button>
        </div>
      </aside>

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
