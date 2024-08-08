
GO


CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](500) NULL,
	[Mobile] [nvarchar](50) NULL,
	[DateOfBirth] [nvarchar](15) NULL,
	[Gender] [varchar](1) NULL,
	[Password] [nvarchar](50) NULL,
	[PasswordSalt] [nvarchar](50) NULL,
	[IsApprove] [bit] NULL,
	[IsLockedOut] [bit] NULL,
	[LastLockedOutOn] [datetime] NULL,
	[FailedpasswordAttemptCount] [int] NULL,
	[LastModifiedBy] [bigint] NULL,
	[CreatedBy] [bigint] NULL,
	[ModifiedByRole] [int] NULL,
	[CreatedByRole] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsActive]
GO

 

CREATE TABLE [dbo].[Companies](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Exchange] [varchar](50) NOT NULL,
	[Ticker] [varchar](10) NOT NULL,
	[Isin] [varchar](50) NOT NULL,
	[WebSite] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_Companies_Isin] UNIQUE NONCLUSTERED 
(
	[Isin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Companies] ADD  CONSTRAINT [DF_Companies_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO


GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Mobile], [DateOfBirth], [Gender], [Password], [PasswordSalt], [IsApprove], [IsLockedOut], [LastLockedOutOn], [FailedpasswordAttemptCount], [LastModifiedBy], [CreatedBy], [ModifiedByRole], [CreatedByRole], [CreatedDate], [ModifiedDate], [IsDeleted], [IsActive])
VALUES (4, N'Rupesh', N'Pandey', N'test@gmail.com', N'894759070', N'01/08/1980', N'M', N'test@90', N'pass', 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO

