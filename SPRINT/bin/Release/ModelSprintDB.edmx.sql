
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2018 12:29:32
-- Generated from EDMX file: C:\SPRINT\SPRINT\ModelSprintDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
CREATE DATABASE sprintDB
GO
USE [sprintDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CottagerBilling]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillingSet] DROP CONSTRAINT [FK_CottagerBilling];
GO
IF OBJECT_ID(N'[dbo].[FK_MonthConsumptionBilling]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BillingSet] DROP CONSTRAINT [FK_MonthConsumptionBilling];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BillingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BillingSet];
GO
IF OBJECT_ID(N'[dbo].[CostOfWaterSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CostOfWaterSet];
GO
IF OBJECT_ID(N'[dbo].[CottagerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CottagerSet];
GO
IF OBJECT_ID(N'[dbo].[MonthConsumptionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MonthConsumptionSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CottagerSet'
CREATE TABLE [dbo].[CottagerSet] (
    [CottagerID] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Square] decimal(16,2)  NOT NULL
);
GO

-- Creating table 'MonthConsumptionSet'
CREATE TABLE [dbo].[MonthConsumptionSet] (
    [MonthBillingID] int IDENTITY(1,1) NOT NULL,
    [Date] int  NOT NULL,
    [Consumption] decimal(16,2)  NOT NULL
);
GO

-- Creating table 'BillingSet'
CREATE TABLE [dbo].[BillingSet] (
    [BillingID] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Date] int  NOT NULL,
    [Bill] decimal(16,2)  NOT NULL,
    [MonthConsumption_MonthBillingID] int  NOT NULL,
    [Cottager_CottagerID] int  NOT NULL
);
GO

-- Creating table 'CostOfWaterSet'
CREATE TABLE [dbo].[CostOfWaterSet] (
    [CostID] int IDENTITY(1,1) NOT NULL,
    [Cost] decimal(16,2)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CottagerID] in table 'CottagerSet'
ALTER TABLE [dbo].[CottagerSet]
ADD CONSTRAINT [PK_CottagerSet]
    PRIMARY KEY CLUSTERED ([CottagerID] ASC);
GO

-- Creating primary key on [MonthBillingID] in table 'MonthConsumptionSet'
ALTER TABLE [dbo].[MonthConsumptionSet]
ADD CONSTRAINT [PK_MonthConsumptionSet]
    PRIMARY KEY CLUSTERED ([MonthBillingID] ASC);
GO

-- Creating primary key on [BillingID] in table 'BillingSet'
ALTER TABLE [dbo].[BillingSet]
ADD CONSTRAINT [PK_BillingSet]
    PRIMARY KEY CLUSTERED ([BillingID] ASC);
GO

-- Creating primary key on [CostID] in table 'CostOfWaterSet'
ALTER TABLE [dbo].[CostOfWaterSet]
ADD CONSTRAINT [PK_CostOfWaterSet]
    PRIMARY KEY CLUSTERED ([CostID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MonthConsumption_MonthBillingID] in table 'BillingSet'
ALTER TABLE [dbo].[BillingSet]
ADD CONSTRAINT [FK_MonthConsumptionBilling]
    FOREIGN KEY ([MonthConsumption_MonthBillingID])
    REFERENCES [dbo].[MonthConsumptionSet]
        ([MonthBillingID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MonthConsumptionBilling'
CREATE INDEX [IX_FK_MonthConsumptionBilling]
ON [dbo].[BillingSet]
    ([MonthConsumption_MonthBillingID]);
GO

-- Creating foreign key on [Cottager_CottagerID] in table 'BillingSet'
ALTER TABLE [dbo].[BillingSet]
ADD CONSTRAINT [FK_CottagerBilling]
    FOREIGN KEY ([Cottager_CottagerID])
    REFERENCES [dbo].[CottagerSet]
        ([CottagerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CottagerBilling'
CREATE INDEX [IX_FK_CottagerBilling]
ON [dbo].[BillingSet]
    ([Cottager_CottagerID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------