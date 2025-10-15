"use client"

import { useState, use } from "react"
import { Card, CardContent, CardHeader, CardTitle } from "@/ui/card"
import { Button } from "@/ui/button"
import { Input } from "@/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { Badge } from "@/ui/badge"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/ui/dialog"
import { Plus, Edit, Trash2, Play, ArrowLeft, Save } from "lucide-react"
import Image from "next/image"
import { useRouter } from "next/navigation"
import type { Episode } from "@/featured/episode/type/episode"

export default async function GerenciarEpisodios({ params }: { params: Promise<{ id: string }> }) {
  

  
  const resolvedParams = use(params)
  const donghuaId = Number.parseInt(resolvedParams.id)
  const router = useRouter()

  // Mock data - donghua info
  const donghua = {
    id: donghuaId,
    title: "Mo Dao Zu Shi",
    titleEnglish: "Grandmaster of Demonic Cultivation",
    image: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
  }

  // Mock data - episódios
  const [episodes, setEpisodes] = useState<Episode[]>([
    {
      id: 1,
      donghuaId: donghuaId,
      episodeNumber: 1,
      title: "O Retorno",
      description: "Wei Wuxian retorna após 13 anos",
      thumbnail: "/placeholder.svg?height=200&width=350",
      videoUrl: "https://example.com/video1.mp4",
      duration: 24,
      releaseDate: new Date("2018-07-09"),
      views: 150000,
    },
    {
      id: 2,
      donghuaId: donghuaId,
      episodeNumber: 2,
      title: "A Caverna Fria",
      description: "Wei Wuxian e Lan Wangji exploram a caverna",
      thumbnail: "/placeholder.svg?height=200&width=350",
      videoUrl: "https://example.com/video2.mp4",
      duration: 24,
      releaseDate: new Date("2018-07-16"),
      views: 145000,
    },
    {
      id: 3,
      donghuaId: donghuaId,
      episodeNumber: 3,
      title: "O Braço Fantasma",
      description: "Mistérios começam a se revelar",
      thumbnail: "/placeholder.svg?height=200&width=350",
      videoUrl: "https://example.com/video3.mp4",
      duration: 24,
      releaseDate: new Date("2018-07-23"),
      views: 140000,
    },
  ])

  const [isAddingEpisode, setIsAddingEpisode] = useState(false)
  const [editingEpisode, setEditingEpisode] = useState<Episode | null>(null)
  const [deletingEpisode, setDeletingEpisode] = useState<Episode | null>(null)
  const [newEpisode, setNewEpisode] = useState<Partial<Episode>>({
    donghuaId: donghuaId,
    episodeNumber: episodes.length + 1,
    title: "",
    description: "",
    thumbnail: "",
    videoUrl: "",
    duration: 24,
    releaseDate: new Date(),
    views: 0,
  })

  const handleAddEpisode = () => {
    if (!newEpisode.title || !newEpisode.videoUrl) {
      alert("Título e URL do vídeo são obrigatórios")
      return
    }

    const episode: Episode = {
      id: episodes.length + 1,
      donghuaId: donghuaId,
      episodeNumber: newEpisode.episodeNumber || episodes.length + 1,
      title: newEpisode.title,
      description: newEpisode.description,
      thumbnail: newEpisode.thumbnail,
      videoUrl: newEpisode.videoUrl,
      duration: newEpisode.duration,
      releaseDate: newEpisode.releaseDate,
      views: newEpisode.views || 0,
    }

    setEpisodes([...episodes, episode])
    setIsAddingEpisode(false)
    setNewEpisode({
      donghuaId: donghuaId,
      episodeNumber: episodes.length + 2,
      title: "",
      description: "",
      thumbnail: "",
      videoUrl: "",
      duration: 24,
      releaseDate: new Date(),
      views: 0,
    })
  }

  const handleEditEpisode = (episode: Episode) => {
    setEditingEpisode({ ...episode })
  }

  const handleSaveEdit = () => {
    if (!editingEpisode) return
    setEpisodes(episodes.map((ep) => (ep.id === editingEpisode.id ? editingEpisode : ep)))
    setEditingEpisode(null)
  }

  const handleDeleteEpisode = (episode: Episode) => {
    setDeletingEpisode(episode)
  }

  const confirmDelete = () => {
    if (!deletingEpisode) return
    setEpisodes(episodes.filter((ep) => ep.id !== deletingEpisode.id))
    setDeletingEpisode(null)
  }

  return (
    <div className="space-y-6">
      <div className="flex items-center gap-4">
        <Button variant="outline" size="icon" onClick={() => router.back()}>
          <ArrowLeft className="h-4 w-4" />
        </Button>
        <div className="flex-1">
          <h1 className="text-3xl font-bold">Gerenciar Episódios</h1>
          <p className="text-muted-foreground mt-1">
            {donghua.title} {donghua.titleEnglish && `(${donghua.titleEnglish})`}
          </p>
        </div>
        <Button onClick={() => setIsAddingEpisode(true)}>
          <Plus className="h-4 w-4 mr-2" />
          Adicionar Episódio
        </Button>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Episódios ({episodes.length})</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="space-y-4">
            {episodes
              .sort((a, b) => a.episodeNumber - b.episodeNumber)
              .map((episode) => (
                <Card key={episode.id}>
                  <CardContent className="p-4">
                    <div className="flex gap-4">
                      <div className="relative h-24 w-40 flex-shrink-0 overflow-hidden rounded-lg bg-muted">
                        <Image
                          src={episode.thumbnail || "/placeholder.svg?height=200&width=350"}
                          alt={episode.title}
                          fill
                          className="object-cover"
                        />
                        <div className="absolute inset-0 flex items-center justify-center bg-black/40 opacity-0 hover:opacity-100 transition-opacity">
                          <Play className="h-8 w-8 text-white" />
                        </div>
                      </div>
                      <div className="flex-1">
                        <div className="flex items-start justify-between">
                          <div>
                            <div className="flex items-center gap-2">
                              <Badge variant="secondary">EP {episode.episodeNumber}</Badge>
                              <h3 className="font-semibold">{episode.title}</h3>
                            </div>
                            {episode.description && (
                              <p className="text-sm text-muted-foreground mt-1 line-clamp-2">{episode.description}</p>
                            )}
                            <div className="flex items-center gap-4 mt-2 text-xs text-muted-foreground">
                              {episode.duration && <span>{episode.duration} min</span>}
                              {episode.releaseDate && <span>{episode.releaseDate.toLocaleDateString("pt-BR")}</span>}
                              {episode.views !== undefined && (
                                <span>{episode.views.toLocaleString()} visualizações</span>
                              )}
                            </div>
                          </div>
                          <div className="flex gap-2">
                            <Button variant="outline" size="icon" onClick={() => handleEditEpisode(episode)}>
                              <Edit className="h-4 w-4" />
                            </Button>
                            <Button variant="outline" size="icon" onClick={() => handleDeleteEpisode(episode)}>
                              <Trash2 className="h-4 w-4 text-destructive" />
                            </Button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}

            {episodes.length === 0 && (
              <div className="text-center py-12 text-muted-foreground">
                <p>Nenhum episódio adicionado ainda.</p>
                <Button className="mt-4" onClick={() => setIsAddingEpisode(true)}>
                  <Plus className="h-4 w-4 mr-2" />
                  Adicionar Primeiro Episódio
                </Button>
              </div>
            )}
          </div>
        </CardContent>
      </Card>

      {/* Dialog para adicionar episódio */}
      <Dialog open={isAddingEpisode} onOpenChange={setIsAddingEpisode}>
        <DialogContent className="max-w-2xl max-h-[90vh] overflow-y-auto">
          <DialogHeader>
            <DialogTitle>Adicionar Novo Episódio</DialogTitle>
            <DialogDescription>Preencha as informações do episódio</DialogDescription>
          </DialogHeader>
          <div className="space-y-4">
            <div className="grid md:grid-cols-2 gap-4">
              <div>
                <Label htmlFor="episodeNumber">Número do Episódio *</Label>
                <Input
                  id="episodeNumber"
                  type="number"
                  value={newEpisode.episodeNumber}
                  onChange={(e) => setNewEpisode({ ...newEpisode, episodeNumber: Number.parseInt(e.target.value) })}
                />
              </div>
              <div>
                <Label htmlFor="duration">Duração (minutos)</Label>
                <Input
                  id="duration"
                  type="number"
                  value={newEpisode.duration}
                  onChange={(e) => setNewEpisode({ ...newEpisode, duration: Number.parseInt(e.target.value) })}
                />
              </div>
            </div>

            <div>
              <Label htmlFor="title">Título do Episódio *</Label>
              <Input
                id="title"
                value={newEpisode.title}
                onChange={(e) => setNewEpisode({ ...newEpisode, title: e.target.value })}
                placeholder="Ex: O Retorno"
              />
            </div>

            <div>
              <Label htmlFor="description">Descrição</Label>
              <Textarea
                id="description"
                value={newEpisode.description}
                onChange={(e) => setNewEpisode({ ...newEpisode, description: e.target.value })}
                placeholder="Breve descrição do episódio"
                rows={3}
              />
            </div>

            <div>
              <Label htmlFor="videoUrl">URL do Vídeo *</Label>
              <Input
                id="videoUrl"
                value={newEpisode.videoUrl}
                onChange={(e) => setNewEpisode({ ...newEpisode, videoUrl: e.target.value })}
                placeholder="https://example.com/video.mp4"
              />
            </div>

            <div>
              <Label htmlFor="thumbnail">URL da Thumbnail</Label>
              <Input
                id="thumbnail"
                value={newEpisode.thumbnail}
                onChange={(e) => setNewEpisode({ ...newEpisode, thumbnail: e.target.value })}
                placeholder="https://example.com/thumbnail.jpg"
              />
            </div>

            <div>
              <Label htmlFor="releaseDate">Data de Lançamento</Label>
              <Input
                id="releaseDate"
                type="date"
                value={newEpisode.releaseDate?.toISOString().split("T")[0]}
                onChange={(e) => setNewEpisode({ ...newEpisode, releaseDate: new Date(e.target.value) })}
              />
            </div>
          </div>
          <DialogFooter>
            <Button variant="outline" onClick={() => setIsAddingEpisode(false)}>
              Cancelar
            </Button>
            <Button onClick={handleAddEpisode}>
              <Save className="w-4 h-4 mr-2" />
              Adicionar Episódio
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      {/* Dialog para editar episódio */}
      <Dialog open={!!editingEpisode} onOpenChange={() => setEditingEpisode(null)}>
        <DialogContent className="max-w-2xl max-h-[90vh] overflow-y-auto">
          <DialogHeader>
            <DialogTitle>Editar Episódio</DialogTitle>
            <DialogDescription>Faça alterações nas informações do episódio</DialogDescription>
          </DialogHeader>
          {editingEpisode && (
            <div className="space-y-4">
              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="edit-episodeNumber">Número do Episódio *</Label>
                  <Input
                    id="edit-episodeNumber"
                    type="number"
                    value={editingEpisode.episodeNumber}
                    onChange={(e) =>
                      setEditingEpisode({ ...editingEpisode, episodeNumber: Number.parseInt(e.target.value) })
                    }
                  />
                </div>
                <div>
                  <Label htmlFor="edit-duration">Duração (minutos)</Label>
                  <Input
                    id="edit-duration"
                    type="number"
                    value={editingEpisode.duration}
                    onChange={(e) =>
                      setEditingEpisode({ ...editingEpisode, duration: Number.parseInt(e.target.value) })
                    }
                  />
                </div>
              </div>

              <div>
                <Label htmlFor="edit-title">Título do Episódio *</Label>
                <Input
                  id="edit-title"
                  value={editingEpisode.title}
                  onChange={(e) => setEditingEpisode({ ...editingEpisode, title: e.target.value })}
                />
              </div>

              <div>
                <Label htmlFor="edit-description">Descrição</Label>
                <Textarea
                  id="edit-description"
                  value={editingEpisode.description}
                  onChange={(e) => setEditingEpisode({ ...editingEpisode, description: e.target.value })}
                  rows={3}
                />
              </div>

              <div>
                <Label htmlFor="edit-videoUrl">URL do Vídeo *</Label>
                <Input
                  id="edit-videoUrl"
                  value={editingEpisode.videoUrl}
                  onChange={(e) => setEditingEpisode({ ...editingEpisode, videoUrl: e.target.value })}
                />
              </div>

              <div>
                <Label htmlFor="edit-thumbnail">URL da Thumbnail</Label>
                <Input
                  id="edit-thumbnail"
                  value={editingEpisode.thumbnail}
                  onChange={(e) => setEditingEpisode({ ...editingEpisode, thumbnail: e.target.value })}
                />
              </div>

              <div>
                <Label htmlFor="edit-releaseDate">Data de Lançamento</Label>
                <Input
                  id="edit-releaseDate"
                  type="date"
                  value={editingEpisode.releaseDate?.toISOString().split("T")[0]}
                  onChange={(e) => setEditingEpisode({ ...editingEpisode, releaseDate: new Date(e.target.value) })}
                />
              </div>

              <div>
                <Label htmlFor="edit-views">Visualizações</Label>
                <Input
                  id="edit-views"
                  type="number"
                  value={editingEpisode.views}
                  onChange={(e) => setEditingEpisode({ ...editingEpisode, views: Number.parseInt(e.target.value) })}
                />
              </div>
            </div>
          )}
          <DialogFooter>
            <Button variant="outline" onClick={() => setEditingEpisode(null)}>
              Cancelar
            </Button>
            <Button onClick={handleSaveEdit}>
              <Save className="w-4 h-4 mr-2" />
              Salvar Alterações
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      {/* Dialog para confirmar exclusão */}
      <Dialog open={!!deletingEpisode} onOpenChange={() => setDeletingEpisode(null)}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Confirmar Exclusão</DialogTitle>
            <DialogDescription>
              Tem certeza que deseja excluir o episódio <strong>{deletingEpisode?.episodeNumber}</strong> -{" "}
              <strong>{deletingEpisode?.title}</strong>? Esta ação não pode ser desfeita.
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button variant="outline" onClick={() => setDeletingEpisode(null)}>
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
