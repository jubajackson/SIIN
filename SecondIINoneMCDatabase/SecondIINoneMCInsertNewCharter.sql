/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	[Id],
    [Name],
    [Active],
    [AddressId],
    [Notes],
    [FacebookId],
    [CreatedBy],
    [CreatedDate],
    [ModifiedBy],
    [ModifiedDate],
    [EmailAddress],
    [FacebookName]
FROM 
	[secondIInone].[dbo].[Charter]

INSERT INTO 
	[dbo].[Charter]
(
	[Name],
	[Active],
	--[AddressId],
	--[Notes],
	[FacebookId],
	[CreatedBy],
	[CreatedDate],
	[ModifiedBy],
	[ModifiedDate],
	[EmailAddress],
	[FacebookName]
)
     VALUES
(
	'Second II None MC Monroe',
	1,
	--AddressId,
	--Notes,
	'100010138419072',
	'DD2AFF48-43AA-44DE-80BC-B92431D3AF12',
	getdate(),
	'DD2AFF48-43AA-44DE-80BC-B92431D3AF12',
	getdate(),
	'monroe@secondIInonemc.com',
	'Monroe SecondIINone'
)

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT 
	[Id],
    [Name],
    [Active],
    [AddressId],
    [Notes],
    [FacebookId],
    [CreatedBy],
    [CreatedDate],
    [ModifiedBy],
    [ModifiedDate],
    [EmailAddress],
    [FacebookName]
FROM 
	[secondIInone].[dbo].[Charter]