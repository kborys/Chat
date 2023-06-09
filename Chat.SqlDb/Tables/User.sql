﻿CREATE TABLE [dbo].[User] (
    [UserId]    INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Email] NVARCHAR(319) NOT NULL UNIQUE,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Password]  CHAR (90)     NOT NULL,
);