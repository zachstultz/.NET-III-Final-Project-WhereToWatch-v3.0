IF EXISTS(SELECT 1
          FROM   master.dbo.sysdatabases
          WHERE  Name = 'wheretowatch_db')
  BEGIN
      DROP DATABASE wheretowatch_db
      PRINT '' PRINT '*** Dropping Database wheretowatch_db ***'
  END
GO

PRINT '' PRINT '*** creating wheretowatch_db ***'
GO
CREATE DATABASE wheretowatch_db
GO

PRINT '' PRINT '*** Using wheretowatch_db ***'
GO
USE wheretowatch_db
GO

print '' print '*** creating user table ***'
GO
CREATE TABLE [dbo].[User](
	[UserID]	[int] IDENTITY(1000000,1) NOT NULL,
	[Email]			[nvarchar](100)	NOT NULL,
	[FirstName]		[nvarchar](50)	NOT NULL,
	[LastName]		[nvarchar](100)	NOT NULL,
	[PhoneNumber]	[nvarchar](15)	NOT NULL,
	[PasswordHash]	[nvarchar](100)	NOT NULL DEFAULT
	'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]		[bit]			NOT NULL DEFAULT 1,
	CONSTRAINT [pk_userID] PRIMARY KEY([UserID] ASC),
	CONSTRAINT [ak_email] UNIQUE([Email] ASC)
)
GO



print '' print '*** creating user test data ***'
GO
INSERT INTO [dbo].[User]
		([Email], [FirstName], [LastName], [PhoneNumber])
	VALUES
		('admin@company.com', 'First', 'Admin', '0000000000'),
		('joanne@company.com', 'Joanne', 'Smith', '3195551111'),
		('martin@company.com', 'Martin', 'Jones', '3195552222'),
		('leo@company.com', 'Leo', 'Williams', '3195553333'),
		('maria@company.com', 'Maria', 'Perez', '3195554444'),
		('ahmed@company.com', 'Ahmed', 'Nassar', '3195555555'),
		('zach@company.com', 'Zach', 'Stultz', '3195555555')
GO

print '' print '*** creating role table ***'
GO
CREATE TABLE [dbo].[Role](
	[RoleID]		[nvarchar](25)	NOT NULL,
	[Description]	[nvarchar](250) NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** creating role test data ***'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Admin', 'User administrator'),
		('User', 'Inventory manager')
GO

print '' print '*** creating userRole table ***'
GO
CREATE TABLE [dbo].[UserRole] (
	[UserID]			[int]			NOT NULL,
	[RoleID]			[nvarchar](25)	NOT NULL,
	CONSTRAINT [pk_userID_roleID] PRIMARY KEY([UserID], [RoleID]),
	CONSTRAINT [fk_userID] FOREIGN KEY([UserID])
		REFERENCES [dbo].[User]([UserID])
)
GO

print '' print '*** adding fk to userRole table ***'
GO
ALTER TABLE [dbo].[UserRole] WITH NOCHECK
	ADD CONSTRAINT [fk_roleID] FOREIGN KEY([RoleID])
		REFERENCES [dbo].[Role]([RoleID])
		ON UPDATE CASCADE
GO

print '' print '*** adding sample userRole records ***'
GO
INSERT INTO [dbo].[UserRole]
		([UserID], [RoleID])
	VALUES
		(1000000, 'Admin'),
		(1000000, 'User')
GO
print '' print '*** USER PROCEDURES FOR USERS ***'
GO
print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
		@Email			[nvarchar](100),
		@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT(Email)
		FROM [User]
		WHERE Email = @Email
		AND PasswordHash = @PasswordHash
		AND Active = 1
	END
GO

print '' print '*** creating sp_update_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash]
	(
		@Email				[nvarchar](100),
		@OldPasswordHash	[nvarchar](100),
		@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [User]
			SET PasswordHash = @NewPasswordHash
			WHERE Email = @Email
			AND PasswordHash = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
		@Email				[nvarchar](100)
	)
AS
	BEGIN
		SELECT UserID, Email, FirstName, LastName, PhoneNumber, Active
		FROM [User]
		WHERE Email = @Email
	END
GO

print '' print '*** creating sp_select_roles_by_UserID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_UserID]
	(
	@UserID				[int]
	)
AS
	BEGIN
		SELECT 	RoleID
		FROM 	UserRole
		WHERE 	UserID = @UserID
	END
GO


print '' print '*** creating sp_update_user_profile_data ***'
GO
CREATE PROCEDURE [dbo].[sp_update_user_profile_data]
	(
		@UserID					[int],
		@NewEmail				[nvarchar](100),
		@NewFirstName			[nvarchar](50),
		@NewLastName			[nvarchar](50),
		@NewPhoneNumber			[nvarchar](15),
		@OldEmail				[nvarchar](100),
		@OldFirstName			[nvarchar](50),
		@OldLastName			[nvarchar](50),
		@OldPhoneNumber			[nvarchar](15)
	)
AS
	BEGIN
		UPDATE [User]
			SET Email = @NewEmail,
				FirstName = @NewFirstName,
				LastName = @NewLastName,
				PhoneNumber = @NewPhoneNumber
			WHERE UserID = @UserID
			AND Email = @OldEmail
			AND Firstname = @OldFirstName
			AND LastName = @OldLastName
			AND PhoneNumber = @OldPhoneNumber
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** USER PROCEDURES FOR ADMINISTRATORS ***'
GO


print '' print '*** creating sp_insert_new_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user]
(
	@Email			[nvarchar](100),
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](100),
	@PhoneNumber	[nvarchar](15)
)
AS
	BEGIN
		INSERT INTO [dbo].[User]
			([Email], [FirstName], [LastName], [PhoneNumber])
		VALUES
			(@Email, @FirstName, @LastName, @PhoneNumber)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_select_all_users ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_users]
AS
	BEGIN
		SELECT UserID, Email, FirstName, LastName, PhoneNumber, Active
		FROM [User]
		ORDER BY LastName ASC
	END
GO

print '' print '*** creating sp_select_users_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_users_by_active]
(
	@Active			[bit]
)
AS
	BEGIN
		SELECT UserID, Email, FirstName, LastName, PhoneNumber, Active
		FROM [User]
		WHERE Active = @Active
		ORDER BY LastName ASC
	END
GO

print '' print '*** creating sp_select_user_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_id]
	(
		@UserID				[int]
	)
AS
	BEGIN
		SELECT UserID, Email, FirstName, LastName, PhoneNumber, Active
		FROM [User]
		WHERE UserID = @UserID
	END
GO

print '' print '*** creating sp_reset_passwordhash ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_passwordhash]
	(
		@Email				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [User]
			SET PasswordHash = '9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E'
			WHERE Email = @Email
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_reactivate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_user]
	(
	@UserID			[int]
	)
AS
	BEGIN
		UPDATE [User]
			SET Active = 1
			WHERE UserID = @UserID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** creating sp_add_userRole ***'
GO
CREATE PROCEDURE [dbo].[sp_add_userRole]
	(
		@UserID				[int],
		@RoleID				[nvarchar](25)
	)
AS
	BEGIN
		INSERT INTO userRole
				([UserID], [RoleID])
			VALUES
				(@UserID, @RoleID)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_select_all_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_roles]
AS
	BEGIN
		SELECT 	RoleID
		FROM 	Role
		ORDER BY RoleID ASC
	END
GO


print '' print '*** creating sp_safely_remove_userRole ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_remove_userRole]
	(
		@UserID				[int],
		@RoleID				[nvarchar](25)
	)
AS
	BEGIN
		DECLARE @Admins int;

		SELECT @Admins = COUNT(RoleID)
		FROM [User].Role
		WHERE RoleID = 'Admin';

		IF @RoleID = 'Admin' AND @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				DELETE FROM [User].Role
					WHERE UserID = @UserID
					  AND RoleID = @RoleID
				RETURN @@ROWCOUNT
			END
	END
GO

print '' print '*** creating sp_safely_deactivate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_safely_deactivate_user]
	(
		@UserID			[int]
	)
AS
	BEGIN
		DECLARE @Admins int;

		SELECT @Admins = COUNT(RoleID)
		FROM UserRole
		WHERE RoleID = 'Admin'
		  AND UserRole.UserID = @UserID;

		IF @Admins = 1
			BEGIN
				RETURN 0
			END
		ELSE
			BEGIN
				UPDATE [User]
					SET Active = 0
					WHERE [User].UserID = @UserID
				RETURN @@ROWCOUNT
			END
	END
GO

PRINT '' PRINT '*** creating Genres Table ***'
GO
CREATE TABLE [dbo].[Genre]
  (
     [id]      [INT] IDENTITY NOT NULL,
     [name]    [NVARCHAR](100)
     PRIMARY KEY ([id] ASC)
  )
GO

