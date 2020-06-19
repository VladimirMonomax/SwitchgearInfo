USE [SwitchgearTemperatureBD]
GO

/****** Object:  StoredProcedure [dbo].[SGSectionsGetByIDSG]    Script Date: 19.06.2020 10:47:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SGSectionsGetByIDSG]
	@IDSG int
AS
BEGIN
	
	SET NOCOUNT ON;

Select *
	From SGSections
	Where (IdSwitchgear=@IDSG)
END

GO


