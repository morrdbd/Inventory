USE [master]
GO
/****** Object:  Database [Inventory]    Script Date: 5/30/2020 10:24:54 AM ******/
CREATE DATABASE [Inventory]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Inventory', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Inventory.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Inventory_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Inventory_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Inventory] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Inventory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Inventory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Inventory] SET ARITHABORT OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Inventory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Inventory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Inventory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Inventory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Inventory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Inventory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Inventory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Inventory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Inventory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Inventory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Inventory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Inventory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Inventory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Inventory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Inventory] SET RECOVERY FULL 
GO
ALTER DATABASE [Inventory] SET  MULTI_USER 
GO
ALTER DATABASE [Inventory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Inventory] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Inventory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Inventory] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Inventory] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Inventory', N'ON'
GO
ALTER DATABASE [Inventory] SET QUERY_STORE = OFF
GO
USE [Inventory]
GO
/****** Object:  User [KBL-IMU-KAMALPO\WDAGUtilityAccount]    Script Date: 5/30/2020 10:24:54 AM ******/
CREATE USER [KBL-IMU-KAMALPO\WDAGUtilityAccount] FOR LOGIN [KBL-IMU-KAMALPO\WDAGUtilityAccount] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [inventory]    Script Date: 5/30/2020 10:24:54 AM ******/
CREATE USER [inventory] FOR LOGIN [Inventory] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [inventory]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [inventory]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [inventory]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [inventory]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [inventory]
GO
ALTER ROLE [db_datareader] ADD MEMBER [inventory]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [inventory]
GO
/****** Object:  Table [dbo].[ActivityLogs]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLogs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[ModifiedTable] [nvarchar](300) NULL,
	[ModfiedId] [bigint] NOT NULL,
	[Action] [nvarchar](300) NULL,
	[UserId] [int] NOT NULL,
	[ModifiedTime] [datetime] NOT NULL,
	[ModifiedData] [ntext] NULL,
 CONSTRAINT [PK_dbo.ActivityLogs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](300) NULL,
	[ClaimValue] [nvarchar](300) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
	[InsertedBy] [int] NULL,
	[InsertedDate] [datetime] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](300) NULL,
	[SecurityStamp] [nvarchar](300) NULL,
	[PhoneNumber] [nvarchar](300) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[PassNeedsChange] [bit] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[EnName] [nvarchar](500) NOT NULL,
	[DrName] [nvarchar](500) NOT NULL,
	[PaName] [nvarchar](500) NOT NULL,
	[ParentDepartmentID] [int] NULL,
	[IsActive] [bit] NULL,
	[InsertedBy] [int] NOT NULL,
	[InsertedDate] [date] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [date] NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DistributionItems]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistributionItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DistributionID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ItemID] [int] NULL,
	[UnitPrice] [int] NOT NULL,
	[DealWithAccount] [nvarchar](500) NULL,
	[IsReturned] [bit] NOT NULL,
	[ReturnDate] [date] NULL,
 CONSTRAINT [PK_TicketItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Distributions]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distributions](
	[DistributionID] [int] IDENTITY(1,1) NOT NULL,
	[TicketNumber] [int] NULL,
	[TicketIssuedDate] [datetime] NULL,
	[Warehouse] [nvarchar](500) NULL,
	[RequestNumber] [int] NULL,
	[RequestDate] [datetime] NULL,
	[EmployeeID] [int] NULL,
	[Details] [nvarchar](max) NULL,
	[FilePath] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
	[InsertedDate] [date] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [date] NULL,
 CONSTRAINT [PK_DistributionTicket] PRIMARY KEY CLUSTERED 
(
	[DistributionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/30/2020 10:24:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NULL,
	[FatherName] [nvarchar](300) NULL,
	[DepartmentID] [int] NULL,
	[Occupation] [nvarchar](300) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[IsActive] [bit] NULL,
	[InsertedBy] [int] NULL,
	[InsertedDate] [date] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [date] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LookupTypes]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LookupTypes](
	[LookupId] [int] IDENTITY(1,1) NOT NULL,
	[LookupCode] [nvarchar](100) NULL,
	[EnName] [nvarchar](300) NULL,
	[DrName] [nvarchar](300) NULL,
	[PaName] [nvarchar](300) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LookupTypes] PRIMARY KEY CLUSTERED 
(
	[LookupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LookupValues]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LookupValues](
	[ValueId] [int] IDENTITY(1,1) NOT NULL,
	[LookupCode] [nvarchar](100) NULL,
	[ValueCode] [nvarchar](100) NULL,
	[EnName] [nvarchar](300) NULL,
	[DrName] [nvarchar](300) NULL,
	[PaName] [nvarchar](300) NULL,
	[ForOrdering] [nvarchar](1) NULL,
	[IsActive] [bit] NOT NULL,
	[GroupValueId] [int] NULL,
 CONSTRAINT [PK_dbo.LookupValues] PRIMARY KEY CLUSTERED 
(
	[ValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] NOT NULL,
	[EnName] [nvarchar](100) NULL,
	[DrName] [nvarchar](100) NULL,
	[PaName] [nvarchar](100) NULL,
	[Icon] [nvarchar](100) NULL,
	[MenuLevel] [int] NULL,
	[SuperMenuId] [int] NULL,
	[HasSubMenu] [bit] NULL,
	[Controller] [nvarchar](100) NULL,
	[Action] [nvarchar](100) NULL,
	[ActionType] [nvarchar](50) NULL,
	[MenuOrder] [int] NULL,
	[IsActive] [bit] NULL,
	[Visible] [bit] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[UsageTypeID] [int] NULL,
	[GroupID] [int] NOT NULL,
	[CategoryID] [int] NULL,
	[UnitID] [int] NULL,
	[PackingID] [int] NULL,
	[SizeID] [int] NULL,
	[SerialCodeNumber] [nvarchar](100) NULL,
	[ProductName] [nvarchar](300) NOT NULL,
	[OriginID] [int] NULL,
	[BrandID] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[ProductCode] [int] NULL,
	[ImagePath] [varchar](300) NULL,
	[IsActive] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
	[InsertedDate] [date] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [date] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleAccess]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleAccess](
	[RoleAccessId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[MenuId] [int] NULL,
	[FullControl] [bit] NULL,
	[Add] [bit] NULL,
	[Edit] [bit] NULL,
	[Search] [bit] NULL,
	[View] [bit] NULL,
	[Delete] [bit] NULL,
	[Print] [bit] NULL,
	[Download] [bit] NULL,
 CONSTRAINT [PK_RoleAccess] PRIMARY KEY CLUSTERED 
(
	[RoleAccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SigninLogs]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SigninLogs](
	[SigninId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Username] [nvarchar](300) NULL,
	[UserIP] [nvarchar](300) NULL,
	[SigninTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.SigninLogs] PRIMARY KEY CLUSTERED 
(
	[SigninId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockInItems]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockInItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockInID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[AvailableQuantity] [int] NULL,
	[UnitPrice] [int] NOT NULL,
	[UsageTypeID] [int] NULL,
	[GroupID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ItemName] [nvarchar](300) NULL,
	[ItemCode] [nvarchar](300) NULL,
	[UnitID] [int] NULL,
	[SizeID] [int] NULL,
	[OriginID] [int] NULL,
	[BrandID] [int] NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
	[InsertedDate] [date] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [date] NULL,
 CONSTRAINT [PK_NondurableStockInItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockIns]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockIns](
	[StockInID] [int] IDENTITY(1,1) NOT NULL,
	[M7Number] [nvarchar](100) NULL,
	[StockInDate] [date] NULL,
	[DeliveryPlace] [nvarchar](500) NULL,
	[ContractorName] [nvarchar](300) NULL,
	[OrderNumber] [nvarchar](100) NULL,
	[OrderDate] [date] NULL,
	[Details] [nvarchar](max) NULL,
	[FilePath] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[InsertedBy] [int] NOT NULL,
	[InsertedDate] [date] NULL,
	[LastUpdatedBy] [int] NULL,
	[LastUpdatedDate] [date] NULL,
 CONSTRAINT [PK_NondurableStockIns] PRIMARY KEY CLUSTERED 
(
	[StockInID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ActivityLogs] ON 

INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1, N'Roles', 2, N'Insert', 5, CAST(N'2019-08-18T14:01:44.717' AS DateTime), N'{"RoleId":0,"RoleName":"test","Menus":[{"RoleAccessId":3,"RoleId":2,"MenuId":1,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":4,"RoleId":2,"MenuId":2,"FullControl":false,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false}]}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2, N'Users', 11, N'Insert', 5, CAST(N'2019-08-18T19:18:57.957' AS DateTime), N'{"Id":0,"DisplayName":"sharif","UserName":"sharif","Password":"Sharif@12345","Role":"test"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1002, N'Roles', 1, N'Update', 5, CAST(N'2019-09-18T10:16:57.727' AS DateTime), N'{"RoleId":1,"RoleName":"Administrator","Menus":[{"RoleAccessId":2002,"RoleId":1,"MenuId":1,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2003,"RoleId":1,"MenuId":2,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2004,"RoleId":1,"MenuId":3,"FullControl":false,"Add":true,"Edit":true,"Search":true,"View":true,"Delete":true,"Print":true,"Download":true},{"RoleAccessId":2005,"RoleId":1,"MenuId":4,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2006,"RoleId":1,"MenuId":5,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2007,"RoleId":1,"MenuId":6,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false}]}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1003, N'Roles', 1, N'Update', 5, CAST(N'2019-09-18T10:17:15.287' AS DateTime), N'{"RoleId":1,"RoleName":"Administrator","Menus":[{"RoleAccessId":2008,"RoleId":1,"MenuId":1,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2009,"RoleId":1,"MenuId":2,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2010,"RoleId":1,"MenuId":3,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2011,"RoleId":1,"MenuId":4,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2012,"RoleId":1,"MenuId":5,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false},{"RoleAccessId":2013,"RoleId":1,"MenuId":6,"FullControl":true,"Add":false,"Edit":false,"Search":false,"View":false,"Delete":false,"Print":false,"Download":false}]}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1004, N'DistributionTicketPC5', 1, N'Insert', 5, CAST(N'2019-12-26T21:08:03.557' AS DateTime), N'{"TicketID":1,"TicketNumber":null,"TicketIssuedDate":null,"TicketIssuedDateForm":null,"Warehouse":null,"RequestNumber":null,"RequestDate":null,"RequestDateForm":null,"Requester":null,"Details":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-26T21:07:58.6706683+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1005, N'DistributionTicketPC5', 2, N'Insert', 5, CAST(N'2019-12-26T21:16:19.580' AS DateTime), N'{"TicketID":2,"TicketNumber":null,"TicketIssuedDate":null,"TicketIssuedDateForm":null,"Warehouse":null,"RequestNumber":null,"RequestDate":null,"RequestDateForm":null,"Requester":null,"Details":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-26T21:16:19.5240355+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1006, N'DistributionTicketPC5', 3, N'Insert', 5, CAST(N'2019-12-26T21:25:01.150' AS DateTime), N'{"TicketID":3,"TicketNumber":1,"TicketIssuedDate":"2019-12-26T00:00:00","TicketIssuedDateForm":null,"Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-26T00:00:00","RequestDateForm":null,"Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-26T21:25:00.9816436+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1007, N'DistributionTicketPC5', 4, N'Insert', 5, CAST(N'2019-12-27T10:08:34.263' AS DateTime), N'{"TicketID":4,"TicketNumber":111,"TicketIssuedDate":"2019-12-27T00:00:00","TicketIssuedDateForm":null,"Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","RequestDateForm":null,"Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T10:08:34.2420929+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1008, N'TicketItemData', 1, N'Insert', 5, CAST(N'2019-12-27T10:08:34.327' AS DateTime), N'{"ID":1,"TicketID":4,"Quantity":1,"UnitID":0,"UnitName":null,"ItemDetails":"1","UnitPrice":1,"DealWithAccount":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T10:08:34.300936+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1009, N'DistributionTicketPC5', 5, N'Insert', 5, CAST(N'2019-12-27T10:13:30.050' AS DateTime), N'{"TicketID":5,"TicketNumber":1,"TicketIssuedDate":"2019-12-27T00:00:00","TicketIssuedDateForm":null,"Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","RequestDateForm":null,"Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T10:13:27.1935163+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1010, N'TicketItemData', 1, N'Insert', 5, CAST(N'2019-12-27T10:13:37.333' AS DateTime), N'{"ID":1,"TicketID":5,"Quantity":1,"UnitID":0,"UnitName":null,"ItemDetails":"1","UnitPrice":1,"DealWithAccount":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T10:13:36.5331767+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1011, N'Ticket', 5, N'Delete', 5, CAST(N'2019-12-28T13:56:16.937' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1012, N'Ticket', 5, N'Delete', 5, CAST(N'2019-12-28T13:58:21.553' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1013, N'Ticket', 5, N'Update', 5, CAST(N'2019-12-29T20:44:55.187' AS DateTime), N'{"TicketID":5,"TicketNumber":1,"TicketIssuedDate":"2019-12-27T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T00:00:00","InsertedDateForm":null,"LastUpdatedBy":5,"LastUpdatedDate":"2019-12-29T20:44:54.7448095+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1014, N'TicketItem', 1, N'Update', 5, CAST(N'2019-12-29T20:45:04.750' AS DateTime), N'{"ID":1,"TicketID":5,"Quantity":1,"UnitID":3,"ItemDetails":"1","UnitPrice":1,"DealWithAccount":"1","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1015, N'Ticket', 5, N'Update', 5, CAST(N'2019-12-29T20:46:06.927' AS DateTime), N'{"TicketID":5,"TicketNumber":1,"TicketIssuedDate":"2019-12-27T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T00:00:00","InsertedDateForm":null,"LastUpdatedBy":5,"LastUpdatedDate":"2019-12-29T20:46:06.9146497+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1016, N'TicketItem', 2, N'Insert', 5, CAST(N'2019-12-29T20:46:06.940' AS DateTime), N'{"ID":2,"TicketID":5,"Quantity":2,"UnitID":3,"ItemDetails":"2","UnitPrice":2,"DealWithAccount":"2","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-29T20:46:06.9375905+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1017, N'Ticket', 5, N'Update', 5, CAST(N'2019-12-29T20:46:54.620' AS DateTime), N'{"TicketID":5,"TicketNumber":1,"TicketIssuedDate":"2019-12-27T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T00:00:00","InsertedDateForm":null,"LastUpdatedBy":5,"LastUpdatedDate":"2019-12-29T20:46:54.6158322+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1018, N'TicketItem', 2, N'Update', 5, CAST(N'2019-12-29T20:46:54.633' AS DateTime), N'{"ID":2,"TicketID":5,"Quantity":2,"UnitID":3,"ItemDetails":"2","UnitPrice":2,"DealWithAccount":"2","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1019, N'TicketItem', 3, N'Insert', 5, CAST(N'2019-12-29T20:46:54.633' AS DateTime), N'{"ID":3,"TicketID":5,"Quantity":3,"UnitID":3,"ItemDetails":"3","UnitPrice":3,"DealWithAccount":"3","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-29T20:46:54.6337847+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1020, N'Ticket', 5, N'Update', 5, CAST(N'2019-12-29T20:48:34.013' AS DateTime), N'{"TicketID":5,"TicketNumber":1,"TicketIssuedDate":"2019-12-27T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T00:00:00","InsertedDateForm":null,"LastUpdatedBy":5,"LastUpdatedDate":"2019-12-29T20:48:34.0070923+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1021, N'TicketItem', 1, N'Update', 5, CAST(N'2019-12-29T20:48:34.027' AS DateTime), N'{"ID":1,"TicketID":5,"Quantity":1,"UnitID":3,"ItemDetails":"1","UnitPrice":1,"DealWithAccount":"1","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1022, N'TicketItem', 2, N'Update', 5, CAST(N'2019-12-29T20:48:34.033' AS DateTime), N'{"ID":2,"TicketID":5,"Quantity":2,"UnitID":3,"ItemDetails":"2","UnitPrice":2,"DealWithAccount":"2","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1023, N'TicketItem', 3, N'Update', 5, CAST(N'2019-12-29T20:48:34.040' AS DateTime), N'{"ID":3,"TicketID":5,"Quantity":3,"UnitID":3,"ItemDetails":"3","UnitPrice":3,"DealWithAccount":"3","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1024, N'Ticket', 5, N'Update', 5, CAST(N'2019-12-29T20:51:59.463' AS DateTime), N'{"TicketID":5,"TicketNumber":1,"TicketIssuedDate":"2019-12-27T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2019-12-27T00:00:00","Requester":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-27T00:00:00","InsertedDateForm":null,"LastUpdatedBy":5,"LastUpdatedDate":"2019-12-29T20:51:58.9328644+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1025, N'TicketItem', 1, N'Update', 5, CAST(N'2019-12-29T20:52:46.217' AS DateTime), N'{"ID":1,"TicketID":5,"Quantity":1,"UnitID":3,"ItemDetails":"1","UnitPrice":1,"DealWithAccount":"1","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1026, N'TicketItem', 2, N'Update', 5, CAST(N'2019-12-29T20:52:55.967' AS DateTime), N'{"ID":2,"TicketID":5,"Quantity":2,"UnitID":3,"ItemDetails":"2","UnitPrice":2,"DealWithAccount":"2","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1027, N'TicketItem', 3, N'Update', 5, CAST(N'2019-12-29T20:53:19.483' AS DateTime), N'{"ID":3,"TicketID":5,"Quantity":3,"UnitID":3,"ItemDetails":"3","UnitPrice":3,"DealWithAccount":"3","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (1028, N'TicketItem', 4, N'Insert', 5, CAST(N'2019-12-29T20:54:32.553' AS DateTime), N'{"ID":4,"TicketID":5,"Quantity":4,"UnitID":3,"ItemDetails":"4","UnitPrice":4,"DealWithAccount":"4","IsActive":true,"InsertedBy":5,"InsertedDate":"2019-12-29T20:54:26.0437998+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2002, N'Ticket', 1, N'Insert', 5, CAST(N'2020-01-01T11:58:46.707' AS DateTime), N'{"ReceiptReportID":1,"ReportNumber":1,"Organization":"1","ReceiptDate":"2020-01-01T00:00:00","SuggBillNumber":1,"Mem3Date":"2020-01-01T00:00:00","DeliveryPlace":"1","ObtainedFromContractor":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-01T11:58:46.6828447+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2003, N'ReceiptReportItem', 1, N'Insert', 5, CAST(N'2020-01-01T11:58:46.767' AS DateTime), N'{"ID":1,"ReceiptReportID":1,"Quantity":1,"ItemDetails":"1","UnitID":3,"UnitPrice":1,"Remarks":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-01T11:58:46.7519557+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2004, N'Ticket', 2, N'Insert', 5, CAST(N'2020-01-01T14:53:27.283' AS DateTime), N'{"ReceiptReportID":2,"ReportNumber":1,"Organization":"1","ReceiptDate":"2020-01-01T00:00:00","SuggBillNumber":1,"Mem3Date":"2020-01-01T00:00:00","DeliveryPlace":"1","ObtainedFromContractor":"1","Details":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-01T14:53:27.098804+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2005, N'ReceiptReportItem', 2, N'Insert', 5, CAST(N'2020-01-01T14:53:27.463' AS DateTime), N'{"ID":2,"ReceiptReportID":2,"Quantity":1,"ItemDetails":"I","UnitID":23,"UnitPrice":1,"Remarks":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-01T14:53:27.4546306+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2006, N'ReceiptReport', 1, N'Update', 5, CAST(N'2020-01-01T15:24:12.923' AS DateTime), N'{"ReceiptReportID":1,"ReportNumber":1,"Organization":"1","ReceiptDate":"2020-01-01T00:00:00","SuggBillNumber":1,"Mem3Date":"2020-01-01T00:00:00","DeliveryPlace":"1","ObtainedFromContractor":"1","Details":"تفصیلات","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-01T00:00:00","InsertedDateForm":null,"LastUpdatedBy":5,"LastUpdatedDate":"2020-01-01T15:24:12.6598156+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (2007, N'ReceiptReportItems', 1, N'Update', 5, CAST(N'2020-01-01T15:24:13.040' AS DateTime), N'{"ID":1,"ReceiptReportID":1,"Quantity":22222,"ItemDetails":"Char and some thing sdfdsf sadf sadf","UnitID":3,"UnitPrice":23322,"Remarks":"adsfsadsadsa asdsa dsad asdsad asd sad","IsActive":false,"InsertedBy":0,"InsertedDate":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3002, N'Ticket', 6, N'Insert', 5, CAST(N'2020-01-04T10:59:28.943' AS DateTime), N'{"TicketID":6,"TicketNumber":1,"TicketIssuedDate":"2020-01-04T00:00:00","Warehouse":"وزارت مهاجرین تحویلخانه","RequestNumber":1,"RequestDate":"2020-01-04T00:00:00","BranchID":1,"Details":"asdfdsa","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T10:59:28.5886743+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3003, N'TicketItem', 1002, N'Insert', 5, CAST(N'2020-01-04T10:59:28.997' AS DateTime), N'{"ID":1002,"TicketID":6,"Quantity":1,"UnitID":23,"ItemDetails":"A4","UnitPrice":12,"DealWithAccount":"1","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T10:59:28.9866103+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3004, N'Ticket', 1002, N'Insert', 5, CAST(N'2020-01-04T11:05:32.247' AS DateTime), N'{"ReceiptReportID":1002,"ReportNumber":1,"Organization":"ui","ReceiptDate":"2020-01-04T00:00:00","SuggBillNumber":1,"Mem3Date":"2019-12-28T00:00:00","DeliveryPlace":"jhjh","ObtainedFromContractor":"jyjyu","Details":"lkhkloiiuoi","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T11:05:32.2372525+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3005, N'ReceiptReportItem', 1002, N'Insert', 5, CAST(N'2020-01-04T11:05:32.270' AS DateTime), N'{"ID":1002,"ReceiptReportID":1002,"Quantity":2,"ItemDetails":"lilj","UnitID":24,"UnitPrice":20,"Remarks":"jhjh","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T11:05:32.2562311+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3006, N'Ticket', 1003, N'Insert', 5, CAST(N'2020-01-04T12:14:57.923' AS DateTime), N'{"ReceiptReportID":1003,"ReportNumber":1,"Organization":"MOrr","ReceiptDate":"2020-01-04T00:00:00","SuggBillNumber":12,"Mem3Date":"2019-12-28T00:00:00","DeliveryPlace":"ware house","ObtainedFromContractor":"ahmad","Details":"From request to ahmad","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T12:14:57.9203782+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3007, N'ReceiptReportItem', 1003, N'Insert', 5, CAST(N'2020-01-04T12:14:57.927' AS DateTime), N'{"ID":1003,"ReceiptReportID":1003,"Quantity":1,"ItemDetails":"computer","UnitID":24,"UnitPrice":12,"Remarks":"Somthing","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T12:14:57.9232726+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3008, N'ReceiptReportItem', 1004, N'Insert', 5, CAST(N'2020-01-04T12:14:57.927' AS DateTime), N'{"ID":1004,"ReceiptReportID":1003,"Quantity":1,"ItemDetails":"compter 0001","UnitID":24,"UnitPrice":12000,"Remarks":"dsf","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T12:14:57.9262961+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (3009, N'ReceiptReportItem', 1005, N'Insert', 5, CAST(N'2020-01-04T12:14:57.930' AS DateTime), N'{"ID":1005,"ReceiptReportID":1003,"Quantity":1,"ItemDetails":"Printer-002","UnitID":24,"UnitPrice":3000,"Remarks":"printer","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-04T12:14:57.9282931+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4002, N'Items', 13, N'Insert', 5, CAST(N'2020-01-11T10:51:02.810' AS DateTime), N'{"ItemID":13,"UsageTypeID":1003,"ItemName":"Cor 3","ItemCode":null,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4003, N'Items', 14, N'Insert', 5, CAST(N'2020-01-11T10:51:30.047' AS DateTime), N'{"ItemID":14,"UsageTypeID":1003,"ItemName":"core 3","ItemCode":null,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4004, N'Items', 15, N'Insert', 5, CAST(N'2020-01-11T10:52:14.733' AS DateTime), N'{"ItemID":15,"UsageTypeID":1003,"ItemName":"core 3","ItemCode":null,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4005, N'Items', 16, N'Insert', 5, CAST(N'2020-01-11T10:53:01.033' AS DateTime), N'{"ItemID":16,"UsageTypeID":1003,"ItemName":"core 3","ItemCode":null,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4006, N'Products', 16, N'Delete', 5, CAST(N'2020-01-11T20:42:51.187' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4007, N'Products', 16, N'Delete', 5, CAST(N'2020-01-11T20:42:52.930' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4008, N'Products', 16, N'Delete', 5, CAST(N'2020-01-11T20:43:01.537' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4009, N'Products', 15, N'Delete', 5, CAST(N'2020-01-11T20:44:10.143' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4010, N'Products', 16, N'Update', 5, CAST(N'2020-01-12T15:39:57.403' AS DateTime), N'{"ProductID":16,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4011, N'Products', 14, N'Update', 5, CAST(N'2020-01-12T15:47:18.990' AS DateTime), N'{"ProductID":14,"UsageTypeID":1003,"ProductName":"core 2","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4012, N'Products', 15, N'Update', 5, CAST(N'2020-01-12T15:47:32.127' AS DateTime), N'{"ProductID":15,"UsageTypeID":1003,"ProductName":"core 5","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4013, N'Products', 16, N'Update', 5, CAST(N'2020-01-12T15:47:52.047' AS DateTime), N'{"ProductID":16,"UsageTypeID":1003,"ProductName":"core 7","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4014, N'Products', 16, N'Update', 5, CAST(N'2020-01-12T15:48:38.883' AS DateTime), N'{"ProductID":16,"UsageTypeID":1003,"ProductName":"core 7","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":1005,"PackingID":1011,"SizeID":1008,"OriginID":1028,"BrandID":1027,"Description":"PC with all device"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4015, N'Products', 13, N'Update', 5, CAST(N'2020-01-12T16:59:27.123' AS DateTime), N'{"ProductID":13,"UsageTypeID":1003,"ProductName":"cor 3","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4016, N'Products', 17, N'Insert', 5, CAST(N'2020-01-12T16:59:54.010' AS DateTime), N'{"ProductID":17,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":1112,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4017, N'Products', 18, N'Insert', 5, CAST(N'2020-01-12T17:00:01.603' AS DateTime), N'{"ProductID":18,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":1113,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4018, N'Products', 19, N'Insert', 5, CAST(N'2020-01-12T17:00:15.607' AS DateTime), N'{"ProductID":19,"UsageTypeID":1003,"ProductName":"core 4","ProductCode":1114,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4019, N'Products', 20, N'Insert', 5, CAST(N'2020-01-12T17:02:59.873' AS DateTime), N'{"ProductID":20,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":1115,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4020, N'Products', 21, N'Insert', 5, CAST(N'2020-01-12T18:33:44.047' AS DateTime), N'{"ProductID":21,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":1116,"UnitID":null,"GroupID":1004,"CategoryID":1005,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4021, N'Products', 22, N'Insert', 5, CAST(N'2020-01-12T18:36:50.063' AS DateTime), N'{"ProductID":22,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":1117,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4022, N'Products', 23, N'Insert', 5, CAST(N'2020-01-12T18:45:34.493' AS DateTime), N'{"ProductID":23,"UsageTypeID":1003,"ProductName":"core","ProductCode":1118,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4023, N'Products', 13, N'Update', 5, CAST(N'2020-01-12T21:16:27.527' AS DateTime), N'{"ProductID":13,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4024, N'Products', 24, N'Insert', 5, CAST(N'2020-01-12T21:23:25.003' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":1112,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4025, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:24:11.850' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4026, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:26:09.230' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4027, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:30:40.360' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4028, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:35:47.563' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4029, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:36:20.800' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4030, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:43:46.157' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4031, N'Products', 24, N'Update', 5, CAST(N'2020-01-12T21:44:12.950' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4032, N'Products', 16, N'Update', 5, CAST(N'2020-01-12T21:44:31.023' AS DateTime), N'{"ProductID":16,"UsageTypeID":1003,"ProductName":"core 7","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":null,"PackingID":1011,"SizeID":1008,"OriginID":1028,"BrandID":1027,"Description":"PC with all device"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4033, N'Products', 15, N'Update', 5, CAST(N'2020-01-12T21:44:40.550' AS DateTime), N'{"ProductID":15,"UsageTypeID":1003,"ProductName":"core 5","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4034, N'Products', 14, N'Update', 5, CAST(N'2020-01-12T21:44:49.847' AS DateTime), N'{"ProductID":14,"UsageTypeID":1003,"ProductName":"core 2","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (4035, N'Products', 13, N'Update', 5, CAST(N'2020-01-12T21:44:59.483' AS DateTime), N'{"ProductID":13,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":0,"UnitID":null,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (5006, N'Products', 16, N'Update', 5, CAST(N'2020-01-15T21:37:19.633' AS DateTime), N'{"ProductID":16,"UsageTypeID":1003,"ProductName":"core 7","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":1005,"PackingID":1011,"SizeID":1008,"OriginID":1028,"BrandID":1027,"Description":"PC with all device"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (6006, N'Products', 15, N'Update', 5, CAST(N'2020-01-18T21:14:28.730' AS DateTime), N'{"ProductID":15,"UsageTypeID":1003,"ProductName":"core 5","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (6007, N'Products', 14, N'Update', 5, CAST(N'2020-01-18T21:14:41.857' AS DateTime), N'{"ProductID":14,"UsageTypeID":1003,"ProductName":"core 2","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (6008, N'Products', 13, N'Update', 5, CAST(N'2020-01-18T21:14:52.730' AS DateTime), N'{"ProductID":13,"UsageTypeID":1003,"ProductName":"core 3","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (6009, N'Products', 24, N'Update', 5, CAST(N'2020-01-18T21:15:02.737' AS DateTime), N'{"ProductID":24,"UsageTypeID":1003,"ProductName":"Printer","ProductCode":0,"UnitID":27,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":1026,"Description":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7006, N'StockIns', 1, N'Insert', 5, CAST(N'2020-01-19T19:57:42.617' AS DateTime), N'{"StockInID":1,"M7Number":1,"StockInDate":"2020-01-19T00:00:00","OrderNumber":1,"OrderDate":"2020-01-19T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"de","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-19T19:57:38.5876331+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7007, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-19T20:00:14.230' AS DateTime), N'{"ID":0,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7008, N'StockIns', 2, N'Insert', 5, CAST(N'2020-01-19T20:06:02.367' AS DateTime), N'{"StockInID":2,"M7Number":1,"StockInDate":"2020-01-19T00:00:00","OrderNumber":1,"OrderDate":"2020-01-19T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"csd","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-19T20:06:02.3507949+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7009, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-19T20:06:02.413' AS DateTime), N'{"ID":0,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":23,"UnitPrice":2,"TotalPrice":0,"Remarks":"sdaf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7010, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-19T20:06:02.430' AS DateTime), N'{"ID":0,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":3,"UnitPrice":12,"TotalPrice":0,"Remarks":"sdf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7011, N'StockIns', 3, N'Insert', 5, CAST(N'2020-01-19T20:41:01.540' AS DateTime), N'{"StockInID":3,"M7Number":1,"StockInDate":"2020-01-19T00:00:00","OrderNumber":1,"OrderDate":"2020-01-19T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"KK","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-19T20:41:01.1718395+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (7012, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-19T20:41:01.590' AS DateTime), N'{"ID":0,"StockInID":0,"ProductCode":1,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":12,"TotalPrice":0,"Remarks":"LL"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8010, N'StockIns', 6, N'Insert', 5, CAST(N'2020-01-20T22:54:20.143' AS DateTime), N'{"StockInID":6,"M7Number":"1","StockInDate":"2020-01-20T00:00:00","OrderNumber":"1","OrderDate":"2020-01-20T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"Report description","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-20T22:54:19.6217698+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8011, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-20T22:54:26.567' AS DateTime), N'{"ID":0,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":12,"TotalPrice":0,"Remarks":"sdf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8012, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-20T22:54:55.007' AS DateTime), N'{"ID":0,"StockInID":0,"ProductCode":111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":2,"UnitPrice":12,"TotalPrice":0,"Remarks":"sdf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8031, N'StockIns', 16, N'Insert', 5, CAST(N'2020-01-21T13:30:59.893' AS DateTime), N'{"StockInID":16,"M7Number":"1","StockInDate":"2020-01-21T00:00:00","OrderNumber":"1","OrderDate":"2020-01-21T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"sdfdsf","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-21T13:30:59.8850236+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8032, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-21T13:30:59.937' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"ss"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8033, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-21T13:31:32.727' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":3,"UnitPrice":2,"TotalPrice":0,"Remarks":"df"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8034, N'StockIns', 17, N'Insert', 5, CAST(N'2020-01-21T13:33:41.300' AS DateTime), N'{"StockInID":17,"M7Number":"1","StockInDate":"2020-01-21T00:00:00","OrderNumber":"1","OrderDate":"2020-01-21T00:00:00","DeliveryPlace":"1","ContractorName":"2","Details":"sdfdsf","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-21T13:33:41.2936403+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8035, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-21T13:33:41.307' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":10,"UnitPrice":1,"TotalPrice":0,"Remarks":"sdfd"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (8036, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-21T13:34:05.397' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":10,"UnitPrice":12,"TotalPrice":0,"Remarks":"dfgfhdfg"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9006, N'StockIns', 1004, N'Insert', 5, CAST(N'2020-01-23T10:12:23.030' AS DateTime), N'{"StockInID":1004,"M7Number":"1","StockInDate":"2020-01-23T00:00:00","OrderNumber":"1","OrderDate":"2020-01-23T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"q","ScanPath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-23T10:12:23.0035641+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
GO
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9007, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-23T10:12:23.110' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"ll"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9008, N'StockIns', 1005, N'Insert', 5, CAST(N'2020-01-23T10:29:48.163' AS DateTime), N'{"StockInID":1005,"M7Number":"1","StockInDate":"2020-01-23T00:00:00","OrderNumber":"2","OrderDate":"2020-01-23T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"sd","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-23T10:29:47.831098+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9009, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-23T10:29:48.247' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"aas"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9010, N'StockIns', 1006, N'Insert', 5, CAST(N'2020-01-23T10:34:25.820' AS DateTime), N'{"StockInID":1006,"M7Number":"1","StockInDate":"2020-01-23T00:00:00","OrderNumber":"1","OrderDate":"2020-01-23T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"ll","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-23T10:34:25.4209041+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9011, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-23T10:34:25.867' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"ll"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9012, N'StockIns', 1007, N'Insert', 5, CAST(N'2020-01-23T10:36:37.773' AS DateTime), N'{"StockInID":1007,"M7Number":"1","StockInDate":"2020-01-23T00:00:00","OrderNumber":"1","OrderDate":"2020-01-23T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"ll","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-23T10:36:37.3930975+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9013, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-23T10:36:37.823' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"l"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9016, N'StockIns', 1009, N'Insert', 5, CAST(N'2020-01-23T11:57:51.013' AS DateTime), N'{"StockInID":1009,"M7Number":"1","StockInDate":"2020-01-23T00:00:00","OrderNumber":"1","OrderDate":"2020-01-23T00:00:00","DeliveryPlace":"11","ContractorName":"1","Details":"asdfsdf","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-23T11:57:50.6049525+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9017, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-23T11:57:51.067' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"xc"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9026, N'StockIns', 1016, N'Insert', 5, CAST(N'2020-01-24T08:29:07.020' AS DateTime), N'{"StockInID":1016,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"Khan","Details":"ssdf","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T08:29:06.9601149+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9027, N'StockIns', 1017, N'Insert', 5, CAST(N'2020-01-24T14:59:25.010' AS DateTime), N'{"StockInID":1017,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"kk","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T14:59:24.9964329+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9028, N'StockIns', 1018, N'Insert', 5, CAST(N'2020-01-24T14:59:32.793' AS DateTime), N'{"StockInID":1018,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"kk","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T14:59:32.7917302+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9030, N'StockIns', 1020, N'Insert', 5, CAST(N'2020-01-24T16:03:37.680' AS DateTime), N'{"StockInID":1020,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"KK","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T16:03:37.6638445+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (9031, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T16:03:40.157' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"K"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10006, N'StockIns', 2005, N'Insert', 5, CAST(N'2020-01-24T20:12:25.800' AS DateTime), N'{"StockInID":2005,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"mm","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T20:12:25.6198948+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10007, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T20:12:25.850' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"kk"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10008, N'StockIns', 2006, N'Insert', 5, CAST(N'2020-01-24T20:15:06.260' AS DateTime), N'{"StockInID":2006,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"mm","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T20:15:06.2597682+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10009, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T20:15:06.260' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"kk"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10010, N'StockIns', 2007, N'Insert', 5, CAST(N'2020-01-24T20:16:42.420' AS DateTime), N'{"StockInID":2007,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"mmm","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T20:16:42.4184634+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10011, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T20:16:42.420' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"kk"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10012, N'StockIns', 2008, N'Insert', 5, CAST(N'2020-01-24T20:17:56.510' AS DateTime), N'{"StockInID":2008,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"kk","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T20:17:56.5092035+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10013, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T20:17:56.513' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"k"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10014, N'StockIns', 2009, N'Insert', 5, CAST(N'2020-01-24T22:10:16.760' AS DateTime), N'{"StockInID":2009,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"11","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T22:10:16.7499487+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10015, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T22:10:19.510' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10016, N'StockIns', 2010, N'Insert', 5, CAST(N'2020-01-24T22:19:41.793' AS DateTime), N'{"StockInID":2010,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T22:19:41.7819012+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10017, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T22:19:42.630' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10018, N'StockIns', 2011, N'Insert', 5, CAST(N'2020-01-24T22:36:19.177' AS DateTime), N'{"StockInID":2011,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T22:36:19.1705477+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10019, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T22:36:22.007' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10020, N'StockIns', 2012, N'Insert', 5, CAST(N'2020-01-24T22:48:10.573' AS DateTime), N'{"StockInID":2012,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T22:48:09.455451+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10021, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T22:48:13.223' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10022, N'StockIns', 2013, N'Insert', 5, CAST(N'2020-01-24T22:50:32.510' AS DateTime), N'{"StockInID":2013,"M7Number":"11","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T22:50:32.5057165+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10023, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T22:50:36.527' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10024, N'StockIns', 2014, N'Insert', 5, CAST(N'2020-01-24T22:53:09.643' AS DateTime), N'{"StockInID":2014,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T22:53:09.6407701+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10025, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-24T22:53:11.430' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10026, N'ReceiptReport', 1016, N'Delete', 5, CAST(N'2020-01-25T14:58:52.380' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10027, N'ReceiptReport', 1016, N'Delete', 5, CAST(N'2020-01-25T14:59:25.630' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10028, N'ReceiptReport', 1017, N'Delete', 5, CAST(N'2020-01-25T15:03:32.530' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10029, N'ReceiptReport', 1018, N'Delete', 5, CAST(N'2020-01-25T15:12:07.223' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10030, N'ReceiptReport', 1020, N'Delete', 5, CAST(N'2020-01-25T15:15:20.663' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10031, N'ReceiptReport', 1020, N'Delete', 5, CAST(N'2020-01-25T15:17:40.927' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10032, N'ReceiptReport', 2005, N'Delete', 5, CAST(N'2020-01-25T15:32:58.903' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10033, N'StockIns', 2006, N'Update', 5, CAST(N'2020-01-26T11:37:33.657' AS DateTime), N'{"StockInID":2006,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"mm","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-01-26T11:37:33.6507628+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10034, N'ReceiptReportItems', 2006, N'Update', 5, CAST(N'2020-01-26T11:37:33.740' AS DateTime), N'{"ID":2006,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":"core 7","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":1,"Remarks":"kk"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10035, N'StockIns', 2006, N'Update', 5, CAST(N'2020-01-26T11:39:30.090' AS DateTime), N'{"StockInID":2006,"M7Number":"1","StockInDate":"2020-01-24T00:00:00","OrderNumber":"1","OrderDate":"2020-01-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"mm","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-24T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-01-26T11:39:30.0773486+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10036, N'ReceiptReportItems', 2006, N'Update', 5, CAST(N'2020-01-26T11:39:30.113' AS DateTime), N'{"ID":2006,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":"core 7","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":1,"Remarks":"kk"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10037, N'StockIns', 1, N'Insert', 5, CAST(N'2020-01-26T13:59:59.737' AS DateTime), N'{"StockInID":1,"M7Number":"1","StockInDate":"2020-01-26T00:00:00","OrderNumber":"1","OrderDate":"2020-01-26T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"asdad","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-26T13:59:59.7247459+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10038, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:00:01.677' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"dd"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10039, N'StockIns', 2, N'Insert', 5, CAST(N'2020-01-26T14:01:16.343' AS DateTime), N'{"StockInID":2,"M7Number":"2","StockInDate":"2020-01-26T00:00:00","OrderNumber":"2","OrderDate":"2020-01-26T00:00:00","DeliveryPlace":"2","ContractorName":"2","Details":"asda","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-26T14:01:16.3312847+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10040, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:01:20.183' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":2,"UnitPrice":2,"TotalPrice":0,"Remarks":"2d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10041, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:01:20.243' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":2,"UnitPrice":2,"TotalPrice":0,"Remarks":"de"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10042, N'StockIns', 3, N'Insert', 5, CAST(N'2020-01-26T14:02:44.983' AS DateTime), N'{"StockInID":3,"M7Number":"3","StockInDate":"2020-01-26T00:00:00","OrderNumber":"3","OrderDate":"2020-01-26T00:00:00","DeliveryPlace":"3","ContractorName":"3","Details":"zdf","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-26T14:02:44.9797042+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10043, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:02:48.810' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10044, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:02:48.817' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1111,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"1d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10045, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:02:48.823' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":11,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10046, N'StockIns', 3, N'Update', 5, CAST(N'2020-01-26T14:04:44.913' AS DateTime), N'{"StockInID":3,"M7Number":"3","StockInDate":"2020-01-26T00:00:00","OrderNumber":"3","OrderDate":"2020-01-26T00:00:00","DeliveryPlace":"3","ContractorName":"3","Details":"zdfasdada","FilePath":"/Uploads/StockInScan/3.jpeg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-26T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-01-26T14:04:41.290758+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10047, N'StockInItems', 4, N'Update', 5, CAST(N'2020-01-26T14:06:24.187' AS DateTime), N'{"ID":4,"UsageTypeID":1003,"StockInID":3,"ProductCode":111,"ProductName":"core 5","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":1,"Remarks":"1d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10048, N'StockInItems', 5, N'Update', 5, CAST(N'2020-01-26T14:06:32.793' AS DateTime), N'{"ID":5,"UsageTypeID":1003,"StockInID":3,"ProductCode":1111,"ProductName":"core 7","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":1,"Remarks":"1d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10049, N'StockInItems', 6, N'Update', 5, CAST(N'2020-01-26T14:06:40.247' AS DateTime), N'{"ID":6,"UsageTypeID":1003,"StockInID":3,"ProductCode":11,"ProductName":"core 2","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"دانه","Quantity":1,"UnitPrice":1,"TotalPrice":1,"Remarks":"d"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10050, N'Products', 1, N'Insert', 5, CAST(N'2020-01-26T14:46:26.180' AS DateTime), N'{"ProductID":1,"UsageTypeID":1003,"ProductName":"corei 7","ProductCode":1,"UnitID":24,"GroupID":1004,"CategoryID":1005,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":"With all equpment"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10051, N'StockIns', 1, N'Insert', 5, CAST(N'2020-01-26T14:52:48.797' AS DateTime), N'{"StockInID":1,"M7Number":"1","StockInDate":"2020-01-26T00:00:00","OrderNumber":"1","OrderDate":"2020-01-26T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"des","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-26T14:52:47.3209882+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10052, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-01-26T14:53:37.187' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"پایه ","Quantity":1,"UnitPrice":1,"TotalPrice":0,"Remarks":"des"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10053, N'StockIns', 1, N'Update', 5, CAST(N'2020-01-26T14:58:57.567' AS DateTime), N'{"StockInID":1,"M7Number":"1","StockInDate":"2020-01-26T00:00:00","OrderNumber":"1","OrderDate":"2020-01-26T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"des","FilePath":"/Uploads/StockInScan/1.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-01-26T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-01-26T14:58:57.1095002+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (10054, N'StockInItems', 1, N'Update', 5, CAST(N'2020-01-26T14:59:40.223' AS DateTime), N'{"ID":1,"UsageTypeID":1003,"StockInID":0,"ProductCode":1,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"پایه ","Quantity":12,"UnitPrice":10,"TotalPrice":0,"Remarks":"des"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (11006, N'Products', 1, N'Update', 5, CAST(N'2020-01-31T21:24:17.740' AS DateTime), N'{"ProductID":1,"UsageTypeID":1003,"ProductName":"corei 7","ProductCode":0,"UnitID":24,"GroupID":1004,"CategoryID":null,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":"With all equpment"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (11007, N'Products', 1, N'Update', 5, CAST(N'2020-01-31T21:24:28.900' AS DateTime), N'{"ProductID":1,"UsageTypeID":1003,"ProductName":"corei 7","ProductCode":0,"UnitID":24,"GroupID":1004,"CategoryID":1005,"PackingID":null,"SizeID":null,"OriginID":null,"BrandID":null,"Description":"With all equpment"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (11008, N'Products', 1, N'Update', 5, CAST(N'2020-01-31T21:46:50.250' AS DateTime), N'{"ProductID":1,"UsageTypeID":1003,"ProductName":"corei 7","ProductCode":0,"UnitID":24,"GroupID":1004,"CategoryID":1005,"PackingID":1011,"SizeID":1007,"OriginID":1020,"BrandID":1026,"Description":"With all equpment"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12006, N'Ticket', 1006, N'Insert', 5, CAST(N'2020-02-03T10:58:34.560' AS DateTime), N'{"DistributionID":1006,"TicketNumber":1,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-02-03T00:00:00","EmployeeID":0,"Details":"سی","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-03T10:47:53.5694004+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12007, N'Ticket', 1007, N'Insert', 5, CAST(N'2020-02-03T11:05:54.217' AS DateTime), N'{"DistributionID":1007,"TicketNumber":1,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-02-03T00:00:00","EmployeeID":0,"Details":"یسب","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-03T11:04:10.7031656+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12008, N'Ticket', 1008, N'Insert', 5, CAST(N'2020-02-03T11:07:02.867' AS DateTime), N'{"DistributionID":1008,"TicketNumber":1,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-02-03T00:00:00","EmployeeID":0,"Details":"یی","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-03T11:06:47.6403132+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12009, N'Ticket', 1009, N'Insert', 5, CAST(N'2020-02-03T11:19:20.170' AS DateTime), N'{"DistributionID":1009,"TicketNumber":1,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-02-03T00:00:00","EmployeeID":0,"Details":"ث","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-03T11:10:43.7334976+04:30","InsertedDateForm":null,"LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12012, N'Products', 2, N'Insert', 5, CAST(N'2020-02-04T11:02:21.830' AS DateTime), N'{"ProductID":2,"UsageTypeID":1003,"ProductName":"Scanner hp","ProductCode":2,"UnitID":24,"GroupID":1004,"CategoryID":1005,"PackingID":1011,"SizeID":null,"OriginID":1028,"BrandID":1026,"Description":"kjhkh"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12013, N'StockIns', 2, N'Insert', 5, CAST(N'2020-02-04T11:06:16.667' AS DateTime), N'{"StockInID":2,"M7Number":"2","StockInDate":"2020-02-04T00:00:00","OrderNumber":"3","OrderDate":"2020-02-04T00:00:00","DeliveryPlace":"warehouse 1","ContractorName":"Khan","Details":"sdfsd","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-04T11:06:16.6429479+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12014, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-02-04T11:06:16.720' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":2,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"پایه ","Quantity":1,"UnitPrice":10000,"TotalPrice":0,"Remarks":"dsfdsf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12015, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-02-04T11:06:16.783' AS DateTime), N'{"ID":0,"UsageTypeID":1003,"StockInID":0,"ProductCode":1,"ProductName":null,"UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"UnitName":"پایه ","Quantity":2,"UnitPrice":1000,"TotalPrice":0,"Remarks":"sdfs"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12019, N'DurableStockIns', 6, N'Insert', 5, CAST(N'2020-02-24T14:46:14.660' AS DateTime), N'{"StockInID":6,"M7Number":"1","StockInDate":"2020-02-24T00:00:00","OrderNumber":"11","OrderDate":"2020-02-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"asd","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-24T14:46:14.6482927+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12020, N'DurableStockIns', 7, N'Insert', 5, CAST(N'2020-02-24T15:08:54.453' AS DateTime), N'{"StockInID":7,"M7Number":"1","StockInDate":"2020-02-24T00:00:00","OrderNumber":"1","OrderDate":"2020-02-24T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"zxc","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-24T15:08:54.4456788+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12021, N'DurableStockIns', 8, N'Insert', 5, CAST(N'2020-02-25T13:40:18.220' AS DateTime), N'{"StockInID":8,"M7Number":"2","StockInDate":"2020-02-25T00:00:00","OrderNumber":"123","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"Ware house 1","ContractorName":"Jamal Khan","Details":"Some text","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T13:40:18.0697881+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12022, N'DurableStockIns', 9, N'Insert', 5, CAST(N'2020-02-25T13:43:48.563' AS DateTime), N'{"StockInID":9,"M7Number":"5","StockInDate":"2020-02-25T00:00:00","OrderNumber":"5","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"Warehouse 2","ContractorName":"Hammad","Details":"sadfdsaf","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T13:43:48.5566313+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12023, N'DurableStockIns', 10, N'Insert', 5, CAST(N'2020-02-25T13:57:21.517' AS DateTime), N'{"StockInID":10,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"xcdfv","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T13:57:21.5136646+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12024, N'DurableStockInItems', 0, N'Insert', 5, CAST(N'2020-02-25T13:57:35.357' AS DateTime), N'{"ID":0,"UsageTypeID":0,"StockInID":0,"ProductCode":1,"ProductName":"wew","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1017,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":23,"Price":2000,"Remarks":"xvcxz"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12025, N'DurableStockIns', 11, N'Insert', 5, CAST(N'2020-02-25T13:58:52.193' AS DateTime), N'{"StockInID":11,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"asdf","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T13:58:52.1878558+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12026, N'DurableStockInItems', 0, N'Insert', 5, CAST(N'2020-02-25T13:58:55.217' AS DateTime), N'{"ID":0,"UsageTypeID":0,"StockInID":0,"ProductCode":123,"ProductName":"qqq","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1017,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":3,"Price":11111,"Remarks":"saasd"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12029, N'DurableStockIns', 10, N'Update', 5, CAST(N'2020-02-25T14:11:35.520' AS DateTime), N'{"StockInID":10,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"xcdfv","FilePath":"/Uploads/StockInScan/10.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-02-25T14:11:35.5126132+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12030, N'DurableStockInItems', 4, N'Update', 5, CAST(N'2020-02-25T14:11:35.543' AS DateTime), N'{"ID":4,"UsageTypeID":0,"StockInID":0,"ProductCode":1,"ProductName":"Printer","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":0,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1017,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":23,"Price":2000,"Remarks":"xvcxz"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12031, N'DurableStockIns', 10, N'Update', 5, CAST(N'2020-02-25T14:27:14.703' AS DateTime), N'{"StockInID":10,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"xcdfv","FilePath":"/Uploads/StockInScan/10.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-02-25T14:27:14.5687957+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12032, N'DurableStockInItems', 4, N'Update', 5, CAST(N'2020-02-25T14:27:14.763' AS DateTime), N'{"ID":4,"UsageTypeID":0,"StockInID":0,"ProductCode":1,"ProductName":"Printer","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":0,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1017,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":23,"Price":2000,"Remarks":"xvcxz"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12033, N'ReceiptReport', 1, N'Delete', 5, CAST(N'2020-02-25T14:31:03.033' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12034, N'DurableStockIns', 2, N'Update', 5, CAST(N'2020-02-25T14:42:49.913' AS DateTime), N'{"StockInID":2,"M7Number":"2","StockInDate":"2020-02-04T00:00:00","OrderNumber":"3","OrderDate":"2020-02-04T00:00:00","DeliveryPlace":"warehouse 1","ContractorName":"Khan","Details":"sdfsd","FilePath":"/Uploads/StockInScan/2.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-04T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-02-25T14:42:49.9087683+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12035, N'DurableStockInItems', 2, N'Update', 5, CAST(N'2020-02-25T14:42:49.937' AS DateTime), N'{"ID":2,"UsageTypeID":1003,"StockInID":2,"ProductCode":2,"ProductName":"some thing","SizeName":"بزرگ","SizeID":1007,"CategoryName":"کمپیوتر","CategoryID":1005,"GroupName":"اجناس برقی","GroupID":1004,"OriginName":"چین","OriginID":1021,"BrandName":" HP","BrandID":1026,"UnitName":"پایه ","UnitID":24,"Price":10000,"Remarks":"dsfdsf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12036, N'DurableStockInItems', 3, N'Update', 5, CAST(N'2020-02-25T14:42:49.937' AS DateTime), N'{"ID":3,"UsageTypeID":1003,"StockInID":2,"ProductCode":1,"ProductName":"some thing","SizeName":"بزرگ","SizeID":1007,"CategoryName":"کمپیوتر","CategoryID":1005,"GroupName":"اجناس برقی","GroupID":1004,"OriginName":"چین","OriginID":1021,"BrandName":" HP","BrandID":1026,"UnitName":"پایه ","UnitID":24,"Price":1000,"Remarks":"sdfs"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12037, N'DurableStockIns', 2, N'Update', 5, CAST(N'2020-02-25T14:43:17.850' AS DateTime), N'{"StockInID":2,"M7Number":"2","StockInDate":"2020-02-04T00:00:00","OrderNumber":"3","OrderDate":"2020-02-04T00:00:00","DeliveryPlace":"warehouse 1","ContractorName":"Khan","Details":"sdfsd","FilePath":"/Uploads/StockInScan/2.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-04T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-02-25T14:43:17.8473091+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12038, N'DurableStockInItems', 2, N'Update', 5, CAST(N'2020-02-25T14:43:17.857' AS DateTime), N'{"ID":2,"UsageTypeID":1003,"StockInID":2,"ProductCode":2,"ProductName":"some thing","SizeName":"بزرگ","SizeID":1007,"CategoryName":"کمپیوتر","CategoryID":1005,"GroupName":"اجناس برقی","GroupID":1004,"OriginName":"چین","OriginID":1021,"BrandName":" HP","BrandID":1026,"UnitName":"پایه ","UnitID":24,"Price":10000,"Remarks":"dsfdsf"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12039, N'NondurableStockIns', 1, N'Insert', 5, CAST(N'2020-02-29T11:25:43.300' AS DateTime), N'{"StockInID":1,"M7Number":"1","StockInDate":"2020-02-29T00:00:00","OrderNumber":"1","OrderDate":"2020-02-29T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-29T11:25:38.2754968+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (12040, N'NondurableStockInItems', 0, N'Insert', 5, CAST(N'2020-02-29T11:25:52.993' AS DateTime), N'{"ID":0,"StockInID":0,"ProductName":"1","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1018,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":24,"Quantity":1,"UnitPrice":1,"Remarks":"1"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (13039, N'DurableStockIns', 11, N'Update', 5, CAST(N'2020-03-02T07:34:02.243' AS DateTime), N'{"StockInID":11,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"asdf","FilePath":"/Uploads/StockInScan/11.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-03-02T07:34:02.2240028+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (13040, N'DurableStockInItems', 5, N'Update', 5, CAST(N'2020-03-02T07:34:02.390' AS DateTime), N'{"ID":5,"UsageTypeID":0,"StockInID":0,"ProductCode":123,"ProductName":"fdsfd","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1017,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":3,"Price":11111,"Remarks":"saasd"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (13041, N'DurableStockIns', 11, N'Update', 5, CAST(N'2020-03-02T10:44:57.010' AS DateTime), N'{"StockInID":11,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"asdfsdfdsf","FilePath":"/Uploads/StockInScan/11.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-03-02T10:44:57.0082087+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (13042, N'DurableStockInItems', 5, N'Update', 5, CAST(N'2020-03-02T10:44:57.013' AS DateTime), N'{"ID":5,"UsageTypeID":0,"StockInID":11,"ProductCode":123,"ProductName":"fdsfd","SizeName":"بزرگ","SizeID":1007,"CategoryName":"کمپیوتر","CategoryID":1005,"GroupName":"اجناس برقی","GroupID":1004,"OriginName":"افغانستان","OriginID":1017,"BrandName":" HP","BrandID":1026,"UnitName":"کیلوګرام","UnitID":3,"Price":11111,"Remarks":"saasd"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (13043, N'DurableStockIns', 10, N'Update', 5, CAST(N'2020-03-02T15:08:09.870' AS DateTime), N'{"StockInID":10,"M7Number":"1","StockInDate":"2020-02-25T00:00:00","OrderNumber":"1","OrderDate":"2020-02-25T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"xcdfv","FilePath":"/Uploads/StockInScan/10.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-25T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-03-02T15:08:09.8624895+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (13044, N'DurableStockInItems', 4, N'Update', 5, CAST(N'2020-03-02T15:08:09.910' AS DateTime), N'{"ID":4,"UsageTypeID":0,"StockInID":0,"ProductCode":1,"ProductName":"Printer","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"GroupID":1004,"OriginName":null,"OriginID":1017,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":23,"Price":2000,"Remarks":"xvcxz"}')
GO
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14039, N'StockIns', 1, N'Update', 5, CAST(N'2020-03-05T05:55:19.287' AS DateTime), N'{"StockInID":1,"M7Number":"1","StockInDate":"2020-02-29T00:00:00","OrderNumber":"1","OrderDate":"2020-02-29T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-29T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-03-05T05:55:19.0640703+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14040, N'StockInItems', 1, N'Update', 5, CAST(N'2020-03-05T05:55:19.360' AS DateTime), N'{"ID":1,"StockInID":0,"ProductName":"1","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"ProductCode":"12","UsageTypeID":1003,"GroupID":1004,"OriginName":null,"OriginID":1018,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":24,"Quantity":1,"UnitPrice":1,"Remarks":"11"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14041, N'StockIns', 1, N'Update', 5, CAST(N'2020-03-05T06:27:47.353' AS DateTime), N'{"StockInID":1,"M7Number":"1","StockInDate":"2020-02-29T00:00:00","OrderNumber":"1","OrderDate":"2020-02-29T00:00:00","DeliveryPlace":"1","ContractorName":"1","Details":"1","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-02-29T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-03-05T06:27:47.1401595+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14042, N'StockInItems', 1, N'Update', 5, CAST(N'2020-03-05T06:27:47.423' AS DateTime), N'{"ID":1,"StockInID":0,"ProductName":"13","SizeName":null,"SizeID":1007,"CategoryName":null,"CategoryID":1005,"GroupName":null,"ProductCode":"33","UsageTypeID":1003,"GroupID":1004,"OriginName":null,"OriginID":1018,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":24,"Quantity":33,"UnitPrice":33,"Remarks":"113"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14043, N'StockIns', 2, N'Insert', 5, CAST(N'2020-03-27T19:16:31.437' AS DateTime), N'{"StockInID":2,"M7Number":"3","StockInDate":"2020-03-27T00:00:00","OrderNumber":"8","OrderDate":"2020-03-27T00:00:00","DeliveryPlace":"Godam","ContractorName":"Khan ahmad","Details":"Some comments","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-03-27T19:16:31.4191807+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14044, N'StockInItems', 0, N'Insert', 5, CAST(N'2020-03-27T19:16:31.477' AS DateTime), N'{"ID":0,"StockInID":0,"ProductName":"camera","SizeName":null,"SizeID":null,"CategoryName":null,"CategoryID":1005,"GroupName":null,"ProductCode":"234","UsageTypeID":1003,"GroupID":1004,"OriginName":null,"OriginID":1018,"BrandName":null,"BrandID":1026,"UnitName":null,"UnitID":27,"Quantity":23,"AvailableQuantity":null,"UnitPrice":120,"Remarks":"For office use"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14057, N'Distribution', 1018, N'Insert', 5, CAST(N'2020-04-10T22:46:25.627' AS DateTime), N'{"DistributionID":1018,"TicketNumber":1,"TicketIssuedDate":null,"Warehouse":"q","RequestNumber":2,"RequestDate":"2020-04-10T00:00:00","EmployeeID":1,"Details":"Details","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-04-10T22:46:25.6143838+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14058, N'DistributionItem', 2, N'Insert', 5, CAST(N'2020-04-10T22:46:25.677' AS DateTime), N'{"ID":2,"ItemID":0,"DistributionID":0,"UnitName":"دانه","UsageTypeID":0,"ProductCode":null,"ProductName":"camera","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"Quantity":3,"AvailableQuantity":0,"UnitPrice":120,"TotalPrice":0,"DealWithAccount":"2"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14060, N'Distribution', 1020, N'Insert', 5, CAST(N'2020-04-11T16:29:59.777' AS DateTime), N'{"DistributionID":1020,"TicketNumber":2,"TicketIssuedDate":null,"Warehouse":"2","RequestNumber":3,"RequestDate":"2020-04-11T00:00:00","EmployeeID":1,"Details":"Details","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-04-11T16:29:59.7688177+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14061, N'DistributionItem', 2, N'Insert', 5, CAST(N'2020-04-11T16:30:02.143' AS DateTime), N'{"ID":2,"ItemID":0,"DistributionID":0,"UnitName":"دانه","UsageTypeID":0,"ProductCode":null,"ProductName":"camera","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"Quantity":20,"AvailableQuantity":0,"UnitPrice":120,"TotalPrice":0,"DealWithAccount":"2"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14062, N'Distribution', 1021, N'Insert', 5, CAST(N'2020-05-12T11:16:59.290' AS DateTime), N'{"DistributionID":1021,"TicketNumber":1,"TicketIssuedDate":null,"Warehouse":"sd","RequestNumber":22,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"cd","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-12T11:16:59.2851304+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14063, N'DistributionItem', 1, N'Insert', 5, CAST(N'2020-05-12T11:16:59.327' AS DateTime), N'{"ID":1,"ItemID":0,"DistributionID":0,"UnitName":"پایه ","UsageTypeID":1003,"ProductCode":"33","ProductName":"Computer","UsageTypeName":"دوامدار","ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"Quantity":33,"AvailableQuantity":0,"UnitPrice":33,"TotalPrice":0,"DealWithAccount":"q"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14064, N'Distribution', 1022, N'Insert', 5, CAST(N'2020-05-12T11:58:25.513' AS DateTime), N'{"DistributionID":1022,"TicketNumber":2,"TicketIssuedDate":null,"Warehouse":"2","RequestNumber":2,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"some text","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-12T11:58:25.3389258+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14065, N'DistributionItem', 2, N'Insert', 5, CAST(N'2020-05-12T11:58:25.543' AS DateTime), N'{"ID":2,"ItemID":0,"DistributionID":0,"UnitName":"دانه","UsageTypeID":0,"ProductCode":null,"ProductName":"camera","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"Quantity":2,"AvailableQuantity":0,"UnitPrice":120,"TotalPrice":0,"DealWithAccount":"2"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14067, N'Distribution', 1024, N'Insert', 5, CAST(N'2020-05-12T14:46:19.363' AS DateTime), N'{"DistributionID":1024,"TicketNumber":1,"TicketIssuedDate":null,"Warehouse":"1","RequestNumber":1,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"dd","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-12T14:46:19.346992+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14068, N'DistributionItem', 2, N'Insert', 5, CAST(N'2020-05-12T14:46:38.797' AS DateTime), N'{"ID":2,"ItemID":2,"DistributionID":0,"UnitName":"دانه","UsageTypeID":0,"ProductCode":null,"ProductName":"camera","UsageTypeName":null,"ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"Quantity":2,"AvailableQuantity":0,"UnitPrice":120,"TotalPrice":0,"DealWithAccount":"3"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14069, N'Distribution', 1025, N'Insert', 5, CAST(N'2020-05-12T14:51:53.573' AS DateTime), N'{"DistributionID":1025,"TicketNumber":2,"TicketIssuedDate":null,"Warehouse":"1","RequestNumber":1,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"weq","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-12T14:51:52.3990626+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14070, N'DistributionItem', 1, N'Insert', 5, CAST(N'2020-05-12T14:52:09.573' AS DateTime), N'{"ID":1,"ItemID":1,"DistributionID":0,"UnitName":"پایه ","UsageTypeID":1003,"ProductCode":"33","ProductName":"Computer","UsageTypeName":"دوامدار","ProductSizeName":null,"ProductCategoryName":null,"ProductGroupName":null,"ProductOriginName":null,"Quantity":1,"AvailableQuantity":0,"UnitPrice":33,"TotalPrice":0,"DealWithAccount":"2"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14071, N'Distributions', 1025, N'Update', 5, CAST(N'2020-05-15T13:35:03.833' AS DateTime), N'{"DistributionID":1025,"TicketNumber":2,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"samad khan","FilePath":"/Uploads/Distribution/1025.jpeg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-12T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-15T13:35:03.7919121+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14072, N'Distributions', 1025, N'Update', 5, CAST(N'2020-05-15T17:16:52.733' AS DateTime), N'{"DistributionID":1025,"TicketNumber":2,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"samad khan","FilePath":"/Uploads/Distribution/1025.jpeg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-12T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-15T17:16:52.5692509+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14073, N'Ticket', 1025, N'Delete', 5, CAST(N'2020-05-15T17:17:39.843' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14074, N'Distributions', 1025, N'Delete', 5, CAST(N'2020-05-15T21:19:37.047' AS DateTime), N'{"DistributionID":1025,"TicketNumber":2,"TicketIssuedDate":"2020-02-03T00:00:00","Warehouse":"1","RequestNumber":1,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"samad khan","FilePath":"/Uploads/Distribution/1025.jpeg","IsActive":false,"InsertedBy":5,"InsertedDate":"2020-05-12T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-15T00:00:00"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14075, N'DistributionItems', 14, N'Delete', 5, CAST(N'2020-05-15T21:19:37.220' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14076, N'Distributions', 1018, N'Update', 5, CAST(N'2020-05-15T21:51:19.810' AS DateTime), N'{"DistributionID":1018,"TicketNumber":1,"TicketIssuedDate":"2020-05-14T00:00:00","Warehouse":"q","RequestNumber":2,"RequestDate":"2020-04-10T00:00:00","EmployeeID":1,"Details":"Details","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-04-10T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-15T21:51:19.798051+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14077, N'Distributions', 1018, N'Delete', 5, CAST(N'2020-05-15T21:57:53.393' AS DateTime), N'{"DistributionID":1018,"TicketNumber":1,"TicketIssuedDate":"2020-05-14T00:00:00","Warehouse":"q","RequestNumber":2,"RequestDate":"2020-04-10T00:00:00","EmployeeID":1,"Details":"Details","FilePath":null,"IsActive":false,"InsertedBy":5,"InsertedDate":"2020-04-10T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-15T00:00:00"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14078, N'DistributionItems', 15, N'Delete', 5, CAST(N'2020-05-15T21:58:00.233' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14079, N'Distributions', 1020, N'Update', 5, CAST(N'2020-05-16T06:17:47.037' AS DateTime), N'{"DistributionID":1020,"TicketNumber":2,"TicketIssuedDate":"2020-04-27T00:00:00","Warehouse":"2","RequestNumber":3,"RequestDate":"2020-04-11T00:00:00","EmployeeID":1,"Details":"Details","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-04-11T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-16T06:17:47.0242552+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14080, N'Distributions', 1020, N'Delete', 5, CAST(N'2020-05-16T06:18:13.503' AS DateTime), N'{"DistributionID":1020,"TicketNumber":2,"TicketIssuedDate":"2020-04-27T00:00:00","Warehouse":"2","RequestNumber":3,"RequestDate":"2020-04-11T00:00:00","EmployeeID":1,"Details":"Details","FilePath":null,"IsActive":false,"InsertedBy":5,"InsertedDate":"2020-04-11T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-16T00:00:00"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14081, N'Distributions', 1021, N'Delete', 5, CAST(N'2020-05-16T06:20:00.953' AS DateTime), N'{"DistributionID":1021,"TicketNumber":1,"TicketIssuedDate":null,"Warehouse":"sd","RequestNumber":22,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"cd","FilePath":"/Uploads/Distribution/1021.png","IsActive":false,"InsertedBy":5,"InsertedDate":"2020-05-12T00:00:00","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14082, N'Distributions', 1022, N'Delete', 5, CAST(N'2020-05-16T06:20:23.750' AS DateTime), N'{"DistributionID":1022,"TicketNumber":2,"TicketIssuedDate":null,"Warehouse":"2","RequestNumber":2,"RequestDate":"2020-05-12T00:00:00","EmployeeID":1,"Details":"some text","FilePath":"/Uploads/Distribution/1022.jpg","IsActive":false,"InsertedBy":5,"InsertedDate":"2020-05-12T00:00:00","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14083, N'Distribution', 1, N'Insert', 5, CAST(N'2020-05-18T11:17:27.323' AS DateTime), N'{"DistributionID":1,"TicketNumber":1,"TicketIssuedDate":"2020-05-18T00:00:00","Warehouse":"1","RequestNumber":12,"RequestDate":"2020-05-18T00:00:00","EmployeeID":1,"Details":"Some details for items","FilePath":null,"IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-18T11:17:27.0993767+04:30","LastUpdatedBy":null,"LastUpdatedDate":null}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14084, N'DistributionItem', 1, N'Insert', 5, CAST(N'2020-05-18T11:17:27.363' AS DateTime), N'{"ID":1,"ItemID":1,"DistributionID":0,"UnitName":"پایه ","UsageTypeID":1003,"ItemCode":"33","ItemName":"Computer","UsageTypeName":"دوامدار","ItemSizeName":null,"ItemCategoryName":null,"ItemGroupName":null,"ItemOriginName":null,"BrandName":" HP","Quantity":2,"UnitPrice":33,"TotalPrice":0,"DealWithAccount":"12"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14085, N'DistributionItem', 2, N'Insert', 5, CAST(N'2020-05-18T11:17:27.387' AS DateTime), N'{"ID":2,"ItemID":2,"DistributionID":0,"UnitName":"دانه","UsageTypeID":0,"ItemCode":null,"ItemName":"camera","UsageTypeName":null,"ItemSizeName":null,"ItemCategoryName":null,"ItemGroupName":null,"ItemOriginName":null,"BrandName":" HP","Quantity":2,"UnitPrice":120,"TotalPrice":0,"DealWithAccount":"23"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14086, N'Distributions', 1, N'Update', 5, CAST(N'2020-05-18T11:19:12.280' AS DateTime), N'{"DistributionID":1,"TicketNumber":1,"TicketIssuedDate":"2020-05-18T00:00:00","Warehouse":"1","RequestNumber":12,"RequestDate":"2020-05-18T00:00:00","EmployeeID":1,"Details":"Some details for items","FilePath":"/Uploads/Distribution/1.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-18T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-18T11:19:12.2759217+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14087, N'Distributions', 1, N'Update', 5, CAST(N'2020-05-18T11:19:29.950' AS DateTime), N'{"DistributionID":1,"TicketNumber":1,"TicketIssuedDate":"2020-05-18T00:00:00","Warehouse":"1","RequestNumber":12,"RequestDate":"2020-05-18T00:00:00","EmployeeID":1,"Details":"Some details for items2222222222222","FilePath":"/Uploads/Distribution/1.jpg","IsActive":true,"InsertedBy":5,"InsertedDate":"2020-05-18T00:00:00","LastUpdatedBy":5,"LastUpdatedDate":"2020-05-18T11:19:29.9496571+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14088, N'DistributionItems', 5, N'Returned', 5, CAST(N'2020-05-19T10:07:17.883' AS DateTime), N'{"ID":5,"DistributionID":1,"Quantity":2,"ItemID":1,"UnitPrice":33,"DealWithAccount":"12","IsReturned":true}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14089, N'StockInItems', 1, N'Return', 5, CAST(N'2020-05-19T10:07:18.420' AS DateTime), NULL)
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14090, N'DistributionItems', 6, N'Returned', 5, CAST(N'2020-05-19T13:25:08.037' AS DateTime), N'{"ID":6,"DistributionID":1,"Quantity":2,"ItemID":2,"UnitPrice":120,"DealWithAccount":"23","IsReturned":true,"ReturnDate":"2020-05-19T13:25:07.3585496+04:30"}')
INSERT [dbo].[ActivityLogs] ([LogId], [ModifiedTable], [ModfiedId], [Action], [UserId], [ModifiedTime], [ModifiedData]) VALUES (14091, N'StockInItems', 2, N'Return', 5, CAST(N'2020-05-19T13:25:08.707' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ActivityLogs] OFF
SET IDENTITY_INSERT [dbo].[AspNetRoles] ON 

INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (1, N'Administrator')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (2, N'test')
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (5, 1)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (11, 2)
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 

INSERT [dbo].[AspNetUsers] ([Id], [DisplayName], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate], [LastLogin], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [PassNeedsChange]) VALUES (5, N'Admin User', 1, NULL, NULL, NULL, NULL, CAST(N'2020-05-30T10:15:14.343' AS DateTime), NULL, 0, N'ANS3JIxVHvzi4/hTWA7RgEA7YyOGVD+iXHRjs5yiMYmJVobuBx0sADr1y+p3iMenvQ==', N'ANS3JIxVHvzi4/hTWA7RgEA7YyOGVD+iXHRjs5yiMYmJVobuBx0sADr1y+p3iMenvQ==', NULL, 0, 0, NULL, 0, 0, N'admin', 0)
INSERT [dbo].[AspNetUsers] ([Id], [DisplayName], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate], [LastLogin], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [PassNeedsChange]) VALUES (10, N'khan', 1, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'ANS3JIxVHvzi4/hTWA7RgEA7YyOGVD+iXHRjs5yiMYmJVobuBx0sADr1y+p3iMenvQ==', NULL, NULL, 0, 0, NULL, 0, 0, N'khan', 0)
INSERT [dbo].[AspNetUsers] ([Id], [DisplayName], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate], [LastLogin], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [PassNeedsChange]) VALUES (11, N'sharif', 1, 5, CAST(N'2019-08-18T19:18:48.117' AS DateTime), NULL, NULL, NULL, NULL, 0, N'AIV4Sw4D8GzIsL34IhePK+RxkwvmcwWd960EKs+R254yVJ4zUG5DEGQqW9r2xpKpZg==', N'4007a1af-f978-4127-bb5a-57df127e9501', NULL, 0, 0, NULL, 0, 0, N'sharif', 1)
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentID], [EnName], [DrName], [PaName], [ParentDepartmentID], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (1, N'Information Technology (IT)', N'ریاست تکنالوژی معلوماتی', N'معلوماتی تکنالوژې ریاست', NULL, 1, 5, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Departments] OFF
SET IDENTITY_INSERT [dbo].[DistributionItems] ON 

INSERT [dbo].[DistributionItems] ([ID], [DistributionID], [Quantity], [ItemID], [UnitPrice], [DealWithAccount], [IsReturned], [ReturnDate]) VALUES (5, 1, 2, 1, 33, N'12', 1, CAST(N'2020-05-19' AS Date))
INSERT [dbo].[DistributionItems] ([ID], [DistributionID], [Quantity], [ItemID], [UnitPrice], [DealWithAccount], [IsReturned], [ReturnDate]) VALUES (6, 1, 2, 2, 120, N'23', 1, CAST(N'2020-05-19' AS Date))
SET IDENTITY_INSERT [dbo].[DistributionItems] OFF
SET IDENTITY_INSERT [dbo].[Distributions] ON 

INSERT [dbo].[Distributions] ([DistributionID], [TicketNumber], [TicketIssuedDate], [Warehouse], [RequestNumber], [RequestDate], [EmployeeID], [Details], [FilePath], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (1, 1, CAST(N'2020-05-18T00:00:00.000' AS DateTime), N'1', 12, CAST(N'2020-05-18T00:00:00.000' AS DateTime), 1, N'Some details for items2222222222222', N'/Uploads/Distribution/1.jpg', 1, 5, CAST(N'2020-05-18' AS Date), 5, CAST(N'2020-05-18' AS Date))
SET IDENTITY_INSERT [dbo].[Distributions] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [Name], [FatherName], [DepartmentID], [Occupation], [PhoneNumber], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (1, N'Ahmad', N'جمال', 1, N'خانه سامان', N'0794671076', 1, 5, CAST(N'2020-01-03' AS Date), 5, CAST(N'2020-01-03' AS Date))
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[LookupTypes] ON 

INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (1, N'UNIT', N'UNIT', N'یونیت', N'یونیت', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (2, N'UTYPE', N'Usage Type', N'نوع استعمال', N'استعمال ډول', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (3, N'ITEMGROUP', N'Item Group', N'گروپ جنس', N'د توکي ګروپ', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (6, N'ITEMCATEGORY', N'Item Category', N'کتگوری جنس', N'د توکي کټګورۍ', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (7, N'ITEMSIZE', N'Item Size', N'اندازه جنس', N'د توکي اندازه', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (9, N'ITEMPACKAGE', N'Item Package', N'بسته بندی جنس', N'د توکی بسته بندي', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (10, N'ITEMORIGIN', N'Item Origin', N'اصل جنس', N'د توکی اصل', 1)
INSERT [dbo].[LookupTypes] ([LookupId], [LookupCode], [EnName], [DrName], [PaName], [IsActive]) VALUES (11, N'ITEMBRAND', N'Item Brand', N'نام تجارتی جنس', N'د توکی سوداګریز نوم', 1)
SET IDENTITY_INSERT [dbo].[LookupTypes] OFF
SET IDENTITY_INSERT [dbo].[LookupValues] ON 

INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (3, N'UNIT', N'Kilogram', N'Kilogram', N'کیلوګرام', N'کیلوګرام', N'a', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (23, N'UNIT', N'Gram', N'gram', N'ګرام ', N'گرام ', N'b', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (24, N'UNIT', N'Paya', N'پایه ', N'پایه ', N'پایه ', N'c', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (25, N'UNIT', N'Can', N'Can', N'قطی', N'قطی', N'd', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (26, N'UNIT', N'Dozen', N'Dozen', N'درجن', N'درجن ', N'e', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (27, N'UNIT', N'Piece', N'Piece', N'دانه', N'دانه ', N'f', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (28, N'UNIT', N'Mehrab', N'محراب', N'محراب', N'محراب ', N'g', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (29, N'UNIT', N'Toghra', N'طغرا', N'طغرا', N'طغرا', N'h', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (30, N'UNIT', N'Jold', N'جلد', N'جلد', N'جلد ', N'i', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (31, N'UNIT', N'Bundle', N'گده', N'گده', N'گده', N'j', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (32, N'UNIT', N'Mawazi', N'موازی', N'موازی', N'موازی ', N'k', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (33, N'UNIT', N'Arada', N'عراده', N'عراده', N'عراده', N'l', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (34, N'UNIT', N'Sheet', N'ورق', N'ورق', N'ورق', N'm', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (35, N'UNIT', N'Set', N'Set', N'سیت', N'سیت', N'n', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (36, N'UNIT', N'Halqa', N'حلقه', N'حلقه', N'حلقه', N'o', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (37, N'UNIT', N'Kit', N'Kit', N'بسته', N'بسته ', N'p', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (38, N'UNIT', N'Roll', N'Roll', N'رول', N'رول', N'q', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (39, N'UNIT', N'Bunch', N'دسته', N'دسته', N'دسته ', N'r', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (40, N'UNIT', N'Number', N'Number', N'عدد', N'عدد', N's', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (41, N'UNIT', N'Foot', N'Foot', N'فوټ', N'فوت', N't', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (42, N'UNIT', N'Bottle', N'Bottle', N'بوتل', N'بوتل', N'u', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1002, N'UTYPE', N'Disposable', N'Disposable', N'مصرفی', N'مصرفی', N'a', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1003, N'UTYPE', N'Reusable', N'Reusable', N'دوامدار', N'دوامدار', N'b', 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1004, N'ITEMGROUP', N'ElectricItem', N'Electric Item', N'اجناس برقی', N'برقي توکی', NULL, 1, 1003)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1005, N'ITEMCATEGORY', N'Computer', N'Computer', N'کمپیوتر', N'کمپیوټر', NULL, 1, 1004)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1007, N'ITEMSIZE', N'Large', N'Large', N'بزرگ', N'غټ', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1008, N'ITEMSIZE', N'Medium', N'Medium', N'متوسظ', N'متوسط', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1010, N'ITEMSIZE', N'Small', N'Small', N'خورد', N'وړوکي', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1011, N'ITEMPACKAGE', N'1x1', N'1x1', N'1x1', N'1x1', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1013, N'ITEMPACKAGE', N'1x2', N'1x2', N'1x2', N'1x2', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1014, N'ITEMPACKAGE', N'1x6', N'1x6', N'1x6', N'1x6', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1015, N'ITEMPACKAGE', N'1x12', N'1x12', N'1x12', N'1x12', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1016, N'ITEMPACKAGE', N'1x24', N'1x24', N'1x24', N'1x24', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1017, N'ITEMORIGIN', N'Afghanistan', N'Afghanistan', N'افغانستان', N'افغانستان', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1018, N'ITEMORIGIN', N'Pakistan', N'Pakistan', N'پاکستان', N'پاکستان', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1019, N'ITEMORIGIN', N'Turkey', N'Turkey', N'ترکی', N'ترکی', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1020, N'ITEMORIGIN', N'IRAN', N'Iran', N'ایران', N'ایران', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1021, N'ITEMORIGIN', N'CHINA', N'China', N'چین', N'چین', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1023, N'ITEMORIGIN', N'India', N'India', N'هند', N'هند', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1024, N'ITEMORIGIN', N'Japan', N'Japan', N'جاپان', N'جاپان', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1025, N'ITEMORIGIN', N'Russia', N'Russia', N'روسی', N'روسی', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1026, N'ITEMBRAND', N'HP', N'HP', N' HP', N'HP', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1027, N'ITEMBRAND', N'DELL', N'DELL', N'DELL', N'DELL', NULL, 1, NULL)
INSERT [dbo].[LookupValues] ([ValueId], [LookupCode], [ValueCode], [EnName], [DrName], [PaName], [ForOrdering], [IsActive], [GroupValueId]) VALUES (1028, N'ITEMORIGIN', N'America', N'America', N'امریکا', N'امریکا', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[LookupValues] OFF
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (1, N'Dashboard', N'صفحه اصلی', N'اصلی صفحه ', N'icon-home', 1, 0, 0, N'Home', N'Dashboard', N'View', 10, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (2, N'User Management', N'مدیریت کاربران', N'د کاروونکی ادراه کول', N'icon-users', 1, 0, 0, N'Users', N'Index', N'View', 150, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (3, N'Distribution', N'توزیع', N' توزیع ', N'icon-note', 1, 0, 1, N'Distribution', NULL, N'View', 20, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (4, N'Add Distribution', N'ثبت توزیع', N'د ویش ثبت', N'icon-note', 2, 3, 0, N'Distribution', N'Add', N'Add', 21, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (5, N'Search Distribution', N'جستجو توزیع', N'د ویش لټون', N'icon-note', 2, 3, 0, N'Distribution', N'Search', N'Search', 22, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (6, N'Stock In', N'ازدیاد اجسام', N'د جنس اضافه', N'icon-note', 1, 0, 1, N'StockIn', NULL, N'View', 30, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (7, N'Add StockIn', N'ازدیاد به دیپو', N'دیپو ته اضافه کول', N'icon-note', 2, 6, 0, N'StockIn', N'Add', N'Add', 31, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (8, N'Search StockIn', N' جستجو رسیدات', N'درسیدونو لټون', N'icon-note', 2, 6, 0, N'StockIn', N'Search', N'Search', 32, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (9, N'Lookups', N'لوکاپ ها', N'لوکاپ ها', N'icon-settings', 1, 0, 1, N'Lookups', NULL, N'View', 140, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (10, N'General Lookups', N'لوکاپ های عمومی', N'عمومي لټون', N'icon-eyeglasses', 2, 9, 0, N'Lookups', N'General', N'Search', 150, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (11, N'Product', N'اجناس', N'اجناس', N'icon-pointer', 2, 9, 0, N'Lookups', N'ProductSearch', N'Search', 160, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (12, N'Item In Use', N'اجناس در استعمال', N'اجناس په استعمال کی ', N'icon-note', 2, 3, 0, N'Distribution', N'ItemsInUse', N'Search', 23, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (13, N'Item in Warehouse', N'اجناس در تحویلخانه', N'اجناس په تحویلخانه کی', N'icon-note', 2, 6, 0, N'StockIn', N'ItemInWarehouse', N'Search', 33, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (14, N'Employee', N'کارمندان', N'کارکوونکی', N'icon-users', 1, 0, 0, N'Employee', N'Search', N'View', 60, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (15, N'Item Returned', N'اجسام تسلیم شده', N'تسلیم شوی اجسام', N'icon-note', 1, 0, 0, N'Distribution', N'ReturnedItems', N'Search', 70, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (16, N'Useless Items', N'داغمه', N'داغمه', N'icon-note', 1, 0, 1, N'UselessItem', N'Null', N'View', 80, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (17, N'Add Useless Items', N'ازدیاد داغمه', N'د داغمه لـټون', N'icon-note', 2, 16, 1, N'UselessItem', N'Add', N'Add', 81, 1, 1)
INSERT [dbo].[Menus] ([MenuId], [EnName], [DrName], [PaName], [Icon], [MenuLevel], [SuperMenuId], [HasSubMenu], [Controller], [Action], [ActionType], [MenuOrder], [IsActive], [Visible]) VALUES (18, N'Search Useless Items', N'جستجو داغمه', N'داغمه لټون', N'icon-note', 2, 16, 1, N'UselessItem', N'Search', N'Search', 82, 1, 1)
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [UsageTypeID], [GroupID], [CategoryID], [UnitID], [PackingID], [SizeID], [SerialCodeNumber], [ProductName], [OriginID], [BrandID], [Description], [ProductCode], [ImagePath], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (1, 1003, 1004, 1005, 24, 1011, 1007, N'12P', N'corei 7', 1020, 1026, N'With all equpment', 1, N'/Uploads/ProductsImages/1.jpg', 1, 5, CAST(N'2020-01-26' AS Date), 5, CAST(N'2020-01-31' AS Date))
INSERT [dbo].[Products] ([ProductID], [UsageTypeID], [GroupID], [CategoryID], [UnitID], [PackingID], [SizeID], [SerialCodeNumber], [ProductName], [OriginID], [BrandID], [Description], [ProductCode], [ImagePath], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (2, 1003, 1004, 1005, 24, 1011, NULL, NULL, N'Scanner hp', 1028, 1026, N'kjhkh', 2, N'/Uploads/ProductsImages/2.jpg', 1, 5, CAST(N'2020-02-04' AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[RoleAccess] ON 

INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (4, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (2008, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (2009, 1, 2, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (2010, 1, 3, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (2011, 1, 4, 1, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (2012, 1, 5, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (2013, 1, 6, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3002, 1, 7, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3003, 1, 8, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3008, 1, 9, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3011, 1, 12, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3012, 1, 13, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (3013, 1, 14, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (4013, 1, 15, 1, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RoleAccess] ([RoleAccessId], [RoleId], [MenuId], [FullControl], [Add], [Edit], [Search], [View], [Delete], [Print], [Download]) VALUES (5013, 1, 16, 1, 0, 0, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[RoleAccess] OFF
SET IDENTITY_INSERT [dbo].[SigninLogs] ON 

INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1, 5, N'admin', N'::1', CAST(N'2019-08-17T13:30:32.363' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2, 5, N'admin', N'::1', CAST(N'2019-08-18T11:34:51.103' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3, 5, N'admin', N'::1', CAST(N'2019-08-18T12:00:18.920' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4, 5, N'admin', N'::1', CAST(N'2019-08-18T13:48:01.567' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5, 5, N'admin', N'::1', CAST(N'2019-08-18T13:48:49.050' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6, 5, N'admin', N'::1', CAST(N'2019-08-18T13:54:41.983' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7, 5, N'admin', N'::1', CAST(N'2019-08-18T13:56:17.793' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (8, 5, N'admin', N'::1', CAST(N'2019-08-18T14:06:45.463' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9, 5, N'admin', N'::1', CAST(N'2019-08-18T19:09:16.913' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10, 5, N'admin', N'::1', CAST(N'2019-08-18T19:13:06.397' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (11, 5, N'admin', N'::1', CAST(N'2019-08-18T19:17:59.903' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (12, 5, N'admin', N'::1', CAST(N'2019-08-19T15:42:01.113' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (13, 5, N'admin', N'::1', CAST(N'2019-08-20T09:44:49.490' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14, 5, N'admin', N'::1', CAST(N'2019-08-20T13:37:41.170' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (15, 5, N'admin', N'::1', CAST(N'2019-08-20T14:33:49.010' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (16, 5, N'admin', N'::1', CAST(N'2019-08-20T14:38:51.177' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17, 5, N'admin', N'::1', CAST(N'2019-08-23T07:11:33.807' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (18, 5, N'admin', N'::1', CAST(N'2019-08-23T15:37:42.537' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (19, 5, N'admin', N'::1', CAST(N'2019-08-23T17:30:35.297' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20, 5, N'admin', N'::1', CAST(N'2019-08-23T17:31:13.760' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (21, 5, N'admin', N'::1', CAST(N'2019-08-23T17:32:04.733' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22, 5, N'admin', N'::1', CAST(N'2019-08-23T17:32:44.253' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (23, 5, N'admin', N'::1', CAST(N'2019-08-23T17:55:47.557' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24, 5, N'admin', N'::1', CAST(N'2019-08-23T18:01:04.080' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25, 5, N'admin', N'::1', CAST(N'2019-08-23T18:01:57.517' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26, 5, N'admin', N'::1', CAST(N'2019-08-23T18:03:48.863' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27, 5, N'admin', N'::1', CAST(N'2019-08-23T18:05:59.940' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (28, 5, N'admin', N'::1', CAST(N'2019-08-23T19:48:54.770' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (29, 5, N'admin', N'::1', CAST(N'2019-08-23T19:52:15.917' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30, 5, N'admin', N'::1', CAST(N'2019-08-23T19:53:58.963' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (31, 5, N'admin', N'::1', CAST(N'2019-08-23T19:57:31.927' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32, 5, N'admin', N'::1', CAST(N'2019-08-23T19:59:02.167' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33, 5, N'admin', N'::1', CAST(N'2019-08-23T20:07:17.787' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34, 5, N'admin', N'::1', CAST(N'2019-08-23T20:12:38.840' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (35, 5, N'admin', N'::1', CAST(N'2019-08-23T20:16:59.197' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (36, 5, N'admin', N'::1', CAST(N'2019-08-23T21:46:16.657' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (37, 5, N'admin', N'::1', CAST(N'2019-08-23T21:53:38.123' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (38, 5, N'admin', N'::1', CAST(N'2019-08-23T21:55:55.407' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (39, 5, N'admin', N'::1', CAST(N'2019-08-23T21:57:01.900' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (40, 5, N'admin', N'::1', CAST(N'2019-08-24T06:12:43.787' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (41, 5, N'admin', N'::1', CAST(N'2019-08-24T06:14:58.840' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (42, 5, N'admin', N'::1', CAST(N'2019-08-24T06:19:47.360' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (43, 5, N'admin', N'::1', CAST(N'2019-08-24T06:21:35.200' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (44, 5, N'admin', N'::1', CAST(N'2019-08-24T06:23:49.040' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (45, 5, N'admin', N'::1', CAST(N'2019-08-24T06:25:05.323' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (46, 5, N'admin', N'::1', CAST(N'2019-08-24T10:59:06.363' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (47, 5, N'admin', N'::1', CAST(N'2019-08-24T11:02:36.183' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (48, 5, N'admin', N'::1', CAST(N'2019-08-24T11:10:48.517' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (49, 5, N'admin', N'::1', CAST(N'2019-08-24T11:15:53.963' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (50, 5, N'admin', N'::1', CAST(N'2019-08-24T11:26:07.177' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (51, 5, N'admin', N'::1', CAST(N'2019-08-24T11:44:11.777' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (52, 5, N'admin', N'::1', CAST(N'2019-08-24T11:53:06.647' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (53, 5, N'admin', N'::1', CAST(N'2019-08-24T11:57:08.997' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (54, 5, N'admin', N'::1', CAST(N'2019-08-24T11:59:30.833' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (55, 5, N'admin', N'::1', CAST(N'2019-08-24T14:31:46.543' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (56, 5, N'admin', N'::1', CAST(N'2019-08-24T14:36:03.070' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (57, 5, N'admin', N'::1', CAST(N'2019-08-24T15:48:09.783' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (58, 5, N'admin', N'::1', CAST(N'2019-08-24T15:50:52.407' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (59, 5, N'admin', N'::1', CAST(N'2019-08-24T19:28:24.660' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (60, 5, N'admin', N'::1', CAST(N'2019-08-24T19:41:02.523' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (61, 5, N'admin', N'::1', CAST(N'2019-08-25T10:32:00.723' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (62, 5, N'admin', N'::1', CAST(N'2019-08-25T10:49:52.267' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (63, 5, N'admin', N'::1', CAST(N'2019-08-25T11:48:55.580' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (64, 5, N'admin', N'::1', CAST(N'2019-08-25T14:02:22.430' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (65, 5, N'admin', N'127.0.0.1', CAST(N'2019-08-25T15:21:20.157' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (66, 5, N'admin', N'::1', CAST(N'2019-08-26T13:29:15.063' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (67, 5, N'admin', N'::1', CAST(N'2019-08-27T08:39:14.600' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (68, 5, N'admin', N'::1', CAST(N'2019-08-27T08:41:49.847' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (69, 5, N'admin', N'::1', CAST(N'2019-08-27T12:09:21.753' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (70, 5, N'admin', N'::1', CAST(N'2019-08-27T12:18:24.083' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (71, 5, N'admin', N'::1', CAST(N'2019-08-27T14:01:38.150' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (72, 5, N'admin', N'::1', CAST(N'2019-08-27T14:49:07.587' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (73, 5, N'admin', N'::1', CAST(N'2019-08-27T14:58:20.653' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (74, 5, N'admin', N'::1', CAST(N'2019-08-27T15:06:22.360' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (75, 5, N'admin', N'::1', CAST(N'2019-08-27T15:10:53.633' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (76, 5, N'admin', N'::1', CAST(N'2019-08-27T15:51:48.927' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (77, 5, N'admin', N'::1', CAST(N'2019-08-27T16:03:44.977' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (78, 5, N'admin', N'::1', CAST(N'2019-08-27T19:41:58.657' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (79, 5, N'admin', N'::1', CAST(N'2019-08-27T19:43:21.353' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (80, 5, N'admin', N'::1', CAST(N'2019-08-27T19:48:14.230' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (81, 5, N'admin', N'::1', CAST(N'2019-08-27T19:49:56.327' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (82, 5, N'admin', N'::1', CAST(N'2019-08-27T19:51:25.313' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (83, 5, N'admin', N'::1', CAST(N'2019-08-27T19:52:39.497' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (84, 5, N'admin', N'::1', CAST(N'2019-08-27T19:54:09.503' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (85, 5, N'admin', N'::1', CAST(N'2019-08-27T19:55:42.057' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (86, 5, N'admin', N'::1', CAST(N'2019-08-27T19:57:40.017' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (87, 5, N'admin', N'::1', CAST(N'2019-08-27T19:58:56.743' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (88, 5, N'admin', N'::1', CAST(N'2019-08-27T20:00:01.587' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (89, 5, N'admin', N'::1', CAST(N'2019-08-27T20:03:39.860' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (90, 5, N'admin', N'::1', CAST(N'2019-08-27T20:23:30.713' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (91, 5, N'admin', N'::1', CAST(N'2019-08-28T05:11:58.213' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (92, 5, N'admin', N'::1', CAST(N'2019-08-28T09:58:10.233' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (93, 5, N'admin', N'::1', CAST(N'2019-08-28T10:26:43.110' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (94, 5, N'admin', N'::1', CAST(N'2019-08-28T11:03:57.370' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (95, 5, N'admin', N'::1', CAST(N'2019-08-28T14:38:46.977' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (96, 5, N'admin', N'::1', CAST(N'2019-08-28T14:39:30.263' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (97, 5, N'admin', N'::1', CAST(N'2019-08-29T16:56:26.110' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (98, 5, N'admin', N'::1', CAST(N'2019-08-29T17:01:59.397' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (99, 5, N'admin', N'::1', CAST(N'2019-08-29T17:19:21.833' AS DateTime))
GO
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (100, 5, N'admin', N'::1', CAST(N'2019-08-29T19:22:08.703' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (101, 5, N'admin', N'::1', CAST(N'2019-08-29T19:28:19.533' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (102, 5, N'admin', N'::1', CAST(N'2019-08-29T20:34:10.283' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (103, 5, N'admin', N'::1', CAST(N'2019-08-29T20:42:37.530' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (104, 5, N'admin', N'::1', CAST(N'2019-08-29T20:45:44.837' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1002, 5, N'admin', N'::1', CAST(N'2019-08-31T09:30:35.900' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1003, 5, N'admin', N'::1', CAST(N'2019-08-31T09:34:37.360' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1004, 5, N'admin', N'::1', CAST(N'2019-08-31T11:04:49.973' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1005, 5, N'admin', N'::1', CAST(N'2019-08-31T12:01:13.847' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1006, 5, N'admin', N'::1', CAST(N'2019-08-31T20:01:19.780' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1007, 5, N'admin', N'::1', CAST(N'2019-08-31T20:09:54.047' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1008, 5, N'admin', N'::1', CAST(N'2019-09-01T19:30:14.380' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1009, 5, N'admin', N'::1', CAST(N'2019-09-02T11:54:04.363' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1010, 5, N'admin', N'::1', CAST(N'2019-09-02T15:14:53.523' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1011, 5, N'admin', N'::1', CAST(N'2019-09-02T15:20:46.940' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1012, 5, N'admin', N'::1', CAST(N'2019-09-02T15:57:54.017' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1013, 5, N'admin', N'::1', CAST(N'2019-09-02T19:18:59.737' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1014, 5, N'admin', N'::1', CAST(N'2019-09-02T19:49:50.357' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1015, 5, N'admin', N'::1', CAST(N'2019-09-02T22:12:44.323' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1016, 5, N'admin', N'::1', CAST(N'2019-09-03T11:15:41.243' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1017, 5, N'admin', N'::1', CAST(N'2019-09-03T12:14:49.770' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1018, 5, N'admin', N'::1', CAST(N'2019-09-04T10:25:45.577' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1019, 5, N'admin', N'::1', CAST(N'2019-09-04T10:31:09.603' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1020, 5, N'admin', N'::1', CAST(N'2019-09-04T10:37:27.183' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1021, 5, N'admin', N'::1', CAST(N'2019-09-04T10:38:46.790' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1022, 5, N'admin', N'::1', CAST(N'2019-09-04T10:40:21.783' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (1023, 5, N'admin', N'::1', CAST(N'2019-09-04T16:54:08.913' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2008, 5, N'admin', N'::1', CAST(N'2019-09-08T14:11:54.967' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2009, 5, N'admin', N'::1', CAST(N'2019-09-08T14:20:55.230' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2010, 5, N'admin', N'::1', CAST(N'2019-09-08T14:31:42.533' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2011, 5, N'admin', N'::1', CAST(N'2019-09-08T14:59:52.383' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2012, 5, N'admin', N'::1', CAST(N'2019-09-08T16:04:39.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2013, 5, N'admin', N'::1', CAST(N'2019-09-10T17:21:23.770' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2014, 5, N'admin', N'::1', CAST(N'2019-09-11T10:50:31.767' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2015, 5, N'admin', N'::1', CAST(N'2019-09-11T11:01:46.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2016, 5, N'admin', N'::1', CAST(N'2019-09-11T11:11:25.577' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2017, 5, N'admin', N'::1', CAST(N'2019-09-12T22:27:46.250' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2018, 5, N'admin', N'::1', CAST(N'2019-09-12T22:29:12.933' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2019, 5, N'admin', N'::1', CAST(N'2019-09-12T22:45:32.163' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2020, 5, N'admin', N'::1', CAST(N'2019-09-13T15:46:14.823' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2021, 5, N'admin', N'::1', CAST(N'2019-09-14T20:39:11.367' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2022, 5, N'admin', N'::1', CAST(N'2019-09-15T10:35:17.273' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2023, 5, N'admin', N'::1', CAST(N'2019-09-15T10:38:59.443' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2024, 5, N'admin', N'::1', CAST(N'2019-09-15T11:56:34.487' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2025, 5, N'admin', N'::1', CAST(N'2019-09-15T20:14:38.243' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2026, 5, N'admin', N'::1', CAST(N'2019-09-15T20:17:33.423' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2027, 5, N'admin', N'::1', CAST(N'2019-09-15T20:23:37.970' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2028, 5, N'admin', N'::1', CAST(N'2019-09-15T20:29:17.233' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2029, 5, N'admin', N'::1', CAST(N'2019-09-15T20:34:41.513' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2030, 5, N'admin', N'::1', CAST(N'2019-09-16T05:54:37.853' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2031, 5, N'admin', N'::1', CAST(N'2019-09-16T06:02:17.753' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2032, 5, N'admin', N'::1', CAST(N'2019-09-16T06:07:18.160' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2033, 5, N'admin', N'::1', CAST(N'2019-09-16T10:22:03.670' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2034, 5, N'admin', N'::1', CAST(N'2019-09-16T11:35:47.743' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2035, 5, N'admin', N'::1', CAST(N'2019-09-16T12:28:37.683' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2036, 5, N'admin', N'::1', CAST(N'2019-09-16T21:04:48.053' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2037, 5, N'admin', N'::1', CAST(N'2019-09-17T09:36:42.477' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2038, 5, N'admin', N'::1', CAST(N'2019-09-17T09:42:01.807' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2039, 5, N'admin', N'::1', CAST(N'2019-09-17T09:52:13.410' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2040, 5, N'admin', N'::1', CAST(N'2019-09-17T14:31:01.823' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2041, 5, N'admin', N'::1', CAST(N'2019-09-18T10:03:56.843' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (2042, 5, N'admin', N'::1', CAST(N'2019-09-18T10:45:51.737' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3041, 5, N'admin', N'::1', CAST(N'2019-09-21T10:54:29.850' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3042, 5, N'admin', N'::1', CAST(N'2019-09-22T09:37:42.613' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3043, 5, N'admin', N'::1', CAST(N'2019-09-22T10:03:00.080' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3044, 5, N'admin', N'::1', CAST(N'2019-09-23T11:57:35.383' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3045, 5, N'admin', N'::1', CAST(N'2019-09-23T14:57:34.600' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3046, 5, N'admin', N'::1', CAST(N'2019-09-24T10:11:18.600' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3047, 5, N'admin', N'::1', CAST(N'2019-09-24T10:12:54.773' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3048, 5, N'admin', N'::1', CAST(N'2019-09-24T10:14:50.677' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3049, 5, N'admin', N'::1', CAST(N'2019-09-24T10:24:51.373' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3050, 5, N'admin', N'::1', CAST(N'2019-09-24T10:52:46.330' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3051, 5, N'admin', N'::1', CAST(N'2019-09-24T10:55:58.613' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3052, 5, N'admin', N'::1', CAST(N'2019-09-24T10:58:53.340' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3053, 5, N'admin', N'::1', CAST(N'2019-09-24T11:13:52.890' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3054, 5, N'admin', N'::1', CAST(N'2019-09-24T11:16:45.523' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3055, 5, N'admin', N'::1', CAST(N'2019-09-24T11:18:06.317' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3056, 5, N'admin', N'::1', CAST(N'2019-09-24T11:20:42.000' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3057, 5, N'admin', N'::1', CAST(N'2019-09-24T11:24:00.180' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3058, 5, N'admin', N'::1', CAST(N'2019-09-24T11:25:58.367' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3059, 5, N'admin', N'::1', CAST(N'2019-09-24T11:28:56.327' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3060, 5, N'admin', N'::1', CAST(N'2019-09-24T11:53:06.987' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3061, 5, N'admin', N'::1', CAST(N'2019-09-24T11:58:16.540' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3062, 5, N'admin', N'::1', CAST(N'2019-09-24T13:55:56.773' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3063, 5, N'admin', N'::1', CAST(N'2019-09-24T14:19:31.067' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3064, 5, N'admin', N'::1', CAST(N'2019-09-24T20:58:42.650' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3065, 5, N'admin', N'::1', CAST(N'2019-09-25T10:07:06.517' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3066, 5, N'admin', N'::1', CAST(N'2019-09-25T13:45:01.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3067, 5, N'admin', N'::1', CAST(N'2019-09-25T19:35:57.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3068, 5, N'admin', N'::1', CAST(N'2019-09-25T19:39:50.823' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3069, 5, N'admin', N'::1', CAST(N'2019-09-25T19:40:26.270' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3070, 5, N'admin', N'::1', CAST(N'2019-09-25T19:41:53.613' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3071, 5, N'admin', N'::1', CAST(N'2019-09-25T19:42:57.753' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (3072, 5, N'admin', N'::1', CAST(N'2019-09-25T19:45:47.987' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4064, 5, N'admin', N'::1', CAST(N'2019-09-26T10:01:26.913' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4065, 5, N'admin', N'::1', CAST(N'2019-09-26T21:12:28.577' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4066, 5, N'admin', N'::1', CAST(N'2019-09-26T21:17:16.123' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4067, 5, N'admin', N'::1', CAST(N'2019-09-26T21:19:43.177' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4068, 5, N'admin', N'::1', CAST(N'2019-09-26T21:27:13.003' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4069, 5, N'admin', N'::1', CAST(N'2019-09-27T15:13:21.233' AS DateTime))
GO
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4070, 5, N'admin', N'::1', CAST(N'2019-09-27T15:45:22.430' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4071, 5, N'admin', N'::1', CAST(N'2019-09-27T15:50:51.010' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4072, 5, N'admin', N'::1', CAST(N'2019-09-27T16:16:00.117' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4073, 5, N'admin', N'::1', CAST(N'2019-09-27T17:00:50.807' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4074, 5, N'admin', N'::1', CAST(N'2019-09-28T09:59:37.640' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4075, 5, N'admin', N'::1', CAST(N'2019-09-28T10:11:42.767' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4076, 5, N'admin', N'::1', CAST(N'2019-09-28T10:15:20.100' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4077, 5, N'admin', N'::1', CAST(N'2019-09-28T10:23:05.023' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4078, 5, N'admin', N'::1', CAST(N'2019-09-28T10:34:02.397' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4079, 5, N'admin', N'::1', CAST(N'2019-09-28T10:54:24.293' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4080, 5, N'admin', N'::1', CAST(N'2019-09-28T10:57:38.827' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4081, 5, N'admin', N'::1', CAST(N'2019-09-28T10:59:13.183' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4082, 5, N'admin', N'::1', CAST(N'2019-09-28T11:02:26.890' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4083, 5, N'admin', N'::1', CAST(N'2019-09-28T12:09:38.940' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4084, 5, N'admin', N'::1', CAST(N'2019-09-28T20:46:30.307' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4085, 5, N'admin', N'::1', CAST(N'2019-09-29T08:58:40.857' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4086, 5, N'admin', N'::1', CAST(N'2019-09-29T10:38:43.253' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4087, 5, N'admin', N'::1', CAST(N'2019-09-29T10:40:44.920' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4088, 5, N'admin', N'127.0.0.1', CAST(N'2019-09-29T10:44:04.113' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4089, 5, N'admin', N'::1', CAST(N'2019-09-29T10:51:27.503' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4090, 5, N'admin', N'::1', CAST(N'2019-09-29T11:13:47.550' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4091, 5, N'admin', N'::1', CAST(N'2019-09-29T14:26:28.190' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (4092, 5, N'admin', N'::1', CAST(N'2019-10-02T15:45:10.647' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5092, 5, N'admin', N'::1', CAST(N'2019-10-09T10:57:50.453' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5093, 5, N'admin', N'::1', CAST(N'2019-10-11T20:58:41.287' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5094, 5, N'admin', N'::1', CAST(N'2019-10-12T06:51:50.840' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5095, 5, N'admin', N'::1', CAST(N'2019-10-12T06:51:52.183' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5096, 5, N'admin', N'::1', CAST(N'2019-10-12T10:31:08.647' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5097, 5, N'admin', N'::1', CAST(N'2019-10-12T11:28:13.763' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5098, 5, N'admin', N'::1', CAST(N'2019-10-12T12:00:03.113' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5099, 5, N'admin', N'::1', CAST(N'2019-10-12T14:00:41.687' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5100, 5, N'admin', N'::1', CAST(N'2019-10-12T14:44:32.883' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5101, 5, N'admin', N'::1', CAST(N'2019-10-12T14:59:20.520' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5102, 5, N'admin', N'::1', CAST(N'2019-10-14T10:54:02.540' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5103, 5, N'admin', N'::1', CAST(N'2019-10-14T10:57:23.993' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5104, 5, N'admin', N'::1', CAST(N'2019-10-14T11:06:43.210' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5105, 5, N'admin', N'::1', CAST(N'2019-10-14T11:12:46.837' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5106, 5, N'admin', N'::1', CAST(N'2019-10-15T10:57:21.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5107, 5, N'admin', N'::1', CAST(N'2019-10-19T09:57:20.390' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5108, 5, N'admin', N'::1', CAST(N'2019-10-19T14:48:25.620' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5109, 5, N'admin', N'::1', CAST(N'2019-10-21T14:01:32.020' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5110, 5, N'admin', N'::1', CAST(N'2019-10-22T10:47:39.760' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5111, 5, N'admin', N'::1', CAST(N'2019-10-22T11:08:15.670' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (5112, 5, N'admin', N'::1', CAST(N'2019-10-22T14:07:25.790' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6097, 5, N'admin', N'::1', CAST(N'2019-10-27T12:07:09.540' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6098, 5, N'admin', N'::1', CAST(N'2019-10-27T12:16:28.190' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6099, 5, N'admin', N'::1', CAST(N'2019-10-27T12:21:32.420' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6100, 5, N'admin', N'::1', CAST(N'2019-10-27T14:21:26.203' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6101, 5, N'admin', N'::1', CAST(N'2019-10-27T14:35:02.640' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6102, 5, N'admin', N'::1', CAST(N'2019-10-27T14:37:08.127' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6103, 5, N'admin', N'::1', CAST(N'2019-10-27T15:14:14.743' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6104, 5, N'admin', N'::1', CAST(N'2019-10-27T15:41:29.063' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6105, 5, N'admin', N'::1', CAST(N'2019-10-27T20:51:37.350' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (6106, 5, N'admin', N'::1', CAST(N'2019-11-16T15:02:45.327' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7097, 5, N'admin', N'::1', CAST(N'2019-11-23T10:08:35.240' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7098, 5, N'admin', N'::1', CAST(N'2019-11-26T11:24:14.317' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7099, 5, N'admin', N'::1', CAST(N'2019-12-01T14:30:55.110' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7100, 5, N'admin', N'::1', CAST(N'2019-12-02T19:13:37.230' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7101, 5, N'admin', N'::1', CAST(N'2019-12-02T19:39:21.693' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (7102, 5, N'admin', N'::1', CAST(N'2019-12-15T14:50:07.597' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (8102, 5, N'admin', N'::1', CAST(N'2019-12-19T16:09:30.170' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (8103, 5, N'admin', N'::1', CAST(N'2019-12-19T20:10:34.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (8104, 5, N'admin', N'::1', CAST(N'2019-12-21T11:52:03.417' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9102, 5, N'admin', N'::1', CAST(N'2019-12-22T10:07:25.467' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9103, 5, N'admin', N'::1', CAST(N'2019-12-22T20:56:37.137' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9104, 5, N'admin', N'::1', CAST(N'2019-12-22T21:01:55.240' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9105, 5, N'admin', N'::1', CAST(N'2019-12-23T10:36:31.620' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9106, 5, N'admin', N'::1', CAST(N'2019-12-23T15:15:37.703' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9107, 5, N'admin', N'::1', CAST(N'2019-12-23T15:19:36.333' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9108, 5, N'admin', N'::1', CAST(N'2019-12-23T15:25:01.363' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9109, 5, N'admin', N'::1', CAST(N'2019-12-23T15:33:03.833' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9110, 5, N'admin', N'::1', CAST(N'2019-12-23T19:29:05.457' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9111, 5, N'admin', N'::1', CAST(N'2019-12-23T19:30:40.193' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9112, 5, N'admin', N'::1', CAST(N'2019-12-23T19:37:20.153' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9113, 5, N'admin', N'::1', CAST(N'2019-12-23T19:57:20.030' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9114, 5, N'admin', N'::1', CAST(N'2019-12-23T20:10:15.070' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9115, 5, N'admin', N'::1', CAST(N'2019-12-23T20:18:02.660' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9116, 5, N'admin', N'::1', CAST(N'2019-12-23T20:21:06.703' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9117, 5, N'admin', N'::1', CAST(N'2019-12-23T20:30:06.490' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9118, 5, N'admin', N'::1', CAST(N'2019-12-23T20:35:30.847' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9119, 5, N'admin', N'::1', CAST(N'2019-12-23T20:41:34.400' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9120, 5, N'admin', N'::1', CAST(N'2019-12-23T21:08:22.693' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9121, 5, N'admin', N'::1', CAST(N'2019-12-23T21:10:18.040' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9122, 5, N'admin', N'::1', CAST(N'2019-12-23T21:11:52.923' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9123, 5, N'admin', N'::1', CAST(N'2019-12-23T21:13:29.183' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9124, 5, N'admin', N'::1', CAST(N'2019-12-23T21:19:54.010' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9125, 5, N'admin', N'::1', CAST(N'2019-12-24T07:20:04.747' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9126, 5, N'admin', N'::1', CAST(N'2019-12-24T07:20:41.207' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9127, 5, N'admin', N'::1', CAST(N'2019-12-24T14:07:56.900' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9128, 5, N'admin', N'::1', CAST(N'2019-12-24T14:11:46.470' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9129, 5, N'admin', N'::1', CAST(N'2019-12-24T14:24:35.990' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9130, 5, N'admin', N'::1', CAST(N'2019-12-24T14:25:48.047' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9131, 5, N'admin', N'::1', CAST(N'2019-12-24T14:57:30.530' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9132, 5, N'admin', N'::1', CAST(N'2019-12-26T12:41:53.217' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9133, 5, N'admin', N'::1', CAST(N'2019-12-26T14:28:12.013' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9134, 5, N'admin', N'::1', CAST(N'2019-12-26T21:04:12.597' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9135, 5, N'admin', N'::1', CAST(N'2019-12-26T21:20:41.887' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9136, 5, N'admin', N'::1', CAST(N'2019-12-26T21:25:08.663' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9137, 5, N'admin', N'::1', CAST(N'2019-12-27T09:07:23.260' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9138, 5, N'admin', N'::1', CAST(N'2019-12-27T09:51:19.033' AS DateTime))
GO
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9139, 5, N'admin', N'::1', CAST(N'2019-12-27T09:58:12.460' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9140, 5, N'admin', N'::1', CAST(N'2019-12-27T10:02:35.230' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9141, 5, N'admin', N'::1', CAST(N'2019-12-27T10:06:57.380' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9142, 5, N'admin', N'::1', CAST(N'2019-12-27T10:12:48.640' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9143, 5, N'admin', N'::1', CAST(N'2019-12-27T11:44:17.770' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9144, 5, N'admin', N'::1', CAST(N'2019-12-27T12:04:49.513' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9145, 5, N'admin', N'::1', CAST(N'2019-12-27T12:14:25.140' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9146, 5, N'admin', N'::1', CAST(N'2019-12-27T12:14:38.880' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9147, 5, N'admin', N'::1', CAST(N'2019-12-27T12:16:15.037' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9148, 5, N'admin', N'::1', CAST(N'2019-12-27T18:51:56.513' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9149, 5, N'admin', N'::1', CAST(N'2019-12-27T19:09:00.337' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9150, 5, N'admin', N'::1', CAST(N'2019-12-27T20:41:08.540' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9151, 5, N'admin', N'::1', CAST(N'2019-12-28T13:24:40.910' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9152, 5, N'admin', N'::1', CAST(N'2019-12-28T14:39:34.953' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9153, 5, N'admin', N'127.0.0.1', CAST(N'2019-12-28T15:04:26.317' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9154, 5, N'admin', N'::1', CAST(N'2019-12-29T20:01:31.607' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9155, 5, N'admin', N'::1', CAST(N'2019-12-29T20:30:49.030' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9156, 5, N'admin', N'::1', CAST(N'2019-12-29T20:35:50.393' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9157, 5, N'admin', N'::1', CAST(N'2019-12-29T20:44:26.733' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9158, 5, N'admin', N'::1', CAST(N'2019-12-30T08:25:30.680' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (9159, 5, N'admin', N'::1', CAST(N'2019-12-30T08:28:46.410' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10102, 5, N'admin', N'::1', CAST(N'2019-12-30T21:13:14.767' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10103, 5, N'admin', N'::1', CAST(N'2019-12-31T15:12:18.753' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10104, 5, N'admin', N'::1', CAST(N'2019-12-31T15:18:32.923' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10105, 5, N'admin', N'::1', CAST(N'2020-01-01T10:10:30.777' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10106, 5, N'admin', N'::1', CAST(N'2020-01-01T10:53:13.113' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (10107, 5, N'admin', N'::1', CAST(N'2020-01-01T11:47:30.300' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (11102, 5, N'admin', N'::1', CAST(N'2020-01-02T15:01:41.663' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (11103, 5, N'admin', N'::1', CAST(N'2020-01-02T16:47:51.917' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (11104, 5, N'admin', N'::1', CAST(N'2020-01-02T16:57:16.993' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (12102, 5, N'admin', N'::1', CAST(N'2020-01-03T09:41:42.910' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (12103, 5, N'admin', N'::1', CAST(N'2020-01-03T15:42:42.480' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (12104, 5, N'admin', N'::1', CAST(N'2020-01-03T15:49:44.893' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (13103, 5, N'admin', N'::1', CAST(N'2020-01-03T16:22:46.180' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (13104, 5, N'admin', N'::1', CAST(N'2020-01-03T18:52:26.143' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (13105, 5, N'admin', N'::1', CAST(N'2020-01-03T22:31:10.047' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14104, 5, N'admin', N'::1', CAST(N'2020-01-09T11:27:39.163' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14105, 5, N'admin', N'::1', CAST(N'2020-01-10T14:58:49.360' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14106, 5, N'admin', N'::1', CAST(N'2020-01-10T16:21:31.777' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14107, 5, N'admin', N'::1', CAST(N'2020-01-10T16:28:42.927' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14108, 5, N'admin', N'::1', CAST(N'2020-01-10T16:32:34.477' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14109, 5, N'admin', N'::1', CAST(N'2020-01-10T16:41:58.647' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14110, 5, N'admin', N'::1', CAST(N'2020-01-10T17:03:07.860' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14111, 5, N'admin', N'::1', CAST(N'2020-01-10T17:07:16.273' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14112, 5, N'admin', N'::1', CAST(N'2020-01-10T17:10:19.367' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14113, 5, N'admin', N'::1', CAST(N'2020-01-10T17:15:27.747' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14114, 5, N'admin', N'::1', CAST(N'2020-01-10T17:21:10.303' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14115, 5, N'admin', N'::1', CAST(N'2020-01-10T18:02:45.480' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14116, 5, N'admin', N'::1', CAST(N'2020-01-10T20:06:28.097' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14117, 5, N'admin', N'::1', CAST(N'2020-01-10T20:17:13.697' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14118, 5, N'admin', N'::1', CAST(N'2020-01-10T20:18:29.490' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14119, 5, N'admin', N'::1', CAST(N'2020-01-10T20:20:49.630' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14120, 5, N'admin', N'::1', CAST(N'2020-01-10T20:22:59.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14121, 5, N'admin', N'::1', CAST(N'2020-01-10T20:25:57.393' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14122, 5, N'admin', N'::1', CAST(N'2020-01-10T20:30:56.750' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14123, 5, N'admin', N'::1', CAST(N'2020-01-10T20:34:29.010' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14124, 5, N'admin', N'::1', CAST(N'2020-01-11T12:32:10.217' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14125, 5, N'admin', N'::1', CAST(N'2020-01-11T12:43:10.173' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14126, 5, N'admin', N'::1', CAST(N'2020-01-11T13:26:49.993' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14127, 5, N'admin', N'::1', CAST(N'2020-01-11T14:10:31.887' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14128, 5, N'admin', N'::1', CAST(N'2020-01-11T14:12:08.923' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14129, 5, N'admin', N'::1', CAST(N'2020-01-11T23:14:29.920' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14130, 5, N'admin', N'::1', CAST(N'2020-01-11T23:36:25.277' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14131, 5, N'admin', N'::1', CAST(N'2020-01-12T12:42:47.507' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14132, 5, N'admin', N'::1', CAST(N'2020-01-12T12:46:03.700' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14133, 5, N'admin', N'::1', CAST(N'2020-01-12T15:08:12.237' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14134, 5, N'admin', N'::1', CAST(N'2020-01-12T17:01:32.370' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14135, 5, N'admin', N'::1', CAST(N'2020-01-12T18:32:56.207' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14136, 5, N'admin', N'::1', CAST(N'2020-01-12T18:45:02.137' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14137, 5, N'admin', N'::1', CAST(N'2020-01-12T21:13:03.200' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14138, 5, N'admin', N'::1', CAST(N'2020-01-12T21:25:06.353' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (14139, 5, N'admin', N'::1', CAST(N'2020-01-12T21:30:04.183' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (15125, 5, N'admin', N'::1', CAST(N'2020-01-13T20:48:22.240' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (16125, 5, N'admin', N'::1', CAST(N'2020-01-15T15:22:26.727' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17125, 5, N'admin', N'::1', CAST(N'2020-01-15T20:21:22.560' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17126, 5, N'admin', N'::1', CAST(N'2020-01-15T21:27:30.300' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17127, 5, N'admin', N'::1', CAST(N'2020-01-15T21:39:54.727' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17128, 5, N'admin', N'::1', CAST(N'2020-01-16T20:11:53.393' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17129, 5, N'admin', N'::1', CAST(N'2020-01-16T20:17:08.583' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17130, 5, N'admin', N'::1', CAST(N'2020-01-16T20:29:36.263' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (17131, 5, N'admin', N'::1', CAST(N'2020-01-16T20:31:40.613' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (18125, 5, N'admin', N'::1', CAST(N'2020-01-17T10:59:42.497' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (18126, 5, N'admin', N'::1', CAST(N'2020-01-17T11:58:45.657' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (18127, 5, N'admin', N'::1', CAST(N'2020-01-17T16:59:35.470' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (19125, 5, N'admin', N'::1', CAST(N'2020-01-17T19:59:18.570' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (19126, 5, N'admin', N'::1', CAST(N'2020-01-17T21:49:51.897' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20125, 5, N'admin', N'::1', CAST(N'2020-01-18T20:28:44.587' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20126, 5, N'admin', N'::1', CAST(N'2020-01-19T11:41:32.077' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20127, 5, N'admin', N'::1', CAST(N'2020-01-19T11:45:29.167' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20128, 5, N'admin', N'::1', CAST(N'2020-01-19T12:15:01.390' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20129, 5, N'admin', N'::1', CAST(N'2020-01-19T12:19:45.473' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20130, 5, N'admin', N'::1', CAST(N'2020-01-19T12:24:06.107' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20131, 5, N'admin', N'::1', CAST(N'2020-01-19T12:26:31.657' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20132, 5, N'admin', N'::1', CAST(N'2020-01-19T12:26:34.243' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (20133, 5, N'admin', N'::1', CAST(N'2020-01-19T12:32:35.430' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (21125, 5, N'admin', N'::1', CAST(N'2020-01-19T19:46:25.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (21126, 5, N'admin', N'::1', CAST(N'2020-01-19T20:04:52.960' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (21127, 5, N'admin', N'::1', CAST(N'2020-01-19T20:45:24.523' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (21128, 5, N'admin', N'::1', CAST(N'2020-01-19T20:47:40.747' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22125, 5, N'admin', N'::1', CAST(N'2020-01-20T18:28:07.103' AS DateTime))
GO
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22126, 5, N'admin', N'::1', CAST(N'2020-01-20T22:19:46.280' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22127, 5, N'admin', N'::1', CAST(N'2020-01-20T22:42:28.017' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22128, 5, N'admin', N'::1', CAST(N'2020-01-20T22:47:24.987' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22129, 5, N'admin', N'::1', CAST(N'2020-01-20T22:52:59.853' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22130, 5, N'admin', N'::1', CAST(N'2020-01-20T23:07:29.897' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22131, 5, N'admin', N'::1', CAST(N'2020-01-20T23:22:12.170' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22132, 5, N'admin', N'::1', CAST(N'2020-01-21T11:44:30.257' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22133, 5, N'admin', N'::1', CAST(N'2020-01-21T11:46:31.627' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22134, 5, N'admin', N'::1', CAST(N'2020-01-21T12:05:51.890' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (22135, 5, N'admin', N'::1', CAST(N'2020-01-21T13:30:06.057' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (23125, 5, N'admin', N'::1', CAST(N'2020-01-22T11:18:32.833' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (23126, 5, N'admin', N'::1', CAST(N'2020-01-22T15:08:35.340' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (23127, 5, N'admin', N'::1', CAST(N'2020-01-22T15:26:22.497' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (23128, 5, N'admin', N'::1', CAST(N'2020-01-22T15:54:42.147' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (23129, 5, N'admin', N'::1', CAST(N'2020-01-22T16:00:59.583' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24125, 5, N'admin', N'::1', CAST(N'2020-01-23T10:11:23.350' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24126, 5, N'admin', N'::1', CAST(N'2020-01-23T10:40:18.143' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24127, 5, N'admin', N'::1', CAST(N'2020-01-23T11:59:32.643' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24128, 5, N'admin', N'::1', CAST(N'2020-01-23T12:04:21.870' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24129, 5, N'admin', N'::1', CAST(N'2020-01-23T12:23:22.500' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24130, 5, N'admin', N'::1', CAST(N'2020-01-23T12:26:49.757' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24131, 5, N'admin', N'::1', CAST(N'2020-01-23T12:29:49.257' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24132, 5, N'admin', N'::1', CAST(N'2020-01-24T08:27:37.710' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24133, 5, N'admin', N'::1', CAST(N'2020-01-24T15:05:06.310' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24134, 5, N'admin', N'::1', CAST(N'2020-01-24T15:08:00.917' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24135, 5, N'admin', N'::1', CAST(N'2020-01-24T15:11:34.997' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24136, 5, N'admin', N'::1', CAST(N'2020-01-24T15:19:55.180' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24137, 5, N'admin', N'::1', CAST(N'2020-01-24T15:23:41.520' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24138, 5, N'admin', N'::1', CAST(N'2020-01-24T15:39:15.390' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24139, 5, N'admin', N'::1', CAST(N'2020-01-24T15:41:42.097' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24140, 5, N'admin', N'::1', CAST(N'2020-01-24T15:44:07.877' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24141, 5, N'admin', N'::1', CAST(N'2020-01-24T15:48:30.020' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24142, 5, N'admin', N'::1', CAST(N'2020-01-24T15:50:59.473' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24143, 5, N'admin', N'::1', CAST(N'2020-01-24T15:57:21.900' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24144, 5, N'admin', N'::1', CAST(N'2020-01-24T16:02:04.060' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (24145, 5, N'admin', N'::1', CAST(N'2020-01-24T16:20:05.090' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25125, 5, N'admin', N'::1', CAST(N'2020-01-24T18:51:35.600' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25126, 5, N'admin', N'::1', CAST(N'2020-01-24T20:45:36.373' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25127, 5, N'admin', N'::1', CAST(N'2020-01-24T20:52:54.667' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25128, 5, N'admin', N'::1', CAST(N'2020-01-24T21:13:03.777' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25129, 5, N'admin', N'::1', CAST(N'2020-01-24T21:16:35.830' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25130, 5, N'admin', N'::1', CAST(N'2020-01-24T21:19:37.940' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25131, 5, N'admin', N'::1', CAST(N'2020-01-24T21:27:53.003' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25132, 5, N'admin', N'::1', CAST(N'2020-01-24T21:31:46.850' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25133, 5, N'admin', N'::1', CAST(N'2020-01-24T21:33:22.233' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25134, 5, N'admin', N'::1', CAST(N'2020-01-24T21:35:05.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25135, 5, N'admin', N'::1', CAST(N'2020-01-24T21:56:24.397' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25136, 5, N'admin', N'::1', CAST(N'2020-01-24T21:58:46.660' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25137, 5, N'admin', N'::1', CAST(N'2020-01-24T22:00:35.583' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25138, 5, N'admin', N'::1', CAST(N'2020-01-24T22:02:36.023' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25139, 5, N'admin', N'::1', CAST(N'2020-01-24T22:06:21.687' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25140, 5, N'admin', N'::1', CAST(N'2020-01-24T22:18:38.123' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25141, 5, N'admin', N'::1', CAST(N'2020-01-25T14:54:24.810' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25142, 5, N'admin', N'::1', CAST(N'2020-01-25T14:56:56.390' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25143, 5, N'admin', N'::1', CAST(N'2020-01-25T15:11:04.903' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25144, 5, N'admin', N'::1', CAST(N'2020-01-25T15:14:42.357' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25145, 5, N'admin', N'::1', CAST(N'2020-01-25T15:32:36.343' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25146, 5, N'admin', N'::1', CAST(N'2020-01-25T15:38:04.100' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25147, 5, N'admin', N'::1', CAST(N'2020-01-26T11:26:03.343' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25148, 5, N'admin', N'::1', CAST(N'2020-01-26T11:36:59.967' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25149, 5, N'admin', N'::1', CAST(N'2020-01-26T12:01:27.960' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25150, 5, N'admin', N'::1', CAST(N'2020-01-26T13:20:18.957' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25151, 5, N'admin', N'::1', CAST(N'2020-01-26T13:22:44.010' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25152, 5, N'admin', N'::1', CAST(N'2020-01-26T13:26:44.387' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25153, 5, N'admin', N'::1', CAST(N'2020-01-26T14:11:51.947' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25154, 5, N'admin', N'::1', CAST(N'2020-01-26T14:22:19.633' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25155, 5, N'admin', N'::1', CAST(N'2020-01-26T14:34:08.567' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25156, 5, N'admin', N'::1', CAST(N'2020-01-26T14:37:05.967' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25157, 5, N'admin', N'::1', CAST(N'2020-01-26T14:42:04.740' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25158, 5, N'admin', N'::1', CAST(N'2020-01-26T14:45:33.717' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (25159, 5, N'admin', N'::1', CAST(N'2020-01-26T14:57:24.623' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26125, 5, N'admin', N'::1', CAST(N'2020-01-28T20:25:45.767' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26126, 5, N'admin', N'::1', CAST(N'2020-01-29T09:36:49.980' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26127, 5, N'admin', N'::1', CAST(N'2020-01-29T14:46:02.377' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26128, 5, N'admin', N'::1', CAST(N'2020-01-29T15:08:42.057' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26129, 5, N'admin', N'::1', CAST(N'2020-01-29T15:26:02.403' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26130, 5, N'admin', N'::1', CAST(N'2020-01-29T15:40:02.230' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26131, 5, N'admin', N'::1', CAST(N'2020-01-29T15:58:17.840' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26132, 5, N'admin', N'::1', CAST(N'2020-01-29T16:01:57.657' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26133, 5, N'admin', N'::1', CAST(N'2020-01-29T16:08:48.967' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26134, 5, N'admin', N'::1', CAST(N'2020-01-29T21:00:37.543' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26135, 5, N'admin', N'::1', CAST(N'2020-01-30T16:14:49.447' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26136, 5, N'admin', N'::1', CAST(N'2020-01-30T22:12:51.577' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26137, 5, N'admin', N'::1', CAST(N'2020-01-30T22:44:39.867' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26138, 5, N'admin', N'::1', CAST(N'2020-01-30T22:56:54.120' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26139, 5, N'admin', N'::1', CAST(N'2020-01-31T21:30:12.983' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (26140, 5, N'admin', N'::1', CAST(N'2020-01-31T21:41:05.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27125, 5, N'admin', N'::1', CAST(N'2020-02-03T10:41:45.527' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27126, 5, N'admin', N'::1', CAST(N'2020-02-03T10:47:03.407' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27127, 5, N'admin', N'::1', CAST(N'2020-02-03T11:32:47.147' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27128, 5, N'admin', N'::1', CAST(N'2020-02-03T12:04:35.890' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27129, 5, N'admin', N'::1', CAST(N'2020-02-03T12:13:10.240' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27130, 5, N'admin', N'::1', CAST(N'2020-02-03T12:16:37.973' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27131, 5, N'admin', N'::1', CAST(N'2020-02-03T12:18:50.910' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27132, 5, N'admin', N'::1', CAST(N'2020-02-17T11:23:35.020' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27133, 5, N'admin', N'::1', CAST(N'2020-02-18T09:46:21.697' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27134, 5, N'admin', N'::1', CAST(N'2020-02-18T13:53:20.810' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27135, 5, N'admin', N'::1', CAST(N'2020-02-20T22:58:36.973' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27136, 5, N'admin', N'::1', CAST(N'2020-02-21T10:54:17.413' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27137, 5, N'admin', N'::1', CAST(N'2020-02-21T11:51:15.350' AS DateTime))
GO
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27138, 5, N'admin', N'::1', CAST(N'2020-02-22T21:19:21.357' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27139, 5, N'admin', N'::1', CAST(N'2020-02-23T13:58:11.843' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27140, 5, N'admin', N'::1', CAST(N'2020-02-24T13:48:18.023' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27141, 5, N'admin', N'::1', CAST(N'2020-02-24T14:18:14.937' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27142, 5, N'admin', N'::1', CAST(N'2020-02-24T14:31:10.530' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27143, 5, N'admin', N'::1', CAST(N'2020-02-24T14:35:52.003' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27144, 5, N'admin', N'::1', CAST(N'2020-02-24T14:38:55.607' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27145, 5, N'admin', N'::1', CAST(N'2020-02-24T14:41:27.253' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27146, 5, N'admin', N'::1', CAST(N'2020-02-24T14:45:18.060' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27147, 5, N'admin', N'::1', CAST(N'2020-02-24T14:51:58.943' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27148, 5, N'admin', N'::1', CAST(N'2020-02-25T11:33:05.060' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27149, 5, N'admin', N'::1', CAST(N'2020-02-25T11:45:11.013' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27150, 5, N'admin', N'::1', CAST(N'2020-02-25T11:46:50.353' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27151, 5, N'admin', N'::1', CAST(N'2020-02-25T11:49:49.257' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27152, 5, N'admin', N'::1', CAST(N'2020-02-25T11:52:46.050' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27153, 5, N'admin', N'::1', CAST(N'2020-02-25T13:31:08.063' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27154, 5, N'admin', N'::1', CAST(N'2020-02-25T13:42:22.807' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27155, 5, N'admin', N'::1', CAST(N'2020-02-25T13:56:30.233' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27156, 5, N'admin', N'::1', CAST(N'2020-02-25T14:05:44.143' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27157, 5, N'admin', N'::1', CAST(N'2020-02-25T14:10:00.377' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27158, 5, N'admin', N'::1', CAST(N'2020-02-28T09:44:58.637' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27159, 5, N'admin', N'::1', CAST(N'2020-02-29T11:23:51.330' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (27160, 5, N'admin', N'::1', CAST(N'2020-02-29T11:47:59.097' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (28158, 5, N'admin', N'::1', CAST(N'2020-03-01T15:05:23.673' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (28159, 5, N'admin', N'::1', CAST(N'2020-03-02T15:05:19.100' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (29158, 5, N'admin', N'::1', CAST(N'2020-03-03T20:45:40.663' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (29159, 5, N'admin', N'::1', CAST(N'2020-03-04T09:07:24.757' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (29160, 5, N'admin', N'::1', CAST(N'2020-03-04T09:20:27.547' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30158, 5, N'admin', N'::1', CAST(N'2020-03-04T15:55:12.477' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30159, 5, N'admin', N'::1', CAST(N'2020-03-05T06:21:48.963' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30160, 5, N'admin', N'::1', CAST(N'2020-03-05T13:17:37.783' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30161, 5, N'admin', N'::1', CAST(N'2020-03-05T15:17:26.720' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30162, 5, N'admin', N'::1', CAST(N'2020-03-05T15:26:03.643' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30163, 5, N'admin', N'::1', CAST(N'2020-03-05T23:23:03.443' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30164, 5, N'admin', N'::1', CAST(N'2020-03-14T10:03:59.717' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30165, 5, N'admin', N'::1', CAST(N'2020-03-14T10:16:34.590' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30166, 5, N'admin', N'::1', CAST(N'2020-03-14T10:19:14.367' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30167, 5, N'admin', N'::1', CAST(N'2020-03-14T10:25:56.007' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30168, 5, N'admin', N'::1', CAST(N'2020-03-14T10:50:48.790' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30169, 5, N'admin', N'::1', CAST(N'2020-03-14T14:22:29.953' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30170, 5, N'admin', N'::1', CAST(N'2020-03-22T09:14:35.640' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30171, 5, N'admin', N'::1', CAST(N'2020-03-22T14:39:54.373' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30172, 5, N'admin', N'::1', CAST(N'2020-03-23T11:10:34.313' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30173, 5, N'admin', N'::1', CAST(N'2020-03-23T11:13:22.303' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (30174, 5, N'admin', N'::1', CAST(N'2020-03-23T12:11:42.510' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (31170, 5, N'admin', N'::1', CAST(N'2020-03-26T20:45:03.467' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32170, 5, N'admin', N'::1', CAST(N'2020-03-27T09:23:43.763' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32171, 5, N'admin', N'::1', CAST(N'2020-03-27T09:42:21.593' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32172, 5, N'admin', N'::1', CAST(N'2020-03-27T09:45:29.133' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32173, 5, N'admin', N'::1', CAST(N'2020-03-27T09:48:48.893' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32174, 5, N'admin', N'::1', CAST(N'2020-03-27T09:56:14.507' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32175, 5, N'admin', N'::1', CAST(N'2020-03-27T10:00:29.197' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32176, 5, N'admin', N'::1', CAST(N'2020-03-27T10:03:39.380' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32177, 5, N'admin', N'::1', CAST(N'2020-03-27T10:09:22.633' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32178, 5, N'admin', N'::1', CAST(N'2020-03-27T10:11:24.893' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32179, 5, N'admin', N'::1', CAST(N'2020-03-27T10:18:54.927' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32180, 5, N'admin', N'::1', CAST(N'2020-03-27T10:26:20.243' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32181, 5, N'admin', N'::1', CAST(N'2020-03-27T10:28:52.860' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32182, 5, N'admin', N'::1', CAST(N'2020-03-27T10:31:07.887' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32183, 5, N'admin', N'::1', CAST(N'2020-03-27T10:35:02.460' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32184, 5, N'admin', N'::1', CAST(N'2020-03-27T10:37:46.623' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (32185, 5, N'admin', N'::1', CAST(N'2020-03-27T10:56:43.643' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33170, 5, N'admin', N'::1', CAST(N'2020-03-27T17:56:48.097' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33171, 5, N'admin', N'::1', CAST(N'2020-03-27T17:58:26.503' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33172, 5, N'admin', N'::1', CAST(N'2020-03-27T18:04:03.073' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33173, 5, N'admin', N'::1', CAST(N'2020-03-27T18:08:03.963' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33174, 5, N'admin', N'::1', CAST(N'2020-03-27T18:11:13.157' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33175, 5, N'admin', N'::1', CAST(N'2020-03-27T21:16:59.950' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33176, 5, N'admin', N'::1', CAST(N'2020-03-27T22:04:55.307' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33177, 5, N'admin', N'::1', CAST(N'2020-03-28T06:29:08.833' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33178, 5, N'admin', N'::1', CAST(N'2020-03-28T07:53:50.847' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33179, 5, N'admin', N'::1', CAST(N'2020-04-09T16:02:13.377' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33180, 5, N'admin', N'::1', CAST(N'2020-04-09T18:21:34.067' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33181, 5, N'admin', N'::1', CAST(N'2020-04-09T19:39:06.983' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33182, 5, N'admin', N'::1', CAST(N'2020-04-10T20:08:19.667' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33183, 5, N'admin', N'::1', CAST(N'2020-04-10T20:13:39.153' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33184, 5, N'admin', N'::1', CAST(N'2020-04-10T22:25:17.767' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33185, 5, N'admin', N'::1', CAST(N'2020-04-10T22:42:59.053' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (33186, 5, N'admin', N'::1', CAST(N'2020-04-11T15:23:31.993' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34186, 5, N'admin', N'::1', CAST(N'2020-04-11T15:46:51.117' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34187, 5, N'admin', N'::1', CAST(N'2020-04-11T16:26:00.223' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34188, 5, N'admin', N'::1', CAST(N'2020-04-11T16:28:53.437' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34189, 5, N'admin', N'::1', CAST(N'2020-04-11T17:00:34.713' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34190, 5, N'admin', N'::1', CAST(N'2020-04-11T17:04:04.080' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34191, 5, N'admin', N'::1', CAST(N'2020-05-12T11:14:43.103' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34192, 5, N'admin', N'::1', CAST(N'2020-05-12T11:47:11.470' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34193, 5, N'admin', N'::1', CAST(N'2020-05-12T12:20:36.017' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34194, 5, N'admin', N'::1', CAST(N'2020-05-12T14:29:31.603' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34195, 5, N'admin', N'::1', CAST(N'2020-05-12T14:35:14.367' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34196, 5, N'admin', N'::1', CAST(N'2020-05-12T14:45:17.157' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34197, 5, N'admin', N'::1', CAST(N'2020-05-12T15:17:47.597' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34198, 5, N'admin', N'::1', CAST(N'2020-05-12T15:21:35.293' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34199, 5, N'admin', N'::1', CAST(N'2020-05-12T15:23:02.583' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34200, 5, N'admin', N'::1', CAST(N'2020-05-12T15:27:14.270' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34201, 5, N'admin', N'::1', CAST(N'2020-05-12T15:29:06.743' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34202, 5, N'admin', N'::1', CAST(N'2020-05-12T16:04:50.490' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34203, 5, N'admin', N'::1', CAST(N'2020-05-12T16:13:02.523' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34204, 5, N'admin', N'::1', CAST(N'2020-05-15T10:11:17.133' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34205, 5, N'admin', N'::1', CAST(N'2020-05-15T10:30:59.037' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34206, 5, N'admin', N'::1', CAST(N'2020-05-15T10:32:30.440' AS DateTime))
GO
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34207, 5, N'admin', N'::1', CAST(N'2020-05-15T10:41:27.327' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34208, 5, N'admin', N'::1', CAST(N'2020-05-15T13:34:27.943' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34209, 5, N'admin', N'::1', CAST(N'2020-05-15T18:31:40.387' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34210, 5, N'admin', N'::1', CAST(N'2020-05-15T21:20:44.363' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34211, 5, N'admin', N'::1', CAST(N'2020-05-15T21:44:10.187' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34212, 5, N'admin', N'::1', CAST(N'2020-05-15T21:50:02.730' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34213, 5, N'admin', N'::1', CAST(N'2020-05-16T06:17:13.990' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34214, 5, N'admin', N'::1', CAST(N'2020-05-16T06:19:39.247' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34215, 5, N'admin', N'::1', CAST(N'2020-05-16T06:54:19.647' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34216, 5, N'admin', N'::1', CAST(N'2020-05-16T08:31:56.867' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34217, 5, N'admin', N'::1', CAST(N'2020-05-16T08:40:22.083' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34218, 5, N'admin', N'::1', CAST(N'2020-05-16T08:47:52.953' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34219, 5, N'admin', N'::1', CAST(N'2020-05-16T08:49:25.763' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34220, 5, N'admin', N'::1', CAST(N'2020-05-16T08:52:58.030' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34221, 5, N'admin', N'::1', CAST(N'2020-05-16T16:10:37.283' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34222, 5, N'admin', N'::1', CAST(N'2020-05-16T16:12:49.520' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34223, 5, N'admin', N'::1', CAST(N'2020-05-16T16:17:18.133' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34224, 5, N'admin', N'::1', CAST(N'2020-05-16T16:21:43.717' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34225, 5, N'admin', N'::1', CAST(N'2020-05-16T16:24:21.857' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34226, 5, N'admin', N'::1', CAST(N'2020-05-16T16:26:06.327' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34227, 5, N'admin', N'::1', CAST(N'2020-05-16T16:33:30.930' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34228, 5, N'admin', N'::1', CAST(N'2020-05-16T17:08:26.440' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34229, 5, N'admin', N'::1', CAST(N'2020-05-16T17:11:30.283' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34230, 5, N'admin', N'::1', CAST(N'2020-05-17T11:05:22.970' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34231, 5, N'admin', N'::1', CAST(N'2020-05-17T11:08:01.390' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34232, 5, N'admin', N'::1', CAST(N'2020-05-17T11:51:20.097' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34233, 5, N'admin', N'127.0.0.1', CAST(N'2020-05-17T11:56:13.757' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34234, 5, N'admin', N'::1', CAST(N'2020-05-17T14:30:06.797' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34235, 5, N'admin', N'::1', CAST(N'2020-05-17T14:31:57.463' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34236, 5, N'admin', N'::1', CAST(N'2020-05-18T06:19:42.917' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34237, 5, N'admin', N'::1', CAST(N'2020-05-18T06:25:09.517' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34238, 5, N'admin', N'::1', CAST(N'2020-05-18T06:41:03.933' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34239, 5, N'admin', N'::1', CAST(N'2020-05-18T06:44:16.790' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34240, 5, N'admin', N'::1', CAST(N'2020-05-18T06:51:10.527' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34241, 5, N'admin', N'::1', CAST(N'2020-05-18T06:55:26.320' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34242, 5, N'admin', N'::1', CAST(N'2020-05-18T07:00:15.817' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34243, 5, N'admin', N'::1', CAST(N'2020-05-18T07:02:04.847' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34244, 5, N'admin', N'::1', CAST(N'2020-05-18T07:20:00.170' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34245, 5, N'admin', N'::1', CAST(N'2020-05-18T12:21:09.277' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34246, 5, N'admin', N'::1', CAST(N'2020-05-18T14:08:34.373' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34247, 5, N'admin', N'::1', CAST(N'2020-05-18T14:11:58.670' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34248, 5, N'admin', N'::1', CAST(N'2020-05-18T14:14:59.550' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34249, 5, N'admin', N'::1', CAST(N'2020-05-18T14:16:33.027' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34250, 5, N'admin', N'::1', CAST(N'2020-05-18T14:23:49.510' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34251, 5, N'admin', N'::1', CAST(N'2020-05-18T18:14:29.110' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34252, 5, N'admin', N'::1', CAST(N'2020-05-18T18:14:33.220' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34253, 5, N'admin', N'::1', CAST(N'2020-05-19T08:25:10.027' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (34254, 5, N'admin', N'::1', CAST(N'2020-05-19T13:02:17.220' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (35191, 5, N'admin', N'::1', CAST(N'2020-05-30T09:37:02.417' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (35192, 5, N'admin', N'::1', CAST(N'2020-05-30T09:38:21.037' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (35193, 5, N'admin', N'::1', CAST(N'2020-05-30T09:49:36.930' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (35194, 5, N'admin', N'::1', CAST(N'2020-05-30T10:13:00.083' AS DateTime))
INSERT [dbo].[SigninLogs] ([SigninId], [UserId], [Username], [UserIP], [SigninTime]) VALUES (35195, 5, N'admin', N'::1', CAST(N'2020-05-30T10:15:14.570' AS DateTime))
SET IDENTITY_INSERT [dbo].[SigninLogs] OFF
SET IDENTITY_INSERT [dbo].[StockInItems] ON 

INSERT [dbo].[StockInItems] ([ID], [StockInID], [Quantity], [AvailableQuantity], [UnitPrice], [UsageTypeID], [GroupID], [CategoryID], [ItemName], [ItemCode], [UnitID], [SizeID], [OriginID], [BrandID], [Remarks], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (1, 1, 33, 33, 33, 1003, 1004, 1005, N'Computer', N'33', 24, 1007, 1018, 1026, N'113', 1, 5, CAST(N'2020-02-29' AS Date), 5, CAST(N'2020-03-05' AS Date))
INSERT [dbo].[StockInItems] ([ID], [StockInID], [Quantity], [AvailableQuantity], [UnitPrice], [UsageTypeID], [GroupID], [CategoryID], [ItemName], [ItemCode], [UnitID], [SizeID], [OriginID], [BrandID], [Remarks], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (2, 2, 23, 23, 120, 1003, 1004, 1005, N'camera', N'123', 27, NULL, 1018, 1026, N'For office use', 1, 5, CAST(N'2020-03-27' AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[StockInItems] OFF
SET IDENTITY_INSERT [dbo].[StockIns] ON 

INSERT [dbo].[StockIns] ([StockInID], [M7Number], [StockInDate], [DeliveryPlace], [ContractorName], [OrderNumber], [OrderDate], [Details], [FilePath], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (1, N'1', CAST(N'2020-02-29' AS Date), N'1', N'1', N'1', CAST(N'2020-02-29' AS Date), N'1', N'/Uploads/StockInScan/2.jpg', 1, 5, CAST(N'2020-02-29' AS Date), 5, CAST(N'2020-03-05' AS Date))
INSERT [dbo].[StockIns] ([StockInID], [M7Number], [StockInDate], [DeliveryPlace], [ContractorName], [OrderNumber], [OrderDate], [Details], [FilePath], [IsActive], [InsertedBy], [InsertedDate], [LastUpdatedBy], [LastUpdatedDate]) VALUES (2, N'3', CAST(N'2020-03-27' AS Date), N'Godam', N'Khan ahmad', N'8', CAST(N'2020-03-27' AS Date), N'Some comments', N'/Uploads/StockInScan/2.jpg', 1, 5, CAST(N'2020-03-27' AS Date), NULL, NULL)
SET IDENTITY_INSERT [dbo].[StockIns] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LookupTypes]    Script Date: 5/30/2020 10:24:55 AM ******/
ALTER TABLE [dbo].[LookupTypes] ADD  CONSTRAINT [IX_LookupTypes] UNIQUE NONCLUSTERED 
(
	[LookupCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LookupValues]    Script Date: 5/30/2020 10:24:55 AM ******/
ALTER TABLE [dbo].[LookupValues] ADD  CONSTRAINT [IX_LookupValues] UNIQUE NONCLUSTERED 
(
	[ValueCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DistributionItems] ADD  CONSTRAINT [DF_DistributionItems_IsReturned]  DEFAULT ((0)) FOR [IsReturned]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[LookupValue_List]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[LookupValue_List]
@LookupCode nvarchar(100) = null,
@ValueName nvarchar(100) = null,
@Lang nvarchar(100) = 'en'

as 

select * from LookupValues where LookupCode = @LookupCode
and (@Lang != 'en' or EnName like '%' + @ValueName + '%')
and (@Lang != 'prs' or DrName like '%' + @ValueName + '%')
and (@Lang != 'ps' or PaName like '%' + @ValueName + '%')
order by (case when @Lang = 'prs' then DrName when @Lang = 'ps' then PaName else EnName end)
GO
/****** Object:  StoredProcedure [dbo].[Menu_List]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Menu_List]
@Lang nvarchar(10) = 'en'
as 

SELECT [MenuId]
		, case @Lang when 'prs' then DrName when 'ps' then PaName else EnName end as [Name]
		,case MenuLevel when 1 then MenuId else SuperMenuId end as ModuleId
      ,[Icon]
      ,[MenuLevel]
      ,[SuperMenuId]
      ,[HasSubMenu]
      ,[Controller]
      ,[Action]
      ,ActionType,
      IsActive

  FROM [Menus]
  
  where IsActive = 1 and Visible = 1
  order by MenuOrder
GO
/****** Object:  StoredProcedure [dbo].[ModuleRoleAccess]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ModuleRoleAccess] @RoleId int as

with m as(
select MenuId from Menus where IsActive = 1 and MenuLevel = 1),
r as (select * from RoleAccess where RoleId = @RoleId)
select m.MenuId , ISNULL(r.RoleId , 0) RoleId
, ISNULL(r.[FullControl], 0) [FullControl]
, ISNULL(r.[Add], 0) [Add]
, ISNULL(r.[Edit], 0) [Edit]
, ISNULL(r.[Search], 0) [Search]
, ISNULL(r.[View], 0) [View]
, ISNULL(r.[Delete], 0) [Delete]
, ISNULL(r.[Print], 0) [Print]
, ISNULL(r.[Download], 0) [Download]
from m left join r on m.MenuId = r.MenuId
order by m.MenuId
GO
/****** Object:  StoredProcedure [dbo].[Modules_List]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Modules_List]

@Lang nvarchar(10) = 'en'

as

select MenuId ,
case @Lang when 'prs' then m.DrName when 'ps' then m.PaName else m.EnName end MenuName

from Menus m 
where IsActive = 1 and MenuLevel = 1
order by MenuId
GO
/****** Object:  StoredProcedure [dbo].[Ticket_Items]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Ticket_Items]

@id int,
@Lang nvarchar(10) = 'en'

as

select i.ID,i.UnitID ,i.TicketID , i.Quantity , i.ItemDetails ,i.UnitPrice, i.DealWithAccount,
TotalPrice = i.Quantity * i.UnitPrice,
case @Lang when 'prs' then l.DrName when 'ps' then l.PaName else l.EnName end UnitName

from TicketItems i
join LookupValues l on l.ValueId = i.UnitID

where i.IsActive = 1 and i.TicketID = @id
order by i.ID
GO
/****** Object:  StoredProcedure [dbo].[User_List]    Script Date: 5/30/2020 10:24:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[User_List]

@Lang nvarchar(100) = 'en'

as begin

select u.Id , u.IsActive , u.DisplayName , u.Username , r.Name UserRole


from AspNetUsers u 
left join AspNetUserRoles ur on u.Id = ur.UserId
left join AspNetRoles r on ur.RoleId = r.Id

where u.Username != 'admin'


order by u.Id desc

end
GO
USE [master]
GO
ALTER DATABASE [Inventory] SET  READ_WRITE 
GO
