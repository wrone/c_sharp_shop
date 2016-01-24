//VSE POKUPKI
SELECT
Users.Name,
Users.Lastname,
Products.Name 'Product',
Order_items.Quantity,
Orders.Date
FROM Users
JOIN Orders ON Orders.ID = Users.ID
JOIN Order_items ON Order_items.ID = Orders.ID
JOIN Products ON Products.ID = Order_items.ID
ORDER BY Orders.Date DESC;

//juzera pokupki, obwaja summa
SELECT
Users.Name,
Users.Lastname,
COUNT(Orders.ID) AS TotalOrders,
Order_items.Quantity*Products.Price AS 'Total price'
FROM Users
LEFT JOIN Orders ON Orders.ID = Users.ID
LEFT JOIN Order_items ON Orders.ID = Order_items.ID
LEFT JOIN Products ON Order_items.ID = Products.ID
GROUP BY Name
ORDER BY TotalOrders DESC;

//samij pokupaemij tovar
SELECT SUM(Order_items.Quantity) AS Amount, Products.Name AS Name
FROM Order_items
LEFT JOIN Products ON Order_items.ID = Products.ID
WHERE Products.Quantity > 0 
GROUP BY Products.Name
ORDER BY Order_items.Quantity DESC;

//poisk napitka
DELIMITER //
CREATE PROCEDURE productSearch(nosaukums CHAR(45), kategorija CHAR(45))
BEGIN
SELECT ID
FROM Products
WHERE Name LIKE CONCAT('%',nosaukums,'%') and Category Like kategorija;
END; //
DELIMITER ;

//dohod mezdu datami
DELIMITER //
CREATE PROCEDURE income (date1 VARCHAR(10), date2 VARCHAR(10))
BEGIN
SELECT SUM(Order_items.Quantity * Products.Price) AS Income
FROM Orders
JOIN Order_items ON Order_items.ID = Orders.ID
JOIN Products ON Products.ID = Order_items.ID
WHERE Orders.Date BETWEEN date1 AND date2;
END; //
DELIMITER ;

//dlja istorii
SELECT
Orders.ID
FROM Orders
WHERE Orders.Users_ID = 13;

select 
Products.Name,
Products.Category,
Products.Price,
Order_items.Quantity
FROM Order_items
JOIN Orders on Order_items.Orders_ID = Orders.ID
JOIN Products on Products.ID = Order_items.Products_ID
WHERE Orders.ID = 23;
