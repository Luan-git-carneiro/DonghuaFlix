import { notFound } from "next/navigation"
import Link from "next/link"
import { Button } from "@/ui/button"
import { Card, CardContent } from "@/ui/card"
import { ChevronLeft, ChevronRight, Play } from "lucide-react"

// Dados simulados dos animes (mesmo do arquivo anterior)
const animeDatabase = {
  "1": {
    id: "1",
    title: "Mo Dao Zu Shi",
    englishTitle: "Grandmaster of Demonic Cultivation",
    image: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
    episodes: 23,
    episodesList: Array.from({ length: 23 }, (_, i) => ({
      number: i + 1,
      title: `Episódio ${i + 1}`,
      duration: "24 min",
      thumbnail: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
      description: `Acompanhe Wei Wuxian em sua jornada neste emocionante episódio ${i + 1}.`,
      videoUrl: "https://www.youtube.com/embed/dQw4w9WgXcQ",
    })),
  },
  "2": {
    id: "2",
    title: "The King's Avatar",
    englishTitle: "Quan Zhi Gao Shou",
    image: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
    episodes: 12,
    episodesList: Array.from({ length: 12 }, (_, i) => ({
      number: i + 1,
      title: `Episódio ${i + 1}`,
      duration: "24 min",
      thumbnail: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
      description: `Ye Xiu continua sua jornada de volta ao topo no episódio ${i + 1}.`,
      videoUrl: "https://www.youtube.com/embed/dQw4w9WgXcQ",
    })),
  },
  "3": {
    id: "3",
    title: "Heaven Official's Blessing",
    englishTitle: "Tian Guan Ci Fu",
    image: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
    episodes: 11,
    episodesList: Array.from({ length: 11 }, (_, i) => ({
      number: i + 1,
      title: `Episódio ${i + 1}`,
      duration: "25 min",
      thumbnail: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
      description: `Xie Lian enfrenta novos desafios no reino celestial no episódio ${i + 1}.`,
      videoUrl: "https://www.youtube.com/embed/dQw4w9WgXcQ",
    })),
  },
}

export default async function EpisodePage({
  params,
}: {
  params: { id: string; numeroEpisodio: string }
}) {

    const { id , numeroEpisodio } = await params ; 

  const anime = animeDatabase[id as keyof typeof animeDatabase]
  const episodeNum = Number.parseInt(numeroEpisodio)

  if (!anime || isNaN(episodeNum) || episodeNum < 1 || episodeNum > anime.episodes) {
    notFound()
  }

  const currentEpisode = anime.episodesList[episodeNum - 1]
  const hasPrevious = episodeNum > 1
  const hasNext = episodeNum < anime.episodes

  return (
    <div className="min-h-screen bg-background">
      <div className="container px-4 py-8">
        {/* Breadcrumb */}
        <div className="mb-6">
          <Link
            href={`/donghua/${anime.id}`}
            className="text-sm text-muted-foreground hover:text-primary transition-colors"
          >
            ← Voltar para {anime.title}
          </Link>
        </div>

        <div className="grid lg:grid-cols-[1fr_350px] gap-8">
          {/* Player Section */}
          <div className="space-y-6">
            {/* Video Player */}
            <Card className="overflow-hidden bg-black">
              <div className="relative aspect-video">
                <iframe
                  src={currentEpisode.videoUrl}
                  className="w-full h-full"
                  allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                  allowFullScreen
                  title={currentEpisode.title}
                />
              </div>
            </Card>

            {/* Episode Info */}
            <div className="space-y-4">
              <div>
                <h1 className="text-3xl font-bold text-balance mb-2">
                  {anime.title} - {currentEpisode.title}
                </h1>
                <p className="text-muted-foreground">{currentEpisode.description}</p>
              </div>

              {/* Navigation Buttons */}
              <div className="flex items-center gap-4">
                {hasPrevious ? (
                  <Button asChild variant="outline" size="lg">
                    <Link href={`/donghua/${anime.id}/episodio/${episodeNum - 1}`}>
                      <ChevronLeft className="w-5 h-5 mr-2" />
                      Episódio Anterior
                    </Link>
                  </Button>
                ) : (
                  <Button variant="outline" size="lg" disabled>
                    <ChevronLeft className="w-5 h-5 mr-2" />
                    Episódio Anterior
                  </Button>
                )}

                {hasNext ? (
                  <Button asChild size="lg">
                    <Link href={`/donghua/${anime.id}/episodio/${episodeNum + 1}`}>
                      Próximo Episódio
                      <ChevronRight className="w-5 h-5 ml-2" />
                    </Link>
                  </Button>
                ) : (
                  <Button size="lg" disabled>
                    Próximo Episódio
                    <ChevronRight className="w-5 h-5 ml-2" />
                  </Button>
                )}
              </div>
            </div>
          </div>

          {/* Episodes List Sidebar */}
          <div className="space-y-4">
            <h2 className="text-xl font-bold">Todos os Episódios</h2>
            <div className="space-y-2 max-h-[600px] overflow-y-auto pr-2">
              {anime.episodesList.map((episode) => (
                <Link
                  key={episode.number}
                  href={`/donghua/${anime.id}/episodio/${episode.number}`}
                  className={`block ${episode.number === episodeNum ? "pointer-events-none" : ""}`}
                >
                  <Card
                    className={`group cursor-pointer hover:bg-accent transition-colors ${
                      episode.number === episodeNum ? "bg-primary/10 border-primary" : ""
                    }`}
                  >
                    <CardContent className="p-3">
                      <div className="flex items-center space-x-3">
                        <div className="relative w-24 h-16 rounded overflow-hidden flex-shrink-0">
                          <img
                            src={episode.thumbnail || "/placeholder.svg"}
                            alt={episode.title}
                            className="w-full h-full object-cover"
                          />
                          {episode.number !== episodeNum && (
                            <div className="absolute inset-0 bg-black/0 group-hover:bg-black/50 transition-colors flex items-center justify-center">
                              <Play className="w-6 h-6 text-white opacity-0 group-hover:opacity-100 transition-opacity" />
                            </div>
                          )}
                          {episode.number === episodeNum && (
                            <div className="absolute inset-0 bg-black/50 flex items-center justify-center">
                              <span className="text-white text-xs font-bold">ASSISTINDO</span>
                            </div>
                          )}
                        </div>
                        <div className="flex-1 min-w-0">
                          <h4 className="font-semibold text-sm truncate">{episode.title}</h4>
                          <p className="text-xs text-muted-foreground">{episode.duration}</p>
                        </div>
                      </div>
                    </CardContent>
                  </Card>
                </Link>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
