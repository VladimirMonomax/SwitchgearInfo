USE [SwitchgearTemperatureBD]
GO

/****** Object:  Table [dbo].[Switchgears]    Script Date: 19.06.2020 10:44:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Switchgears](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[NameSw] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Explantation] [nvarchar](max) NULL,
 CONSTRAINT [PK_Switchgears] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


