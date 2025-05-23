using System.Linq.Expressions;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.Backend.src.Infrastructure.Persistence.Repositories;

public class DonghuaRepository : IDonghuaRepository
{
    private readonly AppDbContext _context;

    public DonghuaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Donghua?> GetByIdAsync(Guid donghuaId)
    {
        if(donghuaId == Guid.Empty)
        {
            throw new DomainValidationException(field: nameof(donghuaId) , message: "Id do donghua é inválido.");
        }

        return await _context.Donghuas.FirstOrDefaultAsync(d => d.Id == donghuaId);
    }
    public async Task<List<Donghua>> GetAllAsync(int limit)
    {
        return await _context.Donghuas.Take(limit).ToListAsync();
    }

    public async Task AddAsync(Donghua donghua)
    {
        if(donghua is null)
        {
            throw new DomainValidationException(field: nameof(donghua) , message: "Donghua é nulo.");
        }

        await _context.Donghuas.AddAsync(donghua);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Donghua donghua)
    {
        if(donghua is null)
        {
            throw new DomainValidationException(field: nameof(donghua) , message: "Donghua é nulo.");
        }

        _context.Donghuas.Update(donghua);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Donghua donghua)
    {
        if(donghua is null)
        {
            throw new DomainValidationException(field: nameof(donghua) , message: "Donghua é nulo.");
        }

        _context.Donghuas.Remove(donghua);
        await _context.SaveChangesAsync();
    }

    // Implementação do AnyAsync
    public async Task<bool> AnyAsync(Expression<Func<Donghua, bool>> predicate)
    {
        return await _context.Donghuas.AnyAsync(predicate);
    }

    public async Task<(List<Donghua> Items, int TotalItems)> GetPagedAsync(int page, int pageSize, string? searchTerm = null)
    {
        var query = _context.Donghuas.AsQueryable();

        // Aplicar filtro de pesquisa, se fornecido
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(d => d.Title.Contains(searchTerm));
        }

        // Calcula total de itens
        var totalItems = await query.CountAsync();

        // Aplica paginação
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (items, totalItems);
    }
}