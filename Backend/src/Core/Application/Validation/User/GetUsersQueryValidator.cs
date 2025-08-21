

using DonghuaFlix.Backend.src.Core.Application.Queries.User;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.User;

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");


    }
}