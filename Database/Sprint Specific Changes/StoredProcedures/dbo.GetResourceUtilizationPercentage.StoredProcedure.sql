IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetResourceUtilizationPercentage') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetResourceUtilizationPercentage]

/****** Object:  StoredProcedure [dbo].[.GetResourceUtilizationPercentage]    Script Date: 4/19/2018 1:02:16 PM ******/

-- =============================================  
-- Author:  Vibin MB
-- Create date: 03/08/2018 
-- Description: Sp to select Utilization Percentage
-- =============================================  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
                   
CREATE PROCEDURE [dbo].[GetResourceUtilizationPercentage] 
@ResourceID int
AS
BEGIN
		select Convert(varchar(10),CONVERT(date, StartDate,106),103),Convert(varchar(10),CONVERT(date, EndDate,106),103),UtilizationPercentage 
		from ResourceUtilizationPercentage 
		where ResourceID=@ResourceID
END
GO