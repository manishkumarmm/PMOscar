alter proc GetProductDevelopmentHours_PowerBI
as 
begin
/*
Created By : Aswathy R
Created On : 2018-10-23
Purpose : To get product developement hours for Power BI Company utilization report
*/


--Keep total Project Estimation Billable hours to a tem table
--select pe.ProjectID
--		,sum(pe.BillableHours) BillableHours   
--	into #Estimation
--from ProjectEstimation pe
--join Project p on p.ProjectId =pe.ProjectID 
--join WorkOrder w on w.WorkOrderID =p.WorkOrderID 
--where 
--w.WorkOrderName='NITS/INT/PRO'
----pe.ProjectID =@ProjectID
--group by pe.ProjectID

----Keep all Actual hours data to a temp table 
--select 
--				P.ProjectID
--				,p.ProjectName
--				,Month(t.FromDate) [Month]
--				,year(t.FromDate) [Year]
--				,convert(date,convert(varchar,year(t.FromDate))+'-'+convert(varchar,Month(t.FromDate))+'-01') MonthDate
--				,sum(t.ActualHours) ActualHours into #Time
--		from TimeTracker t 
--		join project p on p.ProjectId=t.ProjectID
--		join WorkOrder w on w.WorkOrderID=p.WorkOrderID
--		where 
--		w.WorkOrderName='NITS/INT/PRO'
--		--p.ProjectID =@ProjectID
--		group by 
--				P.ProjectID	
--				,Month(t.FromDate) 
--				,year(t.FromDate) 
--				,p.ProjectName



--select
--	 t.ProjectID
--	,t.[Month]
--	,t.[Year]
--	,t.ActualHours ActualSpent
--	,t2.ActualHours TotalActualHourstillMonth
--	,e.BillableHours
--	,t2.ActualHours-e.BillableHours HoursDifference

--,	case 
--	  when t2.ActualHours -e.BillableHours > t.ActualHours 
--	  then 0
--	  else case 
--				when t2.ActualHours >BillableHours 
--				then BillableHours 
--				else 0
--			end
--	end ProductHours

--from #Time  t
--left join  #Estimation e on t.ProjectId=e.ProjectID
--outer apply (
--				select 
--						sum(ActualHours) ActualHours
--				from  #Time t1
--				where t1.ProjectId=t.projectid
--						and t1.[MonthDate]<=t.[MonthDate]
--			)t2

--IF OBJECT_ID('tempdb..#Estimation') IS NOT NULL 
--BEGIN 
--    DROP TABLE #Estimation 
--END

--IF OBJECT_ID('tempdb..#Time') IS NOT NULL 
--BEGIN 
--    DROP TABLE #Time 
--END
   

   
select
	 B.ProjectID
	,month(B.fromDate)  [Month]
	,year(B.fromDate) [Year]
	,sum(B.ActualSpentHours) ActualSpent
	--,0 TotalActualHourstillMonth
	--,0 BillableHours
	--,0 HoursDifference
	,sum(B.ActualHours) ProductHours

from BillingDetails B

join project p on p.ProjectId=B.ProjectID
		join WorkOrder w on w.WorkOrderID=p.WorkOrderID
		where 
		w.WorkOrderName='NITS/INT/PRO'
		--p.ProjectID =@ProjectID
		group by 
				
				Month(B.FromDate) 
				,year(B.FromDate) 
				 ,B.ProjectID

END