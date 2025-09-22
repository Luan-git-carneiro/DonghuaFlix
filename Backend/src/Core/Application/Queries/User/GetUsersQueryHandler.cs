

using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Queries.User;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery , GetUsersQueryResult >
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ;
    }

    public async Task<GetUsersQueryResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        // =================== 2. CONTAR REGISTROS (via Repository) ===================
        var TotalCount = await _userRepository.CountUsersAsync(request.SearchTerm);

        // =================== 3. CALCULAR METADADOS DE PAGINAÇÃO ===================
        var TotalPages = TotalCount == 0 ? 1 : (int)Math.Ceiling((double)TotalCount / request.PageSize);

        // =================== 4. 🎯 VALIDAÇÃO INTELIGENTE ===================
        var currentPage = request.Page;
        var WasPageAdjusted = false;

        // Se página solicitada > total páginas, redirecionar para última página válida
        if(request.Page > TotalPages && TotalPages > 0)
        {
            currentPage = TotalPages;
            WasPageAdjusted = true;
        }

        // =================== 5. BUSCAR DADOS (via Repository) ===================
        var users = TotalCount > 0 ? await _userRepository.GetUsersPagedAsync(
            request.Page,
            request.PageSize,
            request.SearchTerm
        ) : new List<UserDto>();

        // =================== 6. MONTAR RESPOSTA COMPLETA ===================
        return new GetUsersQueryResult
        {
            Users = users,
            TotalCount = TotalCount,
            TotalPages = TotalPages,
            CurrentPage = currentPage,
            RequestedPage = request.Page,
            WasPageAdjusted = WasPageAdjusted,
            HasNextPage = currentPage < TotalPages,
            HasPreviousPage = currentPage > 1,
            PageSize = request.PageSize
        };
        
    }
}