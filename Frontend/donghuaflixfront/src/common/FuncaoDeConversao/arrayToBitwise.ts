import { Genre } from "@/Domain/enum/genre";

// Converte array de strings para valor bitwise
export const arrayToBitwise = (genres: string[]): number => {
    return genres.reduce((acc, genre) => {
      const genreValue = Genre[genre as keyof typeof Genre];
      return typeof genreValue === 'number' ? acc | genreValue : acc;
    }, 0);
  }