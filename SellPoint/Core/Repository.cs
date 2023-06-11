using Microsoft.EntityFrameworkCore;
using SellPoint.Core.ICore;
using SellPoint.Models;

namespace Core
{
    public class Repository : IRepository
    {
        public async Task<List<VentaDetalle>> GetAllSales()
        {
            using PruebaContext _context = new();
            return await _context.VentaDetalles
                .Include(v => v.IdVentaNavigation)
                .Include(e => e.IdVentaNavigation.IdLocalNavigation)
                .Include(c => c.IdProductoNavigation)
                .Include(c => c.IdProductoNavigation.IdMarcaNavigation)
                .Where(x => x.IdVentaNavigation.Fecha >= DateTime.Today.AddDays(-30))
                .ToListAsync();
        }
    }
}
