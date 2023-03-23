
CREATE TABLE [dbo].[test](
	[id] [uniqueidentifier] NOT NULL,
	[cartid] [uniqueidentifier] NULL,
	[productid] [uniqueidentifier] NULL,
	[quantity] [decimal](10, 3) NULL,
	[createdAt] [datetimeoffset](7) NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO