CREATE DATABASE GASTracker

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))

BEGIN

CREATE TABLE dbo.Accounts
(
ID int NOT NULL IDENTITY,
UserName varchar(32) NOT NULL,
Password varchar(64) NOT NULL,
Salt varchar(64) NOT NULL,
Created datetime2(7) NOT NULL,
Updated datetime2(7) NOT NULL,
State int NOT NULL,
PRIMARY KEY (ID),
UNIQUE (UserName)
)

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Purchase]') AND type in (N'U'))

BEGIN

CREATE TABLE dbo.Purchase
(
ID int NOT NULL IDENTITY,
Account int FOREIGN KEY REFERENCES Accounts(ID),
Price money NOT NULL,
Amount int NOT NULL,
Distance int NOT NULL,
Date date NOT NULL,
Note char(64),
Grade int,
PRIMARY KEY (ID),
)

END