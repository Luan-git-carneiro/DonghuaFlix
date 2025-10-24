
"use client"

import { useState } from "react"
import { Card, CardContent } from "@/ui/card"
import { Button } from "@/ui/button"
import { Input } from "@/ui/input"
import { Badge } from "@/ui/badge"
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
} from "@/ui/dialog"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"
import { DonghuaType } from "@/Domain/enum/donghuaType"
import { Donghua } from "@/Domain/entities/donghua"
import { Value } from "@radix-ui/react-select"
import { Genre } from "@/Domain/enum/genre"
import { EdicaoDonghua } from "@/featured/admin/manageDonghua/edicaoDonghua"
import { DeleteDonghua } from "@/featured/admin/manageDonghua/deleteDonghua"
import { CardDonghuaAdmin } from "@/featured/admin/manageDonghua/cardDonghua"


export default function GerenciarDonghua() {
  const [donghua, setDonghua] = useState<Donghua[]>([
    {
      DonghuaId: '1',
      Title: "Mo Dao Zu Shi",
      titleEnglish: "Grandmaster of Demonic Cultivation",
      description: "Uma história épica de cultivação e redenção",
      Image: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
      banner: "/mo-dao-zu-shi-anime-poster-with-dark-mystical-atmo.jpg",
      rating: 4.9,
      ReleaseYear: new Date("2018-07-09"),
      Status: DonghuaStatus.Concluido,
      Studio: "Tencent Penguin Pictures",
      Genre: ["Ação", "Fantasia", "Aventura"],
      Sinopse: "Wei Wuxian, um jovem cultivador, é temido por suas práticas heterodoxas...",
      Type: DonghuaType.Serie,
    },
    {
      DonghuaId: '2',
      Title: "The King's Avatar",
      titleEnglish: "Quan Zhi Gao Shou",
      description: "A jornada de um jogador profissional de e-sports",
      Image: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
      banner: "/the-king-s-avatar-gaming-anime-poster-with-futuris.jpg",
      rating: 4.8,
      ReleaseYear: new Date("2017-04-07"),
      Status: DonghuaStatus.Concluido,
      Studio: "G.CMay Animation & Film",
      Genre: ["Ação", "Fantasia", "Aventura"],
      Sinopse: "Ye Xiu, um jogador profissional de e-sports, é forçado a se aposentar...",
      Type: DonghuaType.Serie,
    },
    {
      DonghuaId: '3',
      Title: "Heaven Official's Blessing",
      titleEnglish: "Tian Guan Ci Fu",
      description: "Uma história celestial de amor e redenção",
      Image: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
      banner: "/heaven-official-s-blessing-celestial-anime-poster-.jpg",
      rating: 4.9,
      ReleaseYear: new Date("2020-10-31"),
      Status:  DonghuaStatus.EmAndamento,
      Studio: "Haoliners Animation League",
      Genre: ["Ação", "Fantasia", "Aventura"],
      Sinopse: "Xie Lian, um príncipe que ascendeu aos céus, é banido pela terceira vez...",
      Type: DonghuaType.Serie,
    },
  ])
  const [searchTerm, setSearchTerm] = useState("")
  const [editingDonghua, setEditingDonghua] = useState<Donghua | null>(null)
  const [deletingDonghua, setDeletingDonghua] = useState<Donghua | null>(null)

  const filteredDonghua = donghua.filter((anime) => anime.Title.toLowerCase().includes(searchTerm.toLowerCase()))

  const handleEdit = (anime: Donghua) => {
    setEditingDonghua({ ...anime })
  }

  const handleSaveEdit = () => {
    if (!editingDonghua) return
    setDonghua(donghua.map((anime) => (anime.DonghuaId === editingDonghua.DonghuaId ? editingDonghua : anime)))
    setEditingDonghua(null)
  }

  const handleDelete = (anime: Donghua) => {
    setDeletingDonghua(anime)
  }

  const confirmDelete = () => {
    if (!deletingDonghua) return
    setDonghua(donghua.filter((anime) => anime.DonghuaId !== deletingDonghua.DonghuaId))
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
        <CardDonghuaAdmin 
          donghuas={filteredDonghua}
          handleDelete={handleDelete}
          handleEdit={handleEdit}
        />
      </div>

      <EdicaoDonghua
        editingDonghua={editingDonghua} 
        setEditingDonghua={setEditingDonghua}
        handleSaveEdit={handleSaveEdit}    
      />

      <DeleteDonghua 
        deletingDonghua={deletingDonghua}
        setDeletingDonghua={setDeletingDonghua}
        confirmDelete={confirmDelete}
      />

    </div>
  )
}



