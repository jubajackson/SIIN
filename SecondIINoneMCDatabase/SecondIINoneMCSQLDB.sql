USE [secondIInone]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Users1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_Users2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_Users2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_User_CreatedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[AddressOLD]'))
ALTER TABLE [dbo].[AddressOLD] DROP CONSTRAINT [FK_Address_User_CreatedBy]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_User_ModifiedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[AddressOLD]'))
ALTER TABLE [dbo].[AddressOLD] DROP CONSTRAINT [FK_Address_User_ModifiedBy]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Charter_Address]') AND parent_object_id = OBJECT_ID(N'[dbo].[Charter]'))
ALTER TABLE [dbo].[Charter] DROP CONSTRAINT [FK_Charter_Address]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Guestbook_Charter]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guestbook]'))
ALTER TABLE [dbo].[Guestbook] DROP CONSTRAINT [FK_Guestbook_Charter]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Roles_RoleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [DF_Roles_RoleId]
END

GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.Roles_Role_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] DROP CONSTRAINT [FK_dbo.RoleUsers_dbo.Roles_Role_RoleId]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.RoleUsers_dbo.Users_User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleUsers]'))
ALTER TABLE [dbo].[RoleUsers] DROP CONSTRAINT [FK_dbo.RoleUsers_dbo.Users_User_UserId]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserProfile_Charter]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserProfile]'))
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_Charter]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserProfile_OfficerPosition]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserProfile]'))
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_OfficerPosition]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserProfile_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserProfile]'))
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserProfile_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserProfile]'))
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_Users1]
GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO

/****** Object:  Table [dbo].[AddressOLD]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddressOLD]') AND type in (N'U'))
DROP TABLE [dbo].[AddressOLD]
GO

/****** Object:  Table [dbo].[Charter]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Charter]') AND type in (N'U'))
DROP TABLE [dbo].[Charter]
GO

/****** Object:  Table [dbo].[Guestbook]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guestbook]') AND type in (N'U'))
DROP TABLE [dbo].[Guestbook]
GO

/****** Object:  Table [dbo].[OfficerPosition]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OfficerPosition]') AND type in (N'U'))
DROP TABLE [dbo].[OfficerPosition]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO

/****** Object:  Table [dbo].[RoleUsers]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleUsers]') AND type in (N'U'))
DROP TABLE [dbo].[RoleUsers]
GO

/****** Object:  Table [dbo].[UserOLD]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserOLD]') AND type in (N'U'))
DROP TABLE [dbo].[UserOLD]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[UserProfile]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 08/06/2014 21:33:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 08/06/2014 21:33:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[Address1] [nvarchar](40) NOT NULL,
	[Address2] [nvarchar](40) NULL,
	[Address3] [nvarchar](40) NULL,
	[City] [nvarchar](25) NOT NULL,
	[State] [nvarchar](2) NULL,
	[Province] [nvarchar](3) NULL,
	[PostalCode] [nvarchar](10) NOT NULL,
	[Country] [char](2) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Address_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[AddressOLD]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AddressOLD](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address1] [nvarchar](255) NOT NULL,
	[Address2] [nvarchar](255) NULL,
	[Address3] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zip] [nvarchar](20) NULL,
	[CountryId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Rowstamp] [timestamp] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [secondIInone]
GO

/****** Object:  Table [dbo].[Charter]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Charter](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
	[AddressId] [int] NULL,
	[Notes] [nvarchar](max) NULL,
	[FacebookId] [nvarchar](50) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Charter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[Guestbook]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Guestbook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CharterId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[Website] [nvarchar](255) NULL,
	[Location] [nvarchar](255) NULL,
	[Club] [nvarchar](255) NULL,
	[Message] [varchar](8000) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Guestbook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[OfficerPosition]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OfficerPosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OfficerPosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[RoleUsers]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RoleUsers](
	[Role_RoleId] [uniqueidentifier] NOT NULL,
	[User_UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.RoleUsers] PRIMARY KEY CLUSTERED 
(
	[Role_RoleId] ASC,
	[User_UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[UserOLD]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserOLD](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleInitial] [nvarchar](10) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[AddressId] [int] NULL,
	[Active] [bit] NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
	[Picture] [nvarchar](255) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UserGuid] [nvarchar](40) NULL,
	[Rowstamp] [timestamp] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_User_User name has to be unique.] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CharterId] [int] NULL,
	[RidingName] [nvarchar](50) NULL,
	[OfficerPositionId] [int] NULL,
	[Birthday] [datetime] NULL,
	[Picture] [nvarchar](255) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [secondIInone]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 08/06/2014 21:33:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[IsApproved] [bit] NOT NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[LastActivityDate] [datetime] NULL,
	[LastLockoutDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[IsLockedOut] [bit] NOT NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[PasswordVerificationToken] [nvarchar](max) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Users]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Users1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Users1]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Users2] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Users2]
GO

ALTER TABLE [dbo].[AddressOLD]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_User_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserOLD] ([Id])
GO

ALTER TABLE [dbo].[AddressOLD] CHECK CONSTRAINT [FK_Address_User_CreatedBy]
GO

ALTER TABLE [dbo].[AddressOLD]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_User_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[UserOLD] ([Id])
GO

ALTER TABLE [dbo].[AddressOLD] CHECK CONSTRAINT [FK_Address_User_ModifiedBy]
GO
ALTER TABLE [dbo].[Charter]  WITH CHECK ADD  CONSTRAINT [FK_Charter_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO

ALTER TABLE [dbo].[Charter] CHECK CONSTRAINT [FK_Charter_Address]
GO

ALTER TABLE [dbo].[Guestbook]  WITH CHECK ADD  CONSTRAINT [FK_Guestbook_Charter] FOREIGN KEY([CharterId])
REFERENCES [dbo].[Charter] ([Id])
GO

ALTER TABLE [dbo].[Guestbook] CHECK CONSTRAINT [FK_Guestbook_Charter]
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_RoleId]  DEFAULT (newid()) FOR [RoleId]
GO

ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleUsers_dbo.Roles_Role_RoleId] FOREIGN KEY([Role_RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_dbo.RoleUsers_dbo.Roles_Role_RoleId]
GO

ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleUsers_dbo.Users_User_UserId] FOREIGN KEY([User_UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_dbo.RoleUsers_dbo.Users_User_UserId]
GO

ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Charter] FOREIGN KEY([CharterId])
REFERENCES [dbo].[Charter] ([Id])
GO

ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Charter]
GO

ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_OfficerPosition] FOREIGN KEY([OfficerPositionId])
REFERENCES [dbo].[OfficerPosition] ([Id])
GO

ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_OfficerPosition]
GO

ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Users]
GO

ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Users1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Users1]
GO


