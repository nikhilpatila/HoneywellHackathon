CREATE PROCEDURE [dbo].[usp_AssignTicket]
 -- Add the parameters for the stored procedure here                  
		@IncidentID int,
		@ExecutiveID int
AS          
BEGIN       

DECLARE @IsActive BIT = 1

 -- SET NOCOUNT ON added to prevent extra result sets from                  
 -- interfering with SELECT statements.                  
 INSERT INTO TicketAssignee (          
        [IncidentID],
		[ExecutiveID]	
      )          
     VALUES (          
		@IncidentID,
		@ExecutiveID
      )             
END 