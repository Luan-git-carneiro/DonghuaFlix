//Validação dos parametros

using FluentValidation;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua;

public class ListDonghuasPagedQueryValidator : AbstractValidator<ListDonghuasPagedQuery>
{
    public ListDonghuasPagedQueryValidator()
    {
        RuleFor( x => x.Page)
            .GreaterThan(0).WithMessage("A pagina deve ter mais que 0 Paginas.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("A pagina deve ter entre 1 e 100 itens por pagina.");
    }
}