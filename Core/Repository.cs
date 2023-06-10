using Microsoft.EntityFrameworkCore;
using SellPoint.DTO;
using SellPoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Repository
    {
        public async static Task<List<SaleDTO>> GetAllSales()
        {
            using PruebaContext _context = new();
            var query = "SELECT VD.Cantidad, V.Total, V.Fecha, P.Nombre AS Producto, L.Nombre AS Local, M.Nombre AS Marca " +
                        "FROM [dbo].[VentaDetalle] AS VD" +
                        "INNER JOIN [dbo].[Venta] V" +
                        "ON V.ID_Venta = VD.ID_Venta" +
                        "INNER JOIN [dbo].[Local] AS L" +
                        "ON L.ID_Local = V.ID_Local" +
                        "INNER JOIN [dbo].[Producto] P" +
                        "ON P.ID_Producto = VD.ID_Producto" +
                        "INNER JOIN [dbo].[Marca] M" +
                        "ON M.ID_Marca = P.ID_Marca";

            return await _context.Database.SqlQuery<SaleDTO>($"{query}").ToListAsync();
        }
    }
}
