USE [SwitchgearTemperatureBD]
GO

/****** Object:  StoredProcedure [dbo].[SGSectionGetById]    Script Date: 19.06.2020 10:47:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[SGSectionGetById] 
	@SGSId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT        SGSections.*
FROM            SGSections
WHERE        (id = @SGSId)
END

GO


