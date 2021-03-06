

alter table client add clientCode varchar(3)
alter table client add IsActive bit 
ALTER TABLE client ADD CONSTRAINT UQ_clientCode UNIQUE (clientCode)

  update client set IsActive=1 
  update client set ClientCode='AHF' where ClientId=1
   update client set ClientCode='NI' where ClientId=2
   update client set ClientCode='CF' where ClientId=3
   update client set ClientCode='HUR' where ClientId=4
   update client set ClientCode='IV' where ClientId=5
   update client set ClientCode='LR' where ClientId=6
   update client set ClientCode='NME' where ClientId=7
   update client set ClientCode='SCE' where ClientId=8
   update client set ClientCode='ET' where ClientId=9
   update client set ClientCode='PP' where ClientId=10

      update client set ClientCode='KSC' where ClientId=11
   update client set ClientCode='LD' where ClientId=12
   update client set ClientCode='VT' where ClientId=13
   update client set ClientCode='LDG' where ClientId=14
   update client set ClientCode='SH' where ClientId=15
   update client set ClientCode='GTT' where ClientId=16
   update client set ClientCode='SUN' where ClientId=17
   update client set ClientCode='PSS' where ClientId=18
   update client set ClientCode='IW' where ClientId=19
   update client set ClientCode='IDS' where ClientId=20


   update client set ClientCode='EMP' where ClientId=21
   update client set ClientCode='MC' where ClientId=22
   update client set ClientCode='ZH' where ClientId=23
   update client set ClientCode='MAH' where ClientId=24


   Alter table Project add IsChangeRequest bit
   -----------------------------------------------------------------------------------
   ------Updated on 5/12/2016
   ------Deepa.R

  alter table BudgetRevisionHistory alter column IsApproved char(1)
  alter table BudgetRevision alter column IsApproved char(1)

  update BudgetRevision set IsApproved='P' 
  update BudgetRevisionHistory set IsApproved='P'
  --------------------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ProjectMilestone') AND type in (N'U'))
DROP TABLE ProjectMilestone
GO
  
CREATE TABLE [dbo].[ProjectMilestone](
	[ProjectMilestoneID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[MilestoneName] [varchar](500) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ProjectMilestone] PRIMARY KEY CLUSTERED 
(
	[ProjectMilestoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Alter table BudgetRevision ,BudgetRevisionHistory 

 -----------------------------------------------------------------------------------
   ------Updated on 9/12/2016
   ------kochurani

 ALTER TABLE BudgetRevision DROP COLUMN IsApproved
ALTER TABLE BudgetRevisionHistory  DROP COLUMN IsApproved

ALTER TABLE BudgetRevision
ADD  
ApproveStatus char(1)

GO

ALTER TABLE BudgetRevisionHistory 
ADD  
ApproveStatus char(1)

GO


ALTER TABLE BudgetRevision
ADD  
ApproveRejectComments nvarchar(MAX),
BudgetRevisionApprovedRejectedOn datetime
GO

  update BudgetRevision set ApproveStatus='P' 
  update BudgetRevisionHistory set ApproveStatus='P'


--------------------------------------------------------------------------

--user table change is approver field added

ALTER TABLE [User] 
ADD IsApprover bit 

update [user] set isapprover=0




--------------------------------------------------------------------------

--ProjectEstimation table change IsBudgetHoursUpdate field added

ALTER TABLE [ProjectEstimation] 
ADD IsBudgetHoursUpdate bit 

update [ProjectEstimation] set IsBudgetHoursUpdate=0 




