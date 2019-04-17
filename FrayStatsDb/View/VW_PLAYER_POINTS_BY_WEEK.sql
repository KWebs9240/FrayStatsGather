﻿CREATE VIEW [dbo].[VW_PLAYER_POINTS_BY_WEEK]
	AS
SELECT
	*
FROM (
	SELECT
		POINTED.PLAYER_ID_POINT,
		RN,
		SUM(POINTED.POINTS) CUR_POINTS
	FROM (
		SELECT
			MAT.LOSER_ID AS PLAYER_ID_POINT,
			CASE
				WHEN MAT.MATCH_RANK = 1 THEN 6
				WHEN MAT.MATCH_RANK = 2 THEN 3
				WHEN MAT.MATCH_RANK = 100 THEN 0
				ELSE 1
			END AS POINTS,
			TOUR.RN
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY TOURNAMENT_ID) RN,
				*
			FROM dbo.DB_TOURNAMENT) TOUR

		JOIN DB_MATCH MAT
		ON MAT.TOURNAMENT_ID <= TOUR.TOURNAMENT_ID
		
		UNION ALL
		
		SELECT
			MAT.WINNER_ID AS PLAYER_ID_POINT,
			12 AS POINTS,
			TOUR.RN
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY TOURNAMENT_ID) RN,
				*
			FROM dbo.DB_TOURNAMENT) TOUR

		JOIN DB_MATCH MAT
		ON MAT.TOURNAMENT_ID <= TOUR.TOURNAMENT_ID
		AND MAT.MATCH_RANK = 1) POINTED
	GROUP BY RN, POINTED.PLAYER_ID_POINT ) GROUPED_POINTS

JOIN DB_PLAYER PLAYER
ON PLAYER.PLAYER_ID = GROUPED_POINTS.PLAYER_ID_POINT