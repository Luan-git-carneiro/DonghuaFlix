using DonghuaFlix.Backend.src.Core.Application.Commands.Favorites;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.Favorite;

public class DeleteFavoriteCommandValidator : AbstractValidator<DeleteFavoriteCommand>
{
    public DeleteFavoriteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId cannot be null.")
                .NotEmpty().WithMessage("o Id Passado deve ser valido.");

        RuleFor(x => x.DonghuaId)
            .NotNull().WithMessage("UserId cannot be null.")
                .NotEmpty().WithMessage("o Id Passado deve ser valido.");
    }
}