import { Card, CardContent } from "@/ui/card"
import { Badge } from "@/ui/badge"
import { Star, Play } from "lucide-react"
import Link from "next/link"

const popularAnimes = [
  {
    id: 1,
    title: "Soul Land",
    rating: 9.2,
    year: "2023",
    episodes: "24 eps",
    image: "/soul-land-anime-poster-with-mystical-blue-theme.jpg",
    status: "Completo",
  },
  {
    id: 2,
    title: "Battle Through the Heavens",
    rating: 8.8,
    year: "2023",
    episodes: "36 eps",
    image: "/battle-through-the-heavens-anime-poster-with-fire-.jpg",
    status: "Em Andamento",
  },
  {
    id: 3,
    title: "Perfect World",
    rating: 9.0,
    year: "2023",
    episodes: "20 eps",
    image: "/perfect-world-anime-poster-with-ancient-chinese-el.jpg",
    status: "Completo",
  },
  {
    id: 4,
    title: "Swallowed Star",
    rating: 8.6,
    year: "2023",
    episodes: "28 eps",
    image: "/swallowed-star-anime-poster-with-space-and-stars-t.jpg",
    status: "Em Andamento",
  },
  {
    id: 5,
    title: "Martial Universe",
    rating: 8.4,
    year: "2023",
    episodes: "32 eps",
    image: "/martial-universe-anime-poster-with-martial-arts-th.jpg",
    status: "Completo",
  },
  {
    id: 6,
    title: "Spirit Cage",
    rating: 9.1,
    year: "2023",
    episodes: "16 eps",
    image: "/spirit-cage-anime-poster-with-post-apocalyptic-the.jpg",
    status: "Em Andamento",
  },
]

export function AnimeGrid() {
  return (
    <section className="py-12 bg-muted/30">
      <div className="container px-4 mx-auto">
        <h2 className="text-3xl font-bold mb-8">Populares Agora</h2>
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-6 ">
          {popularAnimes.map((anime) => (

            <Link key={anime.id} href={`/donghua/${anime.id}`}>
              <Card className="group cursor-pointer hover:scale-105 transition-all duration-300 hover:shadow-lg">
                <CardContent className="p-0">
                  <div className="relative overflow-hidden rounded-t-lg">
                    <img
                      src={anime.image || "/placeholder.svg"}
                      alt={anime.title}
                      className="w-full h-64 object-cover group-hover:scale-110 transition-transform duration-300"
                    />
                    <div className="absolute inset-0 bg-black/0 group-hover:bg-black/50 transition-colors duration-300 flex items-center justify-center">
                      <Play className="w-12 h-12 text-white opacity-0 group-hover:opacity-100 transition-opacity duration-300" />
                    </div>
                    <Badge
                      className={`absolute top-2 right-2 ${anime.status === "Completo" ? "bg-secondary" : "bg-primary"}`}
                    >
                      {anime.status}
                    </Badge>
                  </div>
                  <div className="p-3 space-y-2">
                    <h3 className="font-semibold text-sm line-clamp-2 text-balance">{anime.title}</h3>
                    <div className="flex items-center justify-between text-xs text-muted-foreground">
                      <span>{anime.year}</span>
                      <span>{anime.episodes}</span>
                    </div>
                    <div className="flex items-center space-x-1">
                      <Star className="w-3 h-3 fill-yellow-400 text-yellow-400" />
                      <span className="text-xs font-medium">{anime.rating}</span>
                    </div>
                  </div>
                </CardContent>
              </Card>
            </Link>

          ))}
        </div>
      </div>
    </section>
  )
}