print '' print '*** creating Genres Test Record ***'
GO
SET IDENTITY_INSERT [dbo].[genre] ON
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (12, N'Adventure')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (14, N'Animation')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (16, N'Fantasy')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (18, N'Drama')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (27, N'Horror')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (28, N'Action')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (35, N'Comedy')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (36, N'History')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (37, N'Western')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (53, N'Thriller')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (80, N'Crime')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (99, N'Documentary')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (878, N'Science Fiction')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (9648, N'Myster')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (10402, N'Music')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (10749, N'Romance')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (10751, N'Family')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (10752, N'War')
	INSERT INTO [dbo].[genre] ([id], [name]) VALUES (10770, N'TV Movie')
SET IDENTITY_INSERT [dbo].[genre] OFF

PRINT '' PRINT '*** creating Movie Table ***'
GO
CREATE TABLE [dbo].[Movie]
(
	[MovieId] INT NOT NULL,
    [AccountStates] NVARCHAR(MAX) NULL,
    [Adult] BIT NULL,
    [AlternativeTitles] NVARCHAR(MAX) NULL,
    [BackdropPath] NVARCHAR(MAX) NOT NULL,
    [BelongsToCollection] NVARCHAR(MAX) NULL,
    [Budget] BIGINT NOT NULL,
    [Changes] NVARCHAR(MAX) NULL,
    [Credits] NVARCHAR(MAX) NULL,
    [Genres] NVARCHAR(MAX) NOT NULL,
    [Homepage] NVARCHAR(MAX) NOT NULL,
    [Images] NVARCHAR(MAX) NULL,
    [ImdbId] NVARCHAR(MAX) NULL,
    [Keywords] NVARCHAR(MAX) NOT NULL,
    [Lists] NVARCHAR(MAX) NULL,
    [OriginalLanguage] NVARCHAR(MAX) NOT NULL,
    [OriginalTitle] NVARCHAR(MAX) NOT NULL,
    [Overview] NVARCHAR(MAX) NOT NULL,
    [Popularity] FLOAT NOT NULL,
    [PosterPath] NVARCHAR(MAX) NOT NULL,
    [ProductionCompanies] NVARCHAR(MAX) NOT NULL,
    [ProductionCountries] NVARCHAR(MAX) NOT NULL,
    [ReleaseDate] DATETIME NOT NULL,
    [ReleaseDates] NVARCHAR(MAX) NULL,
    [ExternalIds] NVARCHAR(MAX) NOT NULL,
    [Releases] NVARCHAR(MAX) NOT NULL,
    [Revenue] BIGINT NOT NULL,
    [Reviews] NVARCHAR(MAX) NULL,
    [Runtime] INT NOT NULL,
    [Similar] NVARCHAR(MAX) NULL,
    [Recommendations] NVARCHAR(MAX) NULL,
    [SpokenLanguages] NVARCHAR(MAX) NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Tagline] NVARCHAR(MAX) NOT NULL,
    [Title] NVARCHAR(MAX) NOT NULL,
    [Translations] NVARCHAR(MAX) NULL,
    [Video] BIT NOT NULL,
    [Videos] NVARCHAR(MAX) NOT NULL,
    [VoteAverage] FLOAT NOT NULL,
    [VoteCount] INT NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY ([MovieId])
)
GO

CREATE INDEX [IX_Movie_Budget] ON [dbo].[Movie] ([Budget])
GO

CREATE INDEX [IX_Movie_Popularity] ON [dbo].[Movie] ([Popularity])
GO

CREATE INDEX [IX_Movie_ReleaseDate] ON [dbo].[Movie] ([ReleaseDate])
GO

CREATE INDEX [IX_Movie_Revenue] ON [dbo].[Movie] ([Revenue])
GO

CREATE INDEX [IX_Movie_Runtime] ON [dbo].[Movie] ([Runtime])
GO

CREATE INDEX [IX_Movie_VoteAverage] ON [dbo].[Movie] ([VoteAverage])
GO

CREATE INDEX [IX_Movie_VoteCount] ON [dbo].[Movie] ([VoteCount])
GO



/**************************************
Zach Stultz
Created: 2021/05/06
Description: MOVIE Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** MOVIE STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_insert_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_movie]
	(
		@MovieId INT,
		@AccountStates NVARCHAR(MAX),
		@Adult BIT,
		@AlternativeTitles NVARCHAR(MAX),
		@BackdropPath NVARCHAR(MAX),
		@BelongsToCollection NVARCHAR(MAX),
		@Budget BIGINT,
		@Changes NVARCHAR(MAX),
		@Credits NVARCHAR(MAX),
		@Genres NVARCHAR(MAX),
		@Homepage NVARCHAR(MAX),
		@Images NVARCHAR(MAX),
		@ImdbId NVARCHAR(MAX),
		@Keywords NVARCHAR(MAX),
		@Lists NVARCHAR(MAX),
		@OriginalLanguage NVARCHAR(MAX),
		@OriginalTitle NVARCHAR(MAX),
		@Overview NVARCHAR(MAX),
		@Popularity FLOAT,
		@PosterPath NVARCHAR(MAX),
		@ProductionCompanies NVARCHAR(MAX),
		@ProductionCountries NVARCHAR(MAX),
		@ReleaseDate DATETIME,
		@ReleaseDates NVARCHAR(MAX),
		@ExternalIds NVARCHAR(MAX),
		@Releases NVARCHAR(MAX),
		@Revenue BIGINT,
		@Reviews NVARCHAR(MAX),
		@Runtime INT,
		@Similar NVARCHAR(MAX),
		@Recommendations NVARCHAR(MAX),
		@SpokenLanguages NVARCHAR(MAX),
		@Status NVARCHAR(MAX),
		@Tagline NVARCHAR(MAX),
		@Title NVARCHAR(MAX),
		@Translations NVARCHAR(MAX),
		@Video BIT,
		@Videos NVARCHAR(MAX),
		@VoteAverage FLOAT,
		@VoteCount INT
	)
AS
	BEGIN
		INSERT INTO Movie
			(MovieId,AccountStates,Adult,AlternativeTitles,BackdropPath,
			BelongsToCollection,Budget,Changes,Credits,Genres,Homepage,
			Images,ImdbId,Keywords,Lists,OriginalLanguage, OriginalTitle,Overview,Popularity,
			PosterPath,ProductionCompanies,ProductionCountries,ReleaseDate,ReleaseDates,
			ExternalIds,Releases,Revenue,Reviews,Runtime,Similar,Recommendations,
			SpokenLanguages,Status,Tagline,Title,Translations,Video,Videos,
			VoteAverage,VoteCount)
		VALUES
			(@MovieId,@AccountStates,@Adult,@AlternativeTitles,@BackdropPath,
			@BelongsToCollection,@Budget,@Changes,@Credits,@Genres,@Homepage,
			@Images,@ImdbId,@Keywords,@Lists,@OriginalLanguage,@OriginalTitle,@Overview,@Popularity,
			@PosterPath,@ProductionCompanies,@ProductionCountries,@ReleaseDate,@ReleaseDates,
			@ExternalIds,@Releases,@Revenue,@Reviews,@Runtime,@Similar,@Recommendations,
			@SpokenLanguages,@Status,@Tagline,@Title,@Translations,@Video,@Videos,
			@VoteAverage,@VoteCount)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/10
***************************************/
print '' print '*** creating sp_delete_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_movie]
	(
		@MovieId INT
	)
