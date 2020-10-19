CREATE TABLE [dbo].[Orders]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Number] NVARCHAR(MAX)  NOT NULL, 
    [Date] DATETIME2 NOT NULL, 
    [ProviderId] INT NOT NULL
)
