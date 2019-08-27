SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[getProjectDashboardDetails]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[getProjectDashboardDetails]
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getProjectDashboardDetails]
   
	@dayfrom varchar(15),                                    
 @dayto varchar(15),                                    
                         
 @uptodate varchar(15),                                    
 @tabType varchar(50),                                    
 @Status varchar(15),                                    
 @DashboardID int ,
 @IsIncludeProposalAndVAS bit
                                      
AS                                    
BEGIN                                                                   
SET NOCOUNT ON;                                    
if (@Status='Create')                                    
	begin                                     
	if(@tabType='dashboard')                                    
	begin                        
	   Delete from ProjectDashboard  where NOT Exists
					(                          
					   Select distinct A.ProjectID from dbo.Project A 
					   inner join [User] B on A.ProjectOwner=B.UserID                                      
					   inner join [User] C on A.ProjectManager=C.UserID                                       
					   inner join TimeTracker TT on TT.ProjectID=A.ProjectID And FromDate >=@dayfrom and ToDate  <=@dayto -- and isnull(TT.ActualHours,0)>0                                         
					   left join Phase D on D.PhaseID=A.PhaseID 
					   where A.ProjectName Not In('Admin','Open')and A.ProjectID=ProjectDashboard.ProjectID and A.isactive!=NULL  
					) And   ProjectDashboard.DashboardID = @DashboardID  
					-- Added to Update the existing entries in project dashboard--          
	             
					 Update pd Set  
					 pd.ProjectType					= p.ProjectType,          
					 pd.ProjectOwner				= p.ProjectOwner,          
					 pd.ProjectManager				= p.ProjectManager,          
					 pd.PhaseID						= p.PhaseID,          
					 pd.POComments					= p.POComments,          
					 pd.PMComments					= p.PMComments,          
					 pd.DeliveryComments			= p.DeliveryComments
					 from ProjectDashboard pd          
					 join Project p on pd.ProjectID	=p.ProjectID where  
					 Exists          
					 (                          
							Select distinct A.ProjectID from dbo.Project A  
							inner join [User] B on A.ProjectOwner=B.UserID                                      
							inner join [User] C on A.ProjectManager=C.UserID                                       
							left join TimeTracker TT on TT.ProjectID=A.ProjectID  --and isnull(TT.ActualHours,0)>0                                         
							left join Phase D on D.PhaseID=A.PhaseID 
							where A.ProjectName Not In('Admin','Open') and A.IsActive=1 and A.ProjectID=p.ProjectID  
					 ) And pd.DashboardID = @DashboardID          
		              
					-- Added to Update the existing entries in project dashboard--          
	                            
					 Delete from dbo.ProjectDashboardEstimation where Not exists
					 (Select 1 from ProjectDashboard P where P.DashboardID=@DashboardID and P.ProjectID=ProjectDashboardEstimation.ProjectID)
					 And   ProjectDashboardEstimation.DashboardID = @DashboardID                         
			                                        
					 Select distinct A.ProjectID,A.ProjectName,case A.ProjectType when 'F' then 'Fixed Cost' when 'T' then 'T & M' end ProjectType ,                          
					 B.FirstName as ProjectOwner,C.FirstName as ProjectManager,Case A.Isactive when '1' then 'Active' else 'Inactive' end  Status,
					 D.Phase,A.PMComments as Comments,A.PhaseID,A.ShortName,A.ProjectOwner as ProjectOwnerID,A.ProjectManager as ProjectManagerID,
					 A.DeliveryDate,A.RevisedDeliveryDate,A.ApprvChangeRequest,A.PMComments,A.POComments,A.DeliveryComments,A.Isactive,A.Utilization                                      
					 from dbo.Project A inner join [User] B on A.ProjectOwner=B.UserID                                    
					 inner join [User] C on A.ProjectManager=C.UserID                                     
					 left join TimeTracker TT on TT.ProjectID=A.ProjectID --and isnull(TT.ActualHours,0)>0                                    
					 left join Phase D on D.PhaseID=A.PhaseID where A.ProjectName Not In('Admin','Open')   
					 And A.Isactive = '1'
					 order By ProjectName                                                      
	end                                     
                                     
 else                                 
    begin                              
      Select distinct A.projectID,B.PhaseID,A.ProjectName,B.BillableHours,B.BudgetHours,B.RevisedBudgetHours,B.EstimationRoleID,                           
					 estRole.RoleName as EstRoleName,estRole.ShortName as EstRoleshortName,D.Role,D.ShortName,D.EstimationPercentage,                                            
					  CASE A.ProjectType                                 
						WHEN  'F'  THEN round(isnull(E.ActualHours,0) * D.EstimationPercentage ,0) ELSE round(isnull(E.ActualHours,0),0)                                                      
						END PeriodActualHours,                                             
					 CASE A.ProjectType                                 
						WHEN  'F'  THEN round(isnull((E.EstimatedHours * D.EstimationPercentage),0),0) ELSE round(isnull((E.EstimatedHours),0),0)                                         	             
						END periodEstimatedHours,                        
					 CASE A.ProjectType                                 
						WHEN  'F'  THEN round(isnull((F.ActualHours * D.EstimationPercentage),0),0) ELSE round(isnull((F.ActualHours),0),0)                                                 
						END ActualHours
					 ,B.Comments as RevisedComments                        
					 from Project A 
					 left join ProjectEstimation B on A.ProjectID=B.ProjectID                                    
					 inner join Phase C on B.PhaseID=C.PhaseID                                     
					 inner join EstimationRole estRole on estRole.EstimationRoleID=B.EstimationRoleID                                    
					 inner join Role D on  D.EstimationRoleID=estRole.EstimationRoleID                                    
					 left join TimeTracker TT on TT.ProjectID=A.ProjectID and TT.RoleID=D.RoleID and TT.PhaseID=B.PhaseID 
					 --and  isnull(TT.ActualHours,0)>0   chk8                                    
					 left join (
								  Select isnull(sum(A.ActualHours),0)ActualHours,isnull(sum(A.EstimatedHours),0)EstimatedHours,                                             
								  A.phaseID,A.RoleID,A.ProjectId from TimeTracker A   where A.FromDate>=@dayfrom and A.ToDate<=@dayto                                    
								  group by A.RoleID,A.phaseID,A.ProjectId
								)as E on E.phaseID=B.PhaseID and E.ProjectId=B.ProjectId and  E.RoleID=D.RoleID  
					 left join (
								  Select isnull(sum(A.ActualHours),0)ActualHours,A.phaseID,A.RoleID,A.ProjectId from TimeTracker A where 1=1                                    
								  group by A.RoleID,A.phaseID,A.ProjectId
								)as F on F.phaseID=B.PhaseID and F.ProjectId=B.ProjectId and F.RoleID=D.RoleID
					 inner join (
								 Select distinct A.ProjectID, A.PhaseID                                     
								 from dbo.Project A                                     
								 left join TimeTracker TT on TT.ProjectID=A.ProjectID                                    
								 left join Phase D on D.PhaseID=A.PhaseID where A.ProjectName Not In('Admin','Open')   
								 And A.Isactive = '1' 
								) as P on P.ProjectID = B.ProjectId   
								                               
					where   A.ProjectName Not In('Admin','Open')                                     
					group by A.ProjectType,A.ProjectID,B.phaseID,A.ProjectName,B.BillableHours,B.BudgetHours,
					B.RevisedBudgetHours,B.EstimationRoleID,estRole.RoleName,estRole.ShortName,D.Role,D.ShortName,
					D.EstimationPercentage  ,E.EstimatedHours,E.ActualHours ,F.ActualHours, B.Comments order by A.ProjectName                        
                                          
          end                                     
                                    
