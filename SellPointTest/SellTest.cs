using Core;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using SellPoint.Core;
using SellPoint.Models;

namespace SellPointTest
{
    [TestClass]
    public class SellTest
    {
        private readonly Repository _repository;
        private SellService _service;
        public SellTest() => _repository = new Repository();

        [TestMethod]
        public async Task ShouldReturnAlistWithAllSales()
        {
            var result = await _repository.GetAllSales();
            var allSalesAreLess30Days = result.Any(x => x.IdVentaNavigation.Fecha >= DateTime.Today.AddDays(-30));
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(allSalesAreLess30Days);
        }

        [TestMethod]
        public async Task ShouldReturnTheHighestAmount()
        {
            var ventas = await _repository.GetAllSales();
            _service = new SellService(ventas);
            var result = _service.GetHighestAmount();
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IdVentaNavigation.Total == ventas.Max(v => v.IdVentaNavigation.Total));
        }

        [TestMethod]
        public async Task ShouldReturnTheBrandWithHighestAmount()
        {
            var ventas = await _repository.GetAllSales();
            _service = new SellService(ventas);
            var result = _service.GetBrandsSales();
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.MarginProfit > 0);
            Assert.AreEqual(result.Marca.Nombre, "Soley");
        }

        [TestMethod]
        public async Task ShouldReturnTheHighestTotalProduct()
        {
            var ventas = await _repository.GetAllSales();
            _service = new SellService(ventas);
            var result = _service.GetHighestTotalProduct();
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TotalSales > 0);
            Assert.AreEqual(result.Producto.Nombre, "Café Modelo LNBCTD");
        }

        [TestMethod]
        public async Task ShouldReturnTheLocalsWithHighestSales()
        {
            var ventas = await _repository.GetAllSales();
            _service = new SellService(ventas);
            var result = _service.GetLocalsWithProducts();
            //Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }
    }
}