AS
	BEGIN
		DELETE FROM [Movie]
		WHERE MovieId=@MovieId
		END
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_update_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_update_movie]
	(
		@MovieId INT,
		@OldAccountStates NVARCHAR(MAX),
		@OldAdult BIT,
		@OldAlternativeTitles NVARCHAR(MAX),
		@OldBackdropPath NVARCHAR(MAX),
		@OldBelongsToCollection NVARCHAR(MAX),
		@OldBudget BIGINT,
		@OldChanges NVARCHAR(MAX),
		@OldCredits NVARCHAR(MAX),
		@OldGenres NVARCHAR(MAX),
		@OldHomepage NVARCHAR(MAX),
		@OldImages NVARCHAR(MAX),
		@OldImdbId NVARCHAR(MAX),
		@OldKeywords NVARCHAR(MAX),
		@OldLists NVARCHAR(MAX),
		@OldOriginalLanguage NVARCHAR(MAX),
		@OldOriginalTitle NVARCHAR(MAX),
		@OldOverview NVARCHAR(MAX),
		@OldPopularity FLOAT,
		@OldPosterPath NVARCHAR(MAX),
		@OldProductionCompanies NVARCHAR(MAX),
		@OldProductionCountries NVARCHAR(MAX),
		@OldReleaseDate DATETIME,
		@OldReleaseDates NVARCHAR(MAX),
		@OldExternalIds NVARCHAR(MAX),
		@OldReleases NVARCHAR(MAX),
		@OldRevenue BIGINT,
		@OldReviews NVARCHAR(MAX),
		@OldRuntime INT,
		@OldSimilar NVARCHAR(MAX),
		@OldRecommendations NVARCHAR(MAX),
		@OldSpokenLanguages NVARCHAR(MAX),
		@OldStatus NVARCHAR(MAX),
		@OldTagline NVARCHAR(MAX),
		@OldTitle NVARCHAR(MAX),
		@OldTranslations NVARCHAR(MAX),
		@OldVideo BIT,
		@OldVideos NVARCHAR(MAX),
		@OldVoteAverage FLOAT,
		@OldVoteCount INT,
		@NewAccountStates NVARCHAR(MAX),
		@NewAdult BIT,
		@NewAlternativeTitles NVARCHAR(MAX),
		@NewBackdropPath NVARCHAR(MAX),
		@NewBelongsToCollection NVARCHAR(MAX),
		@NewBudget BIGINT,
		@NewChanges NVARCHAR(MAX),
		@NewCredits NVARCHAR(MAX),
		@NewGenres NVARCHAR(MAX),
		@NewHomepage NVARCHAR(MAX),
		@NewImages NVARCHAR(MAX),
		@NewImdbId NVARCHAR(MAX),
		@NewKeywords NVARCHAR(MAX),
		@NewLists NVARCHAR(MAX),
		@NewOriginalLanguage NVARCHAR(MAX),
		@NewOriginalTitle NVARCHAR(MAX),
		@NewOverview NVARCHAR(MAX),
		@NewPopularity FLOAT,
		@NewPosterPath NVARCHAR(MAX),
		@NewProductionCompanies NVARCHAR(MAX),
		@NewProductionCountries NVARCHAR(MAX),
		@NewReleaseDate DATETIME,
		@NewReleaseDates NVARCHAR(MAX),
		@NewExternalIds NVARCHAR(MAX),
		@NewReleases NVARCHAR(MAX),
		@NewRevenue BIGINT,
		@NewReviews NVARCHAR(MAX),
		@NewRuntime INT,
		@NewSimilar NVARCHAR(MAX),
		@NewRecommendations NVARCHAR(MAX),
		@NewSpokenLanguages NVARCHAR(MAX),
		@NewStatus NVARCHAR(MAX),
		@NewTagline NVARCHAR(MAX),
		@NewTitle NVARCHAR(MAX),
		@NewTranslations NVARCHAR(MAX),
		@NewVideo BIT,
		@NewVideos NVARCHAR(MAX),
		@NewVoteAverage FLOAT,
		@NewVoteCount INT
	)
AS
	BEGIN
		UPDATE Movie
			SET AccountStates = @NewAccountStates,
				Adult = @NewAdult,
				AlternativeTitles = @NewAlternativeTitles,
				BackdropPath = @NewBackdropPath,
				BelongsToCollection = @NewBelongsToCollection,
				Budget = @NewBudget,
				Changes = @NewChanges,
				Credits = @NewCredits,
				Genres = @NewGenres,
				Homepage = @NewHomepage,
				Images = @NewImages,
				ImdbId = @NewImdbId,
				Keywords = @NewKeywords,
				Lists = @NewLists,
				OriginalLanguage = @NewOriginalLanguage,
				OriginalTitle = @NewOriginalTitle,
				Overview = @NewOverview,
				Popularity = @NewPopularity,
				PosterPath = @NewPosterPath,
				ProductionCompanies = @NewProductionCompanies,
				ProductionCountries = @NewProductionCountries,
				ReleaseDate = @NewReleaseDate,
				ReleaseDates = @NewReleaseDates,
				ExternalIds = @NewExternalIds,
				Releases = @NewReleases,
				Revenue = @NewRevenue,
				Reviews = @NewReviews,
				Runtime = @NewRuntime,
				Similar = @NewSimilar,
				Recommendations = @NewRecommendations,
				SpokenLanguages = @NewSpokenLanguages,
				Status = @NewStatus,
				Tagline = @NewTagline,
				Title = @NewTitle,
				Translations = @NewTranslations,
				Video = @NewVideo,
				Videos = @NewVideos,
				VoteAverage = @NewVoteAverage,
				VoteCount = @NewVoteCount
			WHERE MovieId = @MovieId
			AND AccountStates = @OldAccountStates
			AND Adult = @OldAdult
			AND AlternativeTitles = @OldAlternativeTitles
			AND BackdropPath = @OldBackdropPath
			AND BelongsToCollection = @OldBelongsToCollection
			AND Budget = @OldBudget
			AND Changes = @OldChanges
			AND Credits = @OldCredits
			AND Genres = @OldGenres
			AND Homepage = @OldHomepage
			AND Images = @OldImages
			AND ImdbId = @OldImdbId
			AND Keywords = @OldKeywords
			AND Lists = @OldLists
			AND OriginalLanguage = @OldOriginalLanguage
			AND OriginalTitle = @OldOriginalTitle
			AND Overview = @OldOverview
			AND Popularity = @OldPopularity
			AND PosterPath = @OldPosterPath
			AND ProductionCompanies = @OldProductionCompanies
			AND ProductionCountries = @OldProductionCountries
			AND ReleaseDate = @OldReleaseDate
			AND ReleaseDates = @OldReleaseDates
			AND ExternalIds = @OldExternalIds
			AND Releases = @OldReleases
			AND Revenue = @OldRevenue
			AND Reviews = @OldReviews
			AND Runtime = @OldRuntime
			AND Similar = @OldSimilar
			AND Recommendations = @OldRecommendations
			AND SpokenLanguages = @OldSpokenLanguages
			AND Status = @OldStatus
			AND Tagline = @OldTagline
			AND Title = @OldTitle
			AND Translations = @OldTranslations
			AND Video = @OldVideo
			AND Videos = @OldVideos
			AND VoteAverage = @OldVoteAverage
			AND VoteCount = @OldVoteCount
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_select_all_movies ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_movies]
AS
	BEGIN
		SELECT	MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM		Movie
		ORDER BY 	MovieId ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_retrieve_movie_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_movie_by_id]
	(
		@MovieId INT
	)
AS
	BEGIN
		SELECT MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM Movie
		WHERE MovieId = @MovieId
	END
GO


PRINT '' PRINT '*** creating Trending Movies Table ***'
GO
CREATE TABLE [dbo].[TrendingMovies]
(
	[MovieId] INT NOT NULL,
    [AccountStates] NVARCHAR(MAX) NULL,
    [Adult] BIT NULL,
    [AlternativeTitles] NVARCHAR(MAX) NULL,
    [BackdropPath] NVARCHAR(MAX) NOT NULL,
    [BelongsToCollection] NVARCHAR(MAX) NULL,
    [Budget] BIGINT NOT NULL,
    [Changes] NVARCHAR(MAX) NULL,
    [Credits] NVARCHAR(MAX) NULL,
    [Genres] NVARCHAR(MAX) NOT NULL,
    [Homepage] NVARCHAR(MAX) NOT NULL,
    [Images] NVARCHAR(MAX) NULL,
    [ImdbId] NVARCHAR(MAX) NULL,
    [Keywords] NVARCHAR(MAX) NOT NULL,
    [Lists] NVARCHAR(MAX) NULL,
    [OriginalLanguage] NVARCHAR(MAX) NOT NULL,
    [OriginalTitle] NVARCHAR(MAX) NOT NULL,
    [Overview] NVARCHAR(MAX) NOT NULL,
    [Popularity] FLOAT NOT NULL,
    [PosterPath] NVARCHAR(MAX) NOT NULL,
    [ProductionCompanies] NVARCHAR(MAX) NOT NULL,
    [ProductionCountries] NVARCHAR(MAX) NOT NULL,
    [ReleaseDate] DATETIME NOT NULL,
    [ReleaseDates] NVARCHAR(MAX) NULL,
    [ExternalIds] NVARCHAR(MAX) NOT NULL,
    [Releases] NVARCHAR(MAX) NOT NULL,
    [Revenue] BIGINT NOT NULL,
    [Reviews] NVARCHAR(MAX) NULL,
    [Runtime] INT NOT NULL,
    [Similar] NVARCHAR(MAX) NULL,
    [Recommendations] NVARCHAR(MAX) NULL,
    [SpokenLanguages] NVARCHAR(MAX) NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Tagline] NVARCHAR(MAX) NOT NULL,
    [Title] NVARCHAR(MAX) NOT NULL,
    [Translations] NVARCHAR(MAX) NULL,
    [Video] BIT NOT NULL,
    [Videos] NVARCHAR(MAX) NOT NULL,
    [VoteAverage] FLOAT NOT NULL,
    [VoteCount] INT NOT NULL,
    CONSTRAINT [PK_Trending_Movie] PRIMARY KEY ([MovieId])
)
GO

