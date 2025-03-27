using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.src.Infrastructure.Persistence.Repositories;

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

        return await _context.Donghuas.Include( d => d.Episodes).ThenInclude( e => e.Video).FirstOrDefaultAsync(d => d.Id == donghuaId);
    }
    public async Task<List<Donghua>> GetAllAsync(int limit)
    {
        return await _context.Donghuas.Include( d => d.Episodes).ThenInclude( e => e.Video).Take(limit).ToListAsync();
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

    public async Task<bool> ExistsAsync(Guid id)
        => await _context.Donghuas.AnyAsync(d => d.Id == id);
}