USE [SwitchgearTemperatureBD]
GO

/****** Object:  Table [dbo].[SGSPoints]    Script Date: 19.06.2020 10:44:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SGSPoints](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdSGSection] [int] NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[LastDTSafeValue] [datetime] NULL CONSTRAINT [DF_SGSPoints_LastDTSafeValue]  DEFAULT (getdate()),
	[Explantation] [nvarchar](max) NULL,
 CONSTRAINT [PK_SGSPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[SGSPoints]  WITH CHECK ADD  CONSTRAINT [FK_SGSPoints_SGSections] FOREIGN KEY([IdSGSection])
REFERENCES [dbo].[SGSections] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[SGSPoints] CHECK CONSTRAINT [FK_SGSPoints_SGSections]
GO


