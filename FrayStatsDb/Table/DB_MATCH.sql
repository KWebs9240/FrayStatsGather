﻿CREATE TABLE [dbo].[DB_MATCH]
(
	[MATCH_ID] BIGINT NOT NULL PRIMARY KEY, 
    [WINNER_ID] BIGINT NULL, 
    [LOSER_ID] BIGINT NULL, 
    [PLAYER1_ID] BIGINT NULL, 
    [PLAYER2_ID] BIGINT NULL, 
    [TOURNAMENT_ID] BIGINT NOT NULL,
	[MATCH_RANK] INT NOT NULL

    CONSTRAINT [FK_MATCH_TOURNAMENT_1] FOREIGN KEY ([TOURNAMENT_ID]) REFERENCES [DB_TOURNAMENT]([TOURNAMENT_ID]), 
    CONSTRAINT [FK_MATCH_PLAYER_1] FOREIGN KEY ([WINNER_ID]) REFERENCES [DB_PLAYER]([PLAYER_ID]),
	CONSTRAINT [FK_MATCH_PLAYER_2] FOREIGN KEY ([LOSER_ID]) REFERENCES [DB_PLAYER]([PLAYER_ID]),
	CONSTRAINT [FK_MATCH_PLAYER_3] FOREIGN KEY ([PLAYER1_ID]) REFERENCES [DB_PLAYER]([PLAYER_ID]),
	CONSTRAINT [FK_MATCH_PLAYER_4] FOREIGN KEY ([PLAYER2_ID]) REFERENCES [DB_PLAYER]([PLAYER_ID])
)
