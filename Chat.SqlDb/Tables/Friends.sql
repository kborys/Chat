CREATE TABLE [dbo].[Friends]
(
	[UserId] INT NOT NULL REFERENCES [dbo].[User](UserId),
	[FriendId] INT NOT NULL REFERENCES [dbo].[User](UserId),
	PRIMARY KEY (UserId, FriendId)
)
