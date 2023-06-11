using SellPoint.DTO;
using SellPoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Core.ICore
{
    public interface ISellService
    {
        public VentaDetalle GetHighestAmount();
        public HighestProduct GetHighestTotalProduct();
        public BrandSales GetBrandsSales();
        public IEnumerable<LocalSales> GetLocalsWithProducts();
    }
}
