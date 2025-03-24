Use TechShop
-- 1. Write an SQL query to retrieve the names and emails of all customers.
Select FirstName, LastName, Email from Customers

-- 2. Write an SQL query to list all orders with their order dates and corresponding customer names.
Select O.OrderID, O.OrderDate, C.FirstName, C.LastName
from Orders O
Join Customers C on O.CustomerID=C.CustomerID

-- 3. Write an SQL query to insert a new customer record into the "Customers" table. Include customer information such as name, email, and address.
Insert into Customers(FirstName, LastName, Email, Phone, Address) Values
('Sundar', 'Pichai', 'sundarpichai88@email.com', 6851995421, '354 Los Altos Hills')

-- Inserted order details for the new customer
Insert into OrderDetails(OrderID, ProductID, Quantity) Values
(13,2,2)

-- 4. Write an SQL query to update the prices of all electronic gadgets in the "Products" table by increasing them by 10%.
Update Products Set Price = Price+(Price*0.1);

-- 5. Write an SQL query to delete a specific order and its associated order details from the "Orders" and "OrderDetails" tables. Allow users to input the order ID as a parameter.
Delete from OrderDetails where OrderID=4
Delete from Orders where OrderID=4

-- 6. Write an SQL query to insert a new order into the "Orders" table. Include the customer ID, order date, and any other necessary information.
Insert into Orders(CustomerID, OrderDate, TotalAmount) Values
(11, '2024-10-06', '20000')

-- 7. Write an SQL query to update the contact information (e.g., email and address) of a specific customer in the "Customers" table. Allow users to input the customer ID and new contact information.
Update Customers
Set Email='dinesh.kumar25@email.com', Address='651 Dharam Colony'
where CustomerID=5

-- 8. Write an SQL query to recalculate and update the total cost of each order in the "Orders" table based on the prices and quantities in the "OrderDetails" table.
Update Orders
Set TotalAmount = (select sum(od.Quantity*p.Price)
from OrderDetails od
join Products p on od.ProductID=p.ProductID
where od.OrderID=orders.OrderID)

-- 9. Write an SQL query to delete all orders and their associated order details for a specific customer from the "Orders" and "OrderDetails" tables. Allow users to input the customer ID as a parameter
delete from OrderDetails where OrderID in (Select OrderId from Orders where CustomerID=7)
delete from Orders where CustomerID=7

-- 10. Write an SQL query to insert a new electronic gadget product into the "Products" table, including product name, category, price, and any other relevant details.
Insert into Products(ProductName, Description, Price) values
('Speaker', 'Crystal Clear and Bass Boosted', 8500)

-- 11. Write an SQL query to update the status of a specific order in the "Orders" table (e.g., from "Pending" to "Shipped"). Allow users to input the order ID and the new status.
alter table Orders add Status varchar(30)
Update Orders
Set Status='Pending'
Update Orders
Set Status='Shipped'
where OrderID=3


-- 12. Write an SQL query to calculate and update the number of orders placed by each customer in the "Customers" table based on the data in the "Orders" table.
alter table Customers add OrderCount int
Update Customers
Set OrderCount=(Select count(*) from Orders
where Orders.CustomerID=Customers.CustomerID)