/**************************************
Zach Stultz
Created: 2021/05/08
Description: TRENDING MOVIE Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** MOVIE STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_insert_trending_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_trending_movie]
	(
		@MovieId INT,
		@AccountStates NVARCHAR(MAX),
		@Adult BIT,
		@AlternativeTitles NVARCHAR(MAX),
		@BackdropPath NVARCHAR(MAX),
		@BelongsToCollection NVARCHAR(MAX),
		@Budget BIGINT,
		@Changes NVARCHAR(MAX),
		@Credits NVARCHAR(MAX),
		@Genres NVARCHAR(MAX),
		@Homepage NVARCHAR(MAX),
		@Images NVARCHAR(MAX),
		@ImdbId NVARCHAR(MAX),
		@Keywords NVARCHAR(MAX),
		@Lists NVARCHAR(MAX),
		@OriginalLanguage NVARCHAR(MAX),
		@OriginalTitle NVARCHAR(MAX),
		@Overview NVARCHAR(MAX),
		@Popularity FLOAT,
		@PosterPath NVARCHAR(MAX),
		@ProductionCompanies NVARCHAR(MAX),
		@ProductionCountries NVARCHAR(MAX),
		@ReleaseDate DATETIME,
		@ReleaseDates NVARCHAR(MAX),
		@ExternalIds NVARCHAR(MAX),
		@Releases NVARCHAR(MAX),
		@Revenue BIGINT,
		@Reviews NVARCHAR(MAX),
		@Runtime INT,
		@Similar NVARCHAR(MAX),
		@Recommendations NVARCHAR(MAX),
		@SpokenLanguages NVARCHAR(MAX),
		@Status NVARCHAR(MAX),
		@Tagline NVARCHAR(MAX),
		@Title NVARCHAR(MAX),
		@Translations NVARCHAR(MAX),
		@Video BIT,
		@Videos NVARCHAR(MAX),
		@VoteAverage FLOAT,
		@VoteCount INT
	)
AS
	BEGIN
		INSERT INTO TrendingMovies
			(MovieId,AccountStates,Adult,AlternativeTitles,BackdropPath,
			BelongsToCollection,Budget,Changes,Credits,Genres,Homepage,
			Images,ImdbId,Keywords,Lists,OriginalLanguage, OriginalTitle,Overview,Popularity,
			PosterPath,ProductionCompanies,ProductionCountries,ReleaseDate,ReleaseDates,
			ExternalIds,Releases,Revenue,Reviews,Runtime,Similar,Recommendations,
			SpokenLanguages,Status,Tagline,Title,Translations,Video,Videos,
			VoteAverage,VoteCount)
		VALUES
			(@MovieId,@AccountStates,@Adult,@AlternativeTitles,@BackdropPath,
			@BelongsToCollection,@Budget,@Changes,@Credits,@Genres,@Homepage,
			@Images,@ImdbId,@Keywords,@Lists,@OriginalLanguage,@OriginalTitle,@Overview,@Popularity,
			@PosterPath,@ProductionCompanies,@ProductionCountries,@ReleaseDate,@ReleaseDates,
			@ExternalIds,@Releases,@Revenue,@Reviews,@Runtime,@Similar,@Recommendations,
			@SpokenLanguages,@Status,@Tagline,@Title,@Translations,@Video,@Videos,
			@VoteAverage,@VoteCount)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_update_trending_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_update_trending_movie]
	(
		@MovieId INT,
		@OldAccountStates NVARCHAR(MAX),
		@OldAdult BIT,
		@OldAlternativeTitles NVARCHAR(MAX),
		@OldBackdropPath NVARCHAR(MAX),
		@OldBelongsToCollection NVARCHAR(MAX),
		@OldBudget BIGINT,
		@OldChanges NVARCHAR(MAX),
		@OldCredits NVARCHAR(MAX),
		@OldGenres NVARCHAR(MAX),
		@OldHomepage NVARCHAR(MAX),
		@OldImages NVARCHAR(MAX),
		@OldImdbId NVARCHAR(MAX),
		@OldKeywords NVARCHAR(MAX),
		@OldLists NVARCHAR(MAX),
		@OldOriginalLanguage NVARCHAR(MAX),
		@OldOriginalTitle NVARCHAR(MAX),
		@OldOverview NVARCHAR(MAX),
		@OldPopularity FLOAT,
		@OldPosterPath NVARCHAR(MAX),
		@OldProductionCompanies NVARCHAR(MAX),
		@OldProductionCountries NVARCHAR(MAX),
		@OldReleaseDate DATETIME,
		@OldReleaseDates NVARCHAR(MAX),
		@OldExternalIds NVARCHAR(MAX),
		@OldReleases NVARCHAR(MAX),
		@OldRevenue BIGINT,
		@OldReviews NVARCHAR(MAX),
		@OldRuntime INT,
		@OldSimilar NVARCHAR(MAX),
		@OldRecommendations NVARCHAR(MAX),
		@OldSpokenLanguages NVARCHAR(MAX),
		@OldStatus NVARCHAR(MAX),
		@OldTagline NVARCHAR(MAX),
		@OldTitle NVARCHAR(MAX),
		@OldTranslations NVARCHAR(MAX),
		@OldVideo BIT,
		@OldVideos NVARCHAR(MAX),
		@OldVoteAverage FLOAT,
		@OldVoteCount INT,
		@NewAccountStates NVARCHAR(MAX),
		@NewAdult BIT,
		@NewAlternativeTitles NVARCHAR(MAX),
		@NewBackdropPath NVARCHAR(MAX),
		@NewBelongsToCollection NVARCHAR(MAX),
		@NewBudget BIGINT,
		@NewChanges NVARCHAR(MAX),
		@NewCredits NVARCHAR(MAX),
		@NewGenres NVARCHAR(MAX),
		@NewHomepage NVARCHAR(MAX),
		@NewImages NVARCHAR(MAX),
		@NewImdbId NVARCHAR(MAX),
		@NewKeywords NVARCHAR(MAX),
		@NewLists NVARCHAR(MAX),
		@NewOriginalLanguage NVARCHAR(MAX),
		@NewOriginalTitle NVARCHAR(MAX),
		@NewOverview NVARCHAR(MAX),
		@NewPopularity FLOAT,
		@NewPosterPath NVARCHAR(MAX),
		@NewProductionCompanies NVARCHAR(MAX),
		@NewProductionCountries NVARCHAR(MAX),
		@NewReleaseDate DATETIME,
		@NewReleaseDates NVARCHAR(MAX),
		@NewExternalIds NVARCHAR(MAX),
		@NewReleases NVARCHAR(MAX),
		@NewRevenue BIGINT,
		@NewReviews NVARCHAR(MAX),
		@NewRuntime INT,
		@NewSimilar NVARCHAR(MAX),
		@NewRecommendations NVARCHAR(MAX),
		@NewSpokenLanguages NVARCHAR(MAX),
		@NewStatus NVARCHAR(MAX),
		@NewTagline NVARCHAR(MAX),
		@NewTitle NVARCHAR(MAX),
		@NewTranslations NVARCHAR(MAX),
		@NewVideo BIT,
		@NewVideos NVARCHAR(MAX),
		@NewVoteAverage FLOAT,
		@NewVoteCount INT
	)
AS
	BEGIN
		UPDATE TrendingMovies
			SET AccountStates = @NewAccountStates,
				Adult = @NewAdult,
				AlternativeTitles = @NewAlternativeTitles,
				BackdropPath = @NewBackdropPath,
				BelongsToCollection = @NewBelongsToCollection,
				Budget = @NewBudget,
				Changes = @NewChanges,
				Credits = @NewCredits,
				Genres = @NewGenres,
				Homepage = @NewHomepage,
				Images = @NewImages,
				ImdbId = @NewImdbId,
				Keywords = @NewKeywords,
				Lists = @NewLists,
				OriginalLanguage = @NewOriginalLanguage,
				OriginalTitle = @NewOriginalTitle,
				Overview = @NewOverview,
				Popularity = @NewPopularity,
				PosterPath = @NewPosterPath,
				ProductionCompanies = @NewProductionCompanies,
				ProductionCountries = @NewProductionCountries,
				ReleaseDate = @NewReleaseDate,
				ReleaseDates = @NewReleaseDates,
				ExternalIds = @NewExternalIds,
				Releases = @NewReleases,
				Revenue = @NewRevenue,
				Reviews = @NewReviews,
				Runtime = @NewRuntime,
				Similar = @NewSimilar,
				Recommendations = @NewRecommendations,
				SpokenLanguages = @NewSpokenLanguages,
				Status = @NewStatus,
				Tagline = @NewTagline,
				Title = @NewTitle,
				Translations = @NewTranslations,
				Video = @NewVideo,
				Videos = @NewVideos,
				VoteAverage = @NewVoteAverage,
				VoteCount = @NewVoteCount
			WHERE MovieId = @MovieId
			AND AccountStates = @OldAccountStates
			AND Adult = @OldAdult
			AND AlternativeTitles = @OldAlternativeTitles
			AND BackdropPath = @OldBackdropPath
			AND BelongsToCollection = @OldBelongsToCollection
			AND Budget = @OldBudget
			AND Changes = @OldChanges
			AND Credits = @OldCredits
			AND Genres = @OldGenres
			AND Homepage = @OldHomepage
			AND Images = @OldImages
			AND ImdbId = @OldImdbId
			AND Keywords = @OldKeywords
			AND Lists = @OldLists
			AND OriginalLanguage = @OldOriginalLanguage
			AND OriginalTitle = @OldOriginalTitle
			AND Overview = @OldOverview
			AND Popularity = @OldPopularity
			AND PosterPath = @OldPosterPath
			AND ProductionCompanies = @OldProductionCompanies
			AND ProductionCountries = @OldProductionCountries
			AND ReleaseDate = @OldReleaseDate
			AND ReleaseDates = @OldReleaseDates
			AND ExternalIds = @OldExternalIds
			AND Releases = @OldReleases
			AND Revenue = @OldRevenue
			AND Reviews = @OldReviews
			AND Runtime = @OldRuntime
			AND Similar = @OldSimilar
			AND Recommendations = @OldRecommendations
			AND SpokenLanguages = @OldSpokenLanguages
			AND Status = @OldStatus
			AND Tagline = @OldTagline
			AND Title = @OldTitle
			AND Translations = @OldTranslations
			AND Video = @OldVideo
			AND Videos = @OldVideos
			AND VoteAverage = @OldVoteAverage
			AND VoteCount = @OldVoteCount
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_select_all_trending_movies ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_trending_movies]
AS
	BEGIN
		SELECT	MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM		TrendingMovies
		ORDER BY 	MovieId ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_retrieve_trending_movie_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_trending_movie_by_id]
	(
		@MovieId INT
	)
AS
	BEGIN
		SELECT MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM TrendingMovies
		WHERE MovieId = @MovieId
	END
GO

PRINT '' PRINT '*** creating TV Table ***'
GO
CREATE TABLE [dbo].[TV]
(
	[TvId] INT NOT NULL,
    [AccountStates] NVARCHAR(MAX) NULL,
    [AlternativeTitles] NVARCHAR(MAX) NULL,
    [BackdropPath] NVARCHAR(MAX) NOT NULL,
    [Changes] NVARCHAR(MAX) NULL,
    [ContentRatings] NVARCHAR(MAX) NULL,
    [CreatedBy] NVARCHAR(MAX) NOT NULL,
    [Credits] NVARCHAR(MAX) NULL,
    [EpisodeRunTime] INT NOT NULL,
    [ExternalIds] NVARCHAR(MAX) NULL,
    [FirstAirDate] DATETIME NOT NULL,
    [GenreIds] INT NULL,
    [Genres] NVARCHAR(MAX) NOT NULL,
    [Homepage] NVARCHAR(MAX) NOT NULL,
    [Images] NVARCHAR(MAX) NULL,
    [InProduction] BIT NOT NULL,
    [Keywords] NVARCHAR(MAX) NOT NULL,
    [Languages] NVARCHAR(MAX) NOT NULL,
    [LastAirDate] DATETIME NOT NULL,
    [LastEpisodeToAir] NVARCHAR(MAX) NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL,
    [NextEpisodeToAir] NVARCHAR(MAX) NULL,
    [Networks] NVARCHAR(MAX) NOT NULL,
    [NumberOfEpisodes] INT NOT NULL,
    [NumberOfSeasons] INT NOT NULL,
    [OriginalLanguage] NVARCHAR(MAX) NOT NULL,
    [OriginalName] NVARCHAR(MAX) NOT NULL,
    [OriginCountry] NVARCHAR(MAX) NOT NULL,
    [Overview] NVARCHAR(MAX) NOT NULL,
    [Popularity] FLOAT NOT NULL,
    [PosterPath] NVARCHAR(MAX) NOT NULL,
    [ProductionCompanies] NVARCHAR(MAX) NOT NULL,
    [Recommendations] NVARCHAR(MAX) NULL,
    [Reviews] NVARCHAR(MAX) NULL,
    [Seasons] NVARCHAR(MAX) NOT NULL,
    [Similar] NVARCHAR(MAX) NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Translations] NVARCHAR(MAX) NULL,
    [Type] NVARCHAR(MAX) NOT NULL,
    [Videos] NVARCHAR(MAX) NOT NULL,
    [VoteAverage] FLOAT NOT NULL,
    [VoteCount] INT NOT NULL,
    CONSTRAINT [PK_Tv] PRIMARY KEY ([TvId])
)
GO

CREATE INDEX [IX_TV_FirstAirDate] ON [dbo].[TV] ([FirstAirDate])
GO

CREATE INDEX [IX_TV_InProduction] ON [dbo].[TV] ([InProduction])
GO

CREATE INDEX [IX_TV_LastAirDate] ON [dbo].[TV] ([LastAirDate])
GO

CREATE INDEX [IX_TV_NumberOfEpisodes] ON [dbo].[TV] ([NumberOfEpisodes])
GO

CREATE INDEX [IX_TV_NumberOfSeasons] ON [dbo].[TV] ([NumberOfSeasons])
GO

CREATE INDEX [IX_TV_Popularity] ON [dbo].[TV] ([Popularity])
GO

CREATE INDEX [IX_TV_VoteAverage] ON [dbo].[TV] ([VoteAverage])
GO

CREATE INDEX [IX_TV_VoteCount] ON [dbo].[TV] ([VoteCount])
GO



/**************************************
Zach Stultz
Created: 2021/05/06
Description: TV-SHOW Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** TV-SHOW STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_insert_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_tv_show]
	(
		@TvId INT,
		@AccountStates NVARCHAR(MAX),
		@AlternativeTitles NVARCHAR(MAX),
		@BackdropPath NVARCHAR(MAX),
		@Changes NVARCHAR(MAX),
		@ContentRatings NVARCHAR(MAX),
		@CreatedBy NVARCHAR(MAX),
		@Credits NVARCHAR(MAX),
		@EpisodeRunTime INT,
		@ExternalIds NVARCHAR(MAX),
		@FirstAirDate DATETIME,
		@GenreIds INT,
		@Genres NVARCHAR(MAX),
		@Homepage NVARCHAR(MAX),
		@Images NVARCHAR(MAX),
		@InProduction BIT,
		@Keywords NVARCHAR(MAX),
		@Languages NVARCHAR(MAX),
		@LastAirDate DATETIME,
		@LastEpisodeToAir NVARCHAR(MAX),
		@Name NVARCHAR(MAX),
		@NextEpisodeToAir NVARCHAR(MAX),
		@Networks NVARCHAR(MAX),
		@NumberOfEpisodes INT,
		@NumberOfSeasons INT,
		@OriginalLanguage NVARCHAR(MAX),
		@OriginalName NVARCHAR(MAX),
		@OriginCountry NVARCHAR(MAX),
		@Overview NVARCHAR(MAX),
		@Popularity FLOAT,
		@PosterPath NVARCHAR(MAX),
		@ProductionCompanies NVARCHAR(MAX),
		@Recommendations NVARCHAR(MAX),
		@Reviews NVARCHAR(MAX),
		@Seasons NVARCHAR(MAX),
		@Similar NVARCHAR(MAX),
		@Status NVARCHAR(MAX),
		@Translations NVARCHAR(MAX),
		@Type NVARCHAR(MAX),
		@Videos NVARCHAR(MAX),
		@VoteAverage FLOAT,
		@VoteCount INT
	)
AS
	BEGIN
		INSERT INTO TV
			(TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
			CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
			Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
			LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
			NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
			Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
			Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
			VoteCount)
		VALUES
			(@TvId, @AccountStates, @AlternativeTitles, @BackdropPath, @Changes, @ContentRatings,
			@CreatedBy, @Credits, @EpisodeRunTime, @ExternalIds, @FirstAirDate, @GenreIds,
			@Genres, @Homepage, @Images, @InProduction, @Keywords, @Languages, @LastAirDate,
			@LastEpisodeToAir, @Name, @NextEpisodeToAir, @Networks, @NumberOfEpisodes,
			@NumberOfSeasons, @OriginalLanguage, @OriginalName, @OriginCountry, @Overview,
			@Popularity, @PosterPath, @ProductionCompanies, @Recommendations, @Reviews,
			@Seasons, @Similar, @Status, @Translations, @Type, @Videos, @VoteAverage,
			@VoteCount)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/10
***************************************/
print '' print '*** creating sp_delete_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_tv_show]
	(
		@TvId INT
	)
