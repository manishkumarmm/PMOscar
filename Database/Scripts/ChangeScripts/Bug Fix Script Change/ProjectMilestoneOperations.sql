
                      
ALTER Procedure [dbo].[ProjectMilestoneOperations]                          
(                                           
@OpMode varchar(10),                       
@ProjectId int ,                          
@ProjectMilestoneName varchar(500),                                               
@ProjectMilestoneStatus varchar(50),                                         
@StartDateText nvarchar(20),                        
@EndDateText nvarchar(20),  
@ProjectMilestoneID INT = NULL,
@IsActive bit,           
@status varchar(15) out                          
)                          
AS                          
                          
Begin    
 
Declare @StartDate datetime,  
 @EndDate datetime                 
       
set @StartDate =   convert(datetime,  @StartDateText,          103)   
set @EndDate =   convert(datetime,  @EndDateText,          103)   
                          
  IF @OpMode = 'UPDATE'                        
                             
    BEGIN
		Update ProjectMilestone  
		SET	
		MilestoneName = @ProjectMilestoneName,
		StartDate = @StartDate,
		EndDate = @EndDate,							
		[Status] = @ProjectMilestoneStatus
		Where ProjectMilestoneID = @ProjectMilestoneID  
		
		RETURN @ProjectMilestoneID;                         
		                  
   END    
                        
--************************************************************                                 
  If @OpMode = 'INSERT'                                        
   Begin                                    
		IF not exists(select *,MilestoneName from [dbo].ProjectMilestone where MilestoneName=@ProjectMilestoneName AND ProjectID = @ProjectId)                                                       
			BEGIN                   
							   Insert into ProjectMilestone                               
							   (     ProjectId,MilestoneName ,StartDate ,EndDate ,[Status]                 
							   )                               
							   Values                              
							  (                          
				             @ProjectId,@ProjectMilestoneName, @StartDate, @EndDate, @ProjectMilestoneStatus
							   )return SCOPE_IDENTITY();                                  
							END                       
					        
		else   begin SET   @status = '1';  end                                  
  End   
                 
--*************************************************************                                     
                      
    If @OpMode = 'SELECT'                        
		Begin   
		    
				select ProjectMilestoneID, ProjectId, MilestoneName ,StartDate ,EndDate ,[Status]
				from ProjectMilestone where ProjectId=@ProjectId    
				ORDER BY StartDate DESC, MilestoneName                     
		                     
		End      
		                    
--*************************************************************   
                   
	 If @OpMode = 'SELECTALL'                        
		Begin                                               
			Select * from ProjectMilestone                                           
		End 
	    
--*************************************************************
                      
     If @OpMode = 'SELECTONE'                        
		Begin   
		    
				select ProjectMilestoneID, ProjectId, MilestoneName ,StartDate ,EndDate ,[Status]
				from ProjectMilestone where ProjectMilestoneID=@ProjectMilestoneID                         
		                     
		End      
		                    
--*************************************************************                      
End





