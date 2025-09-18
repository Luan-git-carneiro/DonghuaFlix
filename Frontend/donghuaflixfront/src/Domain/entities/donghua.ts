import { DonghuaStatus } from "../enum/donghuaStatus"
import { DonghuaType } from "../enum/donghuaType"
import { Genre } from "../enum/genre"

export class Donghua {
    DonghuaId!: string;
    Title!: string;
    Sinopse!: string;
    Studio!: string;
    ReleaseYear!: string;
    Genre!: string[];
    Type!: DonghuaType;
    Status!: DonghuaStatus;
    Image!: string | null;
  
    constructor(
      DonghuaId: string,
      Title: string,
      Sinopse: string,
      Studio: string,
      ReleaseYear: string,
      Genre: string[],
      Type: DonghuaType,
      Status: DonghuaStatus,
      Image: string | null
    ) {
      this.DonghuaId = DonghuaId;
      this.Title = Title;
      this.Sinopse = Sinopse;
      this.Studio = Studio;
      this.ReleaseYear = ReleaseYear;
      this.Genre = Genre;
      this.Type = Type;
      this.Status = Status;
      this.Image = Image;
    }
  }