AS
	BEGIN
		DELETE FROM [TV]
		WHERE TvId=@TvId
		END
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_update_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_update_tv_show]
	(
		@TvId INT,
		@OldAccountStates NVARCHAR(MAX),
		@OldAlternativeTitles NVARCHAR(MAX),
		@OldBackdropPath NVARCHAR(MAX),
		@OldChanges NVARCHAR(MAX),
		@OldContentRatings NVARCHAR(MAX),
		@OldCreatedBy NVARCHAR(MAX),
		@OldCredits NVARCHAR(MAX),
		@OldEpisodeRunTime INT,
		@OldExternalIds NVARCHAR(MAX),
		@OldFirstAirDate DATETIME,
		@OldGenreIds INT,
		@OldGenres NVARCHAR(MAX),
		@OldHomepage NVARCHAR(MAX),
		@OldImages NVARCHAR(MAX),
		@OldInProduction BIT,
		@OldKeywords NVARCHAR(MAX),
		@OldLanguages NVARCHAR(MAX),
		@OldLastAirDate DATETIME,
		@OldLastEpisodeToAir NVARCHAR(MAX),
		@OldName NVARCHAR(MAX),
		@OldNextEpisodeToAir NVARCHAR(MAX),
		@OldNetworks NVARCHAR(MAX),
		@OldNumberOfEpisodes INT,
		@OldNumberOfSeasons INT,
		@OldOriginalLanguage NVARCHAR(MAX),
		@OldOriginalName NVARCHAR(MAX),
		@OldOriginCountry NVARCHAR(MAX),
		@OldOverview NVARCHAR(MAX),
		@OldPopularity FLOAT,
		@OldPosterPath NVARCHAR(MAX),
		@OldProductionCompanies NVARCHAR(MAX),
		@OldRecommendations NVARCHAR(MAX),
		@OldReviews NVARCHAR(MAX),
		@OldSeasons NVARCHAR(MAX),
		@OldSimilar NVARCHAR(MAX),
		@OldStatus NVARCHAR(MAX),
		@OldTranslations NVARCHAR(MAX),
		@OldType NVARCHAR(MAX),
		@OldVideos NVARCHAR(MAX),
		@OldVoteAverage FLOAT,
		@OldVoteCount INT,

		@NewAccountStates NVARCHAR(MAX),
		@NewAlternativeTitles NVARCHAR(MAX),
		@NewBackdropPath NVARCHAR(MAX),
		@NewChanges NVARCHAR(MAX),
		@NewContentRatings NVARCHAR(MAX),
		@NewCreatedBy NVARCHAR(MAX),
		@NewCredits NVARCHAR(MAX),
		@NewEpisodeRunTime INT,
		@NewExternalIds NVARCHAR(MAX),
		@NewFirstAirDate DATETIME,
		@NewGenreIds INT,
		@NewGenres NVARCHAR(MAX),
		@NewHomepage NVARCHAR(MAX),
		@NewImages NVARCHAR(MAX),
		@NewInProduction BIT,
		@NewKeywords NVARCHAR(MAX),
		@NewLanguages NVARCHAR(MAX),
		@NewLastAirDate DATETIME,
		@NewLastEpisodeToAir NVARCHAR(MAX),
		@NewName NVARCHAR(MAX),
		@NewNextEpisodeToAir NVARCHAR(MAX),
		@NewNetworks NVARCHAR(MAX),
		@NewNumberOfEpisodes INT,
		@NewNumberOfSeasons INT,
		@NewOriginalLanguage NVARCHAR(MAX),
		@NewOriginalName NVARCHAR(MAX),
		@NewOriginCountry NVARCHAR(MAX),
		@NewOverview NVARCHAR(MAX),
		@NewPopularity FLOAT,
		@NewPosterPath NVARCHAR(MAX),
		@NewProductionCompanies NVARCHAR(MAX),
		@NewRecommendations NVARCHAR(MAX),
		@NewReviews NVARCHAR(MAX),
		@NewSeasons NVARCHAR(MAX),
		@NewSimilar NVARCHAR(MAX),
		@NewStatus NVARCHAR(MAX),
		@NewTranslations NVARCHAR(MAX),
		@NewType NVARCHAR(MAX),
		@NewVideos NVARCHAR(MAX),
		@NewVoteAverage FLOAT,
		@NewVoteCount INT
	)
