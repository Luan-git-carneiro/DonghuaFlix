import { AnimeCard } from "@/components/ui/donghua-card"
import { Sparkles } from "lucide-react"

const newReleases = [
  {
    id: 9,
    title: "Perfect World",
    image: "/perfect-world-anime-poster-new-release.jpg",
    rating: 8.6,
    year: 2024,
    episodes: 12,
  },
  {
    id: 10,
    title: "Swallowed Star",
    image: "/swallowed-star-anime-poster-sci-fi.jpg",
    rating: 8.3,
    year: 2024,
    episodes: 8,
  },
  {
    id: 11,
    title: "Battle Through the Heavens Season 5",
    image: "/battle-through-heavens-anime-poster-season-5.jpg",
    rating: 8.7,
    year: 2024,
    episodes: 24,
  },
  {
    id: 12,
    title: "Tales of Dark River",
    image: "/tales-dark-river-anime-poster-mystery.jpg",
    rating: 8.2,
    year: 2024,
    episodes: 10,
  },
  {
    id: 13,
    title: "Stellar Transformations Season 4",
    image: "/stellar-transformations-anime-poster-cultivation.jpg",
    rating: 8.5,
    year: 2024,
    episodes: 16,
  },
  {
    id: 14,
    title: "The Legend of Hei",
    image: "/legend-of-hei-anime-poster-action-adventure.jpg",
    rating: 8.9,
    year: 2024,
    episodes: 12,
  },
]

export default function LancamentosPage() {
  return (
    <div className="container px-4 py-8">
      <div className="mb-8">
        <div className="flex items-center gap-3 mb-2">
          <Sparkles className="w-8 h-8 text-accent" />
          <h1 className="text-4xl font-bold text-balance">Novos Lançamentos</h1>
        </div>
        <p className="text-muted-foreground text-lg">Os donghua mais recentes e aguardados de 2024</p>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4">
        {newReleases.map((anime) => (
          <AnimeCard key={anime.id} anime={anime} />
        ))}
      </div>
    </div>
  )
}
