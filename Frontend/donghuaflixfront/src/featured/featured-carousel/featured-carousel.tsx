import { Carousel } from "@/components/carousel/Carousel"
import { Donghua } from "@/Domain/entities/donghua"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"
import { DonghuaType } from "@/Domain/enum/donghuaType"
import { useState } from "react"

export const FeaturedCarousel = () =>
{
  const [donghuas , SetDonghuas] =  useState( 
      [
          // Objeto 1: Mo Dao Zu Shi (conforme o banner da imagem)
        new Donghua(
          '1', // DonghuaId
          'Mo Dao Zu Shi', // Title
          'Wei Wuxian, conhecido como o Yiling Patriarch, retorna ao mundo dos vivos para desvendar mistérios do passado.', // Sinopse
          'B.C. & E.C.', // Studio
          '2018', // ReleaseYear
          ["Ação , Aventura"], // Genre
          DonghuaType.Serie, // Type
          DonghuaStatus.Concluido, // Status
          'https://via.placeholder.com/1200x675.png?text=Mo+Dao+Zu+Shi+Banner' // Image (substitua pela URL real)
        ),

        // Objeto 2: Um anime de Ação e Romance
        new Donghua(
          '2',
          'Battle Through the Heavens',
          'Xiao Yan, um gênio da cultivação, perde todo o seu poder e precisa embarcar em uma jornada para recuperá-lo.',
          'Magic-Antimation',
          '2017',
          ["Aventura" , "Comedia"],
          DonghuaType.Serie,
          DonghuaStatus.EmAndamento,
          'https://via.placeholder.com/300x450.png?text=Battle+Through+the+Heavens'
        ),
        
        // Objeto 3: Um anime Histórico
        new Donghua(
          '3',
          'The Legend of Qin',
          'A história do jovem Tianming, que se envolve na tumultuada ascensão da dinastia Qin.',
          'Sparkly Key Animation Studio',
          '2007',
          ["Aventura" , "Comedia"],
          DonghuaType.Serie,
          DonghuaStatus.EmAndamento,
          'https://via.placeholder.com/300x450.png?text=The+Legend+of+Qin'
        ),

        // Objeto 4: Um anime de Comédia
        new Donghua(
          '4',
          'A Mortal\'s Journey to Immortality',
          'Um garoto rural acidentalmente se junta a uma seita e embarca em sua jornada para se tornar um imortal.',
          'Bilibili',
          '2020',
          ["Aventura" , "Comedia"],
          DonghuaType.Serie,
          DonghuaStatus.EmAndamento,
          'https://via.placeholder.com/300x450.png?text=A+Mortal%27s+Journey'
        ),
    ]
  )


  return (
    <>
      <Carousel Donghuas={donghuas} />
    </>
  )

}