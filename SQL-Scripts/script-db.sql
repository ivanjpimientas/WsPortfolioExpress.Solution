USE [PortfolioExpressDB]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF__Users__CreateDat__6FE99F9F]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF__Users__Confirmed__6EF57B66]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF__Users__Restore__6E01572D]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/07/2024 03:53:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 15/07/2024 03:53:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
DROP TABLE [dbo].[Customers]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15/07/2024 03:53:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
USE [master]
GO
/****** Object:  Database [PortfolioExpressDB]    Script Date: 15/07/2024 03:53:02 ******/
DROP DATABASE [PortfolioExpressDB]
GO
/****** Object:  Database [PortfolioExpressDB]    Script Date: 15/07/2024 03:53:02 ******/
CREATE DATABASE [PortfolioExpressDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PortfolioExpressDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLEXPRESS\MSSQL\DATA\PortfolioExpressDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PortfolioExpressDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLEXPRESS\MSSQL\DATA\PortfolioExpressDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PortfolioExpressDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PortfolioExpressDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PortfolioExpressDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PortfolioExpressDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PortfolioExpressDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PortfolioExpressDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PortfolioExpressDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PortfolioExpressDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PortfolioExpressDB] SET  MULTI_USER 
GO
ALTER DATABASE [PortfolioExpressDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PortfolioExpressDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PortfolioExpressDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PortfolioExpressDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PortfolioExpressDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [PortfolioExpressDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15/07/2024 03:53:03 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 15/07/2024 03:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SurName] [nvarchar](50) NOT NULL,
	[Document] [nvarchar](25) NOT NULL,
	[DocumentType] [nvarchar](3) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[Imagen] [nvarchar](500) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/07/2024 03:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Restore] [bit] NOT NULL,
	[Confirmed] [bit] NOT NULL,
	[Token] [nvarchar](200) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240714074936_InitialAddUsers', N'8.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240715061802_AddCustomersEditUsers', N'8.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240715063929_AddFieldImagenCustomers', N'8.0.7')
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [Name], [SurName], [Document], [DocumentType], [Email], [CreateDate], [Imagen]) VALUES (1, N'Eduardo Alfonzo', N'Duque Beltran', N'89009786', N'CC', N'ealfonzodb@correo.com', CAST(N'2024-07-15T02:47:47.1547392' AS DateTime2), N'/storage/customers/img/78b18c55-709c-4943-b2c8-da2a014c9b28_user-icon-profile.png')
INSERT [dbo].[Customers] ([Id], [Name], [SurName], [Document], [DocumentType], [Email], [CreateDate], [Imagen]) VALUES (2, N'Maria', N'Renata Ortiz', N'1099890764', N'CC', N'mrenata@correo.com', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'/storage/customers/img/80ebba81-db28-4da7-a3c5-3712e94ba3aa_default-profile.png')
INSERT [dbo].[Customers] ([Id], [Name], [SurName], [Document], [DocumentType], [Email], [CreateDate], [Imagen]) VALUES (4, N'Andres', N'Ortiz', N'345234545', N'CC', N'aortiz@correo.com', CAST(N'2024-07-15T03:42:34.0936670' AS DateTime2), N'/storage/customers/img/40b7db0d-ef6a-4d91-bae1-f94d454b1fcc_png-transparent-ninja-illustration-computer-programming-programming-language-programmer-logo-introduction-miscellaneous-computer-computer-program.png')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Restore], [Confirmed], [Token], [CreateDate]) VALUES (1, N'Ivan Pimienta', N'ivanjpimientas.developer@gmail.com', N'607A9536B0C1AFDCD3EC3FCC45B9E12DFE222E83962835C5D578D8F7FC487D07', 0, 1, N'0d0e2b62210b4ed1b233bc4bbbdd8634', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Restore], [Confirmed], [Token], [CreateDate]) VALUES (2, N'Administrador', N'admin@correo.com', N'240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 0, 1, N'5c2806c58c4e414e918e2b49793031b7', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Restore], [Confirmed], [Token], [CreateDate]) VALUES (3, N'Guest', N'guest@correo.com', N'6B93CCBA414AC1D0AE1E77F3FAC560C748A6701ED6946735A49D463351518E16', 0, 1, N'a0bc120485244a40b3222b5ad062c352', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Restore]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Confirmed]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateDate]
GO
USE [master]
GO
ALTER DATABASE [PortfolioExpressDB] SET  READ_WRITE 
GO
