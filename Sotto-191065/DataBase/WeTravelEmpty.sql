USE [master]
GO
/****** Object:  Database [WeTravel]    Script Date: 15-Oct-20 3:43:51 AM ******/
CREATE DATABASE [WeTravel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WeTravel', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\WeTravel.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WeTravel_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\WeTravel_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WeTravel] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WeTravel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WeTravel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WeTravel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WeTravel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WeTravel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WeTravel] SET ARITHABORT OFF 
GO
ALTER DATABASE [WeTravel] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WeTravel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WeTravel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WeTravel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WeTravel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WeTravel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WeTravel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WeTravel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WeTravel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WeTravel] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WeTravel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WeTravel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WeTravel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WeTravel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WeTravel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WeTravel] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [WeTravel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WeTravel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WeTravel] SET  MULTI_USER 
GO
ALTER DATABASE [WeTravel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WeTravel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WeTravel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WeTravel] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [WeTravel] SET DELAYED_DURABILITY = DISABLED 
GO
USE [WeTravel]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Categories_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lodgings]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lodgings](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Stars] [int] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Images] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[PricePerNight] [int] NOT NULL,
	[Available] [bit] NOT NULL,
	[Telephone] [nvarchar](max) NULL,
	[InformationText] [nvarchar](max) NULL,
	[TuristLocationId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Lodgings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReserveDescriptions]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReserveDescriptions](
	[ReserveId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_ReserveDescriptions] PRIMARY KEY CLUSTERED 
(
	[ReserveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserves]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserves](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckIn] [datetime2](7) NOT NULL,
	[CheckOut] [datetime2](7) NOT NULL,
	[Adults] [int] NOT NULL,
	[Children] [int] NOT NULL,
	[Babies] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Telephone] [nvarchar](max) NOT NULL,
	[InformationText] [nvarchar](max) NULL,
	[ContactFirstName] [nvarchar](max) NOT NULL,
	[ContactLastName] [nvarchar](max) NOT NULL,
	[ContactEmail] [nvarchar](max) NOT NULL,
	[LodgingId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Reserves] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Token] [uniqueidentifier] NOT NULL,
	[UserEmail] [nvarchar](450) NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TuristLocationCategories]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuristLocationCategories](
	[CategoryId] [uniqueidentifier] NOT NULL,
	[TuristLocationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TuristLocationCategories] PRIMARY KEY CLUSTERED 
(
	[TuristLocationId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TuristLocations]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuristLocations](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[RegionId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TuristLocations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15-Oct-20 3:43:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Email] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Lodgings_TuristLocationId]    Script Date: 15-Oct-20 3:43:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_Lodgings_TuristLocationId] ON [dbo].[Lodgings]
(
	[TuristLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reserves_LodgingId]    Script Date: 15-Oct-20 3:43:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_Reserves_LodgingId] ON [dbo].[Reserves]
(
	[LodgingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Sessions_UserEmail]    Script Date: 15-Oct-20 3:43:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_UserEmail] ON [dbo].[Sessions]
(
	[UserEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TuristLocationCategories_CategoryId]    Script Date: 15-Oct-20 3:43:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_TuristLocationCategories_CategoryId] ON [dbo].[TuristLocationCategories]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TuristLocations_RegionId]    Script Date: 15-Oct-20 3:43:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_TuristLocations_RegionId] ON [dbo].[TuristLocations]
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lodgings]  WITH CHECK ADD  CONSTRAINT [FK_Lodgings_TuristLocations_TuristLocationId] FOREIGN KEY([TuristLocationId])
REFERENCES [dbo].[TuristLocations] ([Id])
GO
ALTER TABLE [dbo].[Lodgings] CHECK CONSTRAINT [FK_Lodgings_TuristLocations_TuristLocationId]
GO
ALTER TABLE [dbo].[ReserveDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_ReserveDescriptions_Reserves_ReserveId] FOREIGN KEY([ReserveId])
REFERENCES [dbo].[Reserves] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReserveDescriptions] CHECK CONSTRAINT [FK_ReserveDescriptions_Reserves_ReserveId]
GO
ALTER TABLE [dbo].[Reserves]  WITH CHECK ADD  CONSTRAINT [FK_Reserves_Lodgings_LodgingId] FOREIGN KEY([LodgingId])
REFERENCES [dbo].[Lodgings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reserves] CHECK CONSTRAINT [FK_Reserves_Lodgings_LodgingId]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Users_UserEmail] FOREIGN KEY([UserEmail])
REFERENCES [dbo].[Users] ([Email])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Users_UserEmail]
GO
ALTER TABLE [dbo].[TuristLocationCategories]  WITH CHECK ADD  CONSTRAINT [FK_TuristLocationCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TuristLocationCategories] CHECK CONSTRAINT [FK_TuristLocationCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[TuristLocationCategories]  WITH CHECK ADD  CONSTRAINT [FK_TuristLocationCategories_TuristLocations_TuristLocationId] FOREIGN KEY([TuristLocationId])
REFERENCES [dbo].[TuristLocations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TuristLocationCategories] CHECK CONSTRAINT [FK_TuristLocationCategories_TuristLocations_TuristLocationId]
GO
ALTER TABLE [dbo].[TuristLocations]  WITH CHECK ADD  CONSTRAINT [FK_TuristLocations_Regions_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[TuristLocations] CHECK CONSTRAINT [FK_TuristLocations_Regions_RegionId]
GO
USE [master]
GO
ALTER DATABASE [WeTravel] SET  READ_WRITE 
GO
