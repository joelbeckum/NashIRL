USE [master]

IF db_id('NashIRL') IS NULL
  CREATE DATABASE NashIRL
GO

USE NashIRL
GO

DROP TABLE IF EXISTS [UserProfile];
DROP TABLE IF EXISTS [Hobby];
DROP TABLE IF EXISTS [Event];
DROP TABLE IF EXISTS [UserEvent];
DROP TABLE IF EXISTS [Comment];
DROP TABLE IF EXISTS [EventTag];
DROP TABLE IF EXISTS [Tag];
DROP TABLE IF EXISTS [UserType];

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] int,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [UserTypeId] int NOT NULL,
  [ImageUrl] nvarchar(255)
)
GO

CREATE TABLE [Hobby] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [IsApproved] bit NOT NULL,
  [ApprovedBy] int,
  [ApprovedOn] datetime
)
GO

CREATE TABLE [Event] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255),
  [CreatedOn] datetime NOT NULL,
  [EventOn] datetime NOT NULL,
  [ImageUrl] nvarchar(255),
  [UserProfileId] int NOT NULL,
  [HobbyId] int NOT NULL
)
GO

CREATE TABLE [UserEvent] (
  [UserProfileId] int NOT NULL,
  [EventId] int NOT NULL,
  PRIMARY KEY ([UserProfileId], [EventId])
)
GO

CREATE TABLE [Comment] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Body] nvarchar(255),
  [UserProfileId] int NOT NULL,
  [EventId] int NOT NULL,
  [CreatedOn] datetime NOT NULL
)
GO

CREATE TABLE [EventTag] (
  [EventId] int NOT NULL,
  [TagId] int NOT NULL,
  PRIMARY KEY ([EventId, TagId])
)
GO

CREATE TABLE [Tag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [IsApproved] bit NOT NULL,
  [ApprovedBy] int,
  [ApprovedOn] datetime
)
GO

CREATE TABLE [UserType] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Type] nvarchar(255) NOT NULL
)
GO

ALTER TABLE [UserProfile] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id])
GO

ALTER TABLE [Hobby] ADD FOREIGN KEY ([ApprovedBy]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Event] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Tag] ADD FOREIGN KEY ([ApprovedBy]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([EventId]) REFERENCES [Event] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [EventTag] ADD FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserEvent] ADD FOREIGN KEY ([EventId]) REFERENCES [Event] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserEvent] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO
