USE [PersonsDatatbase]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[GetPersAllPersons]

SELECT	@return_value as 'Return Value'

GO
