﻿CREATE VIEW [dbo].[VW_PLAYER_POINTS]
	AS
SELECT
	P.CHALLONGE_USERNAME,
	ISNULL(WIN.WINS,0) CHAMPION,
	ISNULL(SECONDS.SECOND_PLACE,0) SECOND_PLACE,
	ISNULL(SEMI.SEMI_LOSS,0) SEMI_LOSS,
	(ISNULL(LOSSES.TOTAL_LOSSES,0)+ISNULL(WIN.WINS,0)) APPERANCES,
	(ISNULL(WIN.WINS,0)*100/(ISNULL(LOSSES.TOTAL_LOSSES,0)+ISNULL(WIN.WINS,0))) CHAMPION_PERCENT,
	(ISNULL(WIN.WINS,0)*12 + ISNULL(SECONDS.SECOND_PLACE,0)*5 + ISNULL(SEMI.SEMI_LOSS,0)*2 + ISNULL(LOSSES.TOTAL_LOSSES,0)) POINTS
FROM DB_PLAYER P

LEFT JOIN (
	SELECT
		P.PLAYER_ID,
		COUNT(*) WINS
	FROM DB_PLAYER P

	JOIN DB_MATCH M
	ON P.PLAYER_ID = M.WINNER_ID

	WHERE M.MATCH_RANK = 1

	GROUP BY P.PLAYER_ID) WIN 
ON P.PLAYER_ID = WIN.PLAYER_ID

LEFT JOIN (
	SELECT
		P.PLAYER_ID,
		COUNT(*) SECOND_PLACE
	FROM DB_PLAYER P

	JOIN DB_MATCH M 
	ON P.PLAYER_ID = M.LOSER_ID

	WHERE M.MATCH_RANK = 1

	GROUP BY P.PLAYER_ID) SECONDS
ON P.PLAYER_ID = SECONDS.PLAYER_ID

LEFT JOIN (
	SELECT
		P.PLAYER_ID,
		COUNT(*) SEMI_LOSS 
	FROM DB_PLAYER P

	JOIN DB_MATCH M 
	ON P.PLAYER_ID = M.LOSER_ID

	WHERE M.MATCH_RANK =2
	GROUP BY P.PLAYER_ID) SEMI 
ON SEMI.PLAYER_ID = P.PLAYER_ID

LEFT JOIN (
	SELECT
		P.PLAYER_ID,
		COUNT(DISTINCT(M.TOURNAMENT_ID)) TOTAL_LOSSES 
	FROM DB_PLAYER P

	JOIN DB_MATCH M
	ON P.PLAYER_ID = M.LOSER_ID
	GROUP BY P.PLAYER_ID) LOSSES
ON LOSSES.PLAYER_ID = P.PLAYER_ID

WHERE P.PLAYER_ID <> 16