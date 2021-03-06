USE [master]
GO
/****** Object:  Database [MrRobotWebshopDB]    Script Date: 9/2/2020 4:42:39 AM ******/
CREATE DATABASE [MrRobotWebshopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MrRobotWebshopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MrRobotWebshopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MrRobotWebshopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MrRobotWebshopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MrRobotWebshopDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MrRobotWebshopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MrRobotWebshopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MrRobotWebshopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MrRobotWebshopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MrRobotWebshopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MrRobotWebshopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MrRobotWebshopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MrRobotWebshopDB] SET  MULTI_USER 
GO
ALTER DATABASE [MrRobotWebshopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MrRobotWebshopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MrRobotWebshopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MrRobotWebshopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MrRobotWebshopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MrRobotWebshopDB] SET QUERY_STORE = OFF
GO
USE [MrRobotWebshopDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](255) NULL,
	[ProductDescription] [varchar](255) NULL,
	[Price] [decimal](18, 0) NULL,
	[ImageURL] [varchar](255) NULL,
	[SubCategoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductInfo]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInfo](
	[ProductInfoID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NULL,
	[Discount] [decimal](18, 0) NULL,
	[ProductID] [int] NOT NULL,
	[ReceiptID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[ReceiptID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](255) NULL,
	[FinalPrice] [varchar](255) NULL,
	[WebshopUserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReceiptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryName] [varchar](255) NULL,
	[CategoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebshopUser]    Script Date: 9/2/2020 4:42:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebshopUser](
	[WebshopUserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Salt] [varchar](255) NULL,
	[Firstname] [varchar](255) NULL,
	[Lastname] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[WebshopUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (28, N'Processors')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (29, N'Graphics cards')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (30, N'Ram')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (31, N'Peripheral')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (23, N'CPU AMD Ryzen 7 3800XT', N'4.7 Ghz, 8 Cores, 16 Threads', CAST(3887 AS Decimal(18, 0)), N'4d948403-b996-4846-84bd-d3d319603dd7_1.jpg', 4)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (24, N'CPU AMD Ryzen 9 3900XT', N'4.7 Ghz, 12 Cores, 24 Threads', CAST(4998 AS Decimal(18, 0)), N'881aea2f-e6d1-428b-bf9e-249f56b18673_2.jpg', 4)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (27, N'CPU AMD Ryzen 5 3600XT', N'4.5 Ghz, 6 Cores, 12 Threads', CAST(2243 AS Decimal(18, 0)), N'f5f251ae-6937-48a0-aa5f-e3e8a251bed1_3.jpg', 4)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (28, N'Intel Core i9-10900K', N'5.3 Ghz, 10 Cores, 20 Threads', CAST(6322 AS Decimal(18, 0)), N'8e4654c9-0c48-4245-8bdb-e5997bfc13db_4.jpg', 5)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (29, N'Intel Core i9-9900KF', N'5.0 Ghz, 8 Cores, 16 Threads', CAST(3801 AS Decimal(18, 0)), N'5a358187-5ca2-43d5-9ecd-a3f91883a361_5.jpg', 5)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (30, N'Intel Core i7-10700', N'4.8 Ghz, 8 Cores, 16 Threads', CAST(3427 AS Decimal(18, 0)), N'23d09bce-9953-46c5-a5e1-49a37ed7e6d4_6.jpg', 5)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (31, N'Sapphire RX5700XT Pulse', N'Chip clock 1670MHz, Boost: 1925MHz
Memory 8GB GDDR6, 1750MHz, 256bit, 448GB/s', CAST(3835 AS Decimal(18, 0)), N'47129574-dc4d-4bf5-b71a-c7cfa482545b_7.jpg', 6)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (32, N'Asrock RX5600XT Challenger', N'Chip clock 1280MHz, Boost: 1495-1620MHz
Memory 6GB GDDR6, 1500MHz, 192bit, 288GB/s', CAST(2750 AS Decimal(18, 0)), N'c92c1e8f-fe85-407e-800c-fe52f56a4445_8.jpg', 6)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (33, N'ASUS RTX2080Ti STRIX', N'Chip clock 1350MHz, Boost: 1590MHz (OC Mode)
Memory 11GB GDDR6, 1750MHz, 352bit, 616GB/s', CAST(12777 AS Decimal(18, 0)), N'928cb5e0-9812-49f2-b2e2-48c8debe6ba7_9.jpg', 7)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (34, N'ASUS RTX2080 SUPER Strix', N'Chip clock 1680MHz, Boost: 1890MHz (OC Mode)
Memory 8GB GDDR6, 1938MHz, 256bit, 496GB/s', CAST(7999 AS Decimal(18, 0)), N'21ae40fa-cdbe-4955-a92d-da5da88a0fd8_10.jpg', 7)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (35, N'DDR3 16GB (2x8) Kingston 1866MHz Fury White', N'CAS Latency CL 10 (conforms ~10.72ns)
Voltage 1.50V', CAST(776 AS Decimal(18, 0)), N'b18a0ba8-28ca-49f5-b4a6-8457dd086950_1.jpg', 8)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (36, N'DDR3 8GB (1x8) Corsair, 1600Hz, CL10, Vengeance LP black', N'Modules 1x 8GB
CAS Latency CL 10 (conforms ~12.50ns)', CAST(437 AS Decimal(18, 0)), N'4c179691-cbb9-404d-b131-99a8dfc3542e_2.jpg', 8)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (37, N'DDR4 128GB (4x32) Corsair 2666MHz', N'Modules 4x 32GB
JEDEC PC4-21300U
CAS Latency CL 16 (conforms ~12.00ns)', CAST(5500 AS Decimal(18, 0)), N'de09f9e7-aa74-47d5-89bc-060a146c3e53_3.jpg', 9)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (38, N'DDR4 64GB (4x16) Corsair 3200MHz Dominator Platinum', N'Modules 4x 16GB
JEDEC PC4-25600U
CAS Latency CL 16 (conforms ~10.00ns)', CAST(3940 AS Decimal(18, 0)), N'632df446-dce4-44c3-b8f2-8c18e44abb28_4.jpg', 9)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (41, N'Slušalice SENNHEISER GSP 370', N'bežicne, mikrofon, crne', CAST(1553 AS Decimal(18, 0)), N'05b0220c-29a9-43c7-9dff-77cc4517ef19_6.jpg', 12)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (43, N'Slušalice SENNHEISER Momentum', N'bežicne, mikrofon, crne', CAST(2500 AS Decimal(18, 0)), N'5dadba63-cc83-446e-8bba-1bdc27ac40e6_5.jpg', 12)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (44, N'DUCKY Shine 7 Gunmetal', N'DUCKY Shine 7 Gunmetal US', CAST(1306 AS Decimal(18, 0)), N'2989beec-8701-427c-92d5-245cb12112e0_7.jpg', 10)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (45, N'Kingston HyperX Mechanical Gaming Keyboard, Alloy Elite', N'Cherry MX blueUS', CAST(1124 AS Decimal(18, 0)), N'6434102d-c08f-4d27-8e1a-69c1b1a2b5ee_8.jpg', 10)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (46, N'LOGITECH Gaming G502 Lightspeed', N'16000dpi wireless', CAST(1043 AS Decimal(18, 0)), N'10078bd5-6fdc-44db-9393-2895ed8364d3_11.jpg', 11)
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Price], [ImageURL], [SubCategoryID]) VALUES (47, N'RAZER DeathAdder Essential', N'6400dpi', CAST(293 AS Decimal(18, 0)), N'00a1be20-60c1-4f12-bc0a-4855b68fb8f9_12.jpg', 11)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[SubCategory] ON 

INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (4, N'AMD', 28)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (5, N'Intel', 28)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (6, N'AMD GPU', 29)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (7, N'NVDIA GPU', 29)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (8, N'DDR3', 30)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (9, N'DDR4', 30)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (10, N'Keyboards', 31)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (11, N'Mice', 31)
INSERT [dbo].[SubCategory] ([SubCategoryID], [SubCategoryName], [CategoryID]) VALUES (12, N'Headsets', 31)
SET IDENTITY_INSERT [dbo].[SubCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[WebshopUser] ON 

INSERT [dbo].[WebshopUser] ([WebshopUserID], [Username], [Password], [Salt], [Firstname], [Lastname]) VALUES (1, N'test2', N'F7onFR0XDDtSAKLhGh49/QReLq4xo22Vm71hcvVJugI=', N'KR/iOZ4gJegQ6dtWfljHPQ==', N'test2', N'test2')
SET IDENTITY_INSERT [dbo].[WebshopUser] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([SubCategoryID])
REFERENCES [dbo].[SubCategory] ([SubCategoryID])
GO
ALTER TABLE [dbo].[ProductInfo]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[ProductInfo]  WITH CHECK ADD FOREIGN KEY([ReceiptID])
REFERENCES [dbo].[Receipt] ([ReceiptID])
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD FOREIGN KEY([WebshopUserID])
REFERENCES [dbo].[WebshopUser] ([WebshopUserID])
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
USE [master]
GO
ALTER DATABASE [MrRobotWebshopDB] SET  READ_WRITE 
GO
