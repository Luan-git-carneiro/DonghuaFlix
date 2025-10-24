'use client'

import { Donghua } from "@/Domain/entities/donghua"
import { Badge } from "@/ui/badge"
import { Button } from "@/ui/button"
import { Card, CardContent } from "@/ui/card"
import { Star, Play, Eye, Edit, Trash2  } from "lucide-react"
import Link from "next/link"
import Image from "next/image"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"

interface CardDonghuaAdminProps
{
    donghuas: Donghua[] ;
    handleEdit: (donghua: Donghua) => void ;
    handleDelete: (donghua: Donghua)  => void ;

}

export const CardDonghuaAdmin = ({ donghuas, handleEdit , handleDelete }: CardDonghuaAdminProps) => 
{
    return (
        
        <>
            {
                donghuas.map((donghua: Donghua) => (
                        <Card key={donghua.DonghuaId}>
                        <CardContent className="p-6">
                            <div className="flex gap-6">
                            <div className="relative h-32 w-24 flex-shrink-0 overflow-hidden rounded-lg">
                                <Image src={donghua.Image || "/placeholder.svg"} alt={donghua.Title} fill className="object-cover" />
                            </div>
                            <div className="flex-1">
                                <div className="flex items-start justify-between">
                                <div>
                                    <h3 className="text-xl font-bold">{donghua.Title}</h3>
                                    {donghua.titleEnglish && <p className="text-sm text-muted-foreground">{donghua.titleEnglish}</p>}
                                    <div className="mt-2 flex flex-wrap items-center gap-3 text-sm text-muted-foreground">
                                    <span className="flex items-center gap-1">
                                        <Star className="h-4 w-4 fill-yellow-500 text-yellow-500" />
                                        {donghua.rating}
                                    </span>
                                    {donghua.ReleaseYear && <span>{donghua.ReleaseYear.getFullYear()}</span>}
                                    <Badge variant={donghua.Status === DonghuaStatus.Concluido ? "default" : "secondary"}>
                                        {donghua.Status}
                                    </Badge>
                                    <Badge variant="outline">{donghua.Type}</Badge>
                                    </div>
                                    <div className="mt-2 flex flex-wrap gap-2">
                                    {donghua.Genre?.map((genre) => (
                                        <Badge key={genre} variant="outline">
                                        {genre}
                                        </Badge>
                                    ))}
                                    </div>
                                    {donghua.Studio && <p className="mt-2 text-sm text-muted-foreground">Estúdio: {donghua.Studio}</p>}
                                    <p className="mt-2 text-sm text-muted-foreground line-clamp-2">{donghua.Sinopse}</p>
                                </div>
                                <div className="flex gap-2">
                                    <Link href={`/admin/donghua/${donghua.DonghuaId}/episodios`}>
                                    <Button variant="outline" size="icon" title="Gerenciar Episódios">
                                        <Play className="h-4 w-4" />
                                    </Button>
                                    </Link>
                                    <Link href={`/donghua/${donghua.DonghuaId}`}>
                                    <Button variant="outline" size="icon">
                                        <Eye className="h-4 w-4" />
                                    </Button>
                                    </Link>
                                    <Button variant="outline" size="icon" onClick={() => handleEdit(donghua)}>
                                    <Edit className="h-4 w-4" />
                                    </Button>
                                    <Button variant="outline" size="icon" onClick={() => handleDelete(donghua)}>
                                    <Trash2 className="h-4 w-4 text-destructive" />
                                    </Button>
                                </div>
                                </div>
                            </div>
                            </div>
                        </CardContent>
                        </Card>
                    )
                )
            }
        </>
    )
}