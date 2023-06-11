using SellPoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.DTO
{
    public class HighestProduct
    {
        public Producto Producto { get; set; }
        public int TotalSales { get; set; }
    }
}
