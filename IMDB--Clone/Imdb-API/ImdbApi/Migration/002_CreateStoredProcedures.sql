CREATE OR ALTER PROCEDURE [dbo].[usp_insert_actor]
    @chvName nvarchar(500),
    @txtBio nvarchar(max),
    @dtDOB date,
    @chrSex char(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Foundation].[Actors] ([Name], [Bio], [DOB], [Sex])
    VALUES (@chvName, @txtBio, @dtDOB, @chrSex);

    SELECT CONVERT(int, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_update_actor]
    @intId int,
    @chvName nvarchar(500),
    @txtBio nvarchar(max),
    @dtDOB date,
    @chrSex char(20),
    @dtmUpdateAt datetime2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Foundation].[Actors]
    SET [Name] = @chvName,
        [Bio] = @txtBio,
        [DOB] = @dtDOB,
        [Sex] = @chrSex,
        [UpdatedAt] = @dtmUpdateAt
    WHERE [Id] = @intId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_delete_actor]
    @intId int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    DELETE FROM [Foundation].[Actors_Movies]
    WHERE [Actor_Id] = @intId;

    DELETE FROM [Foundation].[Actors]
    WHERE [Id] = @intId;

    COMMIT TRANSACTION;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_insert_producer]
    @chvName nvarchar(500),
    @txtBio nvarchar(max),
    @dtDOB date,
    @chrSex char(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Foundation].[Producers] ([Name], [Bio], [DOB], [Sex])
    VALUES (@chvName, @txtBio, @dtDOB, @chrSex);

    SELECT CONVERT(int, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_update_producer]
    @intId int,
    @chvName nvarchar(500),
    @txtBio nvarchar(max),
    @dtDOB date,
    @chrSex char(20),
    @dtmUpdateAt datetime2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Foundation].[Producers]
    SET [Name] = @chvName,
        [Bio] = @txtBio,
        [DOB] = @dtDOB,
        [Sex] = @chrSex,
        [UpdatedAt] = @dtmUpdateAt
    WHERE [Id] = @intId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_delete_producer]
    @intId int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [Foundation].[Producers]
    WHERE [Id] = @intId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_insert_genre]
    @chvName nvarchar(500)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Foundation].[Genres] ([Name])
    VALUES (@chvName);

    SELECT CONVERT(int, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_update_genre]
    @intId int,
    @chvName nvarchar(500),
    @dtmUpdatedAt datetime2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Foundation].[Genres]
    SET [Name] = @chvName,
        [UpdatedAt] = @dtmUpdatedAt
    WHERE [Id] = @intId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_delete_genre]
    @intId int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    DELETE FROM [Foundation].[Genres_Movies]
    WHERE [Genre_Id] = @intId;

    DELETE FROM [Foundation].[Genres]
    WHERE [Id] = @intId;

    COMMIT TRANSACTION;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_insert_review]
    @txtMessage nvarchar(max),
    @intMovieId int
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Foundation].[Reviews] ([Message], [Movie_Id])
    VALUES (@txtMessage, @intMovieId);

    SELECT CONVERT(int, SCOPE_IDENTITY());
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_update_review]
    @intId int,
    @txtMessage nvarchar(max),
    @dtmUpdatedAt datetime2
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Foundation].[Reviews]
    SET [Message] = @txtMessage,
        [UpdatedAt] = @dtmUpdatedAt
    WHERE [Id] = @intId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_delete_review]
    @intId int
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [Foundation].[Reviews]
    WHERE [Id] = @intId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_insert_movie]
    @chvName nvarchar(500),
    @intYearOfRelease int,
    @txtPlot nvarchar(max),
    @chvPoster varchar(8000),
    @chrMovieLanguage nvarchar(100),
    @intProfit decimal(18,2),
    @intProducerId int,
    @chvActorIds nvarchar(max),
    @chvGenreIds nvarchar(max)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    INSERT INTO [Foundation].[Movies]
        ([Name], [year_of_release], [Plot], [Poster], [language], [profit], [Producer_Id])
    VALUES
        (@chvName, @intYearOfRelease, @txtPlot, @chvPoster, @chrMovieLanguage, @intProfit, @intProducerId);

    DECLARE @movieId int = CONVERT(int, SCOPE_IDENTITY());

    INSERT INTO [Foundation].[Actors_Movies] ([Movie_Id], [Actor_Id])
    SELECT @movieId, CONVERT(int, LTRIM(RTRIM([value])))
    FROM STRING_SPLIT(@chvActorIds, N',');

    INSERT INTO [Foundation].[Genres_Movies] ([Movie_Id], [Genre_Id])
    SELECT @movieId, CONVERT(int, LTRIM(RTRIM([value])))
    FROM STRING_SPLIT(@chvGenreIds, N',');

    COMMIT TRANSACTION;

    SELECT @movieId;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_update_movie]
    @intId int,
    @chvName nvarchar(500),
    @intYearOfRelease int,
    @txtPlot nvarchar(max),
    @chvPoster varchar(8000),
    @chrMovieLanguage nvarchar(100),
    @intProfit decimal(18,2),
    @intProducerId int,
    @chvActorIds nvarchar(max),
    @chvGenreIds nvarchar(max),
    @dtmUpdatedAt datetime2
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    UPDATE [Foundation].[Movies]
    SET [Name] = @chvName,
        [year_of_release] = @intYearOfRelease,
        [Plot] = @txtPlot,
        [Poster] = @chvPoster,
        [language] = @chrMovieLanguage,
        [profit] = @intProfit,
        [Producer_Id] = @intProducerId,
        [UpdatedAt] = @dtmUpdatedAt
    WHERE [Id] = @intId;

    DELETE FROM [Foundation].[Actors_Movies]
    WHERE [Movie_Id] = @intId;

    INSERT INTO [Foundation].[Actors_Movies] ([Movie_Id], [Actor_Id])
    SELECT @intId, CONVERT(int, LTRIM(RTRIM([value])))
    FROM STRING_SPLIT(@chvActorIds, N',');

    DELETE FROM [Foundation].[Genres_Movies]
    WHERE [Movie_Id] = @intId;

    INSERT INTO [Foundation].[Genres_Movies] ([Movie_Id], [Genre_Id])
    SELECT @intId, CONVERT(int, LTRIM(RTRIM([value])))
    FROM STRING_SPLIT(@chvGenreIds, N',');

    COMMIT TRANSACTION;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_delete_movie]
    @intId int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    DELETE FROM [Foundation].[Actors_Movies]
    WHERE [Movie_Id] = @intId;

    DELETE FROM [Foundation].[Genres_Movies]
    WHERE [Movie_Id] = @intId;

    DELETE FROM [Foundation].[Reviews]
    WHERE [Movie_Id] = @intId;

    DELETE FROM [Foundation].[Movies]
    WHERE [Id] = @intId;

    COMMIT TRANSACTION;
END;
GO
