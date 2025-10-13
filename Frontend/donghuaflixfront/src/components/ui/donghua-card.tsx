import Link from "next/link"
import Image from "next/image"
import { Card, CardContent } from "@/ui/card"
import { Star } from "lucide-react"

interface AnimeCardProps {
  anime: {
    id: number
    title: string
    image: string
    rating: number
    year: number
    episodes: number
  }
}

export function AnimeCard({ anime }: AnimeCardProps) {
  return (
    <Link href={`/anime/${anime.id}`}>
      <Card className="overflow-hidden hover:border-primary transition-all hover:shadow-lg cursor-pointer h-full">
        <div className="relative aspect-[2/3] overflow-hidden">
          <Image
            src={anime.image || "/placeholder.svg"}
            alt={anime.title}
            fill
            className="object-cover transition-transform hover:scale-105"
          />
          <div className="absolute top-2 right-2 bg-black/80 backdrop-blur-sm px-2 py-1 rounded-md flex items-center gap-1">
            <Star className="w-3 h-3 text-yellow-500 fill-yellow-500" />
            <span className="text-xs font-semibold text-white">{anime.rating}</span>
          </div>
        </div>
        <CardContent className="p-3">
          <h3 className="font-semibold text-sm line-clamp-2 mb-1">{anime.title}</h3>
          <div className="flex items-center gap-2 text-xs text-muted-foreground">
            <span>{anime.year}</span>
            <span>•</span>
            <span>{anime.episodes} eps</span>
          </div>
        </CardContent>
      </Card>
    </Link>
  )
}
