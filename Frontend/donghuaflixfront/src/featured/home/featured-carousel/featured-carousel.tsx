"use client"

import { Carousel } from "@/components/carousel/Carousel"
import { Donghua } from "@/Domain/entities/donghua"
import { DonghuaStatus } from "@/Domain/enum/donghuaStatus"
import { DonghuaType } from "@/Domain/enum/donghuaType"
import { useState } from "react"

export const FeaturedCarousel = () =>
{
  const [donghuas , SetDonghuas] =  useState<Donghua[]>( 
      [
          // Objeto 1: Mo Dao Zu Shi (conforme o banner da imagem)
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

        // Objeto 4: Um anime de Comédia
        {
          DonghuaId: '4',
          Title: 'A Mortal\'s Journey to Immortality',
          titleEnglish: "Tian Guan Ci Fu",
          description: 'Um garoto rural acidentalmente se junta a uma seita e embarca em sua jornada para se tornar um imortal.',
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
    ]
  )


  return (
    <>
      <Carousel Donghuas={donghuas} />
    </>
  )

}