-- Creating a Database
Create Database AssetHub
Use AssetHub

-- Creating Employees Table
Create Table Employees(
Employee_id int identity(100,1) Primary Key,
Name varchar(max),
Department varchar(max),
Email nvarchar(200) unique not null,
Password varchar(max)
)

-- Creating Assets Table
Create Table Assets(
Asset_id int identity(200,1) Primary Key,
Name varchar(max) not null,
Type varchar(max) not null,
Serial_Number varchar(100) unique not null,
Purchase_Date date,
Location varchar(max),
Status varchar(max),
Owner_id int,
Foreign Key (Owner_id) References Employees (Employee_id)
)

-- Creating Maintenance Records Table
Create Table Maintenance_Records(
Maintenance_id int identity(300,1) Primary Key,
Asset_id int not null,
Maintenance_Date Date not null,
Description varchar(max),
Cost Decimal (18,2)
Foreign Key (Asset_id) references Assets(Asset_id)
)

-- Creating Asset Allocations Table
Create Table Asset_Allocations(
Allocation_id int identity(400,1) Primary Key,
Asset_id int not null,
Employee_id int not null,
Allocation_Date date not null,
Return_Date date,
Foreign Key (Asset_id) references Assets(Asset_id),
Foreign Key (Employee_id) references Employees(Employee_id)
)

-- Creating Reservations Table
Create Table Reservations(
Reservation_id int identity(500,1) Primary Key,
Asset_id int not null,
Employee_id int not null,
Reservation_Date Date not null,
Start_Date Date not null,
End_Date Date not null,
Status varchar(max) not null,
Foreign Key (Asset_id) references Assets(Asset_id),
Foreign Key (Employee_id) references Employees(Employee_id)
)

-- Inserting Sample Records into Employees Table
Insert into Employees (Name, Department, Email, Password) Values
('Aarav Sharma', 'IT', 'aarav.sharma@gmail.com', 'Password@123'),
('Ishita Patel', 'Finance', 'ishita.patel@gmail.com', 'Welcome@123'),
('Rohan Mehta', 'HR', 'rohan.mehta@gmail.com', 'Secure@123'),
('Ananya Rao', 'Marketing', 'ananya.rao@gmail.com', 'Hello@123'),
('Vihaan Iyer', 'Operations', 'vihaan.iyer@gmail.com', 'Ops@123'),
('Meera Joshi', 'Sales', 'meera.joshi@gmail.com', 'Sales@123'),
('Kabir Nair', 'IT', 'kabir.nair@gmail.com', 'Tech@123'),
('Sneha Kapoor', 'Finance', 'sneha.kapoor@gmail.com', 'Money@123'),
('Arjun Deshmukh', 'Logistics', 'arjun.deshmukh@gmail.com', 'MoveIt@123'),
('Pooja Reddy', 'Admin', 'pooja.reddy@gmail.com', 'Admin@123')

-- Inserting Sample Records into Assets Table
Insert into Assets (Name, Type, Serial_Number, Purchase_Date, Location, Status, Owner_id) Values
('Dell Inspiron 15', 'Laptop', 'DLINSP12345', '2024-01-15', 'Mumbai Office', 'In Use', 100),
('HP LaserJet Pro', 'Printer', 'HPLJ45678', '2024-02-20', 'Bangalore Office', 'In Use', 101),
('Hyundai Verna', 'Vehicle', 'MH12AB1234', '2024-03-10', 'Pune Garage', 'Under Maintenance', 102),
('Lenovo ThinkPad X1', 'Laptop', 'LTPX112233', '2024-04-05', 'Chennai Office', 'In Use', 103),
('Canon EOS 90D', 'Camera', 'CANEOS9988', '2024-05-25', 'Hyderabad Office', 'In Use', 104),
('Maruti Suzuki Ertiga', 'Vehicle', 'DL8CAF8765', '2024-06-30', 'Delhi Garage', 'Decommissioned', 105),
('Apple MacBook Air', 'Laptop', 'MBK123456', '2024-07-18', 'Bangalore Office', 'In Use', 106),
('Samsung Galaxy Tab S6', 'Tablet', 'SAMSNG98765', '2024-08-10', 'Kolkata Office', 'In Use', 107),
('Brother HL-L2350DW', 'Printer', 'BRTHL45678', '2024-09-15', 'Mumbai Office', 'In Use', 108),
('Tata Nexon EV', 'Vehicle', 'MH01DE1122', '2025-01-05', 'Pune Garage', 'In Use', 109)

-- Inserting Sample Records into Maintenance Records Table
Insert into Maintenance_Records (Asset_id, Maintenance_Date, Description, Cost) values
(200, '2024-03-20', 'Battery replacement', 4500.00),
(201, '2024-04-15', 'Toner refill', 1200.00),
(202, '2024-05-10', 'Oil change and service', 7500.00),
(203, '2024-06-05', 'Keyboard replacement', 3500.00),
(204, '2024-07-22', 'Lens cleaning', 800.00),
(205, '2024-08-30', 'Engine tuning', 6500.00),
(206, '2024-09-18', 'Screen replacement', 10000.00),
(207, '2024-10-12', 'Battery service', 2500.00),
(208, '2024-11-20', 'Drum unit replacement', 3000.00),
(209, '2025-02-14', 'Tyre replacement', 15000.00)

-- Inserting Sample Records into Asset Allocations Table
Insert into Asset_Allocations (Asset_id, Employee_id, Allocation_Date, Return_Date) values
(200, 100, '2024-01-18', '2024-06-18'),
(201, 101, '2024-02-22', NULL),
(202, 102, '2024-03-25', '2024-09-25'),
(203, 103, '2024-04-30', NULL),
(204, 104, '2024-05-10', '2024-11-10'),
(205, 105, '2024-06-20', NULL),
(206, 106, '2024-07-15', '2025-01-15'),
(207, 107, '2024-08-20', NULL),
(208, 108, '2024-09-25', '2025-03-25'),
(209, 109, '2025-01-28', NULL)

-- Inserting Sample Records into Reservations Table
Insert into Reservations (Asset_id, Employee_id, Reservation_Date, Start_Date, End_Date, Status) values
(200, 100, '2024-01-10', '2024-01-15', '2024-01-20', 'Approved'),
(201, 101, '2024-02-05', '2024-02-10', '2024-02-12', 'Approved'),
(202, 102, '2024-03-12', '2024-03-15', '2024-03-25', 'Pending'),
(203, 103, '2024-04-20', '2024-04-25', '2024-04-30', 'Approved'),
(204, 104, '2024-05-01', '2024-05-05', '2024-05-10', 'Canceled'),
(205, 105, '2024-06-15', '2024-06-20', '2024-06-25', 'Approved'),
(206, 106, '2024-07-05', '2024-07-10', '2024-07-15', 'Pending'),
(207, 107, '2024-08-10', '2024-08-15', '2024-08-20', 'Approved'),
(208, 108, '2024-09-01', '2024-09-05', '2024-09-10', 'Pending'),
(209, 109, '2025-01-20', '2025-01-25', '2025-01-30', 'Approved')

