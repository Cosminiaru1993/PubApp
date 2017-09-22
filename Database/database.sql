/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2008 (10.0.2531)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [PubApp]    Script Date: 9/22/2017 5:36:31 PM ******/
CREATE DATABASE [PubApp] ON  PRIMARY 
( NAME = N'PubApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\PubApp.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PubApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\PubApp_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PubApp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PubApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PubApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PubApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PubApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PubApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PubApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [PubApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PubApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PubApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PubApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PubApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PubApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PubApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PubApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PubApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PubApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PubApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PubApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PubApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PubApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PubApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PubApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PubApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PubApp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PubApp] SET  MULTI_USER 
GO
ALTER DATABASE [PubApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PubApp] SET DB_CHAINING OFF 
GO
USE [PubApp]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/22/2017 5:36:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 9/22/2017 5:36:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[comment] [nvarchar](max) NULL,
	[is_paid] [bit] NOT NULL,
	[table_number] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 9/22/2017 5:36:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [float] NOT NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Order]    Script Date: 9/22/2017 5:36:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[price] [float] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_Product_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT ((0)) FOR [table_number]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product_Order]  WITH CHECK ADD  CONSTRAINT [FK_Product_Order_Order] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[Product_Order] CHECK CONSTRAINT [FK_Product_Order_Order]
GO
ALTER TABLE [dbo].[Product_Order]  WITH CHECK ADD  CONSTRAINT [FK_Product_Order_Product] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Product_Order] CHECK CONSTRAINT [FK_Product_Order_Product]
GO
USE [master]
GO
ALTER DATABASE [PubApp] SET  READ_WRITE 
GO
