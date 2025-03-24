-- Karpagashivakumar R

-- TechShop, an electronic gadgets shop-- Create the database name "TechShop"Create Database TechShopUse TechShop
-- 2. Define the schema for the Customers, Products, Orders, OrderDetails and Inventory tables based on the provided schema.

-- 4. Create appropriate Primary Key and Foreign Key constraints for referential integrity.

-- 5. Insert at least 10 sample records into each of the following tables.

-- Creating Customers Table

Create Table Customers (
	CustomerID INT Identity(1,1) Primary Key,
	FirstName Varchar(50),
	LastName Varchar(50),
	Email Varchar(max),
	Phone bigint,
	Address varchar(max)
)

-- Creating Products Table

Create Table Products(
	ProductID int Identity(1,1) Primary Key,
	ProductName varchar(max),
	Description varchar(max),
	Price bigint
)

-- Creating Orders Table

Create Table Orders(
	OrderID int identity(1,1) Primary Key,
	CustomerID int,
	OrderDate Date,
	TotalAmount bigint,
	Foreign Key (CustomerID) references Customers(CustomerID)
)

-- Creating OrderDetails Table

Create Table OrderDetails(
	OrderDetailID int identity(1,1) Primary Key,
	OrderID int,
	ProductID int,
	Quantity int,
	Foreign Key (OrderID) references Orders(OrderID),
	Foreign Key (ProductID) references Products(ProductID)
)

-- Creating Inventory Table

Create Table Inventory(
	InventoryID int identity(1,1) Primary Key,
	ProductID int,
	QuantityInStock int,
	LastStockUpdate Date
	Foreign Key (ProductID) references Products(ProductID)
)

-- Inserting records into cutomers table

INSERT INTO Customers(FirstName, LastName, Email, Phone, Address) VALUES
	('Shiva', 'Kumar', 'shivakumar511@email.com', '9789676155', '6 Kannagi Street'),
	('Ranga', 'Rajan', 'rangarajan189@email.com', '9487225752', '45 Vasuki Nagar'),
	('Bagya', 'Lakshmi', 'bagyalakshmi8364@email.com', '9487225751', '55 Subramaniam Street'),
	('Dharun', 'Kumar', 'dharunkumar1104@email.com', '7010225790', '32 Golf Road'),
	('Dinesh', 'Kumar', 'dineshkumar22@email.com', '9988776655', '64 Netaji Street'),
	('Mani', 'Bharathi', 'manibharathi35@email.com', '9788690011', '99 Park Avenue'),
	('Ajith', 'Kumar', 'ajithkumar63@email.com', '6755843322', '19 Willow Drive'),
	('Florence', 'Pugh', 'florencepugh69@email.com', '8322514435', '74 Diamond Barn'),
	('Sydney', 'Sweeney', 'sydneysweeney100@email.com', '7233415869', '852 Gold Beach'),
	('Virat', 'Kohli', 'viratkohli18@email.com', '6322947688', '9518 New Delhi');

-- Inserting records into products table

Insert into Products(ProductName, Description, Price) Values
	('LED TV', 'Super Immersive Panel', 35000),
	('Laptop', 'High Performance Gaming Laptop', 75000),
	('Smartphone', 'Flagship Killer', 25000),
	('Tablet', '12-inch screen semi pc tablet', 20000),
	('Smartwatch', 'Fitness Tracker and Water Resistant', 2500),
	('Headphones', 'ANC and Super Cool Features', 1500),
	('Wireless Mouse', '1220 dpi wireless mouse', 1200),
	('Keyboard', 'Mechanical backlit gaming keyboard', 1000),
	('Power Bank', '20000 mAh mutli port', 800),
	('SSD', '2TB Fast Transfer', 12500);

-- Inserting records into orders table

Insert into Orders(CustomerID,OrderDate,TotalAmount) Values
	(1, '2024-09-11', 35000),
	(2, '2024-08-05', 75000),
	(3, '2024-10-22', 25000),
	(4, '2024-09-16', 20000),
	(5, '2024-10-01', 2500),
	(6, '2024-09-28', 1500),
	(7, '2024-10-18', 1200),
	(8, '2024-10-07', 1000),
	(9, '2024-09-25', 800),
	(10, '2024-10-12', 12500);

-- Inserting records into order details table

Insert into OrderDetails(OrderID, ProductID, Quantity) values
	(3, 1, 2),
	(4, 2, 1),
	(5, 3, 3),
	(6, 4, 4),
	(7, 5, 7),
	(8, 6, 8),
	(9, 7, 6),
	(10, 8, 5),
	(11, 9, 3),
	(12, 10, 2);

-- Inserting records into Inventory Table

Insert into Inventory(ProductID, QuantityInStock, LastStockUpdate) Values
	(1, 15, '2024-09-10'),
	(2, 12, '2024-08-03'),
	(3, 20, '2024-10-21'),
	(4, 25, '2024-09-14'),
	(5, 35, '2024-09-30'),
	(6, 50, '2024-09-27'),
	(7, 30, '2024-10-17'),
	(8, 40, '2024-10-06'),
	(9, 55, '2024-09-24'),
	(10, 45, '2024-10-11');

select * from Customers
select * from Products
select * from Orders
select * from OrderDetails
select * from Inventory

