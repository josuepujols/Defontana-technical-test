using Core;

//Get Data
var ventas = await Repository.GetAllSales();
//El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
var highestAmount = ventas.OrderByDescending(x => x.IdVentaNavigation.Total).First();

//Indicar cuál es el producto con mayor monto total de ventas
var highestTotalProduct = ventas
    .GroupBy(x => x.IdProductoNavigation)
    .Select(v => new 
    {
        Producto = v.Key,
        TotalSales = v.Sum(c => c.IdVentaNavigation.Total)
    }).OrderByDescending(f => f.TotalSales).First();

//¿Cuál es la marca con mayor margen de ganancias?
var brandsSales = ventas
    .GroupBy(x => x.IdProductoNavigation.IdMarcaNavigation, x => (x.IdVentaNavigation.Total - x.PrecioUnitario) * x.Cantidad)
    .Select(v => new
    {
        Marca = v.Key,
        MarginProfit = v.Sum()
    }).OrderByDescending(e => e.MarginProfit).First();

//¿Cómo obtendrías cuál es el producto que más se vende en cada local?
var localsWithProducts = ventas
    .GroupBy(v => new { Local = v.IdVentaNavigation.IdLocalNavigation.Nombre, Producto = v.IdProductoNavigation.Nombre })
    .OrderByDescending(x => x.Count())
    .Select(f => new { f.Key.Local, f.Key.Producto });


//Results
Console.WriteLine($"El monto total de las ventas es: {ventas.Sum(v => v.IdVentaNavigation.Total)} y la cantidad total fue: {ventas.Sum(x => x.Cantidad)} \n");
Console.WriteLine($"La venta con el monto mas alto fue de: {highestAmount.IdVentaNavigation.Total}, este se realizo el dia: {highestAmount.IdVentaNavigation.Fecha.Day} a la hora {highestAmount.IdVentaNavigation.Fecha.Hour} \n");
Console.WriteLine($"El producto con mayor monto total de ventas es: {highestTotalProduct.Producto.Nombre} con un monto de: {highestTotalProduct.TotalSales} \n");
Console.WriteLine($"El local con mayor monto de ventas es: {highestAmount.IdVentaNavigation.IdLocalNavigation.Nombre} con un monto de: {highestAmount.IdVentaNavigation.Total} \n");
Console.WriteLine($"La marca con mayor margen de ganancia es: {brandsSales.Marca.Nombre} con un margen de: {brandsSales.MarginProfit} \n");
foreach (var item in localsWithProducts)
    Console.WriteLine($"Local: {item.Local}; Producto: {item.Producto}");

Console.ReadLine();