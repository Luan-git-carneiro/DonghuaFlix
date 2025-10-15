using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.Donghuas;

public class CreateDonghuaCommandValidator : AbstractValidator<CreateDonghuaCommand>
{
    public CreateDonghuaCommandValidator()
    {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(500).WithMessage("O título não pode ter mais de 200 caracteres.");

             RuleFor(x => x.TitleEnglish)
                .MaximumLength(500).WithMessage("O Titulo EM english não pode ter mais de 1000 caracteres.");

             RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("A Descrição não pode ter mais de 2000 caracteres.");

            RuleFor(x => x.Sinopse)
                .NotEmpty().WithMessage("A sinopse é obrigatória.")
                .MaximumLength(1000).WithMessage("A sinopse não pode ter mais de 1000 caracteres.");

            RuleFor(x => x.Studio)
                .MaximumLength(100).WithMessage("O estúdio não pode ter mais de 100 caracteres.");

            RuleFor(x => x.ReleaseYear)
                .Must(BeAValidYear).WithMessage("O ano de lançamento deve ser válido.");


            RuleFor(x => x.Genres)
                .Must(BeAValidGenreCombination).WithMessage("Combinação de gêneros inválida.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo de donghua inválido.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status inválido.");

            RuleFor(x => x.Image)
                .MaximumLength(500).WithMessage("A URL da imagem não pode ter mais de 500 caracteres.")
                .Must(BeAValidUrlOrNull).WithMessage("A URL da imagem deve ser válida.")
                .When(x => !string.IsNullOrEmpty(x.Image));

            RuleFor(x => x.Banner)
                .MaximumLength(500).WithMessage("A URL do Banner não pode ter mais de 500 caracteres.");

            RuleFor(x => x.Trailer)
                .MaximumLength(500).WithMessage("A URL do Tariler não pode ter mais de 500 caracteres.");
 
                
    }

        private bool BeAValidYear(DateTime? releaseYear)
        {
            if (!releaseYear.HasValue) return true;
            return releaseYear.Value.Year >= 1900 && releaseYear.Value.Year <= DateTime.Now.Year + 5;
        }

        private bool BeAValidGenreCombination(Genre genres)
        {
            if (genres == 0) return false;
            
            // Obter todos os valores válidos como máscara bitwise
            int allValidValues = 0;
            foreach (Genre value in Enum.GetValues(typeof(Genre)))
            {
                allValidValues |= (int)value;
            }
            
            // Verificar se todos os bits definidos estão na máscara de valores válidos
            return ((int)genres & ~allValidValues) == 0;
        }

        private bool BeAValidUrlOrNull(string url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

}