CREATE PROCEDURE [dbo].[usp_GetTicketStatus]
 -- Add the parameters for the stored procedure here                  
		@IncidentID int		

AS          
BEGIN       

Select * from TicketResolution TR
INNER JOIN TicketAssignee TA ON TR.IncidentID = TA.IncidentID
Where TR.IncidentID = @IncidentID     
END 