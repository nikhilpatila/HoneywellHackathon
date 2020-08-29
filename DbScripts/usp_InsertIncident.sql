CREATE PROCEDURE [dbo].[usp_InsertIncident]
 -- Add the parameters for the stored procedure here                  
		@BusID [varchar](10),
		@Subject [varchar](150),
		@Description [varchar](MAX),
		@IsUrgent [varchar](10),
		@IncidentType [varchar](10),
		@CrewName [varchar](10),
		@IncidentDateTime [varchar]
AS          
BEGIN       

DECLARE @IsActive BIT = 1

 -- SET NOCOUNT ON added to prevent extra result sets from                  
 -- interfering with SELECT statements.                  
 INSERT INTO Incident (          
        [BusID],
		[Subject],
		[Description],
		[IsUrgent],
		[IncidentType],
		[CrewName],
		[IncidentDateTime],
		[IsActive]
      )          
     VALUES (          
		@BusID,
		@Subject,
		@Description,
		@IsUrgent,
		@IncidentType,
		@CrewName,
		@IncidentDateTime,
		@IsActive
      )         
          
    
END 