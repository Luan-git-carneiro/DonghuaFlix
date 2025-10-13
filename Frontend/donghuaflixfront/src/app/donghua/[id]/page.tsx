import { notFound } from "next/navigation"
import { Button } from "@/ui/button"
import { Badge } from "@/ui/badge"
import { Card, CardContent } from "@/ui/card"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Play, Plus, Share2, Star, Calendar, Film, Clock } from "lucide-react"

// Dados simulados dos animes
const animeDatabase = {
  "1": {
    id: "1",
    title: "Mo Dao Zu Shi",
    englishTitle: "Grandmaster of Demonic Cultivation",
    description:
      "Wei Wuxian, conhecido como o Yiling Patriarch, retorna ao mundo dos vivos para desvendar mistérios do passado. Treze anos após sua morte, Wei Wuxian é invocado de volta através de um ritual proibido. Agora em um novo corpo, ele deve descobrir a verdade sobre sua morte e os eventos que levaram à sua queda.",
    image: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
    banner: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
    rating: 9.2,
    year: "2018",
    status: "Completo",
    episodes: 23,
    duration: "24 min",
    studio: "Tencent Penguin Pictures",
    genres: ["Fantasia", "Drama", "Ação", "Sobrenatural"],
    synopsis:
      "A história segue Wei Wuxian, um jovem cultivador que, após ser traído e morto, retorna ao mundo dos vivos treze anos depois. Junto com Lan Wangji, seu antigo colega, ele investiga uma série de mistérios sobrenaturais enquanto descobre segredos sobre seu passado e o verdadeiro motivo de sua morte.",
    trailer: "https://www.youtube.com/embed/example",
    episodesList: Array.from({ length: 23 }, (_, i) => ({
      number: i + 1,
      title: `Episódio ${i + 1}`,
      duration: "24 min",
      thumbnail: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
    })),
  },
  "2": {
    id: "2",
    title: "The King's Avatar",
    englishTitle: "Quan Zhi Gao Shou",
    description:
      "Ye Xiu, um veterano jogador de e-sports, inicia uma nova jornada para retornar ao topo do mundo competitivo após ser forçado a se aposentar.",
    image: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
    banner: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
    rating: 8.8,
    year: "2017",
    status: "Completo",
    episodes: 12,
    duration: "24 min",
    studio: "G.CMay Animation & Film",
    genres: ["Ação", "Esportes", "Drama"],
    synopsis:
      "Ye Xiu é considerado um dos melhores jogadores profissionais do jogo Glory. No entanto, após ser forçado a se aposentar, ele encontra trabalho em um internet café. Quando Glory lança seu décimo servidor, Ye Xiu retorna ao jogo com uma nova identidade e começa sua jornada de volta ao topo.",
    trailer: "https://www.youtube.com/embed/example",
    episodesList: Array.from({ length: 12 }, (_, i) => ({
      number: i + 1,
      title: `Episódio ${i + 1}`,
      duration: "24 min",
      thumbnail: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
    })),
  },
  "3": {
    id: "3",
    title: "Heaven Official's Blessing",
    englishTitle: "Tian Guan Ci Fu",
    description:
      "Xie Lian, um príncipe banido que se tornou deus, embarca em aventuras no reino celestial enquanto enfrenta seu passado misterioso.",
    image: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
    banner: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
    rating: 9.0,
    year: "2020",
    status: "Em Andamento",
    episodes: 11,
    duration: "25 min",
    studio: "Haoliners Animation League",
    genres: ["Fantasia", "Romance", "Aventura"],
    synopsis:
      "Xie Lian é um príncipe do reino de Xianle que ascendeu aos céus pela terceira vez, mas desta vez sem seguidores ou incenso. Para sobreviver, ele desce ao reino mortal para completar missões. Durante uma dessas missões, ele conhece um misterioso jovem vestido de vermelho que se torna seu companheiro.",
    trailer: "https://www.youtube.com/embed/example",
    episodesList: Array.from({ length: 11 }, (_, i) => ({
      number: i + 1,
      title: `Episódio ${i + 1}`,
      duration: "25 min",
      thumbnail: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
    })),
  },
}

