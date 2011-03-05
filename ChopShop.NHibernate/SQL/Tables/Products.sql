USE [ChopShop]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 03/05/2011 23:37:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Sku] [nvarchar](100) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CostId] [int] NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO

