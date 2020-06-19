USE [SwitchgearTemperatureBD]
GO

/****** Object:  Table [dbo].[SGSPointData]    Script Date: 19.06.2020 10:44:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SGSPointData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdSGSPoint] [bigint] NOT NULL,
	[DateOfValue] [datetime] NOT NULL CONSTRAINT [DF_SGSPointData_DateOfValue]  DEFAULT (getdate()),
	[PointValue] [float] NOT NULL,
	[Explantation] [nvarchar](100) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SGSPointData]  WITH CHECK ADD  CONSTRAINT [FK_SGSPointData_SGSPoints] FOREIGN KEY([IdSGSPoint])
REFERENCES [dbo].[SGSPoints] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[SGSPointData] CHECK CONSTRAINT [FK_SGSPointData_SGSPoints]
GO


