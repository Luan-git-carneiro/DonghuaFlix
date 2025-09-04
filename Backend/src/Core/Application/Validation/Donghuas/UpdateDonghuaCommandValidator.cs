using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.Donghuas;

    public class UpdateDonghuaCommandValidator : AbstractValidator<UpdateDonghuaCommand>
    {
        public UpdateDonghuaCommandValidator()
        {
            // Validação do ID (obrigatório)
            RuleFor(x => x.DonghuaId)
                .NotEmpty()
                .WithMessage("DonghuaId é obrigatório.");

            // Validação do Title (opcional, mas se informado deve ser válido)
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório quando informado.")
                .MaximumLength(200).WithMessage("O título não pode ter mais de 200 caracteres.")
                .When(x => x.Title != null);

            // Validação da Sinopse (opcional, mas se informada deve ser válida)
            RuleFor(x => x.Sinopse)
                .NotEmpty().WithMessage("A sinopse é obrigatória quando informada.")
                .MaximumLength(1000).WithMessage("A sinopse não pode ter mais de 1000 caracteres.")
                .When(x => x.Sinopse != null);

            // Validação do Studio (opcional, mas se informado deve ser válido)
            RuleFor(x => x.Studio)
                .NotEmpty().WithMessage("O estúdio é obrigatório quando informado.")
                .MaximumLength(100).WithMessage("O estúdio não pode ter mais de 100 caracteres.")
                .When(x => x.Studio != null);

            // Validação da Data de Lançamento (opcional, mas se informada deve ser válida)
            RuleFor(x => x.releaseDate)
                .Must(BeAValidYear).WithMessage("O ano de lançamento deve ser válido.")
                .When(x => x.releaseDate.HasValue);

            // Validação do Gênero (opcional, mas se informado deve ser válido)
            RuleFor(x => x.Genres)
                .Must(BeAValidGenreCombination).WithMessage("Combinação de gêneros inválida.")
                .When(x => x.Genres.HasValue);

            // Validação do Tipo (opcional, mas se informado deve ser válido)
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo de donghua inválido.")
                .When(x => x.Type.HasValue);

            // Validação do Status (opcional, mas se informado deve ser válido)
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status inválido.")
                .When(x => x.Status.HasValue);

            // Validação da Imagem (opcional, mas se informada deve ser válida)
            RuleFor(x => x.Image)
                .MaximumLength(500).WithMessage("A URL da imagem não pode ter mais de 500 caracteres.")
                .Must(BeAValidUrlOrNull).WithMessage("A URL da imagem deve ser válida.")
                .When(x => x.Image != null);

            // Regra customizada: pelo menos um campo deve ser informado para atualização
            RuleFor(x => x)
                .Must(HaveAtLeastOneFieldToUpdate)
                .WithMessage("Pelo menos um campo deve ser informado para atualização.")
                .OverridePropertyName("UpdateFields");
        }

        private bool BeAValidYear(DateTime? releaseDate)
        {
            if (!releaseDate.HasValue) return true;
            return releaseDate.Value.Year >= 1900 && releaseDate.Value.Year <= DateTime.Now.Year + 5;
        }

        private bool BeAValidGenreCombination(Genre? genres)
        {
            if (!genres.HasValue) return true;
            if (genres.Value == 0) return false;
            
            // Obter todos os valores válidos como máscara bitwise
            int allValidValues = 0;
            foreach (Genre value in Enum.GetValues(typeof(Genre)))
            {
                allValidValues |= (int)value;
            }
            
            // Verificar se todos os bits definidos estão na máscara de valores válidos
            return ((int)genres.Value & ~allValidValues) == 0;
        }

        private bool BeAValidUrlOrNull(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private bool HaveAtLeastOneFieldToUpdate(UpdateDonghuaCommand command)
        {
            return !string.IsNullOrEmpty(command.Title) ||
                   !string.IsNullOrEmpty(command.Sinopse) ||
                   !string.IsNullOrEmpty(command.Studio) ||
                   command.releaseDate.HasValue ||
                   command.Genres.HasValue ||
                   command.Type.HasValue ||
                   command.Status.HasValue ||
                   !string.IsNullOrEmpty(command.Image);
        }
    }