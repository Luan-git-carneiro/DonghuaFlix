import Link from "next/link";

export function Footer() {
    return (
      <footer className="bg-card border-t">
        <div className="container px-4 py-12 mx-auto">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            <div className="space-y-4">
              <h3 className="text-lg font-semibold">DonghuaStream</h3>
              <p className="text-sm text-muted-foreground text-pretty">
                A melhor plataforma para assistir animes chineses (donghua) com legendas em português.
              </p>
            </div>
  
            <div className="space-y-4">
              <h4 className="font-medium">Navegação</h4>
              <ul className="space-y-2 text-sm text-muted-foreground">
                <li>
                  <Link href="/" className="hover:text-primary transition-colors">
                    Início
                  </Link>
                </li>
                <li>
                  <Link href="/generos" className="hover:text-primary transition-colors">
                    Gêneros
                  </Link>
                </li>
                <li>
                  <Link href="/populares" className="hover:text-primary transition-colors">
                    Populares
                  </Link>
                </li>
                <li>
                  <Link href="lancamentos" className="hover:text-primary transition-colors">
                    Lançamentos
                  </Link>
                </li>
              </ul>
            </div>
  
            <div className="space-y-4">
              <h4 className="font-medium">Gêneros</h4>
              <ul className="space-y-2 text-sm text-muted-foreground">
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Ação
                  </a>
                </li>
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Romance
                  </a>
                </li>
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Fantasia
                  </a>
                </li>
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Comédia
                  </a>
                </li>
              </ul>
            </div>
  
            <div className="space-y-4">
              <h4 className="font-medium">Suporte</h4>
              <ul className="space-y-2 text-sm text-muted-foreground">
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Termos de Uso
                  </a>
                </li>
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Política de Privacidade
                  </a>
                </li>
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    Contato
                  </a>
                </li>
                <li>
                  <a href="#" className="hover:text-primary transition-colors">
                    FAQ
                  </a>
                </li>
              </ul>
            </div>
          </div>
  
          <div className="border-t mt-8 pt-8 text-center text-sm text-muted-foreground">
            <p>&copy; 2024 DonghuaStream. Todos os direitos reservados.</p>
          </div>
        </div>
      </footer>
    )
  }