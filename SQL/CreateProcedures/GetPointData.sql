USE [SwitchgearTemperatureBD]
GO

/****** Object:  StoredProcedure [dbo].[GetPointData]    Script Date: 19.06.2020 10:47:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPointData] 
	@Point xml =null,
	@Points xml =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @IDP int, @DateFrom datetime, @DateTo datetime
    -- Insert statements for procedure here
	if(NOT (@Point IS Null))
		begin	
			select  @IDP = Tbl.Col.value('Id[1]', 'int') ,
					@DateFrom = Tbl.Col.value('DateFrom[1]','DateTime'),
					@DateTo =Tbl.Col.value('DateTo[1]','DateTime') 
			from @Point.nodes('/OnePointData') Tbl(Col) ; 			
		if(Not(@DateFrom is null))	
			if(Not (@DateTo is null))
				SELECT        Id, IdSGSPoint, DateOfValue, PointValue, Explantation
			FROM            SGSPointData
			WHERE        (IdSGSPoint = @IDP) AND (DateOfValue BETWEEN @DateFrom AND @DateTo)
			Order by DateOfValue
			else
				SELECT        Id, IdSGSPoint, DateOfValue, PointValue, Explantation
			FROM            SGSPointData
			WHERE        (IdSGSPoint = @IDP) AND (DateOfValue BETWEEN @DateFrom AND GETDATE())
			Order by DateOfValue
		else
			SELECT        Id, IdSGSPoint, DateOfValue, PointValue, Explantation
		FROM            SGSPointData
		WHERE        (IdSGSPoint = @IDP) AND (DateOfValue BETWEEN DATEADD(month,-3,GETDATE()) AND GETDATE())
		Order by DateOfValue
		end
	else 
	if(NOT(@Points is null))
	begin
	select  @DateFrom = Tbld.Col.value('DateFrom[1]','DateTime'), 
			@DateTo =Tbld.Col.value('DateTo[1]','DateTime')
	from @Points.nodes('/ManyPointsData') Tbld(Col) ; 
	if(Not(@DateFrom is null))
		begin 
			if(Not(@DateTo is null))
			SELECT        Id, IdSGSPoint, DateOfValue, PointValue, Explantation
		FROM            SGSPointData
		WHERE        (IdSGSPoint in (select  Tbl.Col.value('text()[1]', 'int') as Id
	from @Points.nodes('/ManyPointsData/PointsId') Tbl(Col))) AND (DateOfValue BETWEEN @DateFrom AND @DateTo)
		Order by DateOfValue 
			else
			SELECT        Id, IdSGSPoint, DateOfValue, PointValue, Explantation
		FROM            SGSPointData
		WHERE        (IdSGSPoint in (select  Tbl.Col.value('text()[1]', 'int') as Id
	from @Points.nodes('/ManyPointsData/PointsId') Tbl(Col))) AND (DateOfValue BETWEEN @DateFrom AND GETDATE())
		Order by DateOfValue 
		end
		else
		SELECT        Id, IdSGSPoint, DateOfValue, PointValue, Explantation
		FROM            SGSPointData
		WHERE        (IdSGSPoint in (select  Tbl.Col.value('text()[1]', 'int') as Id
	from @Points.nodes('/ManyPointsData/PointsId') Tbl(Col))) AND (DateOfValue BETWEEN DATEADD(month,-3,GETDATE()) AND GETDATE())
		Order by DateOfValue  	
	end
	else
	Print 'All null';
	

END

GO


