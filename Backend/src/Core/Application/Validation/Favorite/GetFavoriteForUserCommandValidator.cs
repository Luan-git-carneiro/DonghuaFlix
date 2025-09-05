using DonghuaFlix.Backend.src.Core.Application.Queries.Favorites;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.Favorite;

public class GetFavoriteForUserCommandValidator : AbstractValidator<GetFavoriteForUserQuery>
{
    public GetFavoriteForUserCommandValidator()
    {
            RuleFor( f => f.UserID)
                .NotNull().WithMessage("UserId cannot be null.")
                    .NotEmpty().WithMessage("o Id Passado deve ser valido.");
    }
}