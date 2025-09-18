import { Genre } from "@/Domain/enum/genre";

// Converte valor bitwise para array de strings
export  const BitwiseToArray = (value: number): string[] => 
{
    return Object.entries(Genre)
        .filter(([name , val]) => typeof val == 'number' && (value & val) ===val)
        .map(([name]) => name);
}