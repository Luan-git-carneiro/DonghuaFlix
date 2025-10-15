"use client"

import { useState } from "react"
import { Card, CardContent } from "@/components/ui/card"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Badge } from "@/components/ui/badge"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Search, Edit, Trash2, Eye, Star, X, Save, Play } from "lucide-react"
import Image from "next/image"
import Link from "next/link"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { DonghuaStatus, DonghuaType, Genre, type Donghua } from "@/types/donghua"

export default function GerenciarDonghua() {
  const [donghua, setDonghua] = useState<Donghua[]>([
    {
      id: 1,
      title: "Mo Dao Zu Shi",
      titleEnglish: "Grandmaster of Demonic Cultivation",
      description: "Uma história épica de cultivação e redenção",
      image: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
      banner: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
      rating: 4.9,
      releaseDate: new Date("2018-07-09"),
      status: DonghuaStatus.COMPLETED,
      studio: "Tencent Penguin Pictures",
      genres: [Genre.ACTION, Genre.FANTASY, Genre.SUPERNATURAL],
      sinopse: "Wei Wuxian, um jovem cultivador, é temido por suas práticas heterodoxas...",
      trailer: "https://www.youtube.com/watch?v=example",
      type: DonghuaType.TV,
    },
    {
      id: 2,
      title: "The King's Avatar",
      titleEnglish: "Quan Zhi Gao Shou",
      description: "A jornada de um jogador profissional de e-sports",
      image: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
      banner: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
      rating: 4.8,
      releaseDate: new Date("2017-04-07"),
      status: DonghuaStatus.COMPLETED,
      studio: "G.CMay Animation & Film",
      genres: [Genre.ACTION, Genre.SPORTS, Genre.DRAMA],
      sinopse: "Ye Xiu, um jogador profissional de e-sports, é forçado a se aposentar...",
      trailer: "https://www.youtube.com/watch?v=example2",
      type: DonghuaType.TV,
    },
    {
      id: 3,
      title: "Heaven Official's Blessing",
      titleEnglish: "Tian Guan Ci Fu",
      description: "Uma história celestial de amor e redenção",
      image: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
      banner: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
      rating: 4.9,
      releaseDate: new Date("2020-10-31"),
      status: DonghuaStatus.AIRING,
      studio: "Haoliners Animation League",
      genres: [Genre.FANTASY, Genre.ROMANCE, Genre.ADVENTURE],
      sinopse: "Xie Lian, um príncipe que ascendeu aos céus, é banido pela terceira vez...",
      trailer: "https://www.youtube.com/watch?v=example3",
      type: DonghuaType.TV,
    },
  ])
  const [searchTerm, setSearchTerm] = useState("")
  const [editingDonghua, setEditingDonghua] = useState<Donghua | null>(null)
  const [deletingDonghua, setDeletingDonghua] = useState<Donghua | null>(null)

  const filteredDonghua = donghua.filter((anime) => anime.title.toLowerCase().includes(searchTerm.toLowerCase()))

  const handleEdit = (anime: Donghua) => {
    setEditingDonghua({ ...anime })
  }

  const handleSaveEdit = () => {
    if (!editingDonghua) return
    setDonghua(donghua.map((anime) => (anime.id === editingDonghua.id ? editingDonghua : anime)))
    setEditingDonghua(null)
  }

  const handleDelete = (anime: Donghua) => {
    setDeletingDonghua(anime)
  }

  const confirmDelete = () => {
    if (!deletingDonghua) return
    setDonghua(donghua.filter((anime) => anime.id !== deletingDonghua.id))
    setDeletingDonghua(null)
  }

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold">Gerenciar Donghua</h1>
          <p className="text-muted-foreground mt-2">Edite ou remova donghua do catálogo</p>
        </div>
        <Link href="/admin/adicionar">
          <Button>Adicionar Novo Donghua</Button>
        </Link>
      </div>

      <div className="relative">
        <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
        <Input
          placeholder="Buscar donghua..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="pl-10"
        />
      </div>

      <div className="grid gap-4">
        {filteredDonghua.map((anime) => (
          <Card key={anime.id}>
            <CardContent className="p-6">
              <div className="flex gap-6">
                <div className="relative h-32 w-24 flex-shrink-0 overflow-hidden rounded-lg">
                  <Image src={anime.image || "/placeholder.svg"} alt={anime.title} fill className="object-cover" />
                </div>
                <div className="flex-1">
                  <div className="flex items-start justify-between">
                    <div>
                      <h3 className="text-xl font-bold">{anime.title}</h3>
                      {anime.titleEnglish && <p className="text-sm text-muted-foreground">{anime.titleEnglish}</p>}
                      <div className="mt-2 flex flex-wrap items-center gap-3 text-sm text-muted-foreground">
                        <span className="flex items-center gap-1">
                          <Star className="h-4 w-4 fill-yellow-500 text-yellow-500" />
                          {anime.rating}
                        </span>
                        {anime.releaseDate && <span>{anime.releaseDate.getFullYear()}</span>}
                        <Badge variant={anime.status === DonghuaStatus.COMPLETED ? "default" : "secondary"}>
                          {anime.status}
                        </Badge>
                        <Badge variant="outline">{anime.type}</Badge>
                      </div>
                      <div className="mt-2 flex flex-wrap gap-2">
                        {anime.genres?.map((genre) => (
                          <Badge key={genre} variant="outline">
                            {genre}
                          </Badge>
                        ))}
                      </div>
                      {anime.studio && <p className="mt-2 text-sm text-muted-foreground">Estúdio: {anime.studio}</p>}
                      <p className="mt-2 text-sm text-muted-foreground line-clamp-2">{anime.sinopse}</p>
                    </div>
                    <div className="flex gap-2">
                      <Link href={`/admin/donghua/${anime.id}/episodios`}>
                        <Button variant="outline" size="icon" title="Gerenciar Episódios">
                          <Play className="h-4 w-4" />
                        </Button>
                      </Link>
                      <Link href={`/anime/${anime.id}`}>
                        <Button variant="outline" size="icon">
                          <Eye className="h-4 w-4" />
                        </Button>
                      </Link>
                      <Button variant="outline" size="icon" onClick={() => handleEdit(anime)}>
                        <Edit className="h-4 w-4" />
                      </Button>
                      <Button variant="outline" size="icon" onClick={() => handleDelete(anime)}>
                        <Trash2 className="h-4 w-4 text-destructive" />
                      </Button>
                    </div>
                  </div>
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      <Dialog open={!!editingDonghua} onOpenChange={() => setEditingDonghua(null)}>
        <DialogContent className="max-w-4xl max-h-[90vh] overflow-y-auto">
          <DialogHeader>
            <DialogTitle>Editar Donghua</DialogTitle>
            <DialogDescription>Faça alterações nas informações do donghua</DialogDescription>
          </DialogHeader>
          {editingDonghua && (
            <div className="space-y-4">
              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="edit-title">Título *</Label>
                  <Input
                    id="edit-title"
                    value={editingDonghua.title}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, title: e.target.value })}
                  />
                </div>
                <div>
                  <Label htmlFor="edit-titleEnglish">Título em Inglês</Label>
                  <Input
                    id="edit-titleEnglish"
                    value={editingDonghua.titleEnglish || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, titleEnglish: e.target.value })}
                  />
                </div>
              </div>

              <div>
                <Label htmlFor="edit-description">Descrição</Label>
                <Textarea
                  id="edit-description"
                  value={editingDonghua.description || ""}
                  onChange={(e) => setEditingDonghua({ ...editingDonghua, description: e.target.value })}
                  rows={2}
                />
              </div>

              <div>
                <Label htmlFor="edit-sinopse">Sinopse *</Label>
                <Textarea
                  id="edit-sinopse"
                  value={editingDonghua.sinopse}
                  onChange={(e) => setEditingDonghua({ ...editingDonghua, sinopse: e.target.value })}
                  rows={3}
                />
              </div>

              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="edit-image">URL da Imagem *</Label>
                  <Input
                    id="edit-image"
                    value={editingDonghua.image}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, image: e.target.value })}
                  />
                </div>
                <div>
                  <Label htmlFor="edit-banner">URL do Banner</Label>
                  <Input
                    id="edit-banner"
                    value={editingDonghua.banner || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, banner: e.target.value })}
                  />
                </div>
              </div>

              <div className="grid md:grid-cols-3 gap-4">
                <div>
                  <Label htmlFor="edit-rating">Rating (0-5)</Label>
                  <Input
                    id="edit-rating"
                    type="number"
                    step="0.1"
                    min="0"
                    max="5"
                    value={editingDonghua.rating || 0}
                    onChange={(e) =>
                      setEditingDonghua({ ...editingDonghua, rating: Number.parseFloat(e.target.value) })
                    }
                  />
                </div>
                <div>
                  <Label htmlFor="edit-releaseDate">Data de Lançamento</Label>
                  <Input
                    id="edit-releaseDate"
                    type="date"
                    value={editingDonghua.releaseDate?.toISOString().split("T")[0] || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, releaseDate: new Date(e.target.value) })}
                  />
                </div>
                <div>
                  <Label htmlFor="edit-studio">Estúdio</Label>
                  <Input
                    id="edit-studio"
                    value={editingDonghua.studio || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, studio: e.target.value })}
                  />
                </div>
              </div>

              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="edit-status">Status</Label>
                  <Select
                    value={editingDonghua.status}
                    onValueChange={(value) => setEditingDonghua({ ...editingDonghua, status: value as DonghuaStatus })}
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
                  <Label htmlFor="edit-type">Tipo *</Label>
                  <Select
                    value={editingDonghua.type}
                    onValueChange={(value) => setEditingDonghua({ ...editingDonghua, type: value as DonghuaType })}
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
                <Label htmlFor="edit-genres">Gêneros</Label>
                <Select
                  onValueChange={(value) => {
                    const currentGenres = editingDonghua.genres || []
                    if (!currentGenres.includes(value as Genre)) {
                      setEditingDonghua({ ...editingDonghua, genres: [...currentGenres, value as Genre] })
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
                  {editingDonghua.genres?.map((genre) => (
                    <Badge
                      key={genre}
                      variant="secondary"
                      className="cursor-pointer"
                      onClick={() =>
                        setEditingDonghua({
                          ...editingDonghua,
                          genres: editingDonghua.genres?.filter((g) => g !== genre),
                        })
                      }
                    >
                      {genre} <X className="w-3 h-3 ml-1" />
                    </Badge>
                  ))}
                </div>
              </div>

              <div>
                <Label htmlFor="edit-trailer">URL do Trailer</Label>
                <Input
                  id="edit-trailer"
                  value={editingDonghua.trailer || ""}
                  onChange={(e) => setEditingDonghua({ ...editingDonghua, trailer: e.target.value })}
                />
              </div>
            </div>
          )}
          <DialogFooter>
            <Button variant="outline" onClick={() => setEditingDonghua(null)}>
              Cancelar
            </Button>
            <Button onClick={handleSaveEdit}>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <Dialog open={!!deletingDonghua} onOpenChange={() => setDeletingDonghua(null)}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Confirmar Exclusão</DialogTitle>
            <DialogDescription>
              Tem certeza que deseja excluir <strong>{deletingDonghua?.title}</strong>? Esta ação não pode ser desfeita.
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button variant="outline" onClick={() => setDeletingDonghua(null)}>
              Cancelar
            </Button>
            <Button variant="destructive" onClick={confirmDelete}>
              Excluir
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  )
}
