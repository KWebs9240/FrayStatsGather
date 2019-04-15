﻿CREATE TABLE [dbo].[DB_TEAMS_USER]
(
	[USER_ID] NVARCHAR(200) NOT NULL,
	[USER_NAME] NVARCHAR(50) NOT NULL,
	[IS_TAG] BIT NOT NULL DEFAULT 0,
	[CHANNEL_ID] NVARCHAR(50) NOT NULL,

	CONSTRAINT [PK_DB_TEAMS_USER] PRIMARY KEY ([USER_ID], [CHANNEL_ID])
)
