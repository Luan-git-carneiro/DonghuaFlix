"use client"

import Link from "next/link"
import { Button } from "@/ui/button"
import { Input } from "@/ui/input"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/ui/dropdown-menu"
import { Search, ChevronDown, Play, User, LogOut } from "lucide-react"
import { useAuth } from "@/common/contexts/auth-context"



export function Header() {
  const { user, logout } = useAuth()

  return (
    <header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-16 items-center justify-between px-4">
        {/* Logo */}
        <Link href="/" className="flex items-center space-x-2">
          <div className="flex items-center justify-center w-8 h-8 bg-primary rounded-lg">
            <Play className="w-5 h-5 text-primary-foreground" />
          </div>
          <span className="text-xl font-bold text-foreground">DonghuaStream</span>
        </Link>

        {/* Navigation */}
        <nav className="hidden md:flex items-center space-x-6">
          <Link href="/" className="text-foreground hover:text-primary transition-colors">
            Início
          </Link>

          <DropdownMenu>
            <DropdownMenuTrigger className="flex items-center space-x-1 text-foreground hover:text-primary transition-colors">
              <span>Gêneros</span>
              <ChevronDown className="w-4 h-4" />
            </DropdownMenuTrigger>
            <DropdownMenuContent>
              <DropdownMenuItem>Ação</DropdownMenuItem>
              <DropdownMenuItem>Romance</DropdownMenuItem>
              <DropdownMenuItem>Fantasia</DropdownMenuItem>
              <DropdownMenuItem>Comédia</DropdownMenuItem>
              <DropdownMenuItem>Drama</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>

          <Link href="#" className="text-foreground hover:text-primary transition-colors">
            Populares
          </Link>
          <Link href="#" className="text-foreground hover:text-primary transition-colors">
            Lançamentos
          </Link>
        </nav>

        {/* Search and User */}
        <div className="flex items-center space-x-4">
          <div className="relative hidden sm:block">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground w-4 h-4" />
            <Input placeholder="Buscar animes..." className="pl-10 w-64" />
          </div>

          {user ? (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="ghost" className="flex items-center space-x-2">
                  <User className="w-4 h-4" />
                  <span className="hidden sm:inline">{user.name}</span>
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuItem>
                  <User className="w-4 h-4 mr-2" />
                  Meu Perfil
                </DropdownMenuItem>
                <DropdownMenuItem>Meus Favoritos</DropdownMenuItem>
                <DropdownMenuItem>Histórico</DropdownMenuItem>
                <DropdownMenuItem onClick={logout}>
                  <LogOut className="w-4 h-4 mr-2" />
                  Sair
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          ) : (
            <div className="flex items-center space-x-2">
              <Button variant="ghost" asChild>
                <Link href="/register">Cadastrar</Link>
              </Button>
              <Button asChild>
                <Link href="/login">Entrar</Link>
              </Button>
            </div>
          )}
        </div>
      </div>
    </header>
  )
}