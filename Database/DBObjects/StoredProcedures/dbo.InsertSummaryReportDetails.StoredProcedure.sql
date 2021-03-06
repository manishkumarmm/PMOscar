IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'InsertSummaryReportDetails') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertSummaryReportDetails]
/****** Object:  StoredProcedure [dbo].[InsertSummaryReportDetails]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC InsertSummaryReportDetails '2013-03-01 00:00:00.000',83,6,3,1,1,168,NULL,1,NULL ,NULL ,NULL ,50,20
CREATE PROCEDURE [dbo].[InsertSummaryReportDetails]
	@Date DATETIME,
	@ProjectID INT,
	--@PhaseID INT,
	@ResourceID INT,
	@RoleID INT,
	@TeamID INT,
	@AvailableHours INT=NULL,
	@BilledHours INT=NULL,
	@Finalize BIT,
	@Admin INT=0,
	@Open INT=0,
	@VAS INT=0,
	@Proposal INT=0,
	@ActualHours INT=0
AS
BEGIN	
	INSERT 
	INTO CompanyUtilizationReport ([Date],ProjectID
	--,PhaseID
	,ResourceID,RoleID,TeamID,AvailableHours,BilledHours,Finalize,[Admin],[Open],VAS,Proposal,ActualHours)
	VALUES (@Date,@ProjectID
	--,@PhaseID
	,@ResourceID,@RoleID,@TeamID,@AvailableHours,@BilledHours,@Finalize,@Admin,@Open,@VAS,@Proposal,@ActualHours)
END
GO
