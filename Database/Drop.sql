USE [TrainingTaskDB]
GO
ALTER TABLE [dbo].[ProjectTask] DROP CONSTRAINT [FK_ProjectTask_Project]
GO
ALTER TABLE [dbo].[ProjectTask] DROP CONSTRAINT [FK_ProjectTask_Employee]
GO
/****** Object:  Table [dbo].[ProjectTask]    Script Date: 3/26/2020 1:59:52 PM ******/
DROP TABLE [dbo].[ProjectTask]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 3/26/2020 1:59:52 PM ******/
DROP TABLE [dbo].[Project]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 3/26/2020 1:59:52 PM ******/
DROP TABLE [dbo].[Employee]
GO
USE [master]
GO
/****** Object:  Database [TrainingTaskDB]    Script Date: 3/26/2020 1:59:52 PM ******/
DROP DATABASE [TrainingTaskDB]
GO