end                                    
else                                    
		begin                           
			IF(@tabType='Preview')                                    
				BEGIN                                   
					IF(@IsIncludeProposalAndVAS<>1)
						BEGIN 
							SELECT DISTINCT ISNULL(A.ProjectDashboardID,0)ProjectDashboardID, A.ProjectID,A.ProjectName,
							
							CASE A.Utilization 
								WHEN 'C' THEN 'Commercial'
								WHEN 'S' THEN 'Semi Commercial' 
								WHEN 'I' THEN 'Internal' 
								WHEN 'G' THEN 'GIS' 
								WHEN 'P' THEN 'Product' 
								ELSE ''
							END Category,				
																					
							CASE A.ProjectType 
								WHEN 'F' THEN 'Fixed Cost'
								WHEN 'T' THEN 'T & M'
								ELSE ''  
							END ProjectType ,
														
							A.ClientStatus,A.TimelineStatus,A.BudgetStatus,A.EscalateStatus,                                    
							B.FirstName AS ProjectOwner,C.FirstName AS ProjectManager, D.Phase AS CurrentPhase, A.POComments AS POComments,
							A.PMComments AS PMComments,A.DeliveryComments AS DeliveryComments ,A.comments AS WeeklyComments,db.[status]    
							,CASE WHEN E.ActualHrs>E.BudgetHours THEN 'R' ELSE 'G' END AS BudgetStatus,isnull(E.PeriodEstimetedHrsTotal,0) Estimate,isnull(E.PeriodActualHrsTotal,0)SortOrder                       
							FROM  Dashboard db                              
							JOIN dbo.ProjectDashboard A ON A.DashboardID=db.DashboardID AND db.DashboardID=@DashboardID                      
							INNER JOIN [User] B ON A.ProjectOwner=B.UserID                                    
							INNER JOIN [User] C ON A.ProjectManager=C.UserID                                   
							INNER JOIN [project] P ON A.ProjectId = P.ProjectId -- Added on 06/05/2011                                        
							LEFT JOIN Phase D ON D.PhaseID=A.PhaseID  
							LEFT JOIN ProjectDashboardEstimation PDE On PDE.ProjectId=P.ProjectId  --AND PDE.PhaseId NOT IN (1,6) --EXCLUDED PROPOSAL & VAS                       
							LEFT JOIN
								(
								  SELECT ProjectID,sum(ISNULL(BudgetHours,0)) BudgetHours,sum(ISNULL(RevisedBudgetHours,0)) RevisedBudgetHours,
								  sum(ISNULL(ActualHrs,0)) ActualHrs ,sum(round(PeriodActualHrsTotal,0))PeriodActualHrsTotal,sum(round(PeriodEstimetedHrsTotal,0))PeriodEstimetedHrsTotal
								  FROM
									(SELECT dt1.ProjectID,dt1.PhaseID,sum(dt1.BudgetHours) BudgetHours,sum(dt1.RevisedBudgetHours) RevisedBudgetHours,
									 sum(dt1.ActualHrs) ActualHrs ,sum(round(dt1.PeriodActualHrsTotal,0))PeriodActualHrsTotal,sum(round(dt1.PeriodEstimetedHrsTotal,0))PeriodEstimetedHrsTotal                
									 FROM(	
											SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(ISNULL(projest.BudgetHours,0)) BudgetHours,
											avg(ISNULL(projest.RevisedBudgetHours,0)) RevisedBudgetHours,sum(ISNULL(projest.ActualHrs,0)) ActualHrs,                                    
											sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal,sum(round(projest.PeriodEstimetedHrsAdjusted,0))PeriodEstimetedHrsTotal  
											FROM ProjectDashBoardEstimation projest             
											WHERE DashBoardId =@DashboardID AND PhaseId NOT IN (1,6) --EXCLUDED PROPOSAL & VAS         
											GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
										  ) AS dt1            
									GROUP BY dt1.PhaseID,dt1.ProjectID
									) AS d1                            
									GROUP BY d1.projectID				
								
								) AS E ON E.ProjectID=A.ProjectID
							WHERE (A.DashboardID = @DashboardID AND  db.[status]='F'   
							OR 
							(A.projectID IN (
												SELECT ProjectID FROM ProjectDashboard WHERE ProjectDashboard.DashboardID = @DashboardID   
											)
							AND db.[status]='I')) and P.ShowInDashboard=1            
						                       
						   ORDER BY SortOrder Desc,Estimate Desc,A.ProjectName
		  
		END  
		  
		ELSE
		BEGIN    
			SELECT DISTINCT ISNULL(A.ProjectDashboardID,0)ProjectDashboardID, A.ProjectID,A.ProjectName,
			
			CASE A.Utilization 
				WHEN 'C' THEN 'Commercial'
				WHEN 'S' THEN 'Semi Commercial' 
				WHEN 'I' THEN 'Internal' 
				WHEN 'G' THEN 'GIS'
				WHEN 'P' THEN 'Product'
				ELSE ''
			END Category,	
			CASE A.ProjectType 
				WHEN 'F' THEN 'Fixed Cost'
				WHEN 'T' THEN 'T & M' 
				ELSE ''  
			END ProjectType ,A.ClientStatus,A.TimelineStatus,A.BudgetStatus,A.EscalateStatus,                                    
			B.FirstName AS ProjectOwner,C.FirstName AS ProjectManager, D.Phase AS CurrentPhase, A.POComments AS POComments,
			A.PMComments AS PMComments,A.DeliveryComments AS DeliveryComments ,A.comments AS WeeklyComments,db.[status]    
			,CASE WHEN E.ActualHrs>E.BudgetHours THEN 'R' ELSE 'G' END AS BudgetStatus,isnull(dt3.PeriodEstimetedHrsAdjusted,0) estimate,
			isnull(dt3.PeriodActualHrsAdjusted,0)SortOrder                            
			FROM  Dashboard db                              
			JOIN dbo.ProjectDashboard A ON A.DashboardID=db.DashboardID AND db.DashboardID=@DashboardID                      
			INNER JOIN [User] B ON A.ProjectOwner=B.UserID                                    
			INNER JOIN [User] C ON A.ProjectManager=C.UserID                                   
			INNER JOIN [project] P ON A.ProjectId = P.ProjectId -- Added on 06/05/2011                                        
			LEFT JOIN Phase D ON D.PhaseID=A.PhaseID  
			LEFT JOIN ProjectDashboardEstimation PDE On PDE.ProjectId=P.ProjectId  --AND PDE.PhaseId NOT IN (1,6) --EXCLUDED PROPOSAL & VAS                       
			LEFT JOIN
				(
				  SELECT ProjectID,sum(ISNULL(BudgetHours,0)) BudgetHours,sum(ISNULL(RevisedBudgetHours,0)) RevisedBudgetHours,
				  sum(ISNULL(ActualHrs,0)) ActualHrs ,sum(round(PeriodActualHrsTotal,0))PeriodActualHrsTotal
				  FROM
					(SELECT dt1.ProjectID,dt1.PhaseID,sum(dt1.BudgetHours) BudgetHours,sum(dt1.RevisedBudgetHours) RevisedBudgetHours,
					 sum(dt1.ActualHrs) ActualHrs ,sum(round(dt1.PeriodActualHrsTotal,0))PeriodActualHrsTotal               
					 FROM(	
							SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(ISNULL(projest.BudgetHours,0)) BudgetHours,
							avg(ISNULL(projest.RevisedBudgetHours,0)) RevisedBudgetHours,sum(ISNULL(projest.ActualHrs,0)) ActualHrs,                                    
							sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal  
							FROM ProjectDashBoardEstimation projest             
							WHERE DashBoardId =@DashboardID AND PhaseId NOT IN (1,6) --EXCLUDED PROPOSAL & VAS         
							GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
						  ) AS dt1            
					GROUP BY dt1.PhaseID,dt1.ProjectID
					) AS d1                            
					GROUP BY d1.projectID				
				
				) AS E ON E.ProjectID=A.ProjectID
			LEFT JOIN
					(                                    
						SELECT projest.ProjectID,SUM(ROUND(projest.PeriodActualHrsAdjusted,0)) PeriodActualHrsAdjusted,
						SUM(ROUND(projest.PeriodEstimetedHrsAdjusted,0)) PeriodEstimetedHrsAdjusted  from                                     
						dbo.ProjectDashboardEstimation projest WHERE projest.DashboardID =@DashboardID   group by projest.ProjectID                        
					) AS dt3 ON dt3.ProjectID=P.ProjectID 	
			WHERE (A.DashboardID = @DashboardID AND  db.[status]='F'  
			OR 
			(A.projectID IN (
								SELECT ProjectID FROM ProjectDashboard WHERE ProjectDashboard.DashboardID = @DashboardID   
							)
			AND db.[status]='I')) and P.ShowInDashboard=1             
		                       
		   ORDER BY SortOrder  Desc,Estimate desc,A.ProjectName


		END                              
	END
                                   
	ELSE IF (@tabType='ProjectEffortPhase') 
    /* This block excludes Proposal and VAS phase values (PhaseID=1 and 6) */                              
    BEGIN   
		IF(@IsIncludeProposalAndVAS<>1)
		BEGIN                           
			 SELECT distinct dt1.projectID,dt1.ProjectName,dt1.PhaseID,ph.Phase,dt1.BillableHours,dt1.BudgetHours,                                    
			 dt1.RevisedBudgetHours,dt1.ActualHours,dt2.BillableTotal,dt2.BudgetTotal,dt2.RevBudgTotal ,
			 dt3.ActualHrsTotal, isnull(dt4.PeriodEstimatedTotal,0) Estimate,isnull(dt4.PeriodActualHrsAdjusted,0)Actual                      
			 FROM                                     
			 (
			 SELECT  A.projectID,A.ProjectName, B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,
			 round(sum(isnull(B.ActualHrsAdjusted,0)),0) ActualHours,d.[status] AS DashboardStatus,                                
			 sum(round(B.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal       
			 FROM                          
			 Dashboard d                               
			 LEFT JOIN dbo.ProjectDashboard A  ON A.DashboardID=d.DashboardID AND d.DashboardID=@DashboardID            
			 LEFT JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID  AND B.PhaseID NOT IN(1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services                                  
			 LEFT JOIN                                     
			 (
				SELECT dt1.PhaseID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours,sum(dt1.BudgetHours) BudgetHours,
				sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
				FROM(	
						SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
						avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
						FROM ProjectDashBoardEstimation projest             
						WHERE DashBoardId = @DashboardID          
						GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
					) AS dt1            
			GROUP BY dt1.PhaseID,dt1.ProjectID) AS d1 ON d1.PhaseID=B.PhaseID AND d1.ProjectID=B.ProjectID                                         
			WHERE  A.DashboardID =@DashboardID                            
			GROUP BY A.projectID,A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,d.[status] ) AS dt1                                    
			LEFT JOIN dbo.Project prjt ON prjt.ProjectID=dt1.ProjectID                                    
			LEFT JOIN dbo.Phase ph ON ph.PhaseID=dt1.PhaseID                        
			                
			LEFT JOIN                                     
			(SELECT dt2.ProjectID ,sum(dt2.BillableHours) BillableTotal,sum(dt2.BudgetHours) BudgetTotal,sum(dt2.RevisedBudgetHours) RevBudgTotal            
			 FROM(SELECT dt1.PhaseID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours,sum(dt1.BudgetHours) BudgetHours,sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
				  FROM(SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
						FROM ProjectDashBoardEstimation projest             
						WHERE  DashBoardId = @DashboardID AND projest.PhaseID NOT IN(1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services           
						GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
					  ) AS dt1 GROUP BY dt1.PhaseID,dt1.ProjectID            
				) AS dt2 GROUP BY dt2.ProjectID           
			) AS dt2 ON dt2.ProjectID=dt1.ProjectID                       
			LEFT JOIN                                     
			(SELECT sum(round(isnull(projest.ActualHrsAdjusted,0),0))ActualHrsTotal,projest.ProjectID  
			 FROM dbo.ProjectDashboardEstimation projest 
			 WHERE projest.DashboardID =@DashboardID AND projest.PhaseID NOT IN(1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services   
			 GROUP BY projest.ProjectID                                     
			) AS dt3 ON dt3.ProjectID=dt1.ProjectID 
			LEFT JOIN                                     
			(                                    
				SELECT projest.ProjectID,sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsAdjusted,
				sum(round(projest.PeriodEstimetedHrsAdjusted,0)) PeriodEstimatedTotal   FROM                                     
				ProjectDashboardEstimation projest WHERE projest.DashboardID =@DashboardID	 
				And projest.PhaseID NOT IN(1,6) GROUP BY projest.ProjectID                                    
			) as dt4 on dt4.ProjectID=dt1.ProjectID
                   
			 WHERE  (dt1.DashboardStatus='F'    
			 OR (dt1.projectID in(
					SELECT ProjectID FROM ProjectDashboard WHERE  ProjectDashboard.DashboardID = @DashboardID 
			 ) 
			 AND dt1.DashboardStatus='I') ) and prjt.ShowInDashboard=1               
			ORDER BY Actual DESC,Estimate DESC,dt1.ProjectName

		END
		
		ELSE
		BEGIN
			
			 SELECT distinct dt1.projectID,dt1.ProjectName,dt1.PhaseID,ph.Phase,dt1.BillableHours,dt1.BudgetHours,                                    
			 dt1.RevisedBudgetHours,dt1.ActualHours,dt2.BillableTotal,dt2.BudgetTotal,dt2.RevBudgTotal ,dt3.ActualHrsTotal,
			 isnull(dt3.PeriodEstimatedTotal,0) Estimate,isnull(dt3.PeriodActualHrsTotal,0)Actual                        
			 FROM                                     
			(SELECT  A.projectID, A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,
			 round(sum(isnull(B.ActualHrsAdjusted,0)),0) ActualHours,d.[status] AS DashboardStatus                                
			 FROM                          
			 Dashboard d                               
			 LEFT JOIN dbo.ProjectDashboard A  ON A.DashboardID=d.DashboardID AND d.DashboardID=@DashboardID            
			 LEFT JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID                                   
			 LEFT JOIN                                     
			 (SELECT dt1.PhaseID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours,sum(dt1.BudgetHours) BudgetHours,
			  sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
			  FROM(	SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
			  avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
					FROM ProjectDashBoardEstimation projest             
					WHERE DashBoardId = @DashboardID          
					GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
				  ) AS dt1            
			GROUP BY dt1.PhaseID,dt1.ProjectID) AS d1 ON d1.PhaseID=B.PhaseID AND d1.ProjectID=B.ProjectID                                         
			WHERE  A.DashboardID =@DashboardID                            
			GROUP BY A.projectID, A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,d.[status] ) AS dt1                                    
			LEFT JOIN dbo.Project prjt ON prjt.ProjectID=dt1.ProjectID                                    
			LEFT JOIN dbo.Phase ph ON ph.PhaseID=dt1.PhaseID                        
			                
			LEFT JOIN                                     
			(SELECT dt2.ProjectID ,sum(dt2.BillableHours) BillableTotal,sum(dt2.BudgetHours) BudgetTotal,
			 sum(dt2.RevisedBudgetHours) RevBudgTotal            
			 FROM(SELECT dt1.PhaseID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours,sum(dt1.BudgetHours) BudgetHours,
			 sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
				  FROM(SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
				  avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
						FROM ProjectDashBoardEstimation projest             
						WHERE  DashBoardId = @DashboardID 
						GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
					  ) AS dt1 GROUP BY dt1.PhaseID,dt1.ProjectID            
				) AS dt2 GROUP BY dt2.ProjectID           
			) AS dt2 ON dt2.ProjectID=dt1.ProjectID                       
			LEFT JOIN                                     
			(SELECT sum(round(isnull(projest.ActualHrsAdjusted,0),0))ActualHrsTotal,projest.ProjectID ,
			 sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal,	sum(round(projest.PeriodEstimetedHrsAdjusted,0)) PeriodEstimatedTotal  
			 FROM dbo.ProjectDashboardEstimation projest 
			 WHERE projest.DashboardID =@DashboardID 
			 GROUP BY projest.ProjectID                                     
			) AS dt3 ON dt3.ProjectID=dt1.ProjectID                                    
			 WHERE  (dt1.DashboardStatus='F'                     
			OR (dt1.projectID in( 
				SELECT ProjectID FROM ProjectDashboard WHERE  ProjectDashboard.DashboardID = @DashboardID   
			) 
			AND dt1.DashboardStatus='I')) and prjt.ShowInDashboard=1                 
			ORDER BY Actual DESC,Estimate Desc,dt1.ProjectName 
							 

		END                    
	END        
                                      
    ELSE IF(@tabType='PeriodEffortPhase')                                    
    /* PeriodEffortPhase starts here */
     BEGIN   
		
		IF(@IsIncludeProposalAndVAS<>1)
		BEGIN 
			/* This block excludes Proposal and VAS phase values (PhaseID=1 and 6) */
			SELECT DISTINCT Ph.Phase AS CurrPhase ,A.projectID,A.ProjectName,C.Phase,B.PhaseID,
			sum(round(B.PeriodEstimetedHrsAdjusted,0)) AS PeriodEstimetedHrs,      
			sum(round(B.PeriodActualHrsAdjusted,0)) AS PeriodActualHrs,isnull(D.PeriodEstHrsTotal,0)PeriodEstHrsTotal,
			isnull(D.PeriodActualHrsTotal,0)PeriodActualHrsTotal,isnull(dt3.PeriodActualHrsAdjusted,0)SortOrder                                  
			FROM Project proj                                  
			LEFT JOIN ProjectDashboard A ON proj.ProjectID=A.ProjectID
			LEFT JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID  AND B.PhaseID NOT IN (1,6)
			LEFT JOIN Phase C ON B.PhaseID=C.PhaseID                                       
			LEFT JOIN Phase ph ON ph.PhaseID=A.PhaseID                                    
			LEFT JOIN 
				(SELECT B.ProjectID,sum(round(B.PeriodEstimetedHrsAdjusted,0))PeriodEstHrsTotal,
				sum(round(B.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal                                    
				 FROM ProjectDashboardEstimation B 
				 WHERE  B.DashboardID =@DashboardID AND B.PhaseID NOT IN (1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services
				 GROUP BY B.ProjectID) AS D                                     
			ON D.ProjectID=B.ProjectID 
			LEFT JOIN                                     
				(                                    
					SELECT projest.ProjectID,sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsAdjusted  FROM                                     
					ProjectDashboardEstimation projest WHERE projest.DashboardID =@DashboardID	 
					And projest.PhaseID NOT IN(1,6) GROUP BY projest.ProjectID                                    
				) as dt3 on dt3.ProjectID=B.ProjectID 
			WHERE  A.DashboardID =@DashboardID and proj.ShowInDashboard=1
			GROUP BY  ph.Phase ,A.projectID,A.ProjectName,C.Phase,B.PhaseID,D.PeriodEstHrsTotal,D.PeriodActualHrsTotal,dt3.PeriodActualHrsAdjusted                                    
			ORDER BY SortOrder DESC,PeriodEstHrsTotal DESC,A.ProjectName,B.PhaseID  
			
		END
		
		ELSE
			BEGIN
				/* This block includes Proposal and VAS phase values (PhaseID=1 and 6) */
				SELECT DISTINCT Ph.Phase AS CurrPhase ,A.projectID,A.ProjectName,C.Phase,B.PhaseID,
				sum(round(B.PeriodEstimetedHrsAdjusted,0)) AS PeriodEstimetedHrs,      
				sum(round(B.PeriodActualHrsAdjusted,0)) AS PeriodActualHrs,isnull(D.PeriodEstHrsTotal,0)PeriodEstHrsTotal,
				isnull(D.PeriodActualHrsTotal,0)PeriodActualHrsTotal,isnull(D.PeriodActualHrsAdjusted,0)SortOrder                                  
				FROM  Project proj                                      
				LEFT JOIN ProjectDashboard A ON proj.ProjectID=A.ProjectID 
				LEFT JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID  
				LEFT JOIN Phase C ON B.PhaseID=C.PhaseID                                   
				LEFT JOIN Phase ph ON ph.PhaseID=A.PhaseID                                    
				LEFT JOIN 
					(SELECT B.ProjectID,sum(round(B.PeriodEstimetedHrsAdjusted,0))PeriodEstHrsTotal,
					sum(round(B.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal,
					sum(round(B.PeriodActualHrsAdjusted,0))PeriodActualHrsAdjusted                                     
					 FROM ProjectDashboardEstimation B 
					 WHERE  B.DashboardID =@DashboardID 
					 GROUP BY B.ProjectID) AS D ON D.ProjectID=B.ProjectID                                     
				    
				WHERE  A.DashboardID =@DashboardID  and proj.ShowInDashboard=1                                                          
				GROUP BY  ph.Phase ,A.projectID,A.ProjectName,C.Phase,B.PhaseID,D.PeriodEstHrsTotal,D.PeriodActualHrsTotal ,D.PeriodActualHrsAdjusted                                    
				ORDER BY SortOrder DESC,PeriodEstHrsTotal DESC,A.ProjectName,B.PhaseID 
			
			END                  
     END                                          
      
     /* PeriodEffortPhase ends here */                                   
                                         
    else if (@tabType='ProjectEffortRole')                                    
     begin  
		IF(@IsIncludeProposalAndVAS=1)
		BEGIN 
		
			/* This block includes Proposal and VAS phase values */                                  
			SELECT DISTINCT dt1.projectID,dt1.ProjectName,dt1.EstimationRoleID,dt1.RoleName,                                    
			dt4.RevisedBudgetHours,dt1.ActualHours,dt2.RevBudgTotal ,dt3.ActualHrsTotal,
			isnull(dt3.PeriodEstimatedTotal,0) Estimate,isnull(dt3.PeriodActualHrsTotal,0)SortOrder                                  
			from                                     
			(
				Select A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName,                                    
				round(sum(isnull(B.ActualHrsAdjusted,0)),0) ActualHours,d.[status] as DashboardStatus                        
				from Dashboard d       
				join dbo.ProjectDashboard A on A.DashboardID=d.DashboardID and d.DashboardID=@DashboardID                        
				left  join dbo.ProjectDashboardEstimation B on A.ProjectID=B.ProjectID and A.DashboardID=B.DashboardID                                     
				left  join dbo.EstimationRole estRole on estRole.EstimationRoleID=B.EstimationRoleID                                    
				left  join ProjectEstimation projest  on projest.ProjectID=B.ProjectID  and  B.PhaseID=projest.PhaseID                                    
				and projest.EstimationRoleID=B.EstimationRoleID                                  
				and projest.EstimationRoleID=estRole.EstimationRoleID                                                                  
				where  A.DashboardID =@DashboardID                                   
				group by A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName, d.[status]) as dt1                                    
				left join dbo.Project prjt on prjt.ProjectID=dt1.ProjectID                                    
				left join dbo.ProjectEstimation er on er.EstimationRoleID=dt1.EstimationRoleID                                              
				left join                                     
				(                  
					Select dt2.ProjectID ,sum(dt2.RevisedBudgetHours) RevBudgTotal            
					from(         
							Select dt1.PhaseID,dt1.ProjectID ,sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
							from(            
									Select  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
									avg(projest.BudgetHours) BudgetHours ,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
									from ProjectDashBoardEstimation projest             
									Where  DashBoardId = @DashboardID --And projest.ProjectID=31 --Andnd projest.PhaseID=1            
									group by projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
								) as dt1 group by dt1.PhaseID,dt1.ProjectID   
						) as dt2 group by dt2.ProjectID  
				 ) as dt2 on dt2.ProjectID=dt1.ProjectID                                    
				 left join                           
				 (                                    
					Select dt1.EstimationRoleID,dt1.ProjectID ,sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
					from(            
							Select  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
							avg(projest.BudgetHours) BudgetHours ,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
							from ProjectDashBoardEstimation projest             
							Where DashBoardId = @DashboardID --And projest.ProjectID=31 --And projest.PhaseID=1            
							group by projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
						) as dt1 group by dt1.EstimationRoleID,dt1.ProjectID  
				  ) as dt4 on dt4.EstimationRoleID=dt1.EstimationRoleID and dt4.ProjectID=dt2.ProjectID 
				  left join                                     
				  (                                    
						Select sum(isnull(projest.ActualHrsAdjusted,0))ActualHrsTotal,projest.ProjectID,sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal,
						sum(round(projest.PeriodEstimetedHrsAdjusted,0)) PeriodEstimatedTotal  from                                     
						dbo.ProjectDashboardEstimation projest where projest.DashboardID =@DashboardID   
						group by projest.ProjectID                                    
				   ) as dt3 on dt3.ProjectID=dt1.ProjectID                                      
				  where   (dt1.DashboardStatus='F'  or 
				  ( 
						dt1.projectID  in
						(												  
							SELECT ProjectID FROM ProjectDashboard WHERE  ProjectDashboard.DashboardID = @DashboardID        
						) 
						and dt1.DashboardStatus='I'
			))and prjt.ShowInDashboard=1
			
			order by  SortOrder DESC,Estimate desc,dt1.ProjectName 
				                     
		END
		
		ELSE
		BEGIN
				/* This block EXCLUDES Proposal and VAS phase values (PhaseID=1 and 6) */   
				SELECT DISTINCT dt1.projectID,dt1.ProjectName,dt1.EstimationRoleID,dt1.RoleName                             
				,dt4.RevisedBudgetHours,dt1.ActualHours,dt2.RevBudgTotal ,dt3.ActualHrsTotal,
				isnull(dt3.PeriodEstimatedTotal,0) Estimate,isnull(dt3.PeriodActualHrsTotal,0)SortOrder                                   
				FROM                                     
				(SELECT A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName                                    
				,ROUND(SUM(ISNULL(B.ActualHrsAdjusted,0)),0) ActualHours,d.[status] AS DashboardStatus                        
				FROM Dashboard d       
				JOIN dbo.ProjectDashboard A on A.DashboardID=d.DashboardID AND d.DashboardID=@DashboardID                        
				LEFT JOIN dbo.ProjectDashboardEstimation B on A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID AND B.PhaseID NOT IN (1,6)                                   
				LEFT JOIN dbo.EstimationRole estRole on estRole.EstimationRoleID=B.EstimationRoleID                                    
				LEFT JOIN ProjectEstimation projest  on projest.ProjectID=B.ProjectID AND B.PhaseID=projest.PhaseID                                    
				AND projest.EstimationRoleID=B.EstimationRoleID                                  
				AND projest.EstimationRoleID=estRole.EstimationRoleID                                          
			                             
				WHERE  A.DashboardID = @DashboardID                                   
				GROUP BY A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName, d.[status]) as dt1                                    
				LEFT JOIN dbo.Project prjt on prjt.ProjectID=dt1.ProjectID                                    
				LEFT JOIN dbo.ProjectEstimation er on er.EstimationRoleID=dt1.EstimationRoleID                                                            
				LEFT JOIN                                     
				(                                  
					SELECT dt2.ProjectID ,sum(dt2.RevisedBudgetHours) RevBudgTotal           
					FROM(                        
						SELECT dt1.PhaseID,dt1.ProjectID                                  
						,sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
						FROM(            
							SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
							avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
							FROM ProjectDashBoardEstimation projest             
							WHERE  DashBoardId = @DashboardID AND projest.PhaseID NOT IN(1,6)              
							GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
						) as dt1            
						GROUP BY dt1.PhaseID,dt1.ProjectID            
					) as dt2            
					GROUP BY dt2.ProjectID            
			                                   
				) as dt2 on dt2.ProjectID=dt1.ProjectID                                    
				LEFT JOIN                                                                      
				(                                    
					SELECT dt1.EstimationRoleID,dt1.ProjectID ,sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
					FROM(            
						SELECT  projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,avg(projest.BillableHours) BillableHours,
						avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
						 FROM ProjectDashBoardEstimation projest             
						 WHERE DashBoardId = @DashboardID And projest.PhaseID NOT IN(1,6)         
						 GROUP BY projest.EstimationRoleID,projest.PhaseID,projest.ProjectID            
					) as dt1            
					GROUP BY dt1.EstimationRoleID,dt1.ProjectID                                   
				)                                    
				as dt4 on dt4.EstimationRoleID=dt1.EstimationRoleID AND dt4.ProjectID=dt2.ProjectID                                                                              
				LEFT JOIN                                     
				(                                    
					SELECT sum(isnull(projest.ActualHrsAdjusted,0))ActualHrsTotal,projest.ProjectID,sum(round(projest.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal,
					sum(round(projest.PeriodEstimetedHrsAdjusted,0)) PeriodEstimatedTotal FROM                                     
					dbo.ProjectDashboardEstimation projest WHERE projest.DashboardID =@DashboardID	 
					And projest.PhaseID NOT IN(1,6) GROUP BY projest.ProjectID                                    
				) as dt3 on dt3.ProjectID=dt1.ProjectID                                      
				WHERE   (dt1.DashboardStatus='F'  or 
				( dt1.projectID  in
					(										
						SELECT ProjectID FROM ProjectDashboard WHERE  ProjectDashboard.DashboardID = @DashboardID                                   
					) 
				AND dt1.DashboardStatus='I') )and prjt.ShowInDashboard=1
				                                                          
				order by SortOrder DESC,Estimate DESC,dt1.ProjectName 
			
		END
                                         
     END                                     
   ELSE IF (@tabType='PeriodEffortRole')                                    
     BEGIN                                    
		
		IF(@IsIncludeProposalAndVAS=1)
		BEGIN                         
			/* This block INCLUDES Proposal and VAS phase values */         
            SELECT DISTINCT dt1.projectID,dt1.ProjectName,dt1.EstimationRoleID,dt1.RoleName                                    
			,dt1.PeriodEstimetedHrs,dt1.PeriodActualHrs,dt3.EstTotal ,dt3.ActTotal,isnull(dt3.PeriodActualHrsAdjusted,0)SortOrder                                    
			FROM Project prjt 
			LEFT JOIN	(
					SELECT A.projectID,A.ProjectName,B.EstimationRoleID,estRole.RoleName                                    
					,SUM(ROUND(B.PeriodEstimetedHrsAdjusted,0))  PeriodEstimetedHrs,SUM(ROUND(B.PeriodActualHrsAdjusted,0)) PeriodActualHrs                                    
					FROM
					dbo.ProjectDashboard A                                     
					LEFT JOIN dbo.ProjectDashboardEstimation B on A.ProjectID=B.ProjectID and A.DashboardID=B.DashboardID                                     
					LEFT JOIN dbo.EstimationRole estRole on estRole.EstimationRoleID=B.EstimationRoleID                                    
					LEFT JOIN ProjectEstimation projest  on projest.ProjectID=B.ProjectID  and  B.PhaseID=projest.PhaseID                              
					AND projest.EstimationRoleID=B.EstimationRoleID                                     
					AND projest.EstimationRoleID=estRole.EstimationRoleID                                          
		                                    
					WHERE  A.DashboardID =@DashboardID                                    
					GROUP BY A.projectID,A.ProjectName,B.EstimationRoleID,estRole.RoleName
				) as dt1 on prjt.ProjectID=dt1.ProjectID   
			LEFT JOIN dbo.ProjectEstimation er on er.EstimationRoleID=dt1.EstimationRoleID  
			LEFT JOIN
			(                                    
				SELECT SUM(ROUND(ISNULL(projest.PeriodActualHrsAdjusted,0),0))ActTotal,projest.ProjectID,
				sum(round(projest.PeriodEstimetedHrsAdjusted,0))EstTotal,SUM(ROUND(projest.PeriodActualHrsAdjusted,0)) PeriodActualHrsAdjusted from                                     
				dbo.ProjectDashboardEstimation projest WHERE projest.DashboardID =@DashboardID   group by projest.ProjectID                        
			) AS dt3 ON dt3.ProjectID=dt1.ProjectID            
                                       
			where  dt1.ProjectID>0 and prjt.ShowInDashboard=1 
			ORDER BY SortOrder DESC,EstTotal DESC,dt1.ProjectName 
			
		END
		ELSE
		BEGIN
			/* This block EXCLUDES Proposal and VAS phase values (PhaseID=1 and 6) */         
			SELECT DISTINCT dt1.projectID,dt1.ProjectName,dt1.EstimationRoleID,dt1.RoleName                                    
			,dt1.PeriodEstimetedHrs,dt1.PeriodActualHrs,dt3.EstTotal ,dt3.ActTotal,isnull(D.PeriodActualHrsTotal,0)SortOrder,
			isnull(D.PeriodEstimatedHrsTotal,0) sortAlloc                                    
			FROM 
			(
				SELECT A.projectID,A.ProjectName,B.EstimationRoleID,estRole.RoleName                                    
				,SUM(ROUND(B.PeriodEstimetedHrsAdjusted,0))  PeriodEstimetedHrs,SUM(ROUND(B.PeriodActualHrsAdjusted,0)) PeriodActualHrs                                    
				FROM
				dbo.ProjectDashboard A                                     
				LEFT JOIN dbo.ProjectDashboardEstimation B on A.ProjectID=B.ProjectID and A.DashboardID=B.DashboardID AND B.PhaseID NOT IN (1,6)                                      
				LEFT JOIN dbo.EstimationRole estRole on estRole.EstimationRoleID=B.EstimationRoleID                                  
				LEFT JOIN ProjectEstimation projest  on projest.ProjectID=B.ProjectID  and  B.PhaseID=projest.PhaseID                                    
				AND projest.EstimationRoleID=B.EstimationRoleID                                     
				AND projest.EstimationRoleID=estRole.EstimationRoleID                                          
	                                    
				WHERE  A.DashboardID =@DashboardID 							                                
				GROUP BY A.projectID,A.ProjectName,B.EstimationRoleID,estRole.RoleName
			) as dt1                                    
			LEFT JOIN dbo.Project prjt on prjt.ProjectID=dt1.ProjectID                                    
			LEFT JOIN dbo.ProjectEstimation er on er.EstimationRoleID=dt1.EstimationRoleID   
			LEFT JOIN
			(                                    
				SELECT SUM(ROUND(ISNULL(projest.PeriodActualHrsAdjusted,0),0))ActTotal,projest.ProjectID,
				sum(round(projest.PeriodEstimetedHrsAdjusted,0))EstTotal,SUM(ROUND(projest.PeriodActualHrsAdjusted,0)) PeriodActualHrsAdjusted  from                                     
				dbo.ProjectDashboardEstimation projest WHERE projest.DashboardID =@DashboardID   group by projest.ProjectID                        
			) AS dt3 ON dt3.ProjectID=dt1.ProjectID  
			LEFT JOIN 
			(SELECT PDE.ProjectID,
			sum(round(PDE.PeriodActualHrsAdjusted,0))PeriodActualHrsTotal,
			sum(round(PDE.PeriodEstimetedHrsAdjusted,0))PeriodEstimatedHrsTotal                                    
			 FROM ProjectDashboardEstimation PDE 
			 WHERE  PDE.DashboardID =@DashboardID AND PDE.PhaseID NOT IN (1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services
			 GROUP BY PDE.ProjectID) AS D                                     
		    ON D.ProjectID=dt1.ProjectID  where  prjt.ShowInDashboard=1  
			ORDER BY SortOrder DESC,sortAlloc DESC,dt1.ProjectName 

			
		END                                  
     END                                     
 end                                     
                                         
                                        
END 