AS
	BEGIN
		UPDATE TV
			SET AccountStates = @NewAccountStates,
				AlternativeTitles = @NewAlternativeTitles,
				BackdropPath = @NewBackdropPath,
				Changes = @NewChanges,
				ContentRatings = @NewContentRatings,
				CreatedBy = @NewCreatedBy,
				Credits = @NewCredits,
				EpisodeRunTime = @NewEpisodeRunTime,
				ExternalIds = @NewExternalIds,
				FirstAirDate = @NewFirstAirDate,
				GenreIds = @NewGenreIds,
				Genres = @NewGenres,
				Homepage = @NewHomepage,
				Images = @NewImages,
				InProduction = @NewInProduction,
				Keywords = @NewKeywords,
				Languages = @NewLanguages,
				LastAirDate = @NewLastAirDate,
				LastEpisodeToAir = @NewLastEpisodeToAir,
				Name = @NewName,
				NextEpisodeToAir = @NewNextEpisodeToAir,
				Networks = @NewNetworks,
				NumberOfEpisodes = @NewNumberOfEpisodes,
				NumberOfSeasons = @NewNumberOfSeasons,
				OriginalLanguage = @NewOriginalLanguage,
				OriginalName = @NewOriginalName,
				OriginCountry = @NewOriginCountry,
				Overview = @NewOverview,
				Popularity = @NewPopularity,
				PosterPath = @NewPosterPath,
				ProductionCompanies = @NewProductionCompanies,
				Recommendations = @NewRecommendations,
				Reviews = @NewReviews,
				Seasons = @NewSeasons,
				Similar = @NewSimilar,
				Status = @NewStatus,
				Translations = @NewTranslations,
				Type = @NewType,
				Videos = @NewVideos,
				VoteAverage = @NewVoteAverage,
				VoteCount = @NewVoteCount
			WHERE TvId = @TvId
			AND AccountStates = @OldAccountStates
			AND AlternativeTitles = @OldAlternativeTitles
			AND BackdropPath = @OldBackdropPath
			AND Changes = @OldChanges
			AND ContentRatings = @OldContentRatings
			AND CreatedBy = @OldCreatedBy
			AND Credits = @OldCredits
			AND EpisodeRunTime = @OldEpisodeRunTime
			AND ExternalIds = @OldExternalIds
			AND FirstAirDate = @OldFirstAirDate
			AND GenreIds = @OldGenreIds
			AND Genres = @OldGenres
			AND Homepage = @OldHomepage
			AND Images = @OldImages
			AND InProduction = @OldInProduction
			AND Keywords = @OldKeywords
			AND Languages = @OldLanguages
			AND LastAirDate = @OldLastAirDate
			AND LastEpisodeToAir = @OldLastEpisodeToAir
			AND Name = @OldName
			AND NextEpisodeToAir = @OldNextEpisodeToAir
			AND Networks = @OldNetworks
			AND NumberOfEpisodes = @OldNumberOfEpisodes
			AND NumberOfSeasons = @OldNumberOfSeasons
			AND OriginalLanguage = @OldOriginalLanguage
			AND OriginalName = @OldOriginalName
			AND OriginCountry = @OldOriginCountry
			AND Overview = @OldOverview
			AND Popularity = @OldPopularity
			AND PosterPath = @OldPosterPath
			AND ProductionCompanies = @OldProductionCompanies
			AND Recommendations = @OldRecommendations
			AND Reviews = @OldReviews
			AND Seasons = @OldSeasons
			AND Similar = @OldSimilar
			AND Status = @OldStatus
			AND Translations = @OldTranslations
			AND Type = @OldType
			AND Videos = @OldVideos
			AND VoteAverage = @OldVoteAverage
			AND VoteCount = @OldVoteCount
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_select_all_tv_shows ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_tv_shows]
AS
	BEGIN
		SELECT		TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM		TV
		ORDER BY 	TvId ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/06
***************************************/
print '' print '*** creating sp_retrieve_tv_show_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tv_show_by_id]
	(
		@TvId INT
	)
AS
	BEGIN
		SELECT 	TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM TV
		WHERE TvId = @TvId
	END
GO

