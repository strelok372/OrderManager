INSERT INTO Providers 
VALUES ('Washington Co.'), ('Electronic Arts'), ('Greens'), ('Wallmart'), ('Magnit')

GO

INSERT INTO Orders (Number, Date, ProviderId)
VALUES 
('H6K2X87N2F9D', cast('09.18.2015' as datetime2), 2),
('X6K2XGSDF921', cast('01.14.2012' as datetime2), 5),
('H6SDF9612F9D', cast('04.24.2011' as datetime2), 4),
('GS06924N2F9D', cast('03.05.2012' as datetime2), 1),
('C6K2SHDF824D', cast('03.18.2015' as datetime2), 3),
('Z6KGS924LSKA', cast('12.20.2014' as datetime2), 2)

GO

INSERT INTO OrderItems (OrderId, Name, Quantity, Unit)
VALUES
(1, 'Spoons', 7000, 'piece'),
(2, 'Sand', 12500, 'kg'),
(4, 'Ethanol', 1500, 'literes'),
(6, 'Oil', 10000, 'barrels'),
(3, 'Gas', 50000, 'cubic meter'),
(5, 'Senior .Net Programmer', 5000, 'hours')

GO