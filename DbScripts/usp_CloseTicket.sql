CREATE PROCEDURE [dbo].[usp_CloseTicket]
 -- Add the parameters for the stored procedure here                  
		@IncidentID int,
		@Remarks [varchar](MAX),
		@ClosureDate [Datetime]

AS          
BEGIN       

DECLARE @IsActive BIT = 1

 -- SET NOCOUNT ON added to prevent extra result sets from                  
 -- interfering with SELECT statements.                  
 INSERT INTO TicketResolution (          
        [IncidentID],
		[Remarks],
		[ClosureDate]
      )          
     VALUES (          
		@IncidentID,
		@Remarks,
		@ClosureDate
      )             
END 