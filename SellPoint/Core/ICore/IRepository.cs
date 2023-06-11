using SellPoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Core.ICore
{
    public interface IRepository
    {
        public Task<List<VentaDetalle>> GetAllSales();
    }
}
