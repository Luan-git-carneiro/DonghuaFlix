import { Button } from "@/ui/button"
import { LogOut } from "lucide-react"
import Link from "next/link"
import { usePathname } from "next/navigation"

interface NavItem {

    href: string;
    label: string;
    icon: React.ComponentType<React.SVGProps<SVGSVGElement>>;
}

interface PropsNavItem {

    items: NavItem[];
    onLogout: () => void;
}
export default function PainelAdmin({ items: navItems , onLogout}: PropsNavItem)
{
    const pathname = usePathname()

    return(
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
                    }
                )
            }
            </nav>
            <div className="absolute bottom-4 left-4 right-4">
            <Button variant="outline" className="w-full justify-start gap-3 bg-transparent" onClick={onLogout}>
                <LogOut className="h-5 w-5" />
                Sair
            </Button>
            </div>
        </aside>
    )
}