
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries;

public class GetDonghuaByIdQueryValidator : AbstractValidator<GetDonghuaByIdQuery>
{
    public GetDonghuaByIdQueryValidator()
    {
        RuleFor(x => x.DonghuaId)
            .NotEmpty().WithMessage("Donghua ID cannot be empty.")
            .NotNull().WithMessage("Donghua ID cannot be null.");
    }
}