CREATE TABLE Incident
(
        [IncidentID] [int] IDENTITY(1,1) NOT NULL,
        [BusID] [varchar](10) NOT NULL,
		[Subject] [varchar](150) NULL,
		[Description] [varchar](MAX) NULL,
		[IsUrgent] [varchar](10) NULL,
		[IncidentType] [varchar](10) NULL,
		[CrewName] [varchar](10) NULL,
		[IncidentDateTime] [varchar](10) NULL,
		[IsActive] [bit] NOT NULL
) 