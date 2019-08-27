if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Role_Bak_20110323]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Role_Bak_20110323]
GO

CREATE TABLE [dbo].[Role_Bak_20110323] (
	[RoleId] [int] IDENTITY (1, 1) NOT NULL ,
	[Role] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ShortName] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO


