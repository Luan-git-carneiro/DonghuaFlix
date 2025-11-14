import { AnimeResult } from "@/app/admin/adicionar/page";
import { Badge } from "@/ui/badge";
import { Card, CardContent } from "@/ui/card";
import { Star } from "lucide-react";


interface CardDonghuaProps {
    donghua: AnimeResult
    onSelectDonghua: (donghua: AnimeResult) => void
}

export default function CardDonghua({ donghua, onSelectDonghua }: CardDonghuaProps) {
    return (
        <Card
            key={donghua.mal_id}
            className="overflow-hidden cursor-pointer hover:border-primary transition-all hover:shadow-lg group"
            onClick={() => onSelectDonghua(donghua)}
        >
                  <div className="relative aspect-[2/3] overflow-hidden">
                    <img
                      src={donghua.images.jpg.large_image_url || "/placeholder.svg"}
                      alt={donghua.title}
                      className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-300"
                    />
                    {donghua.score && (
                      <Badge className="absolute top-2 right-2 bg-primary/90">
                        <Star className="w-3 h-3 mr-1 fill-current" />
                        {donghua.score.toFixed(1)}
                      </Badge>
                    )}
                  </div>
                  <CardContent className="p-3">
                    <h3 className="font-semibold text-sm line-clamp-2 mb-1">{donghua.title}</h3>
                    <div className="flex items-center gap-2 text-xs text-muted-foreground">
                      {donghua.year && <span>{donghua.year}</span>}
                      {donghua.episodes && (
                        <>
                          <span>•</span>
                          <span>{donghua.episodes} eps</span>
                        </>
                      )}
                    </div>
                  </CardContent>
        </Card>
    )
}