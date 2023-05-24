CREATE TABLE [dbo].[User] (
    [UserId]    INT           IdENTITY (1, 1) NOT NULL PRIMARY KEY,
    [EmailAddress] NVARCHAR(319) NOT NULL UNIQUE,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Password]  CHAR (90)     NOT NULL,
);