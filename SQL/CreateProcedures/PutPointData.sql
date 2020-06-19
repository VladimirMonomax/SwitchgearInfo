USE [SwitchgearTemperatureBD]
GO

/****** Object:  StoredProcedure [dbo].[PutPointData]    Script Date: 19.06.2020 10:47:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PutPointData] 
	@SGSPDPut xml
AS
BEGIN
		SET NOCOUNT ON;
		Declare @number int =1,
		@id bigint,
		@idSGSPoint bigint,
		@dateOfValue datetime,
		@pointValue float,
		@expl  nvarchar(100),
		@BdId bigint,
		@res xml,
		@xCom xml

		set @res =N'<Root>
</Root>'
		Declare XML_Cursor Cursor FOR
	SELECT Tbl.Col.value('Id[1]', 'bigint') as Id,
		   Tbl.Col.value('IdSGSPoint[1]', 'bigint') As IdSGSPoint,
		   Tbl.Col.value('DateOfValue[1]', 'datetime') as DateOfValue,
		   Tbl.Col.value('PointValue[1]', 'float') as PointValue,
		   Tbl.Col.value('Explantation[1]', 'nvarchar(100)') as Explantation
	from @SGSPDPut.nodes('/SGSPDPut/DataPoints/SGSPointData') Tbl(Col) 

	 Open XML_Cursor
	 FETCH NEXT FROM XML_Cursor   
	 INTO @id, @idSGSPoint, @dateOfValue, @pointValue, @expl
	 WHILE @@FETCH_STATUS = 0  
	 BEGIN
	 set @BdId=null;
		SELECT    @BdId = Id
		FROM          SGSPointData
		WHERE        (DateOfValue = @dateOfValue) AND (IdSGSPoint =  @idSGSPoint)
		
		if(@BdId is not null)
		begin
		Declare @pv float
		Select  @pv =PointValue 
		FROM          SGSPointData
		WHERE        Id = @BdId
		if(@pv<> @pointValue)
		begin
			UPDATE       SGSPointData
			SET                PointValue = @pointValue
			WHERE        (Id = @BdId)
			set @xCom = CONCAT ('
			 <Messadge>
			<MNumber>', @number ,'</MNumber>
			<MessadgeString>Модель существует в БД, значение обновлено.</MessadgeString>
			</Messadge>   					
		');
		SET @res.modify('
			insert sql:variable("@xCom") as last into (/Root)[1]
		');
		end
		else
		begin
		set @xCom = CONCAT ('
			 <Messadge>
			<MNumber>', @number ,'</MNumber>
			<MessadgeString>Модель существует в БД, данные не обновлены.</MessadgeString>
			</Messadge>   					
		');
		SET @res.modify('
			insert sql:variable("@xCom") as last into (/Root)[1]
		');
				
		end
		end
		else
		begin
			INSERT INTO SGSPointData
				           (IdSGSPoint, DateOfValue, PointValue, Explantation)
			VALUES         (@idSGSPoint, @dateOfValue, @pointValue, @expl)
			set @xCom = CONCAT ('
			<Messadge>
			<MNumber>', @number,'</MNumber>
			<MessadgeString>Модель успешно добавлена в БД.</MessadgeString>
			</Messadge>
		');
			SET @res.modify('
			insert sql:variable("@xCom") as last into (/Root)[1]
		');			
		end
		
		set @number=@number+1;
		FETCH NEXT FROM XML_Cursor   
	    INTO @id, @idSGSPoint, @dateOfValue, @pointValue, @expl
	 END
	 close XML_Cursor
	 deallocate XML_Cursor
	
	 Select Tbl.Col.value('MNumber[1]', 'int') as MNumber,
		    Tbl.Col.value('MessadgeString[1]', 'nvarchar(100)') As MessadgeString
	 from @res.nodes('/Root/Messadge') Tbl(Col)
END

GO


