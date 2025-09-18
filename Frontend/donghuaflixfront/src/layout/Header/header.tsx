"use client"
import { useState } from "react"
import { Button } from "@/ui/button"
import { Input } from "@/ui/input"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/ui/dropdown-menu"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/ui/dialog"
import { Search, ChevronDown, Play } from "lucide-react"

export function Header() {
  const [isLoginOpen, setIsLoginOpen] = useState(false)

  return (
    <header className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-16 items-center justify-between px-4">
        {/* Logo */}
        <div className="flex items-center space-x-2">
          <div className="flex items-center justify-center w-8 h-8 bg-primary rounded-lg">
            <Play className="w-5 h-5 text-primary-foreground" />
          </div>
          <span className="text-xl font-bold text-foreground">DonghuaStream</span>
        </div>

        {/* Navigation */}
        <nav className="hidden md:flex items-center space-x-6">
          <a href="#" className="text-foreground hover:text-primary transition-colors">
            Início
          </a>

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

          <a href="#" className="text-foreground hover:text-primary transition-colors">
            Populares
          </a>
          <a href="#" className="text-foreground hover:text-primary transition-colors">
            Lançamentos
          </a>
        </nav>

        {/* Search and Login */}
        <div className="flex items-center space-x-4">
          <div className="relative hidden sm:block">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground w-4 h-4" />
            <Input placeholder="Buscar animes..." className="pl-10 w-64" />
          </div>

          <Dialog open={isLoginOpen} onOpenChange={setIsLoginOpen}>
            <DialogTrigger asChild>
              <Button>Entrar</Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-md">
              <DialogHeader>
                <DialogTitle>Entrar na sua conta</DialogTitle>
                <DialogDescription>
                  Faça login para acessar sua lista de favoritos e continuar assistindo
                </DialogDescription>
              </DialogHeader>
              <div className="space-y-4">
                <div>
                  <label htmlFor="email" className="text-sm font-medium">
                    Email
                  </label>
                  <Input id="email" type="email" placeholder="seu@email.com" />
                </div>
                <div>
                  <label htmlFor="password" className="text-sm font-medium">
                    Senha
                  </label>
                  <Input id="password" type="password" placeholder="••••••••" />
                </div>
                <Button className="w-full">Entrar</Button>
                <p className="text-center text-sm text-muted-foreground">
                  Não tem conta?{" "}
                  <a href="#" className="text-primary hover:underline">
                    Cadastre-se
                  </a>
                </p>
              </div>
            </DialogContent>
          </Dialog>
        </div>
      </div>
    </header>
  )
}