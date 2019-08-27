SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[trg_Insert_ProjectResource]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[trg_Insert_ProjectResource]
GO

CREATE trigger trg_Insert_ProjectResource on     
  ProjectResources     
for insert, update
as        
begin        
      
	if (select count(1) from deleted)=0
		 begin
		  DECLARE @ModifiedBy int = (select TOP 1 pa.UpdatedBy from Project pa inner join inserted i on i.ProjectID = pa.ProjectID where pa.UpdatedDate = (select MAX(UpdatedDate)from Project))
		  DECLARE @CreatedBy int = (select TOP 1 pa.CreatedBy from Project pa inner join inserted i on i.ProjectID = pa.ProjectID where pa.UpdatedDate = (select MAX(UpdatedDate)from Project))
		  DECLARE @CreatedDate datetime = (select TOP 1 pa.CreatedDate from Project pa inner join inserted i on i.ProjectID = pa.ProjectID where pa.UpdatedDate = (select MAX(UpdatedDate)from Project))
		  insert into dbo.ProjectResourcesAudit(ProjectResourcesID,ProjectID,ResourceID,IsAllocated,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
		  select ProjectResourcesID,ProjectID,ResourceID,1,@CreatedBy,@CreatedDate,@ModifiedBy,GETDATE()
		  from inserted
		  
		 end

	if (select count(1) from deleted)>0
		 begin
			DECLARE @Modified int = (select TOP 1 UpdatedBy from Project pa inner join inserted i on i.ProjectID = pa.ProjectID where pa.UpdatedDate = (select MAX(UpdatedDate)from Project))
			 DECLARE @Created int = (select TOP 1 pa.CreatedBy from Project pa inner join inserted i on i.ProjectID = pa.ProjectID where pa.UpdatedDate = (select MAX(UpdatedDate)from Project))
			 DECLARE @CreatedDat datetime = (select TOP 1 pa.CreatedDate from Project pa inner join inserted i on i.ProjectID = pa.ProjectID where pa.UpdatedDate = (select MAX(UpdatedDate)from Project))
		  insert into dbo.ProjectResourcesAudit(ProjectResourcesID,ProjectID,ResourceID,IsAllocated,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
		  select distinct i.ProjectResourcesID,i.ProjectID,i.ResourceID,i.IsActive,@Created,@CreatedDat,@Modified,GETDATE()
		  from inserted i
		  join deleted d
					on  i.ProjectResourcesID=d.ProjectResourcesID 
		  where i.IsActive<>d.IsActive
		  
		 end

end
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
