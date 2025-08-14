using DonghuaFlix.Backend.src.Core.Application.Commands.User;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Validation.User;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Custom( (email , context) => {
                try
                {
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
    
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

    }
}