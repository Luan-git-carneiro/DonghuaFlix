using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class DeleteDonghuaCommandValidator : AbstractValidator<DeleteDonghuaCommand>
{
   public DeleteDonghuaCommandValidator()
    {
        RuleFor(x => x.DonghuaId)
            .NotEqual(Guid.Empty)
            .WithMessage("ID do Donghua é necessário e deve ser válido");
    }

}   
   