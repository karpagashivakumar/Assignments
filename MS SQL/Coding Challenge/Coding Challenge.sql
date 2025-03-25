-- Karpagashivakumar
-- PetPals, The Pet Adoption Platform
-- 1. Provide a SQL script that initializes the database for the Pet Adoption Platform ”PetPals”.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PetPals')
CREATE DATABASE PetPals;
USE PetPals

-- 2. Create tables for pets, shelters, donations, adoption events, and participants.
-- 3. Define appropriate primary keys, foreign keys, and constraints.
-- Creating Pets Table
If OBJECT_ID('Pets','U') is Not Null Drop Table Pets
Create Table Pets(
	PetID int identity(1,1) Primary Key,
	Name Varchar(50) Not Null,
	Age int,
	Breed Varchar(max),
	Type Varchar(max),
	AvailableForAdoption bit,
	OwnerID int NULL,
	ShelterID int
)

-- Creating Shelters Table
If OBJECT_ID('Shelters','U') is not null drop table Shelters
Create Table Shelters(
	ShelterID int identity(1,1) Primary Key,
	Name Varchar(max) Not NUll,
	Location Varchar(max)
)

-- Creating Donations Table
If OBJECT_ID('Donations','U') is not null drop table Donations
Create Table Donations(
	DonationID int identity(1,1) Primary Key,
	DonorName Varchar(50) Not Null,
	DonationType Varchar(max),
	DonationAmount decimal,
	DonationItem Varchar(max),
	DonationDate DateTime, 
)

--Creating Adoption Events Table
If OBJECT_ID('AdoptionEvents','U') is not null drop table AdoptionEvents
Create Table AdoptionEvents(
	EventID int identity(1,1) Primary Key,
	EventName varchar(max) Not Null,
	EventDate DateTime,
	Location Varchar(max)
)

-- Creating Participants Table
If OBJECT_ID('Participants','U') is not null drop table Participants
Create Table Participants(
	ParticipantID int identity(1,1) Primary Key,
	ParticipantName varchar(max) Not Null,
	ParticipantType varchar(max),
	EventID int,
	Foreign Key (EventID) references AdoptionEvents(EventID)
)

-- Inserting records into all tables

-- Sample Pets
Insert Into Pets (Name, Age, Breed, Type, AvailableForAdoption, ShelterID) Values 
	('Spike', 3, 'Labrador', 'Dog', 1, 1),
	('Mittens', 2, 'Persian', 'Cat', 1, 2),
	('Charlie', 5, 'Beagle', 'Dog', 0, 3),
	('Tom', 1, 'Siamese', 'Cat', 1, 4)

-- Sample Shelters

Insert Into Shelters (Name, Location) Values
	('Happy Paws Shelter', 'Chennai'),
	('Safe Haven Rescue', 'Coimbatore'),
	('Pet Lovers Home', 'Madurai'),
	('Animal Care Center', 'Salem')

-- Sample Donations
Insert Into Donations (DonorName, DonationType, DonationAmount, DonationItem, DonationDate) Values
	('Shiva Kumar', 'Cash', 100.00, NULL, '2024-06-15'),
	('Sabari Raja', 'Item', NULL, 'Dog Food', '2024-06-20'),
	('Mukesh Kumar', 'Cash', 250.00, NULL, '2024-07-01'),
	('Akash Jayan', 'Item', NULL, 'Cat Toys', '2024-07-05')

-- Sample Adoption Events
Insert Into AdoptionEvents (EventName, EventDate, Location) Values
	('Summer Adoption Fair', '2024-07-10', 'Anna Nagar'),
	('Rescue Adoption Day', '2024-08-05', 'R S Puram'),
	('Holiday Pet Drive', '2024-12-15', 'Netaji Nagar'),
	('Spring Rescue Gala', '2025-04-20', 'Mettur')

-- Sample Participants
Insert Into Participants (ParticipantName, ParticipantType, EventID) Values
	('Ganesh', 'Shelter', 1),
	('Mohan', 'Adoptor', 2),
	('Tharun', 'Shelter', 3),
	('Pavin', 'Adopter', 4)

-- 5. Write an SQL query that retrieves a list of available pets (those marked as available for adoption) from the "Pets" table. Include the pet's name, age, breed, and type in the result set. Ensure that the query filters out pets that are not available for adoption.
Select Name, Age, Breed, Type from Pets where AvailableForAdoption=1;

