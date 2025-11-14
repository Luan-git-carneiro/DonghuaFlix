import { AnimeResult } from "@/app/admin/adicionar/page"
import { Label } from "@/components/ui/label"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Textarea } from "@/components/ui/textarea"
import { Donghua } from "@/Domain/entities/donghua"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"
import { DonghuaType } from "@/Domain/enum/donghuaType"
import { Genre } from "@/Domain/enum/genre"
import { Badge } from "@/ui/badge"
import { Button } from "@/ui/button"
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/ui/dialog"
import { Input } from "@/ui/input"
import { Save, X } from "lucide-react"


interface PainelForSaveProps 
{
    isOpen: boolean
    donghuaForm: Donghua
    onSetDonghuaForm: (donghua: Donghua ) => void
    onSaveDonghua: () => void
    onClearDonghuaForm: () => void
    
}


export default function PainelForSave({ isOpen , donghuaForm ,onClearDonghuaForm, onSetDonghuaForm , onSaveDonghua}: PainelForSaveProps) 
{
     
    return ( 
        <Dialog open={isOpen} onOpenChange={() => onClearDonghuaForm()}>
        <DialogContent className="!max-w-5xl !h-[80vh] overflow-y-auto">
          {donghuaForm && (
            <>
              <DialogHeader>
                <DialogTitle className="text-2xl">Adicionar Donghua ao Catálogo</DialogTitle>
              </DialogHeader>

              <div className="grid md:grid-cols-[250px,1fr] gap-6">
                <div className="space-y-4">
                  <img
                    src={donghuaForm.Image || "/placeholder.svg"}
                    alt={donghuaForm.Title}
                    className="w-[65%] rounded-lg mx-auto shadow-lg"
                  />
                </div>

                <div className="space-y-4">
                  <div className="grid md:grid-cols-2 gap-4">
                    <div>
                      <Label htmlFor="title">Título *</Label>
                      <Input
                        id="title"
                        value={donghuaForm.Title}
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, Title: e.target.value })}
                        required
                      />
                    </div>
                    <div>
                      <Label htmlFor="titleEnglish">Título em Inglês</Label>
                      <Input
                        id="titleEnglish"
                        value={donghuaForm.titleEnglish}
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, titleEnglish: e.target.value })}
                      />
                    </div>
                  </div>

                  <div>
                    <Label htmlFor="description">Descrição</Label>
                    <Textarea
                      id="description"
                      value={donghuaForm.description}
                      onChange={(e) => onSetDonghuaForm({ ...donghuaForm, description: e.target.value })}
                      rows={2}
                    />
                  </div>

                  <div>
                    <Label htmlFor="sinopse">Sinopse *</Label>
                    <Textarea
                      id="sinopse"
                      value={donghuaForm.Sinopse}
                      onChange={(e) => onSetDonghuaForm({ ...donghuaForm, Sinopse: e.target.value })}
                      rows={3}
                      required
                    />
                  </div>

                  <div className="grid md:grid-cols-2 gap-4">
                    <div>
                      <Label htmlFor="image">URL da Imagem *</Label>
                      <Input
                        id="image"
                        value={!!donghuaForm.Image ?  donghuaForm.Image : ""}
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, Image: e.target.value })}
                        required
                      />
                    </div>
                    <div>
                      <Label htmlFor="banner">URL do Banner</Label>
                      <Input
                        id="banner"
                        value={donghuaForm.banner}
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, banner: e.target.value })}
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
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, rating: Number.parseFloat(e.target.value) })}
                      />
                    </div>
                    <div>
                      <Label htmlFor="releaseDate">Data de Lançamento</Label>
                      <Input
                        id="releaseDate"
                        type="date"
                        value={donghuaForm.ReleaseYear?.toISOString().split("T")[0] || ""}
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, ReleaseYear: new Date(e.target.value) })}
                      />
                    </div>
                    <div>
                      <Label htmlFor="studio">Estúdio</Label>
                      <Input
                        id="studio"
                        value={donghuaForm.Studio}
                        onChange={(e) => onSetDonghuaForm({ ...donghuaForm, Studio: e.target.value })}
                      />
                    </div>
                  </div>

                  <div className="grid md:grid-cols-2 gap-4">
                    <div>
                      <Label htmlFor="status">Status</Label>
                      <Select
                        value={donghuaForm.Status}
                        onValueChange={(value) => onSetDonghuaForm({ ...donghuaForm, Status: value as DonghuaStatus })}
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
                        value={donghuaForm.Type}
                        onValueChange={(value) => onSetDonghuaForm({ ...donghuaForm, Type: value as DonghuaType })}
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
                      <label htmlFor="genres">Gêneros</label>
                      <Select
                        value={donghuaForm?.Genre?.[0]}
                        onValueChange={(value) => { ... }}
                      >
                        <SelectTrigger>
                          <SelectValue placeholder="Adicionar gênero" />
                        </SelectTrigger>

                        <SelectContent>
                          {Object.values(String).map((genre) => (
                            <SelectItem key={genre} value={genre}>
                              {genre}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>

                      <div className="flex flex-wrap gap-2 mt-2">
                        {donghuaForm.Genre?.map((genre) => (
                          <Badge
                            key={genre}
                            variant="secondary"
                            className="cursor-pointer"
                            onClick={() => {
                              onSetDonghuaForm({
                                ...donghuaForm,
                                Genre: donghuaForm.Genre.filter((g) => g !== genre),
                              });
                            }}
                          >
                            {genre} <X className="w-3 h-3 ml-1" />
                          </Badge>
                        ))}
                      </div>
                  </div>
                


                  <div className="flex gap-2 pt-4">
                    <Button type="button" variant="outline" onClick={() => onClearDonghuaForm()} className="flex-1">
                      Cancelar
                    </Button>
                    <Button type="button" onClick={onSaveDonghua} className="flex-1">
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

    )
}