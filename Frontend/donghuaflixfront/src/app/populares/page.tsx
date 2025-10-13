import { AnimeCard } from "@/components/ui/donghua-card"
import { Star } from "lucide-react"

const popularAnimes = [
  {
    id: 1,
    title: "Mo Dao Zu Shi",
    image: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
    rating: 9.2,
    year: 2018,
    episodes: 23,
  },
  {
    id: 2,
    title: "The King's Avatar",
    image: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
    rating: 8.9,
    year: 2017,
    episodes: 12,
  },
  {
    id: 3,
    title: "Heaven Official's Blessing",
    image: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
    rating: 9.0,
    year: 2020,
    episodes: 11,
  },
  {
    id: 4,
    title: "Link Click",
    image: "/link-click-anime-poster-time-travel.jpg",
    rating: 9.1,
    year: 2021,
    episodes: 11,
  },
  {
    id: 5,
    title: "Scissor Seven",
    image: "/scissor-seven-anime-poster-comedy-action.jpg",
    rating: 8.7,
    year: 2018,
    episodes: 10,
  },
  {
    id: 6,
    title: "The Daily Life of the Immortal King",
    image: "/immortal-king-anime-poster-cultivation.jpg",
    rating: 8.5,
    year: 2020,
    episodes: 15,
  },
  {
    id: 7,
    title: "Fog Hill of Five Elements",
    image: "/fog-hill-five-elements-anime-poster-martial-arts.jpg",
    rating: 8.8,
    year: 2020,
    episodes: 3,
  },
  {
    id: 8,
    title: "Soul Land",
    image: "/soul-land-anime-poster-fantasy-adventure.jpg",
    rating: 8.4,
    year: 2018,
    episodes: 265,
  },
]

export default function PopularesPage() {
  return (
    <div className="container px-4 py-8">
      <div className="mb-8">
        <div className="flex items-center gap-3 mb-2">
          <Star className="w-8 h-8 text-primary fill-primary" />
          <h1 className="text-4xl font-bold text-balance">Animes Populares</h1>
        </div>
        <p className="text-muted-foreground text-lg">Os donghua mais assistidos e bem avaliados pela comunidade</p>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-4">
        {popularAnimes.map((anime) => (
          <AnimeCard key={anime.id} anime={anime} />
        ))}
      </div>
    </div>
  )
}
