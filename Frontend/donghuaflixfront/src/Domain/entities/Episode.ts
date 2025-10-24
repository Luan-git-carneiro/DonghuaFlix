export interface Episode {
    id?: number
    donghuaId: number
    episodeNumber: number
    title: string
    description?: string
    thumbnail?: string
    videoUrl: string
    duration?: number // em minutos
    releaseDate?: Date
    views?: number
  }