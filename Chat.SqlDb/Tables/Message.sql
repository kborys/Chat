CREATE TABLE [dbo].[Message]
(
	[MessageId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Sender] INT NOT NULL REFERENCES [dbo].[User](UserId),
	[Receiver] INT NOT NULL REFERENCES [dbo].[User](UserId),
	[Content] NVARCHAR(MAX) NOT NULL
)