-- 6. Write an SQL query that retrieves the names of participants (shelters and adopters) registered or a specific adoption event. Use a parameter to specify the event ID. Ensure that the query joins the necessary tables to retrieve the participant names and types.
Select ParticipantName, ParticipantType From Participants Where EventID = 2

-- 7. Update Shelters Table
Update Shelters
Set Name='Stray Pals' , Location='Tirupur' where ShelterID=4

-- 8. Write an SQL query that calculates and retrieves the total donation amount for each shelter (by shelter name) from the "Donations" table. The result should include the shelter name and the total donation amount. Ensure that the query handles cases where a shelter has received no donations.
SELECT S.Name AS ShelterName, ISNULL(SUM(D.DonationAmount), 0) AS TotalDonationAmount 
FROM Shelters S 
LEFT JOIN Donations D ON S.ShelterID = D.DonationID 
GROUP BY S.Name;

-- 9. Write an SQL query that retrieves the names of pets from the "Pets" table that do not have an owner (i.e., where "OwnerID" is null). Include the pet's name, age, breed, and type in the result set.
Select Name, Age, Breed, Type From Pets Where OwnerID is NULL

-- 10. Write an SQL query that retrieves the total donation amount for each month and year (e.g., January 2023) from the "Donations" table. The result should include the month-year and the corresponding total donation amount. Ensure that the query handles cases where no donations were made in a specific month-year.
Select Format(DonationDate, 'yyyy-MMMM') AS MonthYear, Sum(DonationAmount) as TotalAmount From Donations Group By Format(DonationDate, 'yyyy-MMMM')

-- 11. Retrieve a list of distinct breeds for all pets that are either aged between 1 and 3 years or older than 5 years.
Select Distinct Breed from Pets Where (Age BETWEEN 1 AND 3) OR (Age > 5)

-- 12. Retrieve a list of pets and their respective shelters where the pets are currently available for adoption.
Select P.Name, P.Age, P.Breed, P.Type, S.Name as ShelterName from Pets P join Shelters S ON P.ShelterID = S.ShelterID Where P.AvailableForAdoption = 1

-- 13. Find the total number of participants in events organized by shelters located in specific city. Example: City=Chennai
Select Count(P.ParticipantID) as TotalParticipants from Participants P 
JOIN AdoptionEvents AE on P.EventID = AE.EventID 
JOIN Shelters S ON S.Location = 'Chennai';

-- 14. Retrieve a list of unique breeds for pets with ages between 1 and 5 years.
Select Distinct Breed from Pets where Age BETWEEN 1 AND 5

-- 15. Find the pets that have not been adopted by selecting their information from the 'Pet' table.
SELECT * FROM Pets WHERE OwnerID IS NULL

-- 16. Retrieve the names of all adopted pets along with the adopter's name from the 'Adoption' and	'User' tables.
SELECT P.Name AS PetName, U.Name AS AdopterName FROM Pets P 
JOIN Users U ON P.OwnerID = U.UserID WHERE P.OwnerID IS NOT NULL

-- 17. Retrieve a list of all shelters along with the count of pets currently available for adoption in each shelter.
Select S.Name as ShelterName, count(P.PetID) as AvailablePetsCount 
from Shelters S LEFT JOIN Pets P on S.ShelterID = P.ShelterID 
where P.AvailableForAdoption = 1 group by S.Name;

-- 18. Find pairs of pets from the same shelter that have the same breed.
Select P1.Name as Pet1, P2.Name as Pet2, P1.Breed, S.Name as ShelterName 
from Pets P1 JOIN Pets P2 ON P1.ShelterID = P2.ShelterID and P1.Breed = P2.Breed and P1.PetID < P2.PetID 
JOIN Shelters S ON P1.ShelterID = S.ShelterID;

-- 19. List all possible combinations of shelters and adoption events.
Select S.Name as ShelterName, AE.EventName from Shelters S CROSS JOIN AdoptionEvents AE;

-- 20. Determine the shelter that has the highest number of adopted pets.
Select Top 1 S.Name as ShelterName, count(*) as AdoptedPetsCount from Pets P 
JOIN Shelters S on P.ShelterID = S.ShelterID where P.AvailableForAdoption = 0
group by S.Name order by count(*) DESC;
