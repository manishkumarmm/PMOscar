ALTER TABLE Project
ADD BudgetRevisionApprovedOn datetime,BudgetRevisionRejectedOn datetime,ProjectClosure NVARCHAR(MAX)


ALTER TABLE ProjectEstimation ADD RevisedBillableHours int


ALTER TABLE Project ADD ApproveRejectComments NVARCHAR(MAX)




ALTER TABLE Project ADD BudgetRevisionRequired bit default 1

update Project set BudgetRevisionRequired=1

update Project set BudgetRevisionRequired=0 where ProjectName in('Admin','Open','Proposal','Naico Client Ad-hoc Activities')
 
ALTER TABLE ProjectEstimationAudit ADD RevisedBillableHours int,NewRevisedBillableHours int




