CREATE TABLE [dbo].[OrderItems]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Quantity] DECIMAL(18, 3) NOT NULL, 
    [Unit] NVARCHAR(MAX) NOT NULL
)
