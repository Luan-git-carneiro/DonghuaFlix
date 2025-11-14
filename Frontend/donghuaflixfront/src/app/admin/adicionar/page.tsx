
"use client"

import type React from "react"
import { useState } from "react"
import { useRouter } from "next/navigation"
import { Search } from "lucide-react"
import { Donghua } from "@/Domain/entities/donghua"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"
import { DonghuaType } from "@/Domain/enum/donghuaType"
import { useAuthContext } from "@/common/contexts/auth-context"
import { UserRole } from "@/featured/auth/types/auth.types"
import SearchDonghua from "@/featured/admin/manageDonghua/adicionar/search"
import Loading from "./loading"
import CardDonghua from "@/components/ui/CardDonghua"
import PainelForSave from "@/featured/admin/manageDonghua/adicionar/painelForSave"


export interface AnimeResult {
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
  const { hasRole } = useAuthContext()
  const router = useRouter()
  const [searchQuery, setSearchQuery] = useState("")
  const [results, setResults] = useState<AnimeResult[]>([])
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState("")
  const [isPainelOpen, setIsPainelOpen] = useState(false)
  const [donghuaForm, setDonghuaForm] = useState<Donghua>({
    DonghuaId: "",
    Title: "",
    titleEnglish: "",
    description: "",
    Image: "",
    banner: "",
    rating: 0,
    ReleaseYear: new Date,
    Status: DonghuaStatus.Concluido,
    Studio: "",
    Genre: [],
    Sinopse: "",
    Type: DonghuaType.Serie
  })

  const handleSelectAnime = (anime: AnimeResult) => {
    setDonghuaForm({
      DonghuaId: anime.mal_id.toString(),
      Title: anime.title,
      titleEnglish: anime.title_english || "",
      description: anime.synopsis || "",
      Image: anime.images.jpg.large_image_url,
      banner: anime.images.jpg.large_image_url,
      rating: anime.score || 0,
      ReleaseYear: anime.aired?.from ? new Date(anime.aired.from) : new Date,
      Status: mapStatus(anime.status),
      Studio: anime.studios[0]?.name || "",
      Genre: anime.genres.map((genre) => genre.name),
      Sinopse: anime.synopsis || "",
      Type: DonghuaType.Serie,
    })
    setIsPainelOpen(true)
  }

  const mapStatus = (status: string): DonghuaStatus  => {
    const statusMap: Record<string, DonghuaStatus> = {
      "Currently Airing": DonghuaStatus.EmAndamento,
      "Finished Airing": DonghuaStatus.Concluido,
      "in Paused": DonghuaStatus.Pausado,
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
      console.log("Resultados da busca:", data.data)
    } catch (err) {
      setError("Erro ao buscar animes. Tente novamente.")
      console.error(err)
    } finally {
      setIsLoading(false)
    }
  }

  const handleSaveDonghua = () => {
    console.log("Salvando donghua:", donghuaForm)
    alert(`Donghua "${donghuaForm.Title}" adicionado com sucesso!`)
    setDonghuaForm({
      DonghuaId: "",
      Title: "",
      titleEnglish: "",
      description: "",
      Image: "",
      banner: "",
      rating: 0,
      ReleaseYear: new Date,
      Status: DonghuaStatus.Concluido,
      Studio: "",
      Genre: [],
      Sinopse: "",
      Type: DonghuaType.Serie,
    })
  }

  const clearDonghuaForm = () => {

    setDonghuaForm({
      DonghuaId: "",
      Title: "",
      titleEnglish: "",
      description: "",
      Image: "",
      banner: "",
      rating: 0,
      ReleaseYear: new Date,
      Status: DonghuaStatus.Concluido,
      Studio: "",
      Genre: [],
      Sinopse: "",
      Type: DonghuaType.Serie,
    })
    setIsPainelOpen(false)
  }

  if (!hasRole(UserRole.ADMIN)) {
    router.push("/login")
    return null
  }

  return (
    <div className="min-h-screen bg-background py-8">
      <div className="container mx-auto px-4">
        <div className="mb-8">
          <h1 className="text-4xl font-bold mb-2">Adicionar Donghua</h1>
          <p className="text-muted-foreground">Busque e adicione novos donghua ao catálogo</p>
        </div>

        <SearchDonghua 
          isLoading={isLoading}
          search={searchQuery}
          setSearchQuery={setSearchQuery}
          onSearch={handleSearch}
        />

        {error && (
          <div className="bg-destructive/10 border border-destructive text-destructive px-4 py-3 rounded-lg mb-6">
            {error}
          </div>
        )}

        {isLoading && (
          <Loading />
        )}

        {!isLoading && results.length > 0 && (
            <div>
              <h2 className="text-2xl font-bold mb-4">Resultados ({results.length})</h2>
              <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6 gap-4">
                {results.map((anime) => (
                      <CardDonghua
                        donghua={anime}
                        onSelectDonghua={() => handleSelectAnime(anime)}
                      />  
                    )
                  )
                } 
              </div>
            </div>
          )
        }

        {!isLoading && results.length === 0 && searchQuery && (
            <div className="text-center py-12">
              <div className="w-16 h-16 bg-muted rounded-full flex items-center justify-center mx-auto mb-4">
                <Search className="w-8 h-8 text-muted-foreground" />
              </div>
              <h3 className="text-xl font-semibold mb-2">Nenhum resultado encontrado</h3>
              <p className="text-muted-foreground">Tente buscar com outro termo</p>
            </div>
          )
        }

      </div>

      <PainelForSave 
        isOpen={isPainelOpen}
        donghuaForm={donghuaForm}
        onClearDonghuaForm={clearDonghuaForm}
        onSetDonghuaForm={setDonghuaForm}
        onSaveDonghua={handleSaveDonghua}
      />

    </div>
  )
}

