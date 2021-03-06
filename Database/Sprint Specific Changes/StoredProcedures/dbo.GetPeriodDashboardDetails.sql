USE [PMOscar_Live]
GO
/****** Object:  StoredProcedure [dbo].[GetPeriodDashboardDetails]    Script Date: 6/1/2020 3:42:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Sajid
-- Create date: 04/18/2012
-- Description:	Get Period dashboard detils for different tabs.
-- Modified By : Kochurani Kuriakose
-- Modified On : 2018-06-25
-- Modified By:Haritha E.S
--Modified On:2018-05-07
-- =============================================
-- Exec GetPeriodDashboardDetails 5,221,0,0,'0'
ALTER PROCEDURE [dbo].[GetPeriodDashboardDetails] 
                                     
 @ProgramID int,                                    
 @ProjectID int,
 @StartWeekID int,
 @EndWeekID int,
 @IsIncludeProposalAndVAS bit
AS
BEGIN
		
	IF(@IsIncludeProposalAndVAS = 1)
		BEGIN
		/********************************* Summary Tab Start ******************************************/
		    SELECT DISTINCT D.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort, D.Name AS Period,P.ProjectId,P.ProjStartDate,PD.ProjectName,
			CASE PD.Utilization 
				WHEN 'C' THEN 'Commercial'
				WHEN 'S' THEN 'Semi Commercial' 
				WHEN 'I' THEN 'Internal' 
				WHEN 'P' THEN 'Product' 
				ELSE ''
			END Category, PH.Phase AS CurrentPhase, 
			PD.ClientStatus,PD.TimelineStatus,PD.BudgetStatus,PD.EscalateStatus, 
			ISNULL(Convert(varchar,PD.DeliveryDate,103),'') AS DeliveryDate, ISNULL(Convert(varchar,PD.RevisedDeliveryDate,103),'') AS RevisedDeliveryDate, ISNULL(Convert(varchar,P.MaintClosingDate,103),'') AS MaintClosingDate,
			PD.PMComments,PD.POComments, PD.DeliveryComments, PD.Comments AS WeeklyComments
			FROM Project P
			INNER JOIN ProjectDashboard PD ON PD.ProjectId = P.ProjectId
			INNER JOIN Dashboard D ON PD.DashboardID = D.DashboardID
			INNER JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID
			INNER JOIN Phase PH ON PH.PhaseID=PD.PhaseID			
			WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 	
					AND D.PeriodType != 'M'	
			ORDER BY P.ProjStartDate DESC, D.DashboardID DESC  
			
		/********************************* Summary Tab End *******************************************************/
		
		--================================================= ***** ===============================================--
		
		/********************************* Perid Effor Phase Tab Start ******************************************/		
			SELECT DISTINCT DS.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort,DS.Name AS Period,P.ProjectID,P.ProjStartDate,PD.ProjectName,PH.Phase,PDE.PhaseID,
			SUM(ROUND(PDE.PeriodEstimetedHrsAdjusted,0)) AS AllocatedHrs,      
			cast(SUM(PDE.PeriodActualHrsAdjusted)as decimal(10,2)) AS ActualHrs
			,ISNULL(D.PeriodEstHrsTotal,0)AllocatedHrsTotal,
			cast(ISNULL(D.PeriodActualHrsTotal,0)as decimal(10,2))ActualHrsTotal,cast(ISNULL(SO.PeriodActualHrsAdjusted,0)as decimal(10,2))SortOrder
			,CO.RevisedComments                                  
			FROM Project P                                  
			LEFT JOIN ProjectDashboard PD ON P.ProjectID=PD.ProjectID
			LEFT JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID
			INNER JOIN Phase PH ON PDE.PhaseID=PH.PhaseID     
			LEFT JOIN Dashboard DS ON PDE.DashboardID = DS.DashboardID                                   
			LEFT JOIN 
				(SELECT PRDE.ProjectID,SUM(ROUND(PRDE.PeriodEstimetedHrsAdjusted,0))PeriodEstHrsTotal,cast(SUM(PRDE.PeriodActualHrsAdjusted)as decimal(10,2))PeriodActualHrsTotal                                       
				 FROM ProjectDashboardEstimation PRDE 
				 INNER JOIN dbo.ProjectDashboard PD ON PD.DashboardID = PRDE.DashboardID 
				 INNER JOIN Project P ON P.ProjectID = PD.ProjectID
				 WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
				 GROUP BY PRDE.ProjectID) AS D                                     
			ON D.ProjectID=PDE.ProjectID 
			LEFT JOIN                                     
				(SELECT projest.ProjectID,cast(SUM(projest.PeriodActualHrsAdjusted)as decimal(10,2))PeriodActualHrsAdjusted 
				 FROM  ProjectDashboardEstimation projest 
				 WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
				 GROUP BY projest.ProjectID                                    
				) AS SO ON SO.ProjectID=PDE.ProjectID 
			INNER JOIN  (SELECT top 1 projest.ProjectID,projest.RevisedComments  
				 FROM  ProjectDashboardEstimation projest 
				 WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) AND (projest.ProjectID=@ProjectID OR @ProjectID=0)) CO on CO.ProjectID=PDE.ProjectID 
	
			WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
					AND DS.PeriodType != 'M'
			GROUP BY  DS.DashboardID,DS.Name,P.projectID,P.ProjStartDate, PD.ProjectName,PH.Phase,PDE.PhaseID,D.PeriodEstHrsTotal,D.PeriodActualHrsTotal,SO.PeriodActualHrsAdjusted,CO.RevisedComments                                    
			ORDER BY P.ProjStartDate DESC, DS.DashboardID DESC,PDE.PhaseID	
				
		/********************************* Perid Effor Phase Tab Start End ******************************************/
		
		--================================================= ***** ===================================================--
			
		/*************************** Period Effort Role Start *******************************************************/		
			SELECT DISTINCT DS.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort,DS.Name AS Period,P.ProjectID,P.ProjStartDate,PD.ProjectName,PDE.EstimationRoleID,ER.RoleName,
			SUM(ROUND(PDE.PeriodEstimetedHrsAdjusted,0)) AS AllocatedHrs,      
			cast(SUM(PDE.PeriodActualHrsAdjusted)as decimal(10,2)) AS ActualHrs
			,0 AS AllocatedHrsTotal, 0 AS ActualHrsTotal -- This is given temperorily since removing this require code change			                                  
			,CO.RevisedComments
			FROM Project P                                  
			LEFT JOIN ProjectDashboard PD ON P.ProjectID=PD.ProjectID
			LEFT JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID  
			INNER JOIN dbo.EstimationRole ER ON ER.EstimationRoleID=PDE.EstimationRoleID  
			LEFT JOIN Dashboard DS ON PDE.DashboardID = DS.DashboardID     
			INNER JOIN  (SELECT top 1 projest.ProjectID,projest.RevisedComments  
				 FROM  ProjectDashboardEstimation projest 
				 WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) AND (projest.ProjectID=@ProjectID OR @ProjectID=0)) CO on CO.ProjectID=PDE.ProjectID 
	                            
			WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))
					AND DS.PeriodType != 'M' 
			GROUP BY  DS.DashboardID,DS.Name,P.projectID,P.ProjStartDate, PD.ProjectName,PDE.EstimationRoleID,ER.RoleName,CO.RevisedComments
			ORDER BY P.ProjStartDate DESC, DS.DashboardID DESC, PDE.EstimationRoleID  	
			/*************************** Period Effort Role End *******************************************************/
			
			--================================================= ***** ================================================--
			
			/*************************** Project Effort Phase Start ***************************************************/			
			SELECT DISTINCT dt1.DashboardID,dt1.toSort,dt1.Period,dt1.ProjectID,prjt.ProjStartDate, dt1.ProjectName,dt1.PhaseID,PH.Phase,dt1.BillableHours,dt1.BudgetHours,                                    
			 dt1.RevisedBudgetHours,cast(dt1.ActualHours as decimal(10,2)) ActualHours                     
			 FROM                                     
			 (
			 SELECT  d.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort, d.Name AS Period,A.projectID, A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,
			 cast(sum(isnull(B.ActualHrsAdjusted,0))as decimal(10,2)) ActualHours,d.[status] AS DashboardStatus,                                
			 cast(sum(B.PeriodActualHrsAdjusted)as decimal(10,2))PeriodActualHrsTotal       
			 FROM                          
			 Dashboard d                               
			 LEFT JOIN dbo.ProjectDashboard A  ON A.DashboardID=d.DashboardID 
			 INNER JOIN Project P ON P.ProjectID = A.ProjectID           
			 LEFT JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID  
			 LEFT JOIN                                     
				(
					SELECT dt1.DashboardID,dt1.PhaseID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours,sum(dt1.BudgetHours) BudgetHours,
					sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
					FROM(	
							SELECT  DashboardID,projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,projest.BillableHours BillableHours,
							avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
							FROM ProjectDashBoardEstimation projest             
							WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))        
							GROUP BY DashboardID,projest.EstimationRoleID,projest.PhaseID,projest.ProjectID,projest.BillableHours            
						) AS dt1            
			GROUP BY dt1.PhaseID,dt1.ProjectID,dt1.DashboardID) AS d1 ON d1.PhaseID=B.PhaseID AND d1.ProjectID=B.ProjectID AND d1.DashboardID = B.DashboardID                                        
			WHERE (P.ProgId=@ProgramID OR @ProgramID=0) AND (A.ProjectID=@ProjectID OR @ProjectID=0) AND (A.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
				   AND d.PeriodType != 'M'                         
			GROUP BY d.DashboardID, d.Name,A.projectID, A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,d.[status] ) AS dt1                                    
			INNER JOIN dbo.Project prjt ON prjt.ProjectID=dt1.ProjectID                                    
			INNER JOIN dbo.Phase PH ON PH.PhaseID=dt1.PhaseID 			
			ORDER BY prjt.ProjStartDate DESC, dt1.DashboardID DESC, dt1.PhaseID	
			/*************************** Project Effort Phase End *****************************************************/
			
			--================================================= ***** ================================================--
			
			/*************************** Project Effort Role Start ****************************************************/
			SELECT DISTINCT dt1.DashboardID,dt1.toSort, dt1.Period, dt1.ProjectID,PRJ.ProjStartDate, dt1.ProjectName,dt1.EstimationRoleID, dt1.RoleName,                                    
			dt4.BillableHours AS BillableHours,dt4.BudgetHours AS BudgetHours, dt4.RevisedBudgetHours,cast(dt1.ActualHours as decimal(10,2)) ActualHours,dt1.RevisedComments
			FROM                                     
			(
				SELECT D.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort, D.Name AS Period, A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName,                                    
				cast(sum(isnull(B.ActualHrsAdjusted,0))as decimal(10,2)) ActualHours,D.[status] AS DashboardStatus,B.RevisedComments                        
				FROM Dashboard D       
				JOIN dbo.ProjectDashboard A ON A.DashboardID= D.DashboardID
				INNER JOIN Project P ON P.ProjectID = A.ProjectID                         
				LEFT  JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID  
				INNER  JOIN dbo.EstimationRole estRole ON estRole.EstimationRoleID=B.EstimationRoleID                                    
				LEFT  JOIN ProjectEstimation projest  ON projest.ProjectID=B.ProjectID  AND  B.PhaseID=projest.PhaseID                                    
				AND projest.EstimationRoleID=B.EstimationRoleID                                  
				AND projest.EstimationRoleID=estRole.EstimationRoleID                                                                  
				WHERE (P.ProgId=@ProgramID OR @ProgramID=0) AND (A.ProjectID=@ProjectID OR @ProjectID=0) AND (A.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))                                   
					  AND D.PeriodType != 'M'
				GROUP BY D.DashboardID, D.Name, A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName, d.[status],B.RevisedComments) AS dt1                                    
				LEFT JOIN dbo.Project PRJ ON PRJ.ProjectID = dt1.ProjectID                                    
				INNER JOIN dbo.ProjectEstimation er ON er.EstimationRoleID=dt1.EstimationRoleID                                              
				LEFT JOIN                           
				 (                                    
					SELECT dt1.DashboardID,dt1.EstimationRoleID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours ,sum(dt1.BudgetHours) BudgetHours, sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
					FROM(            
							SELECT DashboardID,projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,projest.BillableHours BillableHours,
							projest.BudgetHours BudgetHours ,projest.RevisedBudgetHours RevisedBudgetHours                                    
							FROM ProjectDashBoardEstimation projest             
							WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))           
							GROUP BY DashboardID, projest.EstimationRoleID, projest.PhaseID,projest.ProjectID,
									 projest.BillableHours, projest.BudgetHours, projest.RevisedBudgetHours
						) AS dt1 GROUP BY dt1.EstimationRoleID,dt1.ProjectID,dt1.DashboardID  
				  ) AS dt4 ON dt4.EstimationRoleID=dt1.EstimationRoleID AND dt4.ProjectID=dt1.ProjectID AND dt4.DashboardID = dt1.DashboardID
				  
			ORDER BY PRJ.ProjStartDate DESC, dt1.DashboardID DESC,dt1.EstimationRoleID 		
			/*************************** Project Effort Role End ******************************************************/
			
		END
		
		--//////// This Section Exclude Proposal and VAS Phases /////////--
	ELSE 
	
		BEGIN
		
		/********************************* Summary Tab Start ******************************************/
		 	SELECT DISTINCT D.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort, D.Name AS Period, P.ProjectId,P.ProjStartDate, PD.ProjectName,	
			CASE PD.Utilization 
				WHEN 'C' THEN 'Commercial'
				WHEN 'S' THEN 'Semi Commercial' 
				WHEN 'I' THEN 'Internal' 
				WHEN 'P' THEN 'Product' 
				ELSE ''
			END Category, PH.Phase AS CurrentPhase,
			PD.ClientStatus,PD.TimelineStatus,PD.BudgetStatus,PD.EscalateStatus,  
			ISNULL(Convert(varchar,PD.DeliveryDate,103),'') AS DeliveryDate, ISNULL(Convert(varchar,PD.RevisedDeliveryDate,103),'') AS RevisedDeliveryDate, ISNULL(Convert(varchar,P.MaintClosingDate,103),'') AS MaintClosingDate,
			PD.PMComments,PD.POComments, PD.DeliveryComments, PD.Comments AS WeeklyComments
			FROM Project P
			INNER JOIN ProjectDashboard PD ON PD.ProjectId = P.ProjectId
			INNER JOIN Dashboard D ON PD.DashboardID = D.DashboardID
			INNER JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID
			INNER JOIN Phase PH ON PH.PhaseID=PD.PhaseID 			
			WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 		
					AND PDE.PhaseID NOT IN (1,6) AND D.PeriodType != 'M'
			ORDER BY P.ProjStartDate DESC, D.DashboardID DESC  	
		
		/********************************* Summary Tab End *******************************************************/
		
		--================================================= ***** ================================================--
		/********************************* Perid Effor Phase Tab Start ******************************************/
			SELECT DISTINCT DS.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort,DS.Name AS Period,P.ProjectID,P.ProjStartDate,PD.ProjectName,PH.Phase,PDE.PhaseID,
			SUM(ROUND(PDE.PeriodEstimetedHrsAdjusted,0)) AS AllocatedHrs,      
			cast(SUM(PDE.PeriodActualHrsAdjusted)as decimal(10,2)) AS ActualHrs,ISNULL(D.PeriodEstHrsTotal,0)AllocatedHrsTotal,
			cast(ISNULL(D.PeriodActualHrsTotal,0)as decimal(10,2))ActualHrsTotal,cast(ISNULL(SO.PeriodActualHrsAdjusted,0)as decimal(10,2))SortOrder
			,CO.RevisedComments                                  
			FROM Project P                                  
			LEFT JOIN ProjectDashboard PD ON P.ProjectID=PD.ProjectID
			LEFT JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID  AND PDE.PhaseID NOT IN (1,6)
			INNER JOIN Phase PH ON PDE.PhaseID=PH.PhaseID     
			LEFT JOIN Dashboard DS ON PDE.DashboardID = DS.DashboardID                                   
			LEFT JOIN 
				(SELECT PRDE.ProjectID,SUM(ROUND(PRDE.PeriodEstimetedHrsAdjusted,0))PeriodEstHrsTotal,cast(SUM(PRDE.PeriodActualHrsAdjusted)as decimal(10,2))PeriodActualHrsTotal                                  
				 FROM ProjectDashboardEstimation PRDE 
				 INNER JOIN dbo.ProjectDashboard PD ON PD.DashboardID = PRDE.DashboardID 
				 INNER JOIN Project P ON P.ProjectID = PD.ProjectID AND PD.PhaseID NOT IN (1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services
				 WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))					
				 GROUP BY PRDE.ProjectID ) AS D                                     
			ON D.ProjectID=PDE.ProjectID 
			LEFT JOIN                                     
				(SELECT projest.ProjectID,cast(SUM(projest.PeriodActualHrsAdjusted)as decimal(10,2))PeriodActualHrsAdjusted   
				 FROM  ProjectDashboardEstimation projest 
				 WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) And projest.PhaseID NOT IN(1,6) 
				 GROUP BY projest.ProjectID                                  
				) AS SO ON SO.ProjectID=PDE.ProjectID  

				inner join  (SELECT top 1 projest.ProjectID,projest.RevisedComments  
				 FROM  ProjectDashboardEstimation projest 
				 WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) AND (projest.ProjectID=@ProjectID OR @ProjectID=0)  And projest.PhaseID NOT IN(1,6)) CO on CO.ProjectID=PDE.ProjectID 
			WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
					AND DS.PeriodType != 'M'
			GROUP BY  DS.DashboardID,DS.Name,P.projectID,P.ProjStartDate, PD.ProjectName,PH.Phase,PDE.PhaseID,D.PeriodEstHrsTotal,D.PeriodActualHrsTotal,SO.PeriodActualHrsAdjusted,CO.RevisedComments                                    
			ORDER BY P.ProjStartDate DESC, DS.DashboardID DESC,PDE.PhaseID  
			/********************************* Perid Effor Phase Tab End **********************************************/
			
			--================================================= ***** ================================================--
		
			/*************************** Period Effort Role Start *******************************************************/			
			SELECT DISTINCT DS.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort,DS.Name AS Period,P.ProjectID,P.ProjStartDate,PD.ProjectName,PDE.EstimationRoleID,ER.RoleName,
			SUM(ROUND(PDE.PeriodEstimetedHrsAdjusted,0)) AS AllocatedHrs,      
			cast(SUM(PDE.PeriodActualHrsAdjusted)as decimal(10,2)) AS ActualHrs
			,0 AS AllocatedHrsTotal, 0 AS ActualHrsTotal -- This is given temperorily since removing this require code change			                                  
			,CO.RevisedComments
			FROM Project P                                  
			LEFT JOIN ProjectDashboard PD ON P.ProjectID=PD.ProjectID
			LEFT JOIN dbo.ProjectDashboardEstimation PDE ON PD.ProjectID=PDE.ProjectID AND PD.DashboardID=PDE.DashboardID  
			AND PDE.PhaseID NOT IN (1,6)
			INNER JOIN dbo.EstimationRole ER ON ER.EstimationRoleID=PDE.EstimationRoleID  
			LEFT JOIN Dashboard DS ON PDE.DashboardID = DS.DashboardID                                  
			inner join  (SELECT top 1 projest.ProjectID,projest.RevisedComments  
				 FROM  ProjectDashboardEstimation projest 
				 WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) AND (projest.ProjectID=@ProjectID OR @ProjectID=0)  And projest.PhaseID NOT IN(1,6)) CO on CO.ProjectID=PDE.ProjectID 
			WHERE  (P.ProgId=@ProgramID OR @ProgramID=0) AND (P.ProjectID=@ProjectID OR @ProjectID=0) 
					AND (PD.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
					AND DS.PeriodType != 'M'
			GROUP BY  DS.DashboardID,DS.Name,P.projectID,P.ProjStartDate, PD.ProjectName,PDE.EstimationRoleID,ER.RoleName,CO.RevisedComments
			ORDER BY P.ProjStartDate DESC, DS.DashboardID DESC, PDE.EstimationRoleID  			
			/*************************** Period Effort Role End *******************************************************/
			
			--================================================= ***** ================================================--
			
			/*************************** Project Effort Phase Start ***************************************************/			
			 SELECT DISTINCT dt1.DashboardID,dt1.toSort, dt1.Period,dt1.ProjectID,prjt.ProjStartDate, dt1.ProjectName,dt1.PhaseID,PH.Phase,dt1.BillableHours,dt1.BudgetHours,                                    
			 dt1.RevisedBudgetHours,cast(dt1.ActualHours as decimal(10,2)) ActualHours                     
			 FROM                                     
			 (
			 SELECT  d.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort, d.Name AS Period,A.projectID, A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,
			 cast(sum(isnull(B.ActualHrsAdjusted,0))as decimal(10,2)) ActualHours,d.[status] AS DashboardStatus,                                
			 cast(sum(B.PeriodActualHrsAdjusted)as decimal(10,2))PeriodActualHrsTotal       
			 FROM                          
			 Dashboard d                               
			 LEFT JOIN dbo.ProjectDashboard A  ON A.DashboardID=d.DashboardID 
			 INNER JOIN Project P ON P.ProjectID = A.ProjectID           
			 LEFT JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID
					   AND B.PhaseID NOT IN(1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services
			 LEFT JOIN                                     
				(
					SELECT dt1.DashboardID,dt1.PhaseID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours,sum(dt1.BudgetHours) BudgetHours,
					sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
					FROM(	
							SELECT  DashboardID,projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,projest.BillableHours BillableHours,
							avg(projest.BudgetHours) BudgetHours,avg(projest.RevisedBudgetHours) RevisedBudgetHours                                    
							FROM ProjectDashBoardEstimation projest             
							WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))        
								  AND projest.PhaseID NOT IN(1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services
							GROUP BY DashboardID,projest.EstimationRoleID,projest.PhaseID,projest.ProjectID,projest.BillableHours            
						) AS dt1            
			GROUP BY dt1.PhaseID,dt1.ProjectID,dt1.DashboardID) AS d1 ON d1.PhaseID=B.PhaseID AND d1.ProjectID=B.ProjectID AND d1.DashboardID = B.DashboardID                                        
			WHERE (P.ProgId=@ProgramID OR @ProgramID=0) AND (A.ProjectID=@ProjectID OR @ProjectID=0) AND (A.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0)) 
				   AND d.PeriodType != 'M'                         
			GROUP BY d.DashboardID, d.Name,A.projectID, A.ProjectName,B.PhaseID,d1.BillableHours,d1.BudgetHours,d1.RevisedBudgetHours,d.[status] ) AS dt1                                    
			INNER JOIN dbo.Project prjt ON prjt.ProjectID=dt1.ProjectID                                    
			INNER JOIN dbo.Phase PH ON PH.PhaseID=dt1.PhaseID
			ORDER BY prjt.ProjStartDate DESC, dt1.DashboardID DESC, dt1.PhaseID				
			/*************************** Project Effort Phase End *****************************************************/
			
			--================================================= ***** ================================================--
			
			/*************************** Project Effort Role Start ****************************************************/
			SELECT DISTINCT dt1.DashboardID,dt1.toSort, dt1.Period, dt1.ProjectID,PRJ.ProjStartDate, dt1.ProjectName,dt1.EstimationRoleID, dt1.RoleName,                                    
			dt4.BillableHours AS BillableHours,dt4.BudgetHours AS BudgetHours, dt4.RevisedBudgetHours, cast(dt1.ActualHours as decimal(10,2)) ActualHours,dt1.RevisedComments
			FROM                                     
			(
				SELECT D.DashboardID,convert(datetime,LEFT([Name], CHARINDEX('-', [Name]) - 1 ),103) AS toSort, D.Name AS Period, A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName,                                    
				cast(sum(isnull(B.ActualHrsAdjusted,0))as decimal(10,2)) ActualHours,D.[status] AS DashboardStatus,B.RevisedComments                        
				FROM Dashboard D       
				JOIN dbo.ProjectDashboard A ON A.DashboardID= D.DashboardID
				INNER JOIN Project P ON P.ProjectID = A.ProjectID                         
				LEFT  JOIN dbo.ProjectDashboardEstimation B ON A.ProjectID=B.ProjectID AND A.DashboardID=B.DashboardID  
				AND B.PhaseID NOT IN(1,6) -- PhaseID=1 => Proposal, PhaseID=6 => Value Added Services                                   
				INNER  JOIN dbo.EstimationRole estRole ON estRole.EstimationRoleID=B.EstimationRoleID                                    
				LEFT  JOIN ProjectEstimation projest  ON projest.ProjectID=B.ProjectID  AND  B.PhaseID=projest.PhaseID                                    
				AND projest.EstimationRoleID=B.EstimationRoleID                                  
				AND projest.EstimationRoleID=estRole.EstimationRoleID                                                                  
				WHERE (P.ProgId=@ProgramID OR @ProgramID=0) AND (A.ProjectID=@ProjectID OR @ProjectID=0) AND (A.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))                                   
					  AND D.PeriodType != 'M'
				GROUP BY D.DashboardID, D.Name, A.projectID, A.ProjectName,B.EstimationRoleID,estRole.RoleName, d.[status],B.RevisedComments) AS dt1                                    
				LEFT JOIN dbo.Project PRJ ON PRJ.ProjectID = dt1.ProjectID                                    
				INNER JOIN dbo.ProjectEstimation er ON er.EstimationRoleID=dt1.EstimationRoleID                                              
				LEFT JOIN                           
				 (                                    
					SELECT dt1.DashboardID,dt1.EstimationRoleID,dt1.ProjectID ,sum(dt1.BillableHours) BillableHours ,sum(dt1.BudgetHours) BudgetHours, sum(dt1.RevisedBudgetHours) RevisedBudgetHours            
					FROM(            
							SELECT DashboardID,projest.EstimationRoleID,projest.PhaseID,projest.ProjectID ,projest.BillableHours BillableHours,
							projest.BudgetHours BudgetHours ,projest.RevisedBudgetHours RevisedBudgetHours                                    
							FROM ProjectDashBoardEstimation projest             
							WHERE (projest.DashboardID BETWEEN @StartWeekID AND @EndWeekID OR (@StartWeekID=0 AND @EndWeekID=0))           
							AND projest.PhaseID NOT IN(1,6)
							GROUP BY DashboardID, projest.EstimationRoleID, projest.PhaseID,projest.ProjectID,
									 projest.BillableHours, projest.BudgetHours, projest.RevisedBudgetHours
						) AS dt1 GROUP BY dt1.EstimationRoleID,dt1.ProjectID,dt1.DashboardID  
				  ) AS dt4 ON dt4.EstimationRoleID=dt1.EstimationRoleID AND dt4.ProjectID=dt1.ProjectID AND dt4.DashboardID = dt1.DashboardID
				  
			ORDER BY PRJ.ProjStartDate DESC, dt1.DashboardID DESC,dt1.EstimationRoleID
			/*************************** Project Effort Role End ******************************************************/
			
		END
END