export default async function AnimePage({ params }: { params: { id: string } }) {
  
const {id} = await params;
  
  const anime = animeDatabase[id as keyof typeof animeDatabase]

  if (!anime) {
    notFound()
  }

  return (
    <div className="min-h-screen bg-background">
      {/* Banner Section */}
      <div className="relative h-[60vh] overflow-hidden">
        <div className="absolute inset-0 bg-cover bg-center" style={{ backgroundImage: `url(${anime.banner})` }}>
          <div className="absolute inset-0 bg-gradient-to-t from-background via-background/80 to-transparent" />
        </div>
      </div>

      {/* Content Section */}
      <div className="container px-4 -mt-40 relative z-10">
        <div className="grid md:grid-cols-[300px_1fr] gap-8">
          {/* Poster */}
          <div className="space-y-4">
            <Card className="overflow-hidden">
              <img src={anime.image || "/placeholder.svg"} alt={anime.title} className="w-full h-auto" />
            </Card>
            <div className="space-y-2">
              <Button className="w-full" size="lg">
                <Play className="w-5 h-5 mr-2" />
                Assistir Agora
              </Button>
              <Button variant="outline" className="w-full bg-transparent" size="lg">
                <Plus className="w-5 h-5 mr-2" />
                Minha Lista
              </Button>
              <Button variant="outline" className="w-full bg-transparent" size="lg">
                <Share2 className="w-5 h-5 mr-2" />
                Compartilhar
              </Button>
            </div>
          </div>

          {/* Info */}
          <div className="space-y-6">
            <div className="space-y-4">
              <div className="flex items-start justify-between">
                <div className="space-y-2">
                  <h1 className="text-4xl font-bold text-balance">{anime.title}</h1>
                  <p className="text-lg text-muted-foreground">{anime.englishTitle}</p>
                </div>
                <Badge className={`${anime.status === "Completo" ? "bg-secondary" : "bg-primary"}`}>
                  {anime.status}
                </Badge>
              </div>

              <div className="flex items-center space-x-6 text-sm">
                <div className="flex items-center space-x-1">
                  <Star className="w-5 h-5 fill-yellow-400 text-yellow-400" />
                  <span className="font-bold text-lg">{anime.rating}</span>
                </div>
                <div className="flex items-center space-x-2">
                  <Calendar className="w-4 h-4 text-muted-foreground" />
                  <span>{anime.year}</span>
                </div>
                <div className="flex items-center space-x-2">
                  <Film className="w-4 h-4 text-muted-foreground" />
                  <span>{anime.episodes} episódios</span>
                </div>
                <div className="flex items-center space-x-2">
                  <Clock className="w-4 h-4 text-muted-foreground" />
                  <span>{anime.duration}</span>
                </div>
              </div>

              <div className="flex flex-wrap gap-2">
                {anime.genres.map((genre) => (
                  <Badge key={genre} variant="secondary">
                    {genre}
                  </Badge>
                ))}
              </div>
            </div>

            <Tabs defaultValue="sinopse" className="w-full">
              <TabsList className="w-full justify-start">
                <TabsTrigger value="sinopse">Sinopse</TabsTrigger>
                <TabsTrigger value="episodios">Episódios</TabsTrigger>
                <TabsTrigger value="detalhes">Detalhes</TabsTrigger>
              </TabsList>

              <TabsContent value="sinopse" className="space-y-4">
                <div className="space-y-4">
                  <div>
                    <h3 className="text-xl font-semibold mb-2">Descrição</h3>
                    <p className="text-muted-foreground leading-relaxed text-pretty">{anime.description}</p>
                  </div>
                  <div>
                    <h3 className="text-xl font-semibold mb-2">Sinopse Completa</h3>
                    <p className="text-muted-foreground leading-relaxed text-pretty">{anime.synopsis}</p>
                  </div>
                </div>
              </TabsContent>

              <TabsContent value="episodios" className="space-y-4">
                <div className="grid gap-4">
                  {anime.episodesList.map((episode) => (
                    <Card key={episode.number} className="group cursor-pointer hover:bg-accent transition-colors">
                      <CardContent className="p-4">
                        <div className="flex items-center space-x-4">
                          <div className="relative w-32 h-20 rounded overflow-hidden flex-shrink-0">
                            <img
                              src={episode.thumbnail || "/placeholder.svg"}
                              alt={episode.title}
                              className="w-full h-full object-cover"
                            />
                            <div className="absolute inset-0 bg-black/0 group-hover:bg-black/50 transition-colors flex items-center justify-center">
                              <Play className="w-8 h-8 text-white opacity-0 group-hover:opacity-100 transition-opacity" />
                            </div>
                          </div>
                          <div className="flex-1">
                            <h4 className="font-semibold">{episode.title}</h4>
                            <p className="text-sm text-muted-foreground">{episode.duration}</p>
                          </div>
                        </div>
                      </CardContent>
                    </Card>
                  ))}
                </div>
              </TabsContent>

              <TabsContent value="detalhes" className="space-y-4">
                <div className="grid gap-4">
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Título Original:</span>
                    <span className="text-muted-foreground">{anime.title}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Título Inglês:</span>
                    <span className="text-muted-foreground">{anime.englishTitle}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Estúdio:</span>
                    <span className="text-muted-foreground">{anime.studio}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Ano:</span>
                    <span className="text-muted-foreground">{anime.year}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Status:</span>
                    <span className="text-muted-foreground">{anime.status}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Episódios:</span>
                    <span className="text-muted-foreground">{anime.episodes}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Duração:</span>
                    <span className="text-muted-foreground">{anime.duration}</span>
                  </div>
                  <div className="grid grid-cols-[120px_1fr] gap-2">
                    <span className="font-semibold">Gêneros:</span>
                    <span className="text-muted-foreground">{anime.genres.join(", ")}</span>
                  </div>
                </div>
              </TabsContent>
            </Tabs>
          </div>
        </div>
      </div>
    </div>
  )
}

