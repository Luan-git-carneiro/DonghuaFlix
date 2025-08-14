using DonghuaFlix.Backend.src.Core.Application.Commands.User.Login;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using FluentValidation;

namespace Backend.Core.Application.Validation.User;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Custom((email, context) => {

                try{

                    new Email(email);
                }
                catch (DomainValidationException ex)
                {
                    context.AddFailure("Email", ex.Message);
                }

            });


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Custom( (password, context) => {

                try
                {
                    new Password(password);
                }
                catch (DomainValidationException ex)
                {
                    context.AddFailure("Password", ex.Message);
                }

            });
    }
}