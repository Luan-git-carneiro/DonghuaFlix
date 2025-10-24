'use client'

import { Label } from "@/components/ui/label"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Textarea } from "@/components/ui/textarea"
import { Donghua } from "@/Domain/entities/donghua"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"
import { DonghuaType } from "@/Domain/enum/donghuaType"
import { Badge } from "@/ui/badge"
import { Button } from "@/ui/button"
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "@/ui/dialog"
import { Input } from "@/ui/input"
import { Save, X } from "lucide-react"
import React from "react"


interface EdicaoDonghuaProps {

    editingDonghua: Donghua | null;
    setEditingDonghua: (donghua: Donghua | null) => void ;
    handleSaveEdit: () => void ;

}


export const EdicaoDonghua = ({ editingDonghua  , setEditingDonghua , handleSaveEdit}: EdicaoDonghuaProps) =>
{

    

    return (
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
                    value={editingDonghua.Title}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, Title: e.target.value })}
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
                  value={editingDonghua.Sinopse}
                  onChange={(e) => setEditingDonghua({ ...editingDonghua, Sinopse: e.target.value })}
                  rows={3}
                />
              </div>

              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="edit-image">URL da Imagem *</Label>
                  <Input
                    id="edit-image"
                    value={editingDonghua.Image || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, Image: e.target.value })}
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
                    value={editingDonghua.ReleaseYear?.toISOString().split("T")[0] || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, ReleaseYear: new Date(e.target.value) })}
                  />
                </div>
                <div>
                  <Label htmlFor="edit-studio">Estúdio</Label>
                  <Input
                    id="edit-studio"
                    value={editingDonghua.Studio || ""}
                    onChange={(e) => setEditingDonghua({ ...editingDonghua, Studio: e.target.value })}
                  />
                </div>
              </div>

              <div className="grid md:grid-cols-2 gap-4">
                <div>
                  <Label htmlFor="edit-status">Status</Label>
                  <Select
                    value={editingDonghua.Status ? String(editingDonghua.Status) : ""}
                    onValueChange={(value) => setEditingDonghua({ ...editingDonghua, Status: convertStringToDonhguaStatus(value) })}
                  >
                    <SelectTrigger>
                      <SelectValue placeholder="Selecione o status" />
                    </SelectTrigger>
                    <SelectContent>
                      {Object.values(DonghuaStatus).map((status) => (
                        <SelectItem key={status} value={editingDonghua.Status ? String(editingDonghua.Status) : ""}>
                          {status}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>
                <div>
                  <Label htmlFor="edit-type">Tipo *</Label>
                  <Select
                    value={editingDonghua.Type}
                    onValueChange={(value) => setEditingDonghua({ ...editingDonghua, Type: convertStringToDonhguaType(value)})}
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
                    const currentGenres = editingDonghua.Genre || []
                    if (!currentGenres.includes(value)) {
                      setEditingDonghua({ ...editingDonghua, Genre: [...currentGenres, value] })
                    }
                  }}
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Adicionar gênero" />
                  </SelectTrigger>
                  <SelectContent>
                    {Object.values(editingDonghua.Genre).map((genre) => (
                      <SelectItem key={genre} value={genre}>
                        {genre}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
                <div className="flex flex-wrap gap-2 mt-2">
                  {editingDonghua.Genre?.map((genre) => (
                    <Badge
                      key={genre}
                      variant="secondary"
                      className="cursor-pointer"
                      onClick={() =>
                        setEditingDonghua({
                          ...editingDonghua,
                        Genre: editingDonghua.Genre?.filter((g) => g !== genre),
                        })
                      }
                    >
                      {genre} <X className="w-3 h-3 ml-1" />
                    </Badge>
                  ))}
                </div>
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
    )
}

function convertStringToDonhguaStatus(ValorEnum: string ): DonghuaStatus
{
  switch(ValorEnum) {
    case "Em Andamento":
      return DonghuaStatus.EmAndamento;
    
    case "Concluido":
      return DonghuaStatus.Concluido;

    case "Pausado":
      return DonghuaStatus.Pausado;
    default: 
        return DonghuaStatus.Concluido;
    }
}

function convertStringToDonhguaType(ValorEnum: string ): DonghuaType
{
  switch(ValorEnum) {
    case "Série":
      return DonghuaType.Serie;
    
    case "Filme":
      return DonghuaType.Movie;

    case "Ova":
      return DonghuaType.Special;

    default:
        return DonghuaType.Serie;

  }
}