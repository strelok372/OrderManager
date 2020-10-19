CREATE TABLE [dbo].[OrderItem]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [OrderId] INT NULL, 
    [Name] NVARCHAR(MAX) NULL, 
    [Quantity] DECIMAL(18, 3) NULL, 
    [Unit] NVARCHAR(MAX) NULL
)
