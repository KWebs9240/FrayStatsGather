﻿CREATE TABLE [dbo].[DB_PLAYER]
(
	[PLAYER_ID] BIGINT NOT NULL PRIMARY KEY DEFAULT NEXT VALUE FOR SEQ_PLAYER_ID, 
    [CHALLONGE_USERNAME] NVARCHAR(50) NULL, 
    [NAME] NVARCHAR(50) NULL
)