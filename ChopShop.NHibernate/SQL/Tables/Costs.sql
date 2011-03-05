USE [ChopShop]
GO

/****** Object:  Table [dbo].[Costs]    Script Date: 03/05/2011 23:37:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Costs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[IsTaxIncluded] [bit] NOT NULL,
	[TaxRate] [decimal](18, 0) NOT NULL,
	[Currency] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_Costs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Costs] ADD  CONSTRAINT [DF_Costs_IsTaxIncluded]  DEFAULT ((0)) FOR [IsTaxIncluded]
GO

ALTER TABLE [dbo].[Costs] ADD  CONSTRAINT [DF_Costs_TaxRate]  DEFAULT ((0)) FOR [TaxRate]
GO

