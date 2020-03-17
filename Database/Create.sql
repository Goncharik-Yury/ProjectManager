USE [master]
GO

/****** Object:  Database [C:\SELEST\PROGRAMS\PROJECTS\DATABASE\TESTTASKDATABASE3.MDF]    Script Date: 3/10/2020 4:44:58 PM ******/
CREATE DATABASE [C:\SELEST\PROGRAMS\PROJECTS\DATABASE\TestTaskDatabase3.MDF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TESTTASKDATABASE3', FILENAME = N'C:\SeleSt\Programs\Projects\Database\TestTaskDatabase3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TESTTASKDATABASE3_log', FILENAME = N'C:\SeleSt\Programs\Projects\Database\TestTaskDatabase3.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

USE [C:\SELEST\PROGRAMS\PROJECTS\DATABASE\TESTTASKDATABASE3.MDF]
GO

/****** Object:  Table [dbo].[ProjectTask]    Script Date: 3/10/2020 4:52:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Lastname] [nvarchar](50) NOT NULL,
    [Firstname] [nvarchar](50) NOT NULL,
    [Patronymic] [nvarchar](50) NULL,
    [Position] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([Id])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO

CREATE TABLE [dbo].[Project] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [ShortName]   NVARCHAR (50)  NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Project2] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[ProjectTask] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [TimeToComplete] INT           NULL,
    [BeginDate]      DATE          NULL,
    [EndDate]        DATE          NULL,
    [Status]         NVARCHAR (50) NOT NULL,
    [ExecutorId]     INT           NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT INTO [dbo].[Employee] ([Id], [Lastname], [Firstname], [Patronymic], [Position]) VALUES (1, N'Goncharik', N'Yury', N'qqq', N'Softaware developer')
INSERT INTO [dbo].[Employee] ([Id], [Lastname], [Firstname], [Patronymic], [Position]) VALUES (2, N'Petrushko', N'Dmitry', N'www', N'noone')
INSERT INTO [dbo].[Employee] ([Id], [Lastname], [Firstname], [Patronymic], [Position]) VALUES (1005, N'qqq', N'qqq', N'qqq', N'qqq')
INSERT INTO [dbo].[Employee] ([Id], [Lastname], [Firstname], [Patronymic], [Position]) VALUES (2002, N'www', N'www', N'', N'www')
INSERT INTO [dbo].[Employee] ([Id], [Lastname], [Firstname], [Patronymic], [Position]) VALUES (3002, N'eee', N'eeettt', N'eee', N'eee')
INSERT INTO [dbo].[Employee] ([Id], [Lastname], [Firstname], [Patronymic], [Position]) VALUES (3005, N'rrr', N'rrr', N'rrr', N'rrr')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO

SET IDENTITY_INSERT [dbo].[Project] ON
INSERT INTO [dbo].[Project] ([Id], [Name], [ShortName], [Description]) VALUES (2, N'qqq', N'qqq', N'qqq')
INSERT INTO [dbo].[Project] ([Id], [Name], [ShortName], [Description]) VALUES (1004, N'www', N'www', N'www')
SET IDENTITY_INSERT [dbo].[Project] OFF
GO

SET IDENTITY_INSERT [dbo].[ProjectTask] ON
INSERT INTO [dbo].[ProjectTask] ([Id], [Name], [TimeToComplete], [BeginDate], [EndDate], [Status], [ExecutorId]) VALUES (2, N'sss', 1, N'0001-01-01', N'0001-01-01', N'5', 0)
INSERT INTO [dbo].[ProjectTask] ([Id], [Name], [TimeToComplete], [BeginDate], [EndDate], [Status], [ExecutorId]) VALUES (3, N'', 1, N'2020-03-09', N'2020-03-09', N'', 0)
INSERT INTO [dbo].[ProjectTask] ([Id], [Name], [TimeToComplete], [BeginDate], [EndDate], [Status], [ExecutorId]) VALUES (4, N'qqq', 2, N'2020-03-09', N'2020-03-09', N'3', 0)
INSERT INTO [dbo].[ProjectTask] ([Id], [Name], [TimeToComplete], [BeginDate], [EndDate], [Status], [ExecutorId]) VALUES (5, N'11111', 11111, N'0001-01-01', N'0001-01-01', N'1111', 1)
SET IDENTITY_INSERT [dbo].[ProjectTask] OFF
GO

