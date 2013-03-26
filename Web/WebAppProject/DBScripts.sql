CREATE DATABASE GASTracker

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))

BEGIN

CREATE TABLE dbo.Accounts
(
UserName varchar(32) NOT NULL,
Password varchar(64) NOT NULL,
Salt varchar(64) NOT NULL,
Created datetime2(7) NOT NULL,
Updated datetime2(7) NOT NULL,
State int NOT NULL,
PRIMARY KEY (UserName),
UNIQUE (UserName)
)

END