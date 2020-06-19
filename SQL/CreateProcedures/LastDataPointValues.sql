USE [SwitchgearTemperatureBD]
GO

/****** Object:  StoredProcedure [dbo].[LastDataPointValues]    Script Date: 19.06.2020 10:47:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LastDataPointValues] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT        SGSPointData.*
FROM            SGSPoints INNER JOIN
                         SGSPointData ON SGSPoints.Id = SGSPointData.IdSGSPoint AND SGSPoints.LastDTSafeValue = SGSPointData.DateOfValue
END

GO


