/*
"use client"

import type React from "react"
import { useState } from "react"
import { useAuth } from "@/featured/auth/hooks/use-auth"
import { useRouter } from "next/navigation"
import { Search, X, Star, Save } from "lucide-react"
import { Input } from "@/ui/input"
import { Button } from "@/ui/button"
import { Card, CardContent } from "@/ui/card"
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/ui/dialog"
import { Badge } from "@/ui/badge"
import { Skeleton } from "@/components/ui/skeleton"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { DonghuaStatus, DonghuaType, Genre, type Donghua } from "@/types/donghua"

interface AnimeResult {
  mal_id: number
  title: string
  title_english: string | null
  images: {
    jpg: {
      large_image_url: string
      image_url: string
    }
  }
  score: number | null
  year: number | null
  episodes: number | null
  status: string
  synopsis: string
  genres: Array<{ mal_id: number; name: string }>
  studios: Array<{ mal_id: number; name: string }>
  duration: string
  rating: string
  popularity: number
  members: number
  trailer?: {
    url: string
  }
  aired?: {
    from: string
  }
}

export default function AdminAddPage() {
  const { user } = useAuth()
  const router = useRouter()
  const [searchQuery, setSearchQuery] = useState("")
  const [results, setResults] = useState<AnimeResult[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [selectedAnime, setSelectedAnime] = useState<AnimeResult | null>(null)
  const [error, setError] = useState("")

  const [donghuaForm, setDonghuaForm] = useState<Donghua>({
    title: "",
    titleEnglish: "",
    description: "",
    image: "",
    banner: "",
    rating: 0,
    releaseDate: undefined,
    status: undefined,
    studio: "",
    genres: [],
    sinopse: "",
    trailer: "",
    type: DonghuaType.TV,
  })

  const handleSelectAnime = (anime: AnimeResult) => {
    setSelectedAnime(anime)
    setDonghuaForm({
      title: anime.title,
      titleEnglish: anime.title_english || "",
      description: anime.synopsis || "",
      image: anime.images.jpg.large_image_url,
      banner: anime.images.jpg.large_image_url,
      rating: anime.score || 0,
      releaseDate: anime.aired?.from ? new Date(anime.aired.from) : undefined,
      status: mapStatus(anime.status),
      studio: anime.studios[0]?.name || "",
      genres: [],
      sinopse: anime.synopsis || "",
      trailer: anime.trailer?.url || "",
      type: DonghuaType.TV,
    })
  }

  const mapStatus = (status: string): DonghuaStatus | undefined => {
    const statusMap: Record<string, DonghuaStatus> = {
      "Currently Airing": DonghuaStatus.AIRING,
      "Finished Airing": DonghuaStatus.COMPLETED,
      "Not yet aired": DonghuaStatus.UPCOMING,
    }
    return statusMap[status]
  }

  const handleSearch = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!searchQuery.trim()) return

    setIsLoading(true)
    setError("")
    setResults([])

    try {
      const response = await fetch(`https://api.jikan.moe/v4/anime?q=${encodeURIComponent(searchQuery)}&limit=12`)

      if (!response.ok) {
        throw new Error("Erro ao buscar animes")
      }

      const data = await response.json()
      setResults(data.data || [])
    } catch (err) {
      setError("Erro ao buscar animes. Tente novamente.")
      console.error(err)
    } finally {
      setIsLoading(false)
    }
  }

  const handleSaveDonghua = () => {
    console.log("Salvando donghua:", donghuaForm)
    alert(`Donghua "${donghuaForm.title}" adicionado com sucesso!`)
    setSelectedAnime(null)
    setDonghuaForm({
      title: "",
      titleEnglish: "",
      description: "",
      image: "",
      banner: "",
      rating: 0,
      releaseDate: undefined,
      status: undefined,
      studio: "",
      genres: [],
      sinopse: "",
      trailer: "",
      type: DonghuaType.TV,
    })
  }

  if (!user) {
    router.push("/login")
    return null
  }

  if (!user.isAdmin) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-background">
        <Card className="max-w-md w-full mx-4">
          <CardContent className="pt-6 text-center">
            <div className="w-16 h-16 bg-destructive/10 rounded-full flex items-center justify-center mx-auto mb-4">
              <X className="w-8 h-8 text-destructive" />
            </div>
            <h2 className="text-2xl font-bold mb-2">Acesso Negado</h2>
            <p className="text-muted-foreground mb-6">Você não tem permissão para acessar esta página.</p>
            <Button onClick={() => router.push("/")} className="w-full">
              Voltar para Home
            </Button>
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-background py-8">
      <div className="container mx-auto px-4">
        <div className="mb-8">
          <h1 className="text-4xl font-bold mb-2">Adicionar Donghua</h1>
          <p className="text-muted-foreground">Busque e adicione novos donghua ao catálogo</p>
        </div>

        <form onSubmit={handleSearch} className="mb-8">
          <div className="flex gap-2">
            <div className="relative flex-1">
              <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-muted-foreground" />
              <Input
                type="text"
                placeholder="Digite o nome do donghua..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="pl-10 h-12 text-lg"
              />
            </div>
            <Button type="submit" size="lg" disabled={isLoading} className="px-8">
              {isLoading ? "Buscando..." : "Buscar"}
            </Button>
          </div>
        </form>

        {error && (
          <div className="bg-destructive/10 border border-destructive text-destructive px-4 py-3 rounded-lg mb-6">
            {error}
          </div>
        )}

        {isLoading && (
          <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6 gap-4">
            {[...Array(12)].map((_, i) => (
              <Card key={i} className="overflow-hidden">
                <Skeleton className="aspect-[2/3] w-full" />
                <CardContent className="p-3">
                  <Skeleton className="h-4 w-full mb-2" />
                  <Skeleton className="h-3 w-2/3" />
                </CardContent>
              </Card>
            ))}
          </div>
        )}

        {!isLoading && results.length > 0 && (
          <div>
            <h2 className="text-2xl font-bold mb-4">Resultados ({results.length})</h2>
            <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6 gap-4">
              {results.map((anime) => (
                <Card
                  key={anime.mal_id}
                  className="overflow-hidden cursor-pointer hover:border-primary transition-all hover:shadow-lg group"
                  onClick={() => handleSelectAnime(anime)}
                >
                  <div className="relative aspect-[2/3] overflow-hidden">
                    <img
                      src={anime.images.jpg.large_image_url || "/placeholder.svg"}
                      alt={anime.title}
                      className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-300"
                    />
                    {anime.score && (
                      <Badge className="absolute top-2 right-2 bg-primary/90">
                        <Star className="w-3 h-3 mr-1 fill-current" />
                        {anime.score.toFixed(1)}
                      </Badge>
                    )}
                  </div>
                  <CardContent className="p-3">
                    <h3 className="font-semibold text-sm line-clamp-2 mb-1">{anime.title}</h3>
                    <div className="flex items-center gap-2 text-xs text-muted-foreground">
                      {anime.year && <span>{anime.year}</span>}
                      {anime.episodes && (
                        <>
                          <span>•</span>
                          <span>{anime.episodes} eps</span>
                        </>
                      )}
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </div>
        )}

        {!isLoading && results.length === 0 && searchQuery && (
          <div className="text-center py-12">
            <div className="w-16 h-16 bg-muted rounded-full flex items-center justify-center mx-auto mb-4">
              <Search className="w-8 h-8 text-muted-foreground" />
            </div>
            <h3 className="text-xl font-semibold mb-2">Nenhum resultado encontrado</h3>
            <p className="text-muted-foreground">Tente buscar com outro termo</p>
          </div>
        )}
      </div>

      <Dialog open={!!selectedAnime} onOpenChange={() => setSelectedAnime(null)}>
        <DialogContent className="max-w-5xl max-h-[90vh] overflow-y-auto">
          {selectedAnime && (
            <>
              <DialogHeader>
                <DialogTitle className="text-2xl">Adicionar Donghua ao Catálogo</DialogTitle>
              </DialogHeader>

              <div className="grid md:grid-cols-[250px,1fr] gap-6">
                <div className="space-y-4">
                  <img
                    src={donghuaForm.image || "/placeholder.svg"}
                    alt={donghuaForm.title}
                    className="w-full rounded-lg shadow-lg"
                  />
                </div>

                <div className="space-y-4">
                  <div className="grid md:grid-cols-2 gap-4">
                    <div>
                      <Label htmlFor="title">Título *</Label>
                      <Input
                        id="title"
                        value={donghuaForm.title}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, title: e.target.value })}
                        required
                      />
                    </div>
                    <div>
                      <Label htmlFor="titleEnglish">Título em Inglês</Label>
                      <Input
                        id="titleEnglish"
                        value={donghuaForm.titleEnglish}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, titleEnglish: e.target.value })}
                      />
                    </div>
                  </div>

                  <div>
                    <Label htmlFor="description">Descrição</Label>
                    <Textarea
                      id="description"
                      value={donghuaForm.description}
                      onChange={(e) => setDonghuaForm({ ...donghuaForm, description: e.target.value })}
                      rows={2}
                    />
                  </div>

                  <div>
                    <Label htmlFor="sinopse">Sinopse *</Label>
                    <Textarea
                      id="sinopse"
                      value={donghuaForm.sinopse}
                      onChange={(e) => setDonghuaForm({ ...donghuaForm, sinopse: e.target.value })}
                      rows={3}
                      required
                    />
                  </div>

                  <div className="grid md:grid-cols-2 gap-4">
                    <div>
                      <Label htmlFor="image">URL da Imagem *</Label>
                      <Input
                        id="image"
                        value={donghuaForm.image}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, image: e.target.value })}
                        required
                      />
                    </div>
                    <div>
                      <Label htmlFor="banner">URL do Banner</Label>
                      <Input
                        id="banner"
                        value={donghuaForm.banner}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, banner: e.target.value })}
                      />
                    </div>
                  </div>

                  <div className="grid md:grid-cols-3 gap-4">
                    <div>
                      <Label htmlFor="rating">Rating (0-5)</Label>
                      <Input
                        id="rating"
                        type="number"
                        step="0.1"
                        min="0"
                        max="5"
                        value={donghuaForm.rating}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, rating: Number.parseFloat(e.target.value) })}
                      />
                    </div>
                    <div>
                      <Label htmlFor="releaseDate">Data de Lançamento</Label>
                      <Input
                        id="releaseDate"
                        type="date"
                        value={donghuaForm.releaseDate?.toISOString().split("T")[0] || ""}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, releaseDate: new Date(e.target.value) })}
                      />
                    </div>
                    <div>
                      <Label htmlFor="studio">Estúdio</Label>
                      <Input
                        id="studio"
                        value={donghuaForm.studio}
                        onChange={(e) => setDonghuaForm({ ...donghuaForm, studio: e.target.value })}
                      />
                    </div>
                  </div>

                  <div className="grid md:grid-cols-2 gap-4">
                    <div>
                      <Label htmlFor="status">Status</Label>
                      <Select
                        value={donghuaForm.status}
                        onValueChange={(value) => setDonghuaForm({ ...donghuaForm, status: value as DonghuaStatus })}
                      >
                        <SelectTrigger>
                          <SelectValue placeholder="Selecione o status" />
                        </SelectTrigger>
                        <SelectContent>
                          {Object.values(DonghuaStatus).map((status) => (
                            <SelectItem key={status} value={status}>
                              {status}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                    </div>
                    <div>
                      <Label htmlFor="type">Tipo *</Label>
                      <Select
                        value={donghuaForm.type}
                        onValueChange={(value) => setDonghuaForm({ ...donghuaForm, type: value as DonghuaType })}
                      >
                        <SelectTrigger>
                          <SelectValue placeholder="Selecione o tipo" />
                        </SelectTrigger>
                        <SelectContent>
                          {Object.values(DonghuaType).map((type) => (
                            <SelectItem key={type} value={type}>
                              {type}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                    </div>
                  </div>

                  <div>
                    <Label htmlFor="genres">Gêneros</Label>
                    <Select
                      value={donghuaForm.genres?.[0]}
                      onValueChange={(value) => {
                        const currentGenres = donghuaForm.genres || []
                        if (!currentGenres.includes(value as Genre)) {
                          setDonghuaForm({ ...donghuaForm, genres: [...currentGenres, value as Genre] })
                        }
                      }}
                    >
                      <SelectTrigger>
                        <SelectValue placeholder="Adicionar gênero" />
                      </SelectTrigger>
                      <SelectContent>
                        {Object.values(Genre).map((genre) => (
                          <SelectItem key={genre} value={genre}>
                            {genre}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                    <div className="flex flex-wrap gap-2 mt-2">
                      {donghuaForm.genres?.map((genre) => (
                        <Badge
                          key={genre}
                          variant="secondary"
                          className="cursor-pointer"
                          onClick={() =>
                            setDonghuaForm({
                              ...donghuaForm,
                              genres: donghuaForm.genres?.filter((g) => g !== genre),
                            })
                          }
                        >
                          {genre} <X className="w-3 h-3 ml-1" />
                        </Badge>
                      ))}
                    </div>
                  </div>

                  <div>
                    <Label htmlFor="trailer">URL do Trailer</Label>
                    <Input
                      id="trailer"
                      value={donghuaForm.trailer}
                      onChange={(e) => setDonghuaForm({ ...donghuaForm, trailer: e.target.value })}
                    />
                  </div>

                  <div className="flex gap-2 pt-4">
                    <Button type="button" variant="outline" onClick={() => setSelectedAnime(null)} className="flex-1">
                      Cancelar
                    </Button>
                    <Button type="button" onClick={handleSaveDonghua} className="flex-1">
                      <Save className="w-4 h-4 mr-2" />
                      Salvar Donghua
                    </Button>
                  </div>
                </div>
              </div>
            </>
          )}
        </DialogContent>
      </Dialog>
    </div>
  )
}

*/