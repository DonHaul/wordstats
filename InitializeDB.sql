DROP TABLE IF EXISTS [dbo].DocWords; 
DROP TABLE IF EXISTS [dbo].Globals; 




CREATE TABLE [dbo].DocWords
(
	[Name] Varchar(256) PRIMARY KEY,
	[Count] INT 
)

CREATE TABLE [dbo].Globals
(
	[Name] VARCHAR(10) NOT NULL PRIMARY KEY,
	[Value] int default 0 
)



INSERT INTO [dbo].Globals (Name, Value)
VALUES ('DocCount', 0);
