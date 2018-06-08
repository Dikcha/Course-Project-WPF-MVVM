
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/08/2018 00:25:39
-- Generated from EDMX file: C:\Users\Димооооон\Downloads\CourceProject\CourceProject\CourceProject\Database\base.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Notes];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserNotes_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNotes] DROP CONSTRAINT [FK_UserNotes_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserNotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserNotes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserNotes'
CREATE TABLE [dbo].[UserNotes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserLogin] nvarchar(50)  NOT NULL,
    [NoteDate] datetime  NOT NULL,
    [Note] nvarchar(3000)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Login] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Login] ASC);
GO

-- Creating primary key on [Id] in table 'UserNotes'
ALTER TABLE [dbo].[UserNotes]
ADD CONSTRAINT [PK_UserNotes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserLogin] in table 'UserNotes'
ALTER TABLE [dbo].[UserNotes]
ADD CONSTRAINT [FK_UserNotes_User]
    FOREIGN KEY ([UserLogin])
    REFERENCES [dbo].[User]
        ([Login])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNotes_User'
CREATE INDEX [IX_FK_UserNotes_User]
ON [dbo].[UserNotes]
    ([UserLogin]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------