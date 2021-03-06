IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetAllProjects') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllProjects]

GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 4/19/2018 1:02:16 PM 
Modified By: Joshwa George
Modified On: 06/06/2018
Modified By: Haritha E.S
Modified on: 20/06/2018
Modified By: Joshwa George
Modified On: 27/06/2018
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GetAllProjects]      
@month varchar(10),
@year varchar(10)
AS    
 Declare @result AS dateTime     
      
Begin      
   set @result=(SELECT CAST(@Year + '-' + @Month + '-' + '01' AS DATE));     
   select ProjectId,ProjectName from Project
   where ProjectId NOT IN (2,16)  and DATEADD(month, DATEDIFF(month, 0, @result), 0)  BETWEEN 
   DATEADD(month, DATEDIFF(month, 0, ProjStartDate), 0) AND
   DATEADD(month, DATEDIFF(month, 0, MaintClosingDate), 0) order by ProjectName; 
      
End 

GO
