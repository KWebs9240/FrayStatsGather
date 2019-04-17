﻿CREATE VIEW [dbo].[VW_WEIGHTED_WIN_LOSS_V2]
	AS
SELECT
	PLAYER_ID,
	IM_LAZY.CHALLONGE_USERNAME,
	SUM(WIN_POINTS)/50 ADJUSTED_WINS,
	SUM(LOSS_POINTS)/50 ADJUSTED_LOSSES,
	100 * SUM(WIN_POINTS) / (SUM(WIN_POINTS) + SUM(LOSS_POINTS) ) ADJUSTED_WIN_RATE
FROM (
	SELECT
		--100 * COALESCE(WINS.WINS, 0) / (COALESCE(WINS.WINS, 0) + COALESCE(LOSSES.LOSSES, 0)) WIN_PCT,
		--100 * COALESCE(LOSSES.LOSSES, 0) / (COALESCE(WINS.WINS, 0) + COALESCE(LOSSES.LOSSES, 0)) LOSS_PCT,
		--PLAYER.PLAYER_ID
		POINTSED_PLAYER.PLAYER_ID,
		POINTSED_PLAYER.CHALLONGE_USERNAME,
		CASE
			WHEN POINTSED_PLAYER.PLAYER_ID = MATCH_HIST.WINNER_ID THEN CAST(100 * COALESCE(WINS.WINS, 0) / (COALESCE(WINS.WINS, 0) + COALESCE(LOSSES.LOSSES, 0)) AS DECIMAL(10,2))
			ELSE 0
		END WIN_POINTS,
		CASE
			WHEN POINTSED_PLAYER.PLAYER_ID = MATCH_HIST.LOSER_ID THEN 100 - CAST(100 * COALESCE(WINS.WINS, 0) / (COALESCE(WINS.WINS, 0) + COALESCE(LOSSES.LOSSES, 0)) AS DECIMAL(10,2))
			ELSE 0
		END LOSS_POINTS
	FROM DB_PLAYER PLAYER

	LEFT JOIN (
		SELECT
			WINNER_ID,
			COUNT(*) WINS
		FROM dbo.DB_MATCH
		WHERE DB_MATCH.MATCH_ID IN (
				SELECT MATCH_ID 
				FROM DB_SET 
				WHERE PLAYER1_SCORE + PLAYER2_SCORE > 0)
		GROUP BY WINNER_ID) WINS
	ON WINS.WINNER_ID = PLAYER.PLAYER_ID

	LEFT JOIN (
		SELECT
			LOSER_ID,
			COUNT(*) LOSSES
		FROM dbo.DB_MATCH
		WHERE DB_MATCH.MATCH_ID IN (
				SELECT MATCH_ID 
				FROM DB_SET 
				WHERE PLAYER1_SCORE + PLAYER2_SCORE > 0)
		GROUP BY LOSER_ID) LOSSES
	ON LOSSES.LOSER_ID = PLAYER.PLAYER_ID

	JOIN dbo.DB_MATCH MATCH_HIST
	ON (MATCH_HIST.WINNER_ID = PLAYER.PLAYER_ID
		OR MATCH_HIST.LOSER_ID = PLAYER.PLAYER_ID)
	AND MATCH_HIST.MATCH_ID IN (
		SELECT MATCH_ID 
			FROM DB_SET 
			WHERE PLAYER1_SCORE + PLAYER2_SCORE > 0)

	JOIN DB_PLAYER POINTSED_PLAYER
	ON (POINTSED_PLAYER.PLAYER_ID = MATCH_HIST.WINNER_ID
		OR POINTSED_PLAYER.PLAYER_ID = MATCH_HIST.LOSER_ID)
	AND POINTSED_PLAYER.PLAYER_ID <> PLAYER.PLAYER_ID

	WHERE PLAYER.PLAYER_ID <> 16 ) IM_LAZY
GROUP BY PLAYER_ID, IM_LAZY.CHALLONGE_USERNAME
