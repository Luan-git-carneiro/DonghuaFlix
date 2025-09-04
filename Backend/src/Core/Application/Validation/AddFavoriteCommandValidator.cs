using DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation;

public class AddFavoriteCommandValidator : AbstractValidator<AddFavoriteCommand>
{
    public AddFavoriteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId cannot be null.")
                .NotEmpty().WithMessage("o Id Passado deve ser valido.");

        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId cannot be null.")
                .NotEmpty().WithMessage("o Id Passado deve ser valido.");
    }
}