PRINT '' PRINT '*** creating Trending TV Table ***'
GO
CREATE TABLE [dbo].[TrendingTV]
(
	[TvId] INT NOT NULL,
    [AccountStates] NVARCHAR(MAX) NULL,
    [AlternativeTitles] NVARCHAR(MAX) NULL,
    [BackdropPath] NVARCHAR(MAX) NOT NULL,
    [Changes] NVARCHAR(MAX) NULL,
    [ContentRatings] NVARCHAR(MAX) NULL,
    [CreatedBy] NVARCHAR(MAX) NOT NULL,
    [Credits] NVARCHAR(MAX) NULL,
    [EpisodeRunTime] INT NOT NULL,
    [ExternalIds] NVARCHAR(MAX) NULL,
    [FirstAirDate] DATETIME NOT NULL,
    [GenreIds] INT NULL,
    [Genres] NVARCHAR(MAX) NOT NULL,
    [Homepage] NVARCHAR(MAX) NOT NULL,
    [Images] NVARCHAR(MAX) NULL,
    [InProduction] BIT NOT NULL,
    [Keywords] NVARCHAR(MAX) NOT NULL,
    [Languages] NVARCHAR(MAX) NOT NULL,
    [LastAirDate] DATETIME NOT NULL,
    [LastEpisodeToAir] NVARCHAR(MAX) NOT NULL,
    [Name] NVARCHAR(MAX) NOT NULL,
    [NextEpisodeToAir] NVARCHAR(MAX) NULL,
    [Networks] NVARCHAR(MAX) NOT NULL,
    [NumberOfEpisodes] INT NOT NULL,
    [NumberOfSeasons] INT NOT NULL,
    [OriginalLanguage] NVARCHAR(MAX) NOT NULL,
    [OriginalName] NVARCHAR(MAX) NOT NULL,
    [OriginCountry] NVARCHAR(MAX) NOT NULL,
    [Overview] NVARCHAR(MAX) NOT NULL,
    [Popularity] FLOAT NOT NULL,
    [PosterPath] NVARCHAR(MAX) NOT NULL,
    [ProductionCompanies] NVARCHAR(MAX) NOT NULL,
    [Recommendations] NVARCHAR(MAX) NULL,
    [Reviews] NVARCHAR(MAX) NULL,
    [Seasons] NVARCHAR(MAX) NOT NULL,
    [Similar] NVARCHAR(MAX) NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Translations] NVARCHAR(MAX) NULL,
    [Type] NVARCHAR(MAX) NOT NULL,
    [Videos] NVARCHAR(MAX) NOT NULL,
    [VoteAverage] FLOAT NOT NULL,
    [VoteCount] INT NOT NULL,
    CONSTRAINT [PK_Trending_Tv] PRIMARY KEY ([TvId])
)
GO

