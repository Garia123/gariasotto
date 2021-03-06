USE [master]
GO
/****** Object:  Database [WeTravel]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lodgings]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[Regions]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[ReserveDescriptions]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[Reserves]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[Sessions]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[TuristLocationCategories]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[TuristLocations]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'aece59f6-a072-5f21-fd18-af4958d7a39f', N'Areas Protegidas')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'Ciudades')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'ee84ec09-ce6e-b1b7-2d16-1bf0631d04f0', N'Galeria de arte')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'940d1a9d-f715-8ea9-2903-0452ed87ef1f', N'Lugares Historicos')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'a9b8f6e6-44a2-a441-3ccf-2f0192d78693', N'Playas')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (N'4e74db4b-fded-1676-79ed-47289635d8e4', N'Pueblos')
GO
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'0b408348-0793-465d-9f9f-017da0bbc2ae', N'Gabtype', 3, N'33 American Plaza', NULL, N'Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.', 4530, 0, N'965-140-5486', N'In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.', N'e6ddd404-0564-71d3-5b30-e67218f3b36b')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'b1f2a2e9-e767-474d-ae8d-115dcfda2b7a', N'Fatz', 3, N'84438 Goodland Center', NULL, N'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.

Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.

Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.', 1699, 1, N'268-227-8581', N'In congue. Etiam justo. Etiam pretium iaculis justo.', N'90fc320f-cdb6-b003-6398-50f44d5bd8e3')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'acbbaab5-3dab-4431-a91b-1402d79890bd', N'Zoomdog', 3, N'28 John Wall Crossing', NULL, N'Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.

Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.

Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.', 859, 1, N'312-370-8777', N'Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.', N'e6ddd404-0564-71d3-5b30-e67218f3b36b')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'c36d30ca-272f-4e94-b723-173df40cc779', N'Ailane', 4, N'96 Forest Dale Plaza', NULL, N'Morbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis.', 4025, 1, N'921-435-7986', N'In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.

Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.

Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.', N'90fc320f-cdb6-b003-6398-50f44d5bd8e3')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'9a375d3c-e3c2-4a24-9d06-1b759dbab333', N'Edgepulse', 3, N'4563 Darwin Point', NULL, N'Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.', 4362, 0, N'776-684-0977', N'Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.

Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.

Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.', N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'e60e25a7-0390-4814-848d-1f9a74785d6b', N'Pixonyx', 3, N'518 Hanover Drive', NULL, N'Nullam sit amet turpis elementum ligula vehicula consequat. Morbi a ipsum. Integer a nibh.', 1410, 0, N'663-237-5973', N'Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.', N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'5e6ba0b6-d3f5-4e8a-94b3-46de3ddff7e9', N'Oyoyo', 4, N'7374 Manley Park', NULL, N'Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.', 2545, 1, N'550-744-8597', N'Morbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis.

Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem.', N'90fc320f-cdb6-b003-6398-50f44d5bd8e3')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'3fc135fa-4d4b-4766-a761-50d5385e16d6', N'Oloo', 3, N'4143 Hoard Way', NULL, N'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.

Etiam vel augue. Vestibulum rutrum rutrum neque. Aenean auctor gravida sem.

Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio.', 3330, 0, N'392-620-5319', N'Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.', N'e6ddd404-0564-71d3-5b30-e67218f3b36b')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'6b3c973d-daa1-40ac-9c4c-559c3cdddce2', N'Flashdog', 4, N'6118 Moulton Avenue', NULL, N'Phasellus in felis. Donec semper sapien a libero. Nam dui.

Proin leo odio, porttitor id, consequat in, consequat ut, nulla. Sed accumsan felis. Ut at dolor quis odio consequat varius.

Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.', 3453, 1, N'509-903-2405', N'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.

Etiam vel augue. Vestibulum rutrum rutrum neque. Aenean auctor gravida sem.', N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'62c9f37b-979c-40eb-b464-6161fb01e152', N'Blogspan', 2, N'9229 Spohn Plaza', NULL, N'Pellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.

Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.', 4078, 1, N'485-310-6667', N'In sagittis dui vel nisl. Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus.

Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.', N'0c877b25-6240-1647-738b-1e0da9f96dbd')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'e47c6df8-adfe-448b-b6b7-6517a39efcb8', N'Flipopia', 3, N'78 Paget Street', NULL, N'Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.', 2622, 1, N'432-532-5055', N'Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.', N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'91d7e6fc-1d03-454c-8ad0-9826854d1c14', N'Podcat', 2, N'9128 Becker Way', NULL, N'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.

Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.

Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.', 2691, 0, N'785-766-6718', N'Proin leo odio, porttitor id, consequat in, consequat ut, nulla. Sed accumsan felis. Ut at dolor quis odio consequat varius.

Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.', N'0c877b25-6240-1647-738b-1e0da9f96dbd')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'15699467-a80c-4d02-8fb7-b898cc309af8', N'Babbleblab', 5, N'39555 Haas Road', NULL, N'Proin leo odio, porttitor id, consequat in, consequat ut, nulla. Sed accumsan felis. Ut at dolor quis odio consequat varius.

Integer ac leo. Pellentesque ultrices mattis odio. Donec vitae nisi.

Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.', 2337, 1, N'366-992-6665', N'Sed ante. Vivamus tortor. Duis mattis egestas metus.

Aenean fermentum. Donec ut mauris eget massa tempor convallis. Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh.', N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'b7ae335c-2d42-443a-b700-c1da5226c82b', N'BlogXS', 1, N'176 Pearson Hill', NULL, N'Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.

Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.

Nullam sit amet turpis elementum ligula vehicula consequat. Morbi a ipsum. Integer a nibh.', 4293, 0, N'285-882-4359', N'Phasellus in felis. Donec semper sapien a libero. Nam dui.', N'e6ddd404-0564-71d3-5b30-e67218f3b36b')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'9eba095b-55d7-414a-8a1d-c86a684c887f', N'Skyble', 4, N'4235 Division Avenue', NULL, N'Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.', 3860, 0, N'987-971-6355', N'In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.

Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui.', N'1b3584a0-4846-af65-b96f-a72a108020e2')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'f370ae1e-0d1b-4423-a256-d50953b00450', N'Voonte', 4, N'911 Independence Circle', NULL, N'Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.

Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.', 2179, 0, N'110-777-1374', N'Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.', N'1b3584a0-4846-af65-b96f-a72a108020e2')
INSERT [dbo].[Lodgings] ([Id], [Name], [Stars], [Address], [Images], [Description], [PricePerNight], [Available], [Telephone], [InformationText], [TuristLocationId]) VALUES (N'882db4f4-939c-41a2-a505-e5c0ab151c2d', N'Teklist', 4, N'5864 Steensland Terrace', NULL, N'Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros.

Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.

In congue. Etiam justo. Etiam pretium iaculis justo.', 1383, 0, N'279-171-3629', N'Quisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus.

Phasellus in felis. Donec semper sapien a libero. Nam dui.', N'1b3584a0-4846-af65-b96f-a72a108020e2')
GO
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (N'ea08f8ff-f74f-e68d-cec2-78e59a8e1293', N'Centro Sur')
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (N'627a7ff6-ddf7-0675-425b-8c2358433a86', N'Corredor Pajaros Pintados')
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (N'f8fcaffe-7ab1-f5a4-83ed-8de0f7bbb032', N'Litoral Norte')
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (N'd413fd27-b543-8f4b-72a7-8f5a11529cea', N'Metropolitana')
INSERT [dbo].[Regions] ([Id], [Name]) VALUES (N'dd3163e0-28ff-c613-c52a-a71b9c3d9e01', N'Este')
GO
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'28c1ffef-e6fc-1a6f-4a29-0e227ce148bc', N'Lore ipsum ', 2)
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'dc12b49e-b5d8-efde-2860-2db9f988e2cb', N'Lore ipsum ', 4)
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'23ec26b3-aa4e-0d07-31b9-33067ef22eb7', N'Lore ipsum ', 2)
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'663e3987-9462-680c-dccf-33dde820e227', N'Lore ipsum ', 1)
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'582b1cb7-e50d-435a-fb11-422d9b429a29', N'Lore ipsum ', 2)
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'71ebe6cb-7671-727d-d4d4-997f7a6ee11d', N'Lore ipsum ', 3)
INSERT [dbo].[ReserveDescriptions] ([ReserveId], [Description], [State]) VALUES (N'f5f4927d-612c-1264-acbf-dc198b1d0eef', N'Lore ipsum ', 4)
GO
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'28c1ffef-e6fc-1a6f-4a29-0e227ce148bc', CAST(N'2019-10-09T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-14T00:00:00.0000000' AS DateTime2), 3, 0, 4, 2818, N'550-209-0696', NULL, N'Dillon', N'Mclaughlin', N'fermentum.vel.mauris@lobortis.com', N'f370ae1e-0d1b-4423-a256-d50953b00450')
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'dc12b49e-b5d8-efde-2860-2db9f988e2cb', CAST(N'2019-10-06T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-16T00:00:00.0000000' AS DateTime2), 1, 5, 4, 4406, N'368-568-0554', NULL, N'Jackson', N'Ayers', N'eleifend.Cras.sed@sem.org', N'62c9f37b-979c-40eb-b464-6161fb01e152')
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'23ec26b3-aa4e-0d07-31b9-33067ef22eb7', CAST(N'2019-10-02T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-02T00:00:00.0000000' AS DateTime2), 5, 2, 0, 846, N'223-548-5407', NULL, N'Raphael', N'Daniels', N'varius@velitdui.ca', N'9a375d3c-e3c2-4a24-9d06-1b759dbab333')
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'663e3987-9462-680c-dccf-33dde820e227', CAST(N'2019-10-08T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-16T00:00:00.0000000' AS DateTime2), 7, 0, 5, 2437, N'149-703-2765', NULL, N'Wang', N'Fitzgerald', N'In.mi.pede@eget.net', N'62c9f37b-979c-40eb-b464-6161fb01e152')
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'582b1cb7-e50d-435a-fb11-422d9b429a29', CAST(N'2019-10-09T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-28T00:00:00.0000000' AS DateTime2), 4, 0, 5, 2907, N'726-810-1527', NULL, N'Graham', N'Burch', N'mauris@estMauris.org', N'882db4f4-939c-41a2-a505-e5c0ab151c2d')
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'71ebe6cb-7671-727d-d4d4-997f7a6ee11d', CAST(N'2019-10-10T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-27T00:00:00.0000000' AS DateTime2), 5, 0, 3, 4110, N'951-104-3984', NULL, N'Cody', N'Delaney', N'dis.parturient@ametmetus.edu', N'882db4f4-939c-41a2-a505-e5c0ab151c2d')
INSERT [dbo].[Reserves] ([Id], [CheckIn], [CheckOut], [Adults], [Children], [Babies], [Price], [Telephone], [InformationText], [ContactFirstName], [ContactLastName], [ContactEmail], [LodgingId]) VALUES (N'f5f4927d-612c-1264-acbf-dc198b1d0eef', CAST(N'2019-10-16T00:00:00.0000000' AS DateTime2), CAST(N'2019-11-14T00:00:00.0000000' AS DateTime2), 2, 4, 2, 4360, N'667-185-1076', NULL, N'Phelan', N'Long', N'viverra.Donec.tempus@semsempererat.org', N'9a375d3c-e3c2-4a24-9d06-1b759dbab333')
GO
INSERT [dbo].[Sessions] ([Token], [UserEmail]) VALUES (N'269de124-bc4f-4407-a672-095eabeb2043', N'admin@wetravel.com')
GO
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'a9b8f6e6-44a2-a441-3ccf-2f0192d78693', N'0ca33a4a-1794-1daa-8082-0d19d551ad3b')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'a9b8f6e6-44a2-a441-3ccf-2f0192d78693', N'4ef1ff60-1327-e0fb-280d-db0f4997feb8')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'a9b8f6e6-44a2-a441-3ccf-2f0192d78693', N'3642de62-392e-42a0-544b-fadf51f4f050')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'0c877b25-6240-1647-738b-1e0da9f96dbd')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'cb584cec-7fb5-9db7-ea01-24d5440a4bb4')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'90fc320f-cdb6-b003-6398-50f44d5bd8e3')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'1b3584a0-4846-af65-b96f-a72a108020e2')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6')
INSERT [dbo].[TuristLocationCategories] ([CategoryId], [TuristLocationId]) VALUES (N'25fc0242-d612-239d-4796-a3258bf1ecec', N'e6ddd404-0564-71d3-5b30-e67218f3b36b')
GO
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'0ca33a4a-1794-1daa-8082-0d19d551ad3b', N'Pixope', N'Curabitur dictum. Phasellus in felis. Nulla tempor augue ac ipsum. Phasellus vitae mauris sit amet lorem semper auctor. Mauris vel', N'http://dummyimage.com/122x158.bmp/dddddd/000000', N'd413fd27-b543-8f4b-72a7-8f5a11529cea')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'0c877b25-6240-1647-738b-1e0da9f96dbd', N'Thoughtmix', N'tincidunt nibh. Phasellus nulla. Integer vulputate, risus a ultricies adipiscing, enim mi tempor lorem, eget mollis lectus', N'http://dummyimage.com/122x158.bmp/dddddd/000000', N'ea08f8ff-f74f-e68d-cec2-78e59a8e1293')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'cb584cec-7fb5-9db7-ea01-24d5440a4bb4', N'Dynabox', N'luctus et ultrices posuere cubilia Curae; Donec tincidunt. Donec vitae erat vel pede blandit congue. In scelerisque scelerisque dui. Suspendisse ac metus vitae velit egestas lacinia.', N'http://dummyimage.com/175x244.png/5fa2dd/ffffff', N'ea08f8ff-f74f-e68d-cec2-78e59a8e1293')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'90fc320f-cdb6-b003-6398-50f44d5bd8e3', N'Jabberbean', N'penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin vel arcu eu odio tristique pharetra. Quisque ac libero nec ligula', N'http://dummyimage.com/120x146.png/dddddd/000000', N'dd3163e0-28ff-c613-c52a-a71b9c3d9e01')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'1b3584a0-4846-af65-b96f-a72a108020e2', N'Vitz', N'Duis elementum, dui quis accumsan convallis, ante lectus convallis est, vitae sodales nisi magna sed dui. Fusce aliquam, enim nec', N'et, rutrum non, hendrerit id, ante. Nunc mauris sapien,', N'dd3163e0-28ff-c613-c52a-a71b9c3d9e01')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'a7a15c79-ef6a-45f3-0a51-c5e97bf497a6', N'Bubblebox', N'lacus vestibulum lorem, sit amet ultricies sem magna nec quam. Curabitur vel lectus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus', N'http://dummyimage.com/180x180.jpg/dddddd/000000', N'ea08f8ff-f74f-e68d-cec2-78e59a8e1293')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'4ef1ff60-1327-e0fb-280d-db0f4997feb8', N'arcu et pede.', N'nunc. Quisque ornare tortor at risus. Nunc ac sem ut dolor dapibus gravida. Aliquam tincidunt, nunc ac mattis ornare, lectus ante', N'http://dummyimage.com/122x158.bmp/dddddd/000000', N'd413fd27-b543-8f4b-72a7-8f5a11529cea')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'e6ddd404-0564-71d3-5b30-e67218f3b36b', N'Twimbo', N'montes, nascetur ridiculus mus. Aenean eget magna. Suspendisse tristique neque venenatis lacus. Etiam bibendum fermentum metus. Aenean sed pede nec ante blandit', N'http://dummyimage.com/229x229.bmp/5fa2dd/ffffff', N'dd3163e0-28ff-c613-c52a-a71b9c3d9e01')
INSERT [dbo].[TuristLocations] ([Id], [Name], [Description], [ImagePath], [RegionId]) VALUES (N'3642de62-392e-42a0-544b-fadf51f4f050', N'Rhyzio', N'convallis ligula. Donec luctus aliquet odio. Etiam ligula', N'http://dummyimage.com/122x158.bmp/dddddd/000000', N'd413fd27-b543-8f4b-72a7-8f5a11529cea')
GO
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'admin@wetravel.com', N'Admin Account', N'AccountForTest123')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'amet.ante@at.edu', N'Brendan Terry', N'PWB65ZYE5WG')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'eu.augue@orci.edu', N'Chadwick Burton', N'BHG88VUW8ZR')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'facilisis.non@molestietellusAenean.org', N'Jacob Miles', N'KPB43EOM9UC')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'interdum.libero@in.ca', N'Griffith Cummings', N'XJT30ADJ0IL')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'Proin.ultrices@feugiatLoremipsum.co.uk', N'Declan Dunn', N'CJY19IFI2JO')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'ridiculus.mus.Proin@consectetueradipiscing.org', N'Garrison Watkins', N'CVI91QWM1QF')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'sit.amet.risus@orciconsectetuereuismod.net', N'Deacon Porter', N'BXC80UPP4EG')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'tempor.augue@Quisquefringillaeuismod.com', N'Mannix Estes', N'AAT92IXQ0UF')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'ut.ipsum.ac@nondapibus.ca', N'Zeph Chaney', N'KDA94LCU2GC')
INSERT [dbo].[Users] ([Email], [FullName], [Password]) VALUES (N'ut.ipsum.ac@nondapibusa.ca', N'Zeph Chaney', N'KDA94LCU2GC')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Categories_Name]    Script Date: 15-Oct-20 3:48:42 AM ******/
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [AK_Categories_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lodgings_TuristLocationId]    Script Date: 15-Oct-20 3:48:42 AM ******/
CREATE NONCLUSTERED INDEX [IX_Lodgings_TuristLocationId] ON [dbo].[Lodgings]
(
	[TuristLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reserves_LodgingId]    Script Date: 15-Oct-20 3:48:42 AM ******/
CREATE NONCLUSTERED INDEX [IX_Reserves_LodgingId] ON [dbo].[Reserves]
(
	[LodgingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Sessions_UserEmail]    Script Date: 15-Oct-20 3:48:42 AM ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_UserEmail] ON [dbo].[Sessions]
(
	[UserEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TuristLocationCategories_CategoryId]    Script Date: 15-Oct-20 3:48:42 AM ******/
CREATE NONCLUSTERED INDEX [IX_TuristLocationCategories_CategoryId] ON [dbo].[TuristLocationCategories]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TuristLocations_RegionId]    Script Date: 15-Oct-20 3:48:42 AM ******/
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
