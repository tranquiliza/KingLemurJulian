CREATE PROCEDURE [Core].[GetQuotes]
AS
BEGIN
	SELECT [Id], [Channel], [QuoteText], [CreationTime], [Deleted] 
	FROM [Core].[Quotes]
END