IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Measurement]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[Measurement]
END
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Statistic]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[Statistic]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statistic](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](64) NULL,
 CONSTRAINT [PK_Statistic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measurement](
	[Id] [int] NOT NULL,
	[StatisticId] [int] NULL,
	[Developer] [nvarchar] (64),
	[PreviousScore] [float] NULL,
	[CurrentScore] [float] NULL,
 CONSTRAINT [PK_Measurement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Measurement_Statistic]    Script Date: 04/09/2007 22:35:59 ******/
ALTER TABLE [dbo].[Measurement]  WITH CHECK ADD  CONSTRAINT [FK_Measurement_Statistic] FOREIGN KEY([StatisticId])
REFERENCES [dbo].[Statistic] ([Id])
GO
ALTER TABLE [dbo].[Measurement] CHECK CONSTRAINT [FK_Measurement_Statistic]
GO

INSERT Statistic SELECT 1, 'LOC per bug'
INSERT Statistic SELECT 2, 'Simplicity Index'
INSERT Statistic SELECT 3, 'Awe Factor'
GO

INSERT Measurement SELECT 1, 1, 'Haacked', 9, 12
INSERT Measurement SELECT 2, 1, 'JG', 0, 1
INSERT Measurement SELECT 3, 1, 'RC', 4, 2
INSERT Measurement SELECT 4, 1, 'JA', 7, 15
INSERT Measurement SELECT 5, 2, 'Haacked', 17, 16
INSERT Measurement SELECT 6, 2, 'JG', 1, 2
INSERT Measurement SELECT 7, 3, 'Haacked', 1, 0
INSERT Measurement SELECT 8, 3, 'JG', 7, 4
INSERT Measurement SELECT 9, 3, 'SH', 5, 3
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Statistics_GetForDeveloper]') AND type in (N'P'))
BEGIN
	DROP PROC [dbo].[Statistics_GetForDeveloper]
END
GO

GO
CREATE PROC [dbo].[Statistics_GetForDeveloper](
	@Developer nvarchar(64)
)
AS
WITH MeasurementCount(StatisticId, MeasurementCount) AS
(
	SELECT s.Id
		,MeasurementCount = COUNT(1)
	FROM Statistic s
		LEFT OUTER JOIN Measurement m ON m.StatisticId = s.Id
	GROUP BY s.Id
)
SELECT 
	Statistic = s.Title
	, Developer
	, CurrentScore
	, PreviousScore
	, mc.MeasurementCount
	, TrendFactor = (CurrentScore - PreviousScore)/mc.MeasurementCount
FROM Statistic s
	INNER JOIN MeasurementCount mc ON mc.StatisticId = s.Id
	LEFT OUTER JOIN Measurement m ON m.StatisticID = s.Id
WHERE Developer = @Developer
GO

Statistics_GetForDeveloper 'Haacked'
GO