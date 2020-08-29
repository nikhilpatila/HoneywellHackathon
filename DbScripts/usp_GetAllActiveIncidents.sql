CREATE PROCEDURE [dbo].[usp_GetAllActiveIncidents]
AS          
BEGIN                  
	SELECT * FROM Incident Where IsActive = 1                   
END 