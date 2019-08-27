IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetAdjustmentFactor') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAdjustmentFactor]

GO
/****** Object:  StoredProcedure [dbo].[GetAdjustmentFactor]    Script Date: 08/3/2018 1:02:16 PM 
Created By: Kochurani Kuriakose
Created On: 08/03/2018
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GetAdjustmentFactor]      
@month varchar(10),
@year varchar(10)
AS    
 Declare @result AS dateTime     
      
Begin      
   set @result=(SELECT CAST(@Year + '-' + @Month + '-' + '01' AS DATE));     
   SELECT *
   FROM (  
		   select distinct 
		   ru.ResourceUtilizationPercentageID,
		   r.ResourceName,
		   ru.UtilizationPercentage,
		   ru.AdjustmentFactor,
		   1 as hidetextbox,
		   0 as showtextbox, ROW_NUMBER() OVER (PARTITION BY r.ResourceName ORDER BY  abs(ru.AdjustmentFactor) DESC,resourceutilizationpercentageid asc) AS ROW_ID
		   from ResourceUtilizationPercentage ru 
		   join resource r on r.ResourceId=ru.ResourceID
		   where @result<=r.ExitDate and DATEADD(month, DATEDIFF(month, 0, @result), 0)  BETWEEN 
		   DATEADD(month, DATEDIFF(month, 0, StartDate), 0) AND
		   DATEADD(month, DATEDIFF(month, 0, EndDate), 0)				
		   
		   )resultset
	WHERE ROW_ID = 1
	order by ResourceName
      
End 

GO
