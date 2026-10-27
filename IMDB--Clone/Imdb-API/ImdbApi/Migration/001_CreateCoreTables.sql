SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF SCHEMA_ID(N'Foundation') IS NULL
        EXEC(N'CREATE SCHEMA [Foundation]');

    IF OBJECT_ID(N'[Foundation].[Producers]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Producers]
        (
            [Id]   int IDENTITY(1,1) NOT NULL,
            [Name] varchar(500) NOT NULL,
            [Sex]  char(20) NULL,
            [DOB]  date NULL,
            [Bio]  varchar(max) NULL,
            [UpdatedAt] datetime2 NULL,
            CONSTRAINT [PK_Producers] PRIMARY KEY ([Id])
        );
    END;

    IF COL_LENGTH(N'[Foundation].[Producers]', N'UpdatedAt') IS NULL
        ALTER TABLE [Foundation].[Producers] ADD [UpdatedAt] datetime2 NULL;

    IF OBJECT_ID(N'[Foundation].[Actors]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Actors]
        (
            [Id]   int IDENTITY(1,1) NOT NULL,
            [Name] varchar(500) NOT NULL,
            [Sex]  char(20) NULL,
            [DOB]  date NULL,
            [Bio]  varchar(max) NULL,
            [UpdatedAt] datetime2 NULL,
            CONSTRAINT [PK_Actors] PRIMARY KEY ([Id])
        );
    END;

    IF COL_LENGTH(N'[Foundation].[Actors]', N'UpdatedAt') IS NULL
        ALTER TABLE [Foundation].[Actors] ADD [UpdatedAt] datetime2 NULL;

    IF OBJECT_ID(N'[Foundation].[Movies]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Movies]
        (
            [Id]             int IDENTITY(1,1) NOT NULL,
            [Name]           varchar(500) NOT NULL,
            [year_of_release] int NULL,
            [Plot]           varchar(max) NULL,
            [Poster]         varchar(8000) NULL,
            [language]       varchar(100) NULL,
            [profit]         decimal(18,2) NULL,
            [Producer_Id]    int NULL,
            [UpdatedAt]      datetime2 NULL,
            CONSTRAINT [PK_Movies] PRIMARY KEY ([Id]),
            CONSTRAINT [FK_Movies_Producers]
                FOREIGN KEY ([Producer_Id])
                REFERENCES [Foundation].[Producers] ([Id])
        );
    END;

    IF COL_LENGTH(N'[Foundation].[Movies]', N'language') IS NULL
        ALTER TABLE [Foundation].[Movies] ADD [language] varchar(100) NULL;

    IF COL_LENGTH(N'[Foundation].[Movies]', N'profit') IS NULL
        ALTER TABLE [Foundation].[Movies] ADD [profit] decimal(18,2) NULL;

    IF COL_LENGTH(N'[Foundation].[Movies]', N'UpdatedAt') IS NULL
        ALTER TABLE [Foundation].[Movies] ADD [UpdatedAt] datetime2 NULL;

    IF OBJECT_ID(N'[Foundation].[Genres]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Genres]
        (
            [Id]        int IDENTITY(1,1) NOT NULL,
            [Name]      varchar(500) NOT NULL,
            [UpdatedAt] datetime2 NULL,
            CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
        );
    END;

    IF COL_LENGTH(N'[Foundation].[Genres]', N'UpdatedAt') IS NULL
        ALTER TABLE [Foundation].[Genres] ADD [UpdatedAt] datetime2 NULL;

    IF OBJECT_ID(N'[Foundation].[Reviews]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Reviews]
        (
            [Id]        int IDENTITY(1,1) NOT NULL,
            [Message]   varchar(max) NOT NULL,
            [Movie_Id]  int NOT NULL,
            [UpdatedAt] datetime2 NULL,
            CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
            CONSTRAINT [FK_Reviews_Movies]
                FOREIGN KEY ([Movie_Id])
                REFERENCES [Foundation].[Movies] ([Id])
        );
    END;

    IF COL_LENGTH(N'[Foundation].[Reviews]', N'UpdatedAt') IS NULL
        ALTER TABLE [Foundation].[Reviews] ADD [UpdatedAt] datetime2 NULL;

    IF OBJECT_ID(N'[Foundation].[Actors_Movies]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Actors_Movies]
        (
            [Movie_Id] int NOT NULL,
            [Actor_Id] int NOT NULL,
            CONSTRAINT [PK_Actors_Movies] PRIMARY KEY ([Movie_Id], [Actor_Id]),
            CONSTRAINT [FK_Actors_Movies_Movies]
                FOREIGN KEY ([Movie_Id])
                REFERENCES [Foundation].[Movies] ([Id]),
            CONSTRAINT [FK_Actors_Movies_Actors]
                FOREIGN KEY ([Actor_Id])
                REFERENCES [Foundation].[Actors] ([Id])
        );
    END;

    IF OBJECT_ID(N'[Foundation].[Genres_Movies]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Foundation].[Genres_Movies]
        (
            [Movie_Id] int NOT NULL,
            [Genre_Id] int NOT NULL,
            CONSTRAINT [PK_Genres_Movies] PRIMARY KEY ([Movie_Id], [Genre_Id]),
            CONSTRAINT [FK_Genres_Movies_Movies]
                FOREIGN KEY ([Movie_Id])
                REFERENCES [Foundation].[Movies] ([Id]),
            CONSTRAINT [FK_Genres_Movies_Genres]
                FOREIGN KEY ([Genre_Id])
                REFERENCES [Foundation].[Genres] ([Id])
        );
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
