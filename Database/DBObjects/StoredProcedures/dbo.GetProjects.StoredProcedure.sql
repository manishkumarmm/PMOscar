IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetProjects') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjects]
/****** Object:  StoredProcedure [dbo].[GetProjects]    Script Date: 4/19/2018 1:02:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
   --[GetProjects] 0,'15151'   
CREATE Procedure [dbo].[GetProjects]          
      
@ProgramID int=0 ,
@EmployeeCode varchar(25) =null
  
AS          
          
Begin          
    IF @ProgramID = 0
		begin 
			if(@EmployeeCode is not null)
				 begin
					Select * 
					From Project P
					JOIN ProjectResources PR on P.ProjectId=PR.ProjectID
					JOIN [Resource] R  on R.ResourceId=PR.ResourceID
					where P.IsActive=1 
						AND PR.IsActive=1
						AND R.emp_Code=@EmployeeCode
					Order By ProjectName
					
				 end
			 else 
				 begin     
					Select * From Project where IsActive=1 Order By ProjectName
					
				 end
		END    
 ELSE 
	Begin   
			if(@EmployeeCode is not null)
				 begin
					Select * 
					From Project P
					JOIN ProjectResources PR on P.ProjectId=PR.ProjectID
					JOIN [Resource] R  on R.ResourceId=PR.ResourceID
					where P.IsActive=1 
						AND PR.IsActive=1
						AND R.emp_Code=@EmployeeCode
						AND ProgId=@ProgramID 
					Order By ProjectName
					
				 end
			 else 
			      Begin 
						Select * 
						From Project 
						where ProgId=@ProgramID 
						AND IsActive=1 
						Order By ProjectName 
						
				 End
	End 
End 
GO
