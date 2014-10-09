
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/03/2014 15:29:50
-- Generated from EDMX file: C:\Projects\Time Tracker\Web\FederalTimeTracker\FederalTimeTracker\Models\Tracker.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FederalTimeTracker];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EmployeeMyProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MyProjects] DROP CONSTRAINT [FK_EmployeeMyProject];
GO
IF OBJECT_ID(N'[dbo].[FK_Employees_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSalary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Salaries] DROP CONSTRAINT [FK_EmployeeSalary];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeTimeSheet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeSheets] DROP CONSTRAINT [FK_EmployeeTimeSheet];
GO
IF OBJECT_ID(N'[dbo].[FK_FYearProjectYearEO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectYearEOs] DROP CONSTRAINT [FK_FYearProjectYearEO];
GO
IF OBJECT_ID(N'[dbo].[FK_FYearTimeSheet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeSheets] DROP CONSTRAINT [FK_FYearTimeSheet];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectMyProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MyProjects] DROP CONSTRAINT [FK_ProjectMyProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectProjectYearEO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectYearEOs] DROP CONSTRAINT [FK_ProjectProjectYearEO];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectTimeEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeEntries] DROP CONSTRAINT [FK_ProjectTimeEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_TimeSheetAuditTimeSheet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuditTimeSheets] DROP CONSTRAINT [FK_TimeSheetAuditTimeSheet];
GO
IF OBJECT_ID(N'[dbo].[FK_TimeSheetTimeEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeEntries] DROP CONSTRAINT [FK_TimeSheetTimeEntry];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AuditTimeSheets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditTimeSheets];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[FYears]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FYears];
GO
IF OBJECT_ID(N'[dbo].[MyProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyProjects];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[ProjectYearEOs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectYearEOs];
GO
IF OBJECT_ID(N'[dbo].[Salaries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Salaries];
GO
IF OBJECT_ID(N'[dbo].[TimeEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeEntries];
GO
IF OBJECT_ID(N'[dbo].[TimeSheets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeSheets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FYears'
CREATE TABLE [dbo].[FYears] (
    [FYearId] int IDENTITY(1,1) NOT NULL,
    [FYearName] nvarchar(250)  NOT NULL,
    [Description] nvarchar(2000)  NULL,
    [Active] bit  NOT NULL,
    [Current] bit  NOT NULL,
    [Note] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectId] int IDENTITY(1,1) NOT NULL,
    [EffectiveDate] datetime  NULL,
    [ClosedDate] datetime  NULL,
    [Active] bit  NOT NULL,
    [ProjectName] nvarchar(500)  NOT NULL,
    [Description] nvarchar(2000)  NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL,
    [Note] nvarchar(max)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(500)  NOT NULL,
    [FirstName] nvarchar(250)  NOT NULL,
    [LastName] nvarchar(250)  NOT NULL,
    [MiddleName] nvarchar(250)  NULL,
    [Title] nvarchar(50)  NULL,
    [Role] smallint  NOT NULL,
    [EmployeeType] smallint  NOT NULL,
    [EmailID] nvarchar(500)  NOT NULL,
    [Active] bit  NOT NULL,
    [ReportingTo] int  NULL,
    [SAMAccountName] nvarchar(max)  NULL,
    [Note] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'MyProjects'
CREATE TABLE [dbo].[MyProjects] (
    [MyProjectId] int IDENTITY(1,1) NOT NULL,
    [EmployeeEmployeeId] int  NOT NULL,
    [ProjectProjectId] int  NOT NULL
);
GO

-- Creating table 'TimeSheets'
CREATE TABLE [dbo].[TimeSheets] (
    [TimeSheetId] int IDENTITY(1,1) NOT NULL,
    [Year] smallint  NOT NULL,
    [Month] smallint  NOT NULL,
    [SubmittedDate] datetime  NULL,
    [ReviewedDate] datetime  NULL,
    [EmployeeComments] nvarchar(max)  NULL,
    [ReviewerComments] nvarchar(max)  NULL,
    [Signature] nvarchar(500)  NULL,
    [FYearFYearId] int  NOT NULL,
    [EmployeeEmployeeId] int  NOT NULL,
    [Status] smallint  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'TimeEntries'
CREATE TABLE [dbo].[TimeEntries] (
    [TimeEntryId] int IDENTITY(1,1) NOT NULL,
    [TimeSheetTimeSheetId] int  NOT NULL,
    [Hours] smallint  NOT NULL,
    [EntryType] int  NOT NULL,
    [ProjectProjectId] int  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL,
    [TimeSheetDate] datetime  NOT NULL
);
GO

-- Creating table 'AuditTimeSheets'
CREATE TABLE [dbo].[AuditTimeSheets] (
    [AuditTimeSheetId] int IDENTITY(1,1) NOT NULL,
    [Status] smallint  NOT NULL,
    [AuditDate] datetime  NOT NULL,
    [Comments] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [TimeSheetTimeSheetId] int  NOT NULL
);
GO

-- Creating table 'Salaries'
CREATE TABLE [dbo].[Salaries] (
    [SalaryId] int IDENTITY(1,1) NOT NULL,
    [EmployeeEmployeeId] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [TotalSalary] float  NOT NULL,
    [Rate] float  NOT NULL,
    [PositionNumber] nvarchar(max)  NOT NULL,
    [EO] nvarchar(max)  NOT NULL,
    [IsITStaff] bit  NOT NULL,
    [Note] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'ProjectYearEOs'
CREATE TABLE [dbo].[ProjectYearEOs] (
    [ProjectYearEOId] int IDENTITY(1,1) NOT NULL,
    [EOCode] nvarchar(50)  NOT NULL,
    [ProjectName] nvarchar(500)  NOT NULL,
    [CreatedBy] nvarchar(250)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedBy] nvarchar(250)  NULL,
    [ModifiedDate] datetime  NULL,
    [Note] nvarchar(max)  NOT NULL,
    [ProjectProjectId] int  NOT NULL,
    [FYearFYearId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [FYearId] in table 'FYears'
ALTER TABLE [dbo].[FYears]
ADD CONSTRAINT [PK_FYears]
    PRIMARY KEY CLUSTERED ([FYearId] ASC);
GO

-- Creating primary key on [ProjectId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [MyProjectId] in table 'MyProjects'
ALTER TABLE [dbo].[MyProjects]
ADD CONSTRAINT [PK_MyProjects]
    PRIMARY KEY CLUSTERED ([MyProjectId] ASC);
GO

-- Creating primary key on [TimeSheetId] in table 'TimeSheets'
ALTER TABLE [dbo].[TimeSheets]
ADD CONSTRAINT [PK_TimeSheets]
    PRIMARY KEY CLUSTERED ([TimeSheetId] ASC);
GO

-- Creating primary key on [TimeEntryId] in table 'TimeEntries'
ALTER TABLE [dbo].[TimeEntries]
ADD CONSTRAINT [PK_TimeEntries]
    PRIMARY KEY CLUSTERED ([TimeEntryId] ASC);
GO

-- Creating primary key on [AuditTimeSheetId] in table 'AuditTimeSheets'
ALTER TABLE [dbo].[AuditTimeSheets]
ADD CONSTRAINT [PK_AuditTimeSheets]
    PRIMARY KEY CLUSTERED ([AuditTimeSheetId] ASC);
GO

-- Creating primary key on [SalaryId] in table 'Salaries'
ALTER TABLE [dbo].[Salaries]
ADD CONSTRAINT [PK_Salaries]
    PRIMARY KEY CLUSTERED ([SalaryId] ASC);
GO

-- Creating primary key on [ProjectYearEOId] in table 'ProjectYearEOs'
ALTER TABLE [dbo].[ProjectYearEOs]
ADD CONSTRAINT [PK_ProjectYearEOs]
    PRIMARY KEY CLUSTERED ([ProjectYearEOId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FYearFYearId] in table 'TimeSheets'
ALTER TABLE [dbo].[TimeSheets]
ADD CONSTRAINT [FK_FYearTimeSheet]
    FOREIGN KEY ([FYearFYearId])
    REFERENCES [dbo].[FYears]
        ([FYearId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FYearTimeSheet'
CREATE INDEX [IX_FK_FYearTimeSheet]
ON [dbo].[TimeSheets]
    ([FYearFYearId]);
GO

-- Creating foreign key on [ProjectProjectId] in table 'MyProjects'
ALTER TABLE [dbo].[MyProjects]
ADD CONSTRAINT [FK_ProjectMyProject]
    FOREIGN KEY ([ProjectProjectId])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectMyProject'
CREATE INDEX [IX_FK_ProjectMyProject]
ON [dbo].[MyProjects]
    ([ProjectProjectId]);
GO

-- Creating foreign key on [ProjectProjectId] in table 'TimeEntries'
ALTER TABLE [dbo].[TimeEntries]
ADD CONSTRAINT [FK_ProjectTimeEntry]
    FOREIGN KEY ([ProjectProjectId])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectTimeEntry'
CREATE INDEX [IX_FK_ProjectTimeEntry]
ON [dbo].[TimeEntries]
    ([ProjectProjectId]);
GO

-- Creating foreign key on [EmployeeEmployeeId] in table 'MyProjects'
ALTER TABLE [dbo].[MyProjects]
ADD CONSTRAINT [FK_EmployeeMyProject]
    FOREIGN KEY ([EmployeeEmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeMyProject'
CREATE INDEX [IX_FK_EmployeeMyProject]
ON [dbo].[MyProjects]
    ([EmployeeEmployeeId]);
GO

-- Creating foreign key on [EmployeeEmployeeId] in table 'TimeSheets'
ALTER TABLE [dbo].[TimeSheets]
ADD CONSTRAINT [FK_EmployeeTimeSheet]
    FOREIGN KEY ([EmployeeEmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeTimeSheet'
CREATE INDEX [IX_FK_EmployeeTimeSheet]
ON [dbo].[TimeSheets]
    ([EmployeeEmployeeId]);
GO

-- Creating foreign key on [EmployeeEmployeeId] in table 'Salaries'
ALTER TABLE [dbo].[Salaries]
ADD CONSTRAINT [FK_EmployeeSalary]
    FOREIGN KEY ([EmployeeEmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSalary'
CREATE INDEX [IX_FK_EmployeeSalary]
ON [dbo].[Salaries]
    ([EmployeeEmployeeId]);
GO

-- Creating foreign key on [TimeSheetTimeSheetId] in table 'TimeEntries'
ALTER TABLE [dbo].[TimeEntries]
ADD CONSTRAINT [FK_TimeSheetTimeEntry]
    FOREIGN KEY ([TimeSheetTimeSheetId])
    REFERENCES [dbo].[TimeSheets]
        ([TimeSheetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TimeSheetTimeEntry'
CREATE INDEX [IX_FK_TimeSheetTimeEntry]
ON [dbo].[TimeEntries]
    ([TimeSheetTimeSheetId]);
GO

-- Creating foreign key on [TimeSheetTimeSheetId] in table 'AuditTimeSheets'
ALTER TABLE [dbo].[AuditTimeSheets]
ADD CONSTRAINT [FK_TimeSheetAuditTimeSheet]
    FOREIGN KEY ([TimeSheetTimeSheetId])
    REFERENCES [dbo].[TimeSheets]
        ([TimeSheetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TimeSheetAuditTimeSheet'
CREATE INDEX [IX_FK_TimeSheetAuditTimeSheet]
ON [dbo].[AuditTimeSheets]
    ([TimeSheetTimeSheetId]);
GO

-- Creating foreign key on [ProjectProjectId] in table 'ProjectYearEOs'
ALTER TABLE [dbo].[ProjectYearEOs]
ADD CONSTRAINT [FK_ProjectProjectYearEO]
    FOREIGN KEY ([ProjectProjectId])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectYearEO'
CREATE INDEX [IX_FK_ProjectProjectYearEO]
ON [dbo].[ProjectYearEOs]
    ([ProjectProjectId]);
GO

-- Creating foreign key on [FYearFYearId] in table 'ProjectYearEOs'
ALTER TABLE [dbo].[ProjectYearEOs]
ADD CONSTRAINT [FK_FYearProjectYearEO]
    FOREIGN KEY ([FYearFYearId])
    REFERENCES [dbo].[FYears]
        ([FYearId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FYearProjectYearEO'
CREATE INDEX [IX_FK_FYearProjectYearEO]
ON [dbo].[ProjectYearEOs]
    ([FYearFYearId]);
GO

-- Creating foreign key on [ReportingTo] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_Employees]
    FOREIGN KEY ([ReportingTo])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Employees_Employees'
CREATE INDEX [IX_FK_Employees_Employees]
ON [dbo].[Employees]
    ([ReportingTo]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------