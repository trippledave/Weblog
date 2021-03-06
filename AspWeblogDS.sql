USE [master]
GO
/****** Object:  Database [AspWeblogDS]    Script Date: 29.06.2014 21:05:58 ******/
CREATE DATABASE [AspWeblogDS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AspWeblogDS', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AspWeblogDS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AspWeblogDS_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AspWeblogDS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AspWeblogDS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AspWeblogDS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AspWeblogDS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AspWeblogDS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AspWeblogDS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AspWeblogDS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AspWeblogDS] SET ARITHABORT OFF 
GO
ALTER DATABASE [AspWeblogDS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AspWeblogDS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AspWeblogDS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AspWeblogDS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AspWeblogDS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AspWeblogDS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AspWeblogDS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AspWeblogDS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AspWeblogDS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AspWeblogDS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AspWeblogDS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AspWeblogDS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AspWeblogDS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AspWeblogDS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AspWeblogDS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AspWeblogDS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AspWeblogDS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AspWeblogDS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AspWeblogDS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AspWeblogDS] SET  MULTI_USER 
GO
ALTER DATABASE [AspWeblogDS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AspWeblogDS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AspWeblogDS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AspWeblogDS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [AspWeblogDS]
GO
/****** Object:  Table [dbo].[AdministratorSettings]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdministratorSettings](
	[SiteName] [nvarchar](25) NOT NULL,
	[AllowComments] [bit] NOT NULL,
	[WelcomeMailSubject] [nvarchar](50) NULL,
	[WelcomeMailText] [nvarchar](max) NULL,
	[PasswordChangeMailSubject] [nvarchar](50) NULL,
	[PasswordChangeMailText] [nvarchar](max) NULL,
	[OptInMailSubject] [nvarchar](50) NULL,
	[OptInMailText] [nvarchar](max) NULL,
	[SiteFooterText] [nvarchar](max) NOT NULL,
	[SiteKeywords] [nvarchar](max) NOT NULL,
	[SmtpServer] [nvarchar](50) NOT NULL,
	[SmtpUser] [nvarchar](50) NULL,
	[SmtpPassword] [nvarchar](50) NULL,
	[SmtpRegisterAtServerNeeded] [bit] NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[EntriesPerSite] [int] NOT NULL,
	[FullEntriesPerSite] [int] NOT NULL,
 CONSTRAINT [PK_AdministratorSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoriesToEntries]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesToEntries](
	[CategoryID] [int] NOT NULL,
	[EntryID] [int] NOT NULL,
 CONSTRAINT [PK_CategoriesToEntries] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC,
	[EntryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[AuthorID] [int] NOT NULL,
	[EntryID] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entries]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entries](
	[EntryID] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[AuthorID] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Entries] PRIMARY KEY CLUSTERED 
(
	[EntryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
	[UserNameLowercase] [nvarchar](56) NOT NULL,
	[Email] [nvarchar](56) NOT NULL,
	[EmailLowercase] [nvarchar](56) NOT NULL,
	[IsLockedByAdmin] [bit] NOT NULL,
	[DisplayName] [nvarchar](56) NULL,
 CONSTRAINT [PK__Users__1788CCAC2FA547C1] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__C9F28456E976C9E5] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 29.06.2014 21:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AdministratorSettings] ADD  CONSTRAINT [DF_AdministratorSettings_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
ALTER TABLE [dbo].[CategoriesToEntries]  WITH CHECK ADD  CONSTRAINT [FK_CategoriesToEntries_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[CategoriesToEntries] CHECK CONSTRAINT [FK_CategoriesToEntries_Categories]
GO
ALTER TABLE [dbo].[CategoriesToEntries]  WITH CHECK ADD  CONSTRAINT [FK_CategoriesToEntries_Entries] FOREIGN KEY([EntryID])
REFERENCES [dbo].[Entries] ([EntryID])
GO
ALTER TABLE [dbo].[CategoriesToEntries] CHECK CONSTRAINT [FK_CategoriesToEntries_Entries]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Entries] FOREIGN KEY([EntryID])
REFERENCES [dbo].[Entries] ([EntryID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Entries]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[Entries]  WITH CHECK ADD  CONSTRAINT [FK_Entries_Users] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Entries] CHECK CONSTRAINT [FK_Entries_Users]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO
USE [master]
GO
ALTER DATABASE [AspWeblogDS] SET  READ_WRITE 
GO
