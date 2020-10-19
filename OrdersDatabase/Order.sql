CREATE TABLE [dbo].[Order]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [Number] NVARCHAR(MAX) NULL, 
    [Date] DATETIME2 NULL, 
    [ProviderId] INT NULL
)
