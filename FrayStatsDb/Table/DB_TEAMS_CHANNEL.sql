﻿CREATE TABLE [dbo].[DB_TEAMS_CHANNEL]
(
	[TEAM_ID] NVARCHAR(50) NOT NULL,
	[CHANNEL_ID] NVARCHAR(50) NOT NULL,
	[CHANNEL_NAME] NVARCHAR(50) NULL,
	[IS_POST] BIT NOT NULL DEFAULT 0,

	CONSTRAINT [PK_DB_TEAMS_CHANNEL] PRIMARY KEY ([CHANNEL_ID], [TEAM_ID])
)
