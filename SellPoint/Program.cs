using SellPoint.Core;
using Core;

//Get Data
Repository _repo = new();
var ventas = await _repo.GetAllSales();
var service = new SellService(ventas);
//El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
var highestAmount = service.GetHighestAmount();
//Indicar cuál es el producto con mayor monto total de ventas
var highestTotalProduct = service.GetHighestTotalProduct();
//¿Cuál es la marca con mayor margen de ganancias?
var brandsSales = service.GetBrandsSales();
//¿Cómo obtendrías cuál es el producto que más se vende en cada local?
var localsWithProducts = service.GetLocalsWithProducts();

//Results
Console.WriteLine($"El monto total de las ventas es: {ventas.Sum(v => v.IdVentaNavigation.Total)} y la cantidad total fue: {ventas.Sum(x => x.Cantidad)} \n");
Console.WriteLine($"La venta con el monto mas alto fue de: {highestAmount.IdVentaNavigation.Total}, este se realizo el dia: {highestAmount.IdVentaNavigation.Fecha.Day} a la hora {highestAmount.IdVentaNavigation.Fecha.Hour} \n");
Console.WriteLine($"El producto con mayor monto total de ventas es: {highestTotalProduct.Producto.Nombre} con un monto de: {highestTotalProduct.TotalSales} \n");
Console.WriteLine($"El local con mayor monto de ventas es: {highestAmount.IdVentaNavigation.IdLocalNavigation.Nombre} con un monto de: {highestAmount.IdVentaNavigation.Total} \n");
Console.WriteLine($"La marca con mayor margen de ganancia es: {brandsSales.Marca.Nombre} con un margen de: {brandsSales.MarginProfit} \n");
foreach (var item in localsWithProducts)
    Console.WriteLine($"Local: {item.Local}; Producto: {item.Producto}");

Console.ReadLine();