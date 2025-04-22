using DonghuaFlix.src.Core.Domain.Enum;

namespace DonghuaFlix.src.Core.Application.Helpers;

public static class GenreConverter
{
    public static Genre ConvertStringsToGenreFlags(IEnumerable<string>? genreNames)
    {
        Genre combinedGenres = 0; // Começa com nenhum flag (valor 0)

        if (genreNames == null)
        {
            return combinedGenres; // Ou talvez lançar exceção se genres for obrigatório?
        }

        foreach (var name in genreNames)
        {
            // Ignora strings vazias ou nulas que possam estar na lista
            if (string.IsNullOrWhiteSpace(name)) continue;

            // Tenta converter a string (case-insensitive) para o enum Genre
            if (Enum.TryParse<Genre>(name.Trim(), true, out var genreValue))
            {
                combinedGenres |= genreValue; // Combina o valor usando OR bitwise
            }
            else
            {
                // O que fazer com um nome inválido?
                // Opção 1: Lançar exceção (recomendado para validação de entrada)
                throw new ArgumentException($"Gênero inválido fornecido: '{name}'");
                // Opção 2: Ignorar o valor inválido (menos seguro)
                // Console.WriteLine($"Aviso: Gênero inválido ignorado: '{name}'");
            }
        }
        return combinedGenres;
    }


    public static List<string> ConvertGenreFlagsToStrings(Genre genreFlags)
    {
        var result = new List<string>();

        // Itera sobre todos os valores definidos no enum Genre
        foreach (Genre value in Enum.GetValues(typeof(Genre)))
        {
            // Ignora o valor '0' se você tiver um Genre.None = 0 (não tem no seu caso)
            // Verifica se o bit correspondente a 'value' está ativo em 'genreFlags'
            if (value != 0 && genreFlags.HasFlag(value)) // HasFlag é mais legível que (genreFlags & value) == value
            {
                result.Add(value.ToString()); // Adiciona o nome do enum à lista
            }
        }
        return result;
    }


}