IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'trg_Update_TimeTrackerHoursHistory')  AND [type]='TR')
DROP TRIGGER [dbo].[trg_Update_TimeTrackerHoursHistory]

/****** Object:  Trigger [dbo].[trg_Update_TimeTrackerHoursHistory]    Script Date: 2018-05-31 13:22:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE TRIGGER [dbo].[trg_Update_TimeTrackerHoursHistory] ON[dbo].[TimeTracker]
FOR Update  
AS

if update(ActualHours)
BEGIN
if (select count(1) from inserted)=1
	begin
		INSERT INTO TimeTrackerActualHoursHistory ([TimeTrackerId],[OldActualHours],[NewActualHours],[CreatedDate],CreatedBy)
		SELECT N.TimeTrackerId,D.ActualHours,N.ActualHours,GETDATE(),N.updatedBy 
		FROM deleted D ,inserted N 
		WHERE N.TimeTrackerId=D.TimeTrackerId 
		and D.ActualHours <> N.ActualHours
	end
ELSE
	Begin
		INSERT INTO TimeTrackerActualHoursHistory ([TimeTrackerId],[OldActualHours],[NewActualHours],[CreatedDate],CreatedBy)
		SELECT N.TimeTrackerId,D.ActualHours,N.ActualHours,GETDATE(),N.updatedBy 
		FROM deleted D ,inserted N 
		WHERE N.TimeTrackerId=D.TimeTrackerId 
		and D.ActualHours <> N.ActualHours 
		and d.ActualHours <> 0;
	end
END



GO


