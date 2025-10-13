import Link from "next/link"
import { Card, CardContent } from "@/ui/card"
import { Swords, Heart, Wand2, Laugh, Drama, Zap, Ghost, Sparkles } from "lucide-react"

const genres = [
  {
    name: "Ação",
    icon: Swords,
    count: 156,
    color: "text-red-500",
    bgColor: "bg-red-500/10",
  },
  {
    name: "Romance",
    icon: Heart,
    count: 89,
    color: "text-pink-500",
    bgColor: "bg-pink-500/10",
  },
  {
    name: "Fantasia",
    icon: Wand2,
    count: 203,
    color: "text-purple-500",
    bgColor: "bg-purple-500/10",
  },
  {
    name: "Comédia",
    icon: Laugh,
    count: 124,
    color: "text-yellow-500",
    bgColor: "bg-yellow-500/10",
  },
  {
    name: "Drama",
    icon: Drama,
    count: 98,
    color: "text-blue-500",
    bgColor: "bg-blue-500/10",
  },
  {
    name: "Aventura",
    icon: Zap,
    count: 187,
    color: "text-orange-500",
    bgColor: "bg-orange-500/10",
  },
  {
    name: "Sobrenatural",
    icon: Ghost,
    count: 142,
    color: "text-indigo-500",
    bgColor: "bg-indigo-500/10",
  },
  {
    name: "Cultivação",
    icon: Sparkles,
    count: 231,
    color: "text-cyan-500",
    bgColor: "bg-cyan-500/10",
  },
]

export default function GenerosPage() {
  return (
    <div className="container px-4 py-8">
      <div className="mb-8">
        <h1 className="text-4xl font-bold mb-2 text-balance">Explorar por Gênero</h1>
        <p className="text-muted-foreground text-lg">
          Descubra donghua por categoria e encontre seu próximo anime favorito
        </p>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
        {genres.map((genre) => {
          const Icon = genre.icon
          return (
            <Link key={genre.name} href={`/generos/${genre.name.toLowerCase()}`}>
              <Card className="hover:border-primary transition-all hover:shadow-lg cursor-pointer h-full">
                <CardContent className="p-6">
                  <div className={`w-16 h-16 rounded-lg ${genre.bgColor} flex items-center justify-center mb-4`}>
                    <Icon className={`w-8 h-8 ${genre.color}`} />
                  </div>
                  <h3 className="text-xl font-semibold mb-1">{genre.name}</h3>
                  <p className="text-sm text-muted-foreground">{genre.count} animes</p>
                </CardContent>
              </Card>
            </Link>
          )
        })}
      </div>
    </div>
  )
}
