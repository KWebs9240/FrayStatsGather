﻿CREATE TABLE [dbo].[TOURNAMENT]
(
	[TOURNAMENT_ID] [NUMERIC](18, 0) PRIMARY KEY,
	[TOURNAMENT_NAME] [NVARCHAR](50) NULL,

	[LAST_CHECK_DATE] [DATETIME2](7) NOT NULL
)

GO