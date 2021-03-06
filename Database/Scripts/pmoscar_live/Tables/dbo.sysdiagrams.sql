if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sysdiagrams]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[sysdiagrams]
GO

CREATE TABLE [dbo].[sysdiagrams] (
	[name] [sysname] NOT NULL ,
	[principal_id] [int] NOT NULL ,
	[diagram_id] [int] IDENTITY (1, 1) NOT NULL ,
	[version] [int] NULL ,
	[definition] [varbinary] (MAX) NULL ,
	 CONSTRAINT [PK__sysdiagrams__47DBAE45] PRIMARY KEY  CLUSTERED 
	(
		[diagram_id]
	)  ON [PRIMARY] ,
	CONSTRAINT [UK_principal_name] UNIQUE  NONCLUSTERED 
	(
		[principal_id],
		[name]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


exec sp_addextendedproperty N'microsoft_database_tools_support', 1, N'user', N'dbo', N'table', N'sysdiagrams'


GO


