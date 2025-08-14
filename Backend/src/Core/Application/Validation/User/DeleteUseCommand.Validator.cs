

using DonghuaFlix.Backend.src.Core.Application.Commands.User;
using DonghuaFlix.Backend.src.Core.Application.Commands.User.DeleteUser;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.User;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .Must(id => id != Guid.Empty).WithMessage("Invalid UserId.");
    }
}

