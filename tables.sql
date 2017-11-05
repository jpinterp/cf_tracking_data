USE [cf_tracking]
GO

/****** Object:  Table [dbo].[timing]    Script Date: 11/5/2017 7:14:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[timing](
	[bib] [nchar](10) NOT NULL,
	[start_time] [datetime] NULL,
	[senior_time] [datetime] NULL,
	[legacy_time] [datetime] NULL,
	[finish_time] [datetime] NULL,
 CONSTRAINT [PK_timing] PRIMARY KEY CLUSTERED 
(
	[bib] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


