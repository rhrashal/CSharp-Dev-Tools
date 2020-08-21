

/******create  Database for Windows Store App***********************/
Go
Use master;
GO
--If DB_ID('MedicalTestDB') IS NOT NULL
DROP DATABASE  IF EXISTS PatientAdmittionForm;

GO

CREATE DATABASE PatientAdmittionForm;
GO
USE PatientAdmittionForm;

/***********************PatientDetails Table***********************/
GO
create TABLE PatientDetails(
						PatientID 		int Primary Key,
						FirstName 		VARCHAR(30),
						LastName 		VARCHAR(30),
						Age 			INT DEFAULT 1,
						MobileNumber	VARCHAR(11),
						Address			VARCHAR(200)
						);

/***********************Insert into Patient Table***********************/
GO
INSERT INTO PatientDetails(PatientID, FirstName, LastName, Age, MobileNumber, Address)
				VALUES (1, 'Kawsar', 'Hossain', 35, '01721454530', 'Dhaka'),
						(2, 'Alauddin', 'Hossain', 26, '01721454531','Dhaka'),
						(3, 'Mohib', 'Ullah', 28, '01721454533', 'Dhaka'),
						(4, 'Masud', 'Hasan', 20, '01721454534', 'Dhaka'),
						(5, 'Emran', 'Hosen', 25, '01721454535', 'Dhaka'),
						(6, 'Sarmin', 'Akter', 23, '01721454536', 'Dhaka'),
						(7, 'Benjir', 'Akter', 30, '01721454537', 'Dhaka'),
						(8, 'Rokya', 'Akter', 19, '01721454538', 'Dhaka'),
						(9, 'Alauddin', 'Jafor', 22, '01721454539', 'Dhaka'),
						(10, 'Arif', 'Hossain', 26, '01721454540', 'Dhaka')
						;

/**********************1251184- Robiul Hossain************************************/
Select * from PatientDetails;