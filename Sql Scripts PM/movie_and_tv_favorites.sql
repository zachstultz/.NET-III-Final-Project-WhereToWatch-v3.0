USE wheretowatch_db
GO

PRINT '' PRINT '*** creating Favorite Movies Table ***'
GO
CREATE TABLE [dbo].[FavoriteMovies]
(
	[FavoriteMovieId]	[int] IDENTITY(1000000,1) NOT NULL,
	[UserId] INT NOT NULL,
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
    CONSTRAINT [PK_FavoriteMovies] PRIMARY KEY ([FavoriteMovieId]),
    CONSTRAINT [FK_FavoriteMovies_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
)
GO

print '' print '*** inserting favorite movies test data ***'
GO
SET IDENTITY_INSERT [dbo].[FavoriteMovies] ON
INSERT INTO [dbo].[FavoriteMovies] ([FavoriteMovieId], [UserId], [MovieId], [AccountStates], [Adult], [AlternativeTitles], [BackdropPath], [BelongsToCollection], [Budget], [Changes], [Credits], [Genres], [Homepage], [Images], [ImdbId], [Keywords], [Lists], [OriginalLanguage], [OriginalTitle], [Overview], [Popularity], [PosterPath], [ProductionCompanies], [ProductionCountries], [ReleaseDate], [ReleaseDates], [ExternalIds], [Releases], [Revenue], [Reviews], [Runtime], [Similar], [Recommendations], [SpokenLanguages], [Status], [Tagline], [Title], [Translations], [Video], [Videos], [VoteAverage], [VoteCount]) VALUES (1000000, 1000006, 11017, N'None', 0, N'None', N'/5ESLsrW33Kw2c3GeLNHrG4Ef9M5.jpg', N'None', 10000000, N'None', N'None', N'Comedy', N'', N'None', N'tt0112508', N'Woman Director', N'None', N'None', N'Billy Madison', N'Billy Madison is the 27 year-old son of Bryan Madison, a very rich man who has made his living in the hotel industry. Billy stands to inherit his father''s empire but only if he can make it through all 12 grades, 2 weeks per grade, to prove that he has what it takes to run the family business.', 12.311, N'http://image.tmdb.org/t/p/w500/1XV42Zu9taxluIKXMeV9OSpYCSq.jpg', N'None', N'None', N'1995-02-10 00:00:00', N'None', N'None', N'None', 26488734, N'None', 89, N'None', N'None', N'None', N'Released', N'Billy is going back to school... Way back.', N'Billy Madison', N'None', 0, N'https://www.youtube.com/watch?v=k3PUNBE9J0A', 6.2, 1151)
INSERT INTO [dbo].[FavoriteMovies] ([FavoriteMovieId], [UserId], [MovieId], [AccountStates], [Adult], [AlternativeTitles], [BackdropPath], [BelongsToCollection], [Budget], [Changes], [Credits], [Genres], [Homepage], [Images], [ImdbId], [Keywords], [Lists], [OriginalLanguage], [OriginalTitle], [Overview], [Popularity], [PosterPath], [ProductionCompanies], [ProductionCountries], [ReleaseDate], [ReleaseDates], [ExternalIds], [Releases], [Revenue], [Reviews], [Runtime], [Similar], [Recommendations], [SpokenLanguages], [Status], [Tagline], [Title], [Translations], [Video], [Videos], [VoteAverage], [VoteCount]) VALUES (1000001, 1000001, 11017, N'None', 0, N'None', N'/5ESLsrW33Kw2c3GeLNHrG4Ef9M5.jpg', N'None', 10000000, N'None', N'None', N'Comedy', N'', N'None', N'tt0112508', N'Woman Director', N'None', N'None', N'Billy Madison', N'Billy Madison is the 27 year-old son of Bryan Madison, a very rich man who has made his living in the hotel industry. Billy stands to inherit his father''s empire but only if he can make it through all 12 grades, 2 weeks per grade, to prove that he has what it takes to run the family business.', 12.311, N'http://image.tmdb.org/t/p/w500/1XV42Zu9taxluIKXMeV9OSpYCSq.jpg', N'None', N'None', N'1995-02-10 00:00:00', N'None', N'None', N'None', 26488734, N'None', 89, N'None', N'None', N'None', N'Released', N'Billy is going back to school... Way back.', N'Billy Madison', N'None', 0, N'https://www.youtube.com/watch?v=k3PUNBE9J0A', 6.2, 1151)
SET IDENTITY_INSERT [dbo].[FavoriteMovies] OFF


/**************************************
Zach Stultz
Created: 2021/05/09
Description: MOVIE Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** FAVORITE MOVIE STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_insert_favorite_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_favorite_movie]
	(
		@UserId INT,
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
		INSERT INTO FavoriteMovies
			(UserId, MovieId,AccountStates,Adult,AlternativeTitles,BackdropPath,
			BelongsToCollection,Budget,Changes,Credits,Genres,Homepage,
			Images,ImdbId,Keywords,Lists,OriginalLanguage, OriginalTitle,Overview,Popularity,
			PosterPath,ProductionCompanies,ProductionCountries,ReleaseDate,ReleaseDates,
			ExternalIds,Releases,Revenue,Reviews,Runtime,Similar,Recommendations,
			SpokenLanguages,Status,Tagline,Title,Translations,Video,Videos,
			VoteAverage,VoteCount)
		VALUES
			(@UserId, @MovieId,@AccountStates,@Adult,@AlternativeTitles,@BackdropPath,
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
print '' print '*** creating sp_delete_favorite_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_favorite_movie]
	(
		@MovieId INT,
		@UserId INT
	)
AS
	BEGIN
		DELETE FROM [FavoriteMovies]
		WHERE MovieId=@MovieId
		AND UserId=@UserId
		END
GO

/**************************************
Zach Stultz
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_update_favorite_movie ***'
GO
CREATE PROCEDURE [dbo].[sp_update_favorite_movie]
	(
		@UserId INT,
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
		UPDATE FavoriteMovies
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
			AND UserId = @UserId
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
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_select_all_favorite_movies ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_favorite_movies]
AS
	BEGIN
		SELECT	UserId, MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM		FavoriteMovies
		ORDER BY 	MovieId ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_retrieve_favorite_movie_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_favorite_movie_by_id]
	(
		@UserId INT
	)
AS
	BEGIN
		SELECT UserId, MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM FavoriteMovies
		WHERE UserId = @UserId
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/08
***************************************/
print '' print '*** creating sp_retrieve_favorite_movie_by_id_and_user_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_favorite_movie_by_id_and_user_id]
	(
		@UserId INT,
		@MovieId INT
	)
AS
	BEGIN
		SELECT UserId, MovieId, AccountStates, Adult, AlternativeTitles, BackdropPath, BelongsToCollection, Budget, Changes, Credits, Genres, Homepage, Images, ImdbId,
				Keywords, Lists, OriginalLanguage, OriginalTitle, Overview, Popularity, PosterPath, ProductionCompanies, ProductionCountries, ReleaseDate,
				ReleaseDates, ExternalIds, Releases, Revenue, Reviews, Runtime, Similar, Recommendations, SpokenLanguages, Status, Tagline, Title, Translations,
				Video, Videos, VoteAverage, VoteCount
		FROM FavoriteMovies
		WHERE UserId = @UserId
		AND MovieId = @MovieId
	END
GO

PRINT '' PRINT '*** creating Favorite TV Table ***'
GO
CREATE TABLE [dbo].[FavoriteTV]
(
	[FavoriteTvId]	[int] IDENTITY(1000000,1) NOT NULL,
	[UserId] INT NOT NULL,
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
	CONSTRAINT [PK_FavoriteTV] PRIMARY KEY ([FavoriteTvId]),
    CONSTRAINT [FK_FavoriteTV_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
)
GO

print '' print '*** inserting favorite tv shows test data ***'
GO
SET IDENTITY_INSERT [dbo].[FavoriteTV] ON
INSERT INTO [dbo].[FavoriteTV] ([FavoriteTvId], [UserId], [TvId], [AccountStates], [AlternativeTitles], [BackdropPath], [Changes], [ContentRatings], [CreatedBy], [Credits], [EpisodeRunTime], [ExternalIds], [FirstAirDate], [GenreIds], [Genres], [Homepage], [Images], [InProduction], [Keywords], [Languages], [LastAirDate], [LastEpisodeToAir], [Name], [NextEpisodeToAir], [Networks], [NumberOfEpisodes], [NumberOfSeasons], [OriginalLanguage], [OriginalName], [OriginCountry], [Overview], [Popularity], [PosterPath], [ProductionCompanies], [Recommendations], [Reviews], [Seasons], [Similar], [Status], [Translations], [Type], [Videos], [VoteAverage], [VoteCount]) VALUES (1000000, 1000001, 1399, N'None', N'None', N'http://image.tmdb.org/t/p/w1280/suopoADq0k8YZr4dQXcU6pToj6s.jpg', N'None', N'None', N'David Benioff', N'None', 0, N'None', N'2011-04-17 00:00:00', 0, N'Sci-Fi & Fantasy, Drama, Action & Adventure', N'http://www.hbo.com/game-of-thrones', N'None', 0, N'Based On Novel Or Book, Kingdom, Dragon, King, Intrigue, Fantasy World', N'None', N'2019-05-19 00:00:00', N'None', N'Game of Thrones', N'None', N'HBO
', 73, 8, N'8', N'Game of Thrones', N'System.Collections.Generic.List`1[System.String]', N'Seven noble families fight for control of the mythical land of Westeros. Friction between the houses leads to full-scale war. All while a very ancient evil awakens in the farthest north. Amidst the war, a neglected military order of misfits, the Night''s Watch, is all that stands between the realms of men and icy horrors beyond.', 571.087, N'http://image.tmdb.org/t/p/w500/u3bZgnGQ9T01sWNhyveQz0wH0Hl.jpg', N'None', N'None', N'None', N'System.Collections.Generic.List`1[TMDbLib.Objects.Search.SearchTvSeason]', N'None', N'Ended', N'None', N'Scripted', N'https://www.youtube.com/watch?v=bjqEWgDVPe0', 8.4, 14273)
INSERT INTO [dbo].[FavoriteTV] ([FavoriteTvId], [UserId], [TvId], [AccountStates], [AlternativeTitles], [BackdropPath], [Changes], [ContentRatings], [CreatedBy], [Credits], [EpisodeRunTime], [ExternalIds], [FirstAirDate], [GenreIds], [Genres], [Homepage], [Images], [InProduction], [Keywords], [Languages], [LastAirDate], [LastEpisodeToAir], [Name], [NextEpisodeToAir], [Networks], [NumberOfEpisodes], [NumberOfSeasons], [OriginalLanguage], [OriginalName], [OriginCountry], [Overview], [Popularity], [PosterPath], [ProductionCompanies], [Recommendations], [Reviews], [Seasons], [Similar], [Status], [Translations], [Type], [Videos], [VoteAverage], [VoteCount]) VALUES (1000001, 1000006, 1399, N'None', N'None', N'http://image.tmdb.org/t/p/w1280/suopoADq0k8YZr4dQXcU6pToj6s.jpg', N'None', N'None', N'David Benioff', N'None', 0, N'None', N'2011-04-17 00:00:00', 0, N'Sci-Fi & Fantasy, Drama, Action & Adventure', N'http://www.hbo.com/game-of-thrones', N'None', 0, N'Based On Novel Or Book, Kingdom, Dragon, King, Intrigue, Fantasy World', N'None', N'2019-05-19 00:00:00', N'None', N'Game of Thrones', N'None', N'HBO
', 73, 8, N'8', N'Game of Thrones', N'System.Collections.Generic.List`1[System.String]', N'Seven noble families fight for control of the mythical land of Westeros. Friction between the houses leads to full-scale war. All while a very ancient evil awakens in the farthest north. Amidst the war, a neglected military order of misfits, the Night''s Watch, is all that stands between the realms of men and icy horrors beyond.', 571.087, N'http://image.tmdb.org/t/p/w500/u3bZgnGQ9T01sWNhyveQz0wH0Hl.jpg', N'None', N'None', N'None', N'System.Collections.Generic.List`1[TMDbLib.Objects.Search.SearchTvSeason]', N'None', N'Ended', N'None', N'Scripted', N'https://www.youtube.com/watch?v=bjqEWgDVPe0', 8.4, 14273)
SET IDENTITY_INSERT [dbo].[FavoriteTV] OFF


/**************************************
Zach Stultz
Created: 2021/05/09
Description: Favorite TV-SHOW Stored Procedures
***************************************/

-- Stored Procedures

print '' print '*** FAVORITE TV-SHOW STORED PROCEDURES ***'
GO

/**************************************
Zach Stultz
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_insert_favorite_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_favorite_tv_show]
	(
		@UserId INT,
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
		INSERT INTO FavoriteTV
			(UserId, TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
			CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
			Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
			LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
			NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
			Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
			Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
			VoteCount)
		VALUES
			(@UserId, @TvId, @AccountStates, @AlternativeTitles, @BackdropPath, @Changes, @ContentRatings,
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
print '' print '*** creating sp_delete_favorite_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_favorite_tv_show]
	(
		@TvId INT,
		@UserId INT
	)
AS
	BEGIN
		DELETE FROM [FavoriteTV]
		WHERE TvId=@TvId
		AND UserId=@UserId
		END
GO

/**************************************
Zach Stultz
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_update_favorite_tv_show ***'
GO
CREATE PROCEDURE [dbo].[sp_update_favorite_tv_show]
	(
		@FavoriteTvId INT,
		@UserId INT,
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
		UPDATE FavoriteTV
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
			WHERE FavoriteTvId = @FavoriteTvId
			AND UserId = @UserId
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
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_select_all_favorite_tv_shows ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_favorite_tv_shows]
AS
	BEGIN
		SELECT		UserId, TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM		FavoriteTV
		ORDER BY 	TvId ASC
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_retrieve_favorite_tv_show_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_favorite_tv_show_by_id]
	(
		@UserId INT
	)
AS
	BEGIN
		SELECT 		UserId, TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM FavoriteTV
		WHERE UserId = @UserId
	END
GO

/**************************************
Zach Stultz
Created: 2021/05/09
***************************************/
print '' print '*** creating sp_retrieve_favorite_tv_show_by_id_and_user_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_favorite_tv_show_by_id_and_user_id]
	(
		@UserId INT,
		@TvId INT
	)
AS
	BEGIN
		SELECT 		UserId, TvId, AccountStates, AlternativeTitles, BackdropPath, Changes, ContentRatings,
					CreatedBy, Credits, EpisodeRunTime, ExternalIds, FirstAirDate, GenreIds,
					Genres, Homepage, Images, InProduction, Keywords, Languages, LastAirDate,
					LastEpisodeToAir, Name, NextEpisodeToAir, Networks, NumberOfEpisodes,
					NumberOfSeasons, OriginalLanguage, OriginalName, OriginCountry, Overview,
					Popularity, PosterPath, ProductionCompanies, Recommendations, Reviews,
					Seasons, Similar, Status, Translations, Type, Videos, VoteAverage,
					VoteCount
		FROM FavoriteTV
		WHERE UserId = @UserId
		AND TvId = @TvId
	END
GO