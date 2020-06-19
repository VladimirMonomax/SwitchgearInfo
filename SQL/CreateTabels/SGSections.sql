USE [SwitchgearTemperatureBD]
GO

/****** Object:  Table [dbo].[SGSections]    Script Date: 19.06.2020 10:44:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SGSections](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IdSwitchgear] [int] NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Explantation] [nvarchar](max) NULL,
 CONSTRAINT [PK_SGSections] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[SGSections]  WITH CHECK ADD  CONSTRAINT [FK_SGSections_Switchgears] FOREIGN KEY([IdSwitchgear])
REFERENCES [dbo].[Switchgears] ([id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[SGSections] CHECK CONSTRAINT [FK_SGSections_Switchgears]
GO


