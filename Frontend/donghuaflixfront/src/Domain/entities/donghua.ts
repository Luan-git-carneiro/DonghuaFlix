import { DonghuaStatus } from "../enum/donghuaStatus"
import { DonghuaType } from "../enum/donghuaType"
import { Genre } from "../enum/genre"

export interface Donghua {
    DonghuaId: string
    Title: string
    titleEnglish: string
    description: string
    banner: string
    rating: number
    Sinopse: string
    Studio: string
    ReleaseYear: Date
    Genre: string[]
    Type: DonghuaType
    Status: DonghuaStatus
    Image: string | null
  

  }