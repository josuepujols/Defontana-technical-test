using Core;
using SellPoint.Core.ICore;
using SellPoint.DTO;
using SellPoint.Models;

namespace SellPoint.Core
{
    public class SellService : ISellService
    {
        private readonly List<VentaDetalle> ventas;
        public  SellService(List<VentaDetalle> _ventas) => ventas = _ventas;

        public VentaDetalle GetHighestAmount() => ventas.OrderByDescending(x => x.IdVentaNavigation.Total).First();

        public BrandSales GetBrandsSales()
        {
            return ventas
                .GroupBy(x => x.IdProductoNavigation.IdMarcaNavigation, x => (x.IdVentaNavigation.Total - x.PrecioUnitario) * x.Cantidad)
                .Select(v => new BrandSales { Marca = v.Key, MarginProfit = v.Sum() })
                .OrderByDescending(e => e.MarginProfit)
                .First();
        }
        
        public HighestProduct GetHighestTotalProduct()
        {
            return ventas
                .GroupBy(x => x.IdProductoNavigation)
                .Select(v => new HighestProduct { Producto = v.Key, TotalSales = v.Sum(c => c.IdVentaNavigation.Total) })              
                .OrderByDescending(f => f.TotalSales)
                .First();
        }

        public IEnumerable<LocalSales> GetLocalsWithProducts()
        {
            return ventas
                .GroupBy(v => new { Local = v.IdVentaNavigation.IdLocalNavigation.Nombre, Producto = v.IdProductoNavigation.Nombre })
                .OrderByDescending(x => x.Count())
                .Select(f => new LocalSales { Local = f.Key.Local, Producto = f.Key.Producto });
        }
    }
}