/**************************************
Zach Stultz
Created: 2021/05/08
Description: TV-SHOW Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** TRENDING TV-SHOW STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_insert_trending_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_trending_tv_show]
	(
		@TvId INT,
		@AccountStates NVARCHAR(MAX),
		@AlternativeTitles NVARCHAR(MAX),
		@BackdropPath NVARCHAR(MAX),
		@Changes NVARCHAR(MAX),
		@ContentRatings NVARCHAR(MAX),
		@CreatedBy NVARCHAR(MAX),
		@Credits NVARCHAR(MAX),
		@EpisodeRunTime INT,
		@ExternalIds NVARCHAR(MAX),
		@FirstAirDate DATETIME,
		@GenreIds INT,
		@Genres NVARCHAR(MAX),
		@Homepage NVARCHAR(MAX),
		@Images NVARCHAR(MAX),
		@InProduction BIT,
		@Keywords NVARCHAR(MAX),
		@Languages NVARCHAR(MAX),
		@LastAirDate DATETIME,
		@LastEpisodeToAir NVARCHAR(MAX),
		@Name NVARCHAR(MAX),
		@NextEpisodeToAir NVARCHAR(MAX),
		@Networks NVARCHAR(MAX),
		@NumberOfEpisodes INT,
		@NumberOfSeasons INT,
		@OriginalLanguage NVARCHAR(MAX),
		@OriginalName NVARCHAR(MAX),
		@OriginCountry NVARCHAR(MAX),
		@Overview NVARCHAR(MAX),
		@Popularity FLOAT,
		@PosterPath NVARCHAR(MAX),
		@ProductionCompanies NVARCHAR(MAX),
		@Recommendations NVARCHAR(MAX),
		@Reviews NVARCHAR(MAX),
		@Seasons NVARCHAR(MAX),
		@Similar NVARCHAR(MAX),
		@Status NVARCHAR(MAX),
		@Translations NVARCHAR(MAX),
		@Type NVARCHAR(MAX),
		@Videos NVARCHAR(MAX),
		@VoteAverage FLOAT,
		@VoteCount INT
	)
AS
	BEGIN
		INSERT INTO TrendingTV
			(TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
			CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
			Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
			LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
			NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
			Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
			Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
			VoteCount)
		VALUES
			(@TvId, @AccountStates, @AlternativeTitles, @BackdropPath, @Changes, @ContentRatings,
			@CreatedBy, @Credits, @EpisodeRunTime, @ExternalIds, @FirstAirDate, @GenreIds,
			@Genres, @Homepage, @Images, @InProduction, @Keywords, @Languages, @LastAirDate,
			@LastEpisodeToAir, @Name, @NextEpisodeToAir, @Networks, @NumberOfEpisodes,
			@NumberOfSeasons, @OriginalLanguage, @OriginalName, @OriginCountry, @Overview,
			@Popularity, @PosterPath, @ProductionCompanies, @Recommendations, @Reviews,
			@Seasons, @Similar, @Status, @Translations, @Type, @Videos, @VoteAverage,
			@VoteCount)
		SELECT SCOPE_IDENTITY()
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_update_trending_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_update_trending_tv_show]
	(
		@TvId INT,
		@OldAccountStates NVARCHAR(MAX),
		@OldAlternativeTitles NVARCHAR(MAX),
		@OldBackdropPath NVARCHAR(MAX),
		@OldChanges NVARCHAR(MAX),
		@OldContentRatings NVARCHAR(MAX),
		@OldCreatedBy NVARCHAR(MAX),
		@OldCredits NVARCHAR(MAX),
		@OldEpisodeRunTime INT,
		@OldExternalIds NVARCHAR(MAX),
		@OldFirstAirDate DATETIME,
		@OldGenreIds INT,
		@OldGenres NVARCHAR(MAX),
		@OldHomepage NVARCHAR(MAX),
		@OldImages NVARCHAR(MAX),
		@OldInProduction BIT,
		@OldKeywords NVARCHAR(MAX),
		@OldLanguages NVARCHAR(MAX),
		@OldLastAirDate DATETIME,
		@OldLastEpisodeToAir NVARCHAR(MAX),
		@OldName NVARCHAR(MAX),
		@OldNextEpisodeToAir NVARCHAR(MAX),
		@OldNetworks NVARCHAR(MAX),
		@OldNumberOfEpisodes INT,
		@OldNumberOfSeasons INT,
		@OldOriginalLanguage NVARCHAR(MAX),
		@OldOriginalName NVARCHAR(MAX),
		@OldOriginCountry NVARCHAR(MAX),
		@OldOverview NVARCHAR(MAX),
		@OldPopularity FLOAT,
		@OldPosterPath NVARCHAR(MAX),
		@OldProductionCompanies NVARCHAR(MAX),
		@OldRecommendations NVARCHAR(MAX),
		@OldReviews NVARCHAR(MAX),
		@OldSeasons NVARCHAR(MAX),
		@OldSimilar NVARCHAR(MAX),
		@OldStatus NVARCHAR(MAX),
		@OldTranslations NVARCHAR(MAX),
		@OldType NVARCHAR(MAX),
		@OldVideos NVARCHAR(MAX),
		@OldVoteAverage FLOAT,
		@OldVoteCount INT,
		@NewAccountStates NVARCHAR(MAX),
		@NewAlternativeTitles NVARCHAR(MAX),
		@NewBackdropPath NVARCHAR(MAX),
		@NewChanges NVARCHAR(MAX),
		@NewContentRatings NVARCHAR(MAX),
		@NewCreatedBy NVARCHAR(MAX),
		@NewCredits NVARCHAR(MAX),
		@NewEpisodeRunTime INT,
		@NewExternalIds NVARCHAR(MAX),
		@NewFirstAirDate DATETIME,
		@NewGenreIds INT,
		@NewGenres NVARCHAR(MAX),
		@NewHomepage NVARCHAR(MAX),
		@NewImages NVARCHAR(MAX),
		@NewInProduction BIT,
		@NewKeywords NVARCHAR(MAX),
		@NewLanguages NVARCHAR(MAX),
		@NewLastAirDate DATETIME,
		@NewLastEpisodeToAir NVARCHAR(MAX),
		@NewName NVARCHAR(MAX),
		@NewNextEpisodeToAir NVARCHAR(MAX),
		@NewNetworks NVARCHAR(MAX),
		@NewNumberOfEpisodes INT,
		@NewNumberOfSeasons INT,
		@NewOriginalLanguage NVARCHAR(MAX),
		@NewOriginalName NVARCHAR(MAX),
		@NewOriginCountry NVARCHAR(MAX),
		@NewOverview NVARCHAR(MAX),
		@NewPopularity FLOAT,
		@NewPosterPath NVARCHAR(MAX),
		@NewProductionCompanies NVARCHAR(MAX),
		@NewRecommendations NVARCHAR(MAX),
		@NewReviews NVARCHAR(MAX),
		@NewSeasons NVARCHAR(MAX),
		@NewSimilar NVARCHAR(MAX),
		@NewStatus NVARCHAR(MAX),
		@NewTranslations NVARCHAR(MAX),
		@NewType NVARCHAR(MAX),
		@NewVideos NVARCHAR(MAX),
		@NewVoteAverage FLOAT,
		@NewVoteCount INT
	)
AS
	BEGIN
		UPDATE TrendingTV
			SET AccountStates = @NewAccountStates,
				AlternativeTitles = @NewAlternativeTitles,
				BackdropPath = @NewBackdropPath,
				Changes = @NewChanges,
				ContentRatings = @NewContentRatings,
				CreatedBy = @NewCreatedBy,
				Credits = @NewCredits,
				EpisodeRunTime = @NewEpisodeRunTime,
				ExternalIds = @NewExternalIds,
				FirstAirDate = @NewFirstAirDate,
				GenreIds = @NewGenreIds,
				Genres = @NewGenres,
				Homepage = @NewHomepage,
				Images = @NewImages,
				InProduction = @NewInProduction,
				Keywords = @NewKeywords,
				Languages = @NewLanguages,
				LastAirDate = @NewLastAirDate,
				LastEpisodeToAir = @NewLastEpisodeToAir,
				Name = @NewName,
				NextEpisodeToAir = @NewNextEpisodeToAir,
				Networks = @NewNetworks,
				NumberOfEpisodes = @NewNumberOfEpisodes,
				NumberOfSeasons = @NewNumberOfSeasons,
				OriginalLanguage = @NewOriginalLanguage,
				OriginalName = @NewOriginalName,
				OriginCountry = @NewOriginCountry,
				Overview = @NewOverview,
				Popularity = @NewPopularity,
				PosterPath = @NewPosterPath,
				ProductionCompanies = @NewProductionCompanies,
				Recommendations = @NewRecommendations,
				Reviews = @NewReviews,
				Seasons = @NewSeasons,
				Similar = @NewSimilar,
				Status = @NewStatus,
				Translations = @NewTranslations,
				Type = @NewType,
				Videos = @NewVideos,
				VoteAverage = @NewVoteAverage,
				VoteCount = @NewVoteCount
			WHERE TvId = @TvId
			AND AccountStates = @OldAccountStates
			AND AlternativeTitles = @OldAlternativeTitles
			AND BackdropPath = @OldBackdropPath
			AND Changes = @OldChanges
			AND ContentRatings = @OldContentRatings
			AND CreatedBy = @OldCreatedBy
			AND Credits = @OldCredits
			AND EpisodeRunTime = @OldEpisodeRunTime
			AND ExternalIds = @OldExternalIds
			AND FirstAirDate = @OldFirstAirDate
			AND GenreIds = @OldGenreIds
			AND Genres = @OldGenres
			AND Homepage = @OldHomepage
			AND Images = @OldImages
			AND InProduction = @OldInProduction
			AND Keywords = @OldKeywords
			AND Languages = @OldLanguages
			AND LastAirDate = @OldLastAirDate
			AND LastEpisodeToAir = @OldLastEpisodeToAir
			AND Name = @OldName
			AND NextEpisodeToAir = @OldNextEpisodeToAir
			AND Networks = @OldNetworks
			AND NumberOfEpisodes = @OldNumberOfEpisodes
			AND NumberOfSeasons = @OldNumberOfSeasons
			AND OriginalLanguage = @OldOriginalLanguage
			AND OriginalName = @OldOriginalName
			AND OriginCountry = @OldOriginCountry
			AND Overview = @OldOverview
			AND Popularity = @OldPopularity
			AND PosterPath = @OldPosterPath
			AND ProductionCompanies = @OldProductionCompanies
			AND Recommendations = @OldRecommendations
			AND Reviews = @OldReviews
			AND Seasons = @OldSeasons
			AND Similar = @OldSimilar
			AND Status = @OldStatus
			AND Translations = @OldTranslations
			AND Type = @OldType
			AND Videos = @OldVideos
			AND VoteAverage = @OldVoteAverage
			AND VoteCount = @OldVoteCount
		RETURN @@ROWCOUNT
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_select_all_trending_tv_shows ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_trending_tv_shows]
AS
	BEGIN
		SELECT		TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM		TrendingTV
		ORDER BY 	TvId ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_retrieve_trending_tv_show_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_trending_tv_show_by_id]
	(
		@TvId INT
	)
AS
	BEGIN
		SELECT 	TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM TrendingTV
		WHERE TvId = @TvId
	END
GO

PRINT '' PRINT '*** creating Actor Table ***'
GO
CREATE TABLE [dbo].[Actor]
  (
     [actorid]      [INT] NOT NULL,
     [adult]        [BIT] NOT NULL DEFAULT '0',
     [alsoknownas]  [VARBINARY],
     [biography]    [NVARCHAR](600),
     [birthday]     [DATETIME] NOT NULL,
     [changes]      [VARBINARY],
     [deathday]     [DATETIME],
     [externalids]  [VARBINARY],
     [gender]       [VARBINARY],
     [homepage]     [NVARCHAR](70),
     [id]           [INT] NOT NULL,
     [images]       [VARBINARY],
     [imdbid]       [NVARCHAR](30) NOT NULL,
     [moviecredits] [VARBINARY],
     [Name]         [NVARCHAR](30) NOT NULL,
     [placeofbirth] [NVARCHAR](100),
     [popularity]   [FLOAT],
     [profilepath]  [NVARCHAR](70),
     [taggedimages] [VARBINARY],
     [tvcredits]    [VARBINARY],
     CONSTRAINT [pk_actorID_ID_imdbID] PRIMARY KEY ([actorid], [id], [imdbid]
     ASC)
  )
GO

CREATE INDEX [SortByActorID]
  ON actor ([actorid]);

CREATE INDEX [SortByAdult]
  ON actor ([adult]);

CREATE INDEX [SortByBirthday]
  ON actor ([birthday]);

CREATE INDEX [SortByID]
  ON actor ([id]);

CREATE INDEX [SortByImdbId]
  ON actor ([imdbid]);

CREATE INDEX [SortByName]
  ON actor ([Name]);

PRINT '' PRINT '*** creating FavoriteMoviesList Table ***'
GO

CREATE TABLE [dbo].[FavoriteMoviesList]
  (
     [FavoriteMoviesListID] [INT] IDENTITY(1000000, 1) NOT NULL,
     [UserID]               [INT] NOT NULL,
     [movies]               [VARBINARY],
     CONSTRAINT [pk_FavoriteMoviesListID] PRIMARY KEY ([FavoriteMoviesListID] ASC),
     /*CONSTRAINT [fk_Email] FOREIGN KEY([UserID])
		REFERENCES [dbo].[user]([UserID])*/
  )
GO

CREATE INDEX [SortByFavoriteMoviesListID]
  ON FavoriteMoviesList ([FavoriteMoviesListID]);

PRINT '' PRINT '*** creating FavoritetvsList Table ***'
GO
CREATE TABLE [dbo].[favoritetvslist]
  (
     [favoritetvslistid] [INT] IDENTITY(1000000, 1) NOT NULL,
     [UserID]                [INT] NOT NULL,
     [tvs]               [VARBINARY],
     CONSTRAINT [pk_favoritetvslistID] PRIMARY KEY ([favoritetvslistid],
     [UserID] ASC)/*,
                    CONSTRAINT [fk_UserID] FOREIGN KEY([UserID])
                      REFERENCES [dbo].[User]([UserID])*/
  )
GO

CREATE INDEX [SortByFavoritetvsListID]
  ON favoritetvslist ([favoritetvslistid]);

PRINT '' PRINT '*** creating FavoriteActorsList Table ***'
GO
CREATE TABLE [dbo].[FavoriteActorsList]
  (
     [FavoriteActorsListid] [INT] IDENTITY(1000000, 1) NOT NULL,
     [UserID]               [INT] NOT NULL,
     [actors]               [VARBINARY],
     CONSTRAINT [pk_FavoriteActorsListID] PRIMARY KEY ([FavoriteActorsListid],
     [UserID] ASC)/*,
                    CONSTRAINT [fk_UserID] FOREIGN KEY([UserID])
                      REFERENCES [dbo].[User]([UserID])*/
  )
GO

CREATE INDEX [SortByFavoriteFavoriteActorsListID]
  ON FavoriteActorsList ([FavoriteActorsListid]);