using CazuelaChapina.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.Application.Features.Combos.Commands.DeleteCombo;

public class DeleteComboHandler
{
    private readonly IAppDbContext _context;

    public DeleteComboHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(Guid id)
    {
        var combo = await _context.Combos.FirstOrDefaultAsync(c => c.Id == id);
        if (combo == null)
            return false;

        _context.Combos.Remove(combo);
        await _context.SaveChangesAsync();
        return true;
    }
}
