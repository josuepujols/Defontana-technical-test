--El total de ventas de los últimos 30 días (monto total y cantidad total de ventas
SELECT SUM(V.Total) AS 'Total de Ventas', SUM(VD.Cantidad) AS 'Cantidad Total'
FROM [dbo].[VentaDetalle] VD
INNER JOIN [dbo].[Venta] V
ON V.ID_Venta = VD.ID_Venta
INNER JOIN [dbo].[Producto] P
ON P.ID_Producto = VD.ID_Producto
INNER JOIN [dbo].[Marca] M
ON M.ID_Marca = P.ID_Marca
INNER JOIN [dbo].[Local] L
ON L.ID_Local = V.ID_Local
WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE())




--El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto)
SELECT TOP 1 V.Total AS 'Monto Mas Alto', DAY(v.Fecha) AS Dia, DATEPART(HOUR, v.Fecha) AS Hora FROM [dbo].[Venta] V
INNER JOIN [dbo].[Local] L
ON L.ID_Local = V.ID_Local
WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE())
ORDER BY V.Total DESC

--Indicar cuál es el producto con mayor monto total de ventas
SELECT TOP 1 P.Nombre AS Producto, SUM(V.Total) AS TotalSales
FROM [dbo].[VentaDetalle] VD
INNER JOIN [dbo].[Producto] P 
ON VD.ID_Producto = P.ID_Producto
INNER JOIN [dbo].[Venta] V 
ON VD.ID_Venta = V.ID_Venta
WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY P.ID_PRODUCTO, P.Nombre
ORDER BY TotalSales DESC;

--Indicar el local con mayor monto de ventas.
SELECT TOP 1 L.Nombre AS Local, V.Total FROM [dbo].[Venta] V
INNER JOIN [dbo].[Local] L
ON L.ID_Local = V.ID_Local
WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE())
ORDER BY V.Total DESC

--¿Cuál es la marca con mayor margen de ganancias?
SELECT TOP 1 M.Nombre AS Marca, SUM((V.Total - VD.Precio_Unitario) * VD.Cantidad) AS MarginProfit
FROM [dbo].[VentaDetalle] VD
INNER JOIN [dbo].[Venta] V 
ON VD.ID_Venta = V.ID_Venta
INNER JOIN [dbo].[Producto] P 
ON VD.ID_Producto = P.ID_Producto
INNER JOIN [dbo].[Marca] M 
ON P.ID_Marca = M.ID_Marca
WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY M.Nombre
ORDER BY SUM((V.Total - VD.Precio_Unitario) * VD.Cantidad) DESC;

--¿Cómo obtendrías cuál es el producto que más se vende en cada local?
SELECT L.Nombre, P.Nombre AS Producto FROM [dbo].[VentaDetalle] VD
INNER JOIN [dbo].[Producto] AS P
ON P.ID_Producto = VD.ID_Producto
INNER JOIN [dbo].[Venta] AS V
ON V.ID_Venta = VD.ID_Venta
INNER JOIN [dbo].[Local] AS L
ON L.ID_Local = V.ID_Local
WHERE V.Fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY L.Nombre, P.Nombre
ORDER BY COUNT(VD.ID_Producto) DESC

