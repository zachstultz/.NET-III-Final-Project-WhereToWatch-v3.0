using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using DomainModels;
using Movie = DomainModels.Movie;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/05/06
    ///     The movie accessor.
    /// </summary>
    public class TrendingMovieAccessor : ITrendingMovieAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Inserts the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>A bool.</returns>
        public bool InsertMovie(Movie movie)
        {
            bool result = false;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_trending_movie", conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.Add("@MovieId", SqlDbType.Int);
            cmd.Parameters.Add("@AccountStates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Adult", SqlDbType.Bit);
            cmd.Parameters.Add("@AlternativeTitles", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@BackdropPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@BelongsToCollection", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Budget", SqlDbType.BigInt);
            cmd.Parameters.Add("@Changes", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Credits", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Genres", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Homepage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Images", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ImdbId", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Keywords", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Lists", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OriginalLanguage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OriginalTitle", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Overview", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Popularity", SqlDbType.Float);
            cmd.Parameters.Add("@PosterPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ProductionCompanies", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ProductionCountries", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ReleaseDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@ReleaseDates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ExternalIds", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Releases", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Revenue", SqlDbType.BigInt);
            cmd.Parameters.Add("@Reviews", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Runtime", SqlDbType.Int);
            cmd.Parameters.Add("@Similar", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Recommendations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@SpokenLanguages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Tagline", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Translations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Video", SqlDbType.Bit);
            cmd.Parameters.Add("@Videos", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@VoteAverage", SqlDbType.Float);
            cmd.Parameters.Add("@VoteCount", SqlDbType.Int);

            cmd.Parameters["@MovieId"].Value = movie.Id;
            cmd.Parameters["@AccountStates"].Value = movie.AccountStates;
            cmd.Parameters["@Adult"].Value = movie.Adult;
            cmd.Parameters["@AlternativeTitles"].Value = movie.AlternativeTitles;
            cmd.Parameters["@BackdropPath"].Value = movie.BackdropPath;
            cmd.Parameters["@BelongsToCollection"].Value = movie.BelongsToCollection;
            cmd.Parameters["@Budget"].Value = movie.Budget;
            cmd.Parameters["@Changes"].Value = movie.Changes;
            cmd.Parameters["@Credits"].Value = movie.Credits;
            cmd.Parameters["@Genres"].Value = movie.Genres;
            cmd.Parameters["@Homepage"].Value = movie.Homepage;
            cmd.Parameters["@Images"].Value = movie.Images;
            cmd.Parameters["@ImdbId"].Value = movie.ImdbId;
            cmd.Parameters["@Keywords"].Value = movie.Keywords;
            cmd.Parameters["@Lists"].Value = movie.Lists;
            cmd.Parameters["@OriginalLanguage"].Value = movie.OriginalLanguage;
            cmd.Parameters["@OriginalTitle"].Value = movie.OriginalTitle;
            cmd.Parameters["@Overview"].Value = movie.Overview;
            cmd.Parameters["@Popularity"].Value = movie.Popularity;
            cmd.Parameters["@PosterPath"].Value = movie.PosterPath;
            cmd.Parameters["@ProductionCompanies"].Value = movie.ProductionCompanies;
            cmd.Parameters["@ProductionCountries"].Value = movie.ProductionCountries;
            cmd.Parameters["@ReleaseDate"].Value = movie.ReleaseDate;
            cmd.Parameters["@ReleaseDates"].Value = movie.ReleaseDates;
            cmd.Parameters["@ExternalIds"].Value = movie.ExternalIds;
            cmd.Parameters["@Releases"].Value = movie.Releases;
            cmd.Parameters["@Revenue"].Value = movie.Revenue;
            cmd.Parameters["@Reviews"].Value = movie.Reviews;
            cmd.Parameters["@Runtime"].Value = movie.Runtime;
            cmd.Parameters["@Similar"].Value = movie.Similar;
            cmd.Parameters["@Recommendations"].Value = movie.Recommendations;
            cmd.Parameters["@SpokenLanguages"].Value = movie.SpokenLanguages;
            cmd.Parameters["@Status"].Value = movie.Status;
            cmd.Parameters["@Tagline"].Value = movie.Tagline;
            cmd.Parameters["@Title"].Value = movie.Title;
            cmd.Parameters["@Translations"].Value = movie.Translations;
            cmd.Parameters["@Video"].Value = movie.Video;
            cmd.Parameters["@Videos"].Value = movie.Videos;
            cmd.Parameters["@VoteAverage"].Value = movie.VoteAverage;
            cmd.Parameters["@VoteCount"].Value = movie.VoteCount;

            try
            {
                conn.Open();
                int currentResult = cmd.ExecuteNonQuery();
                if (currentResult == 1)
                {
                    result = true;
                }
                else if (currentResult == 0)
                {
                    throw new ApplicationException("The movie could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Updates the movie.
        /// </summary>
        /// <param name="oldMovie">The old movie.</param>
        /// <param name="newMovie">The new movie.</param>
        /// <returns>A bool.</returns>
        public int UpdateMovie(Movie oldMovie, Movie newMovie)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_trending_movie", conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.Add("@MovieId", SqlDbType.Int);

            cmd.Parameters.Add("@OldAccountStates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldAdult", SqlDbType.Bit);
            cmd.Parameters.Add("@OldAlternativeTitles", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldBackdropPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldBelongsToCollection", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldBudget", SqlDbType.BigInt);
            cmd.Parameters.Add("@OldChanges", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldCredits", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldGenres", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldHomepage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldImages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldImdbId", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldKeywords", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldLists", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldOriginalLanguage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldOriginalTitle", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldOverview", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldPopularity", SqlDbType.Float);
            cmd.Parameters.Add("@OldPosterPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldProductionCompanies", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldProductionCountries", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldReleaseDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldReleaseDates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldExternalIds", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldReleases", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldRevenue", SqlDbType.BigInt);
            cmd.Parameters.Add("@OldReviews", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldRuntime", SqlDbType.Int);
            cmd.Parameters.Add("@OldSimilar", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldRecommendations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldSpokenLanguages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldStatus", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldTagline", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldTitle", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldTranslations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldVideo", SqlDbType.Bit);
            cmd.Parameters.Add("@OldVideos", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldVoteAverage", SqlDbType.Float);
            cmd.Parameters.Add("@OldVoteCount", SqlDbType.Int);

            cmd.Parameters.Add("@NewAccountStates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewAdult", SqlDbType.Bit);
            cmd.Parameters.Add("@NewAlternativeTitles", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewBackdropPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewBelongsToCollection", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewBudget", SqlDbType.BigInt);
            cmd.Parameters.Add("@NewChanges", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewCredits", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewGenres", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewHomepage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewImages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewImdbId", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewKeywords", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewLists", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewOriginalLanguage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewOriginalTitle", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewOverview", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewPopularity", SqlDbType.Float);
            cmd.Parameters.Add("@NewPosterPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewProductionCompanies", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewProductionCountries", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewReleaseDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewReleaseDates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewExternalIds", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewReleases", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewRevenue", SqlDbType.BigInt);
            cmd.Parameters.Add("@NewReviews", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewRuntime", SqlDbType.Int);
            cmd.Parameters.Add("@NewSimilar", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewRecommendations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewSpokenLanguages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewTagline", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewTitle", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewTranslations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewVideo", SqlDbType.Bit);
            cmd.Parameters.Add("@NewVideos", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewVoteAverage", SqlDbType.Float);
            cmd.Parameters.Add("@NewVoteCount", SqlDbType.Int);

            cmd.Parameters["@MovieId"].Value = oldMovie.Id;

            cmd.Parameters["@OldAccountStates"].Value = oldMovie.AccountStates;
            cmd.Parameters["@OldAdult"].Value = oldMovie.Adult;
            cmd.Parameters["@OldAlternativeTitles"].Value = oldMovie.AlternativeTitles;
            cmd.Parameters["@OldBackdropPath"].Value = oldMovie.BackdropPath;
            cmd.Parameters["@OldBelongsToCollection"].Value = oldMovie.BelongsToCollection;
            cmd.Parameters["@OldBudget"].Value = oldMovie.Budget;
            cmd.Parameters["@OldChanges"].Value = oldMovie.Changes;
            cmd.Parameters["@OldCredits"].Value = oldMovie.Credits;
            cmd.Parameters["@OldGenres"].Value = oldMovie.Genres;
            cmd.Parameters["@OldHomepage"].Value = oldMovie.Homepage;
            cmd.Parameters["@OldImages"].Value = oldMovie.Images;
            cmd.Parameters["@OldImdbId"].Value = oldMovie.ImdbId;
            cmd.Parameters["@OldKeywords"].Value = oldMovie.Keywords;
            cmd.Parameters["@OldLists"].Value = oldMovie.Lists;
            cmd.Parameters["@OldOriginalLanguage"].Value = oldMovie.OriginalLanguage;
            cmd.Parameters["@OldOriginalTitle"].Value = oldMovie.OriginalTitle;
            cmd.Parameters["@OldOverview"].Value = oldMovie.Overview;
            cmd.Parameters["@OldPopularity"].Value = oldMovie.Popularity;
            cmd.Parameters["@OldPosterPath"].Value = oldMovie.PosterPath;
            cmd.Parameters["@OldProductionCompanies"].Value = oldMovie.ProductionCompanies;
            cmd.Parameters["@OldProductionCountries"].Value = oldMovie.ProductionCountries;
            cmd.Parameters["@OldReleaseDate"].Value = oldMovie.ReleaseDate;
            cmd.Parameters["@OldReleaseDates"].Value = oldMovie.ReleaseDates;
            cmd.Parameters["@OldExternalIds"].Value = oldMovie.ExternalIds;
            cmd.Parameters["@OldReleases"].Value = oldMovie.Releases;
            cmd.Parameters["@OldRevenue"].Value = oldMovie.Revenue;
            cmd.Parameters["@OldReviews"].Value = oldMovie.Reviews;
            cmd.Parameters["@OldRuntime"].Value = oldMovie.Runtime;
            cmd.Parameters["@OldSimilar"].Value = oldMovie.Similar;
            cmd.Parameters["@OldRecommendations"].Value = oldMovie.Recommendations;
            cmd.Parameters["@OldSpokenLanguages"].Value = oldMovie.SpokenLanguages;
            cmd.Parameters["@OldStatus"].Value = oldMovie.Status;
            cmd.Parameters["@OldTagline"].Value = oldMovie.Tagline;
            cmd.Parameters["@OldTitle"].Value = oldMovie.Title;
            cmd.Parameters["@OldTranslations"].Value = oldMovie.Translations;
            cmd.Parameters["@OldVideo"].Value = oldMovie.Video;
            cmd.Parameters["@OldVideos"].Value = oldMovie.Videos;
            cmd.Parameters["@OldVoteAverage"].Value = oldMovie.VoteAverage;
            cmd.Parameters["@OldVoteCount"].Value = oldMovie.VoteCount;

            cmd.Parameters["@NewAccountStates"].Value = newMovie.AccountStates;
            cmd.Parameters["@NewAdult"].Value = newMovie.Adult;
            cmd.Parameters["@NewAlternativeTitles"].Value = newMovie.AlternativeTitles;
            cmd.Parameters["@NewBackdropPath"].Value = newMovie.BackdropPath;
            cmd.Parameters["@NewBelongsToCollection"].Value = newMovie.BelongsToCollection;
            cmd.Parameters["@NewBudget"].Value = newMovie.Budget;
            cmd.Parameters["@NewChanges"].Value = newMovie.Changes;
            cmd.Parameters["@NewCredits"].Value = newMovie.Credits;
            cmd.Parameters["@NewGenres"].Value = newMovie.Genres;
            cmd.Parameters["@NewHomepage"].Value = newMovie.Homepage;
            cmd.Parameters["@NewImages"].Value = newMovie.Images;
            cmd.Parameters["@NewImdbId"].Value = newMovie.ImdbId;
            cmd.Parameters["@NewKeywords"].Value = newMovie.Keywords;
            cmd.Parameters["@NewLists"].Value = newMovie.Lists;
            cmd.Parameters["@NewOriginalLanguage"].Value = newMovie.OriginalLanguage;
            cmd.Parameters["@NewOriginalTitle"].Value = newMovie.OriginalTitle;
            cmd.Parameters["@NewOverview"].Value = newMovie.Overview;
            cmd.Parameters["@NewPopularity"].Value = newMovie.Popularity;
            cmd.Parameters["@NewPosterPath"].Value = newMovie.PosterPath;
            cmd.Parameters["@NewProductionCompanies"].Value = newMovie.ProductionCompanies;
            cmd.Parameters["@NewProductionCountries"].Value = newMovie.ProductionCountries;
            cmd.Parameters["@NewReleaseDate"].Value = newMovie.ReleaseDate;
            cmd.Parameters["@NewReleaseDates"].Value = newMovie.ReleaseDates;
            cmd.Parameters["@NewExternalIds"].Value = newMovie.ExternalIds;
            cmd.Parameters["@NewReleases"].Value = newMovie.Releases;
            cmd.Parameters["@NewRevenue"].Value = newMovie.Revenue;
            cmd.Parameters["@NewReviews"].Value = newMovie.Reviews;
            cmd.Parameters["@NewRuntime"].Value = newMovie.Runtime;
            cmd.Parameters["@NewSimilar"].Value = newMovie.Similar;
            cmd.Parameters["@NewRecommendations"].Value = newMovie.Recommendations;
            cmd.Parameters["@NewSpokenLanguages"].Value = newMovie.SpokenLanguages;
            cmd.Parameters["@NewStatus"].Value = newMovie.Status;
            cmd.Parameters["@NewTagline"].Value = newMovie.Tagline;
            cmd.Parameters["@NewTitle"].Value = newMovie.Title;
            cmd.Parameters["@NewTranslations"].Value = newMovie.Translations;
            cmd.Parameters["@NewVideo"].Value = newMovie.Video;
            cmd.Parameters["@NewVideos"].Value = newMovie.Videos;
            cmd.Parameters["@NewVoteAverage"].Value = newMovie.VoteAverage;
            cmd.Parameters["@NewVoteCount"].Value = newMovie.VoteCount;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Selects the all movies.
        /// </summary>
        /// <returns>A list of Movies.</returns>
        public List<Movie> SelectAllMovies()
        {
            List<Movie> data = new List<Movie>();

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_select_all_trending_movies", conn) {CommandType = CommandType.StoredProcedure};

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new Movie
                        {
                            Id = reader.GetInt32(0),
                            AccountStates = reader.GetString(1),
                            Adult = reader.GetBoolean(2),
                            AlternativeTitles = reader.GetString(3),
                            BackdropPath = reader.GetString(4),
                            BelongsToCollection = reader.GetString(5),
                            Budget = reader.GetInt64(6),
                            Changes = reader.GetString(7),
                            Credits = reader.GetString(8),
                            Genres = reader.GetString(9),
                            Homepage = reader.GetString(10),
                            Images = reader.GetString(11),
                            ImdbId = reader.GetString(12),
                            Keywords = reader.GetString(13),
                            Lists = reader.GetString(14),
                            OriginalLanguage = reader.GetString(15),
                            OriginalTitle = reader.GetString(16),
                            Overview = reader.GetString(17),
                            Popularity = reader.GetDouble(18),
                            PosterPath = reader.GetString(19),
                            ProductionCompanies = reader.GetString(20),
                            ProductionCountries = reader.GetString(21),
                            ReleaseDate = reader.GetDateTime(22),
                            ReleaseDates = reader.GetString(23),
                            ExternalIds = reader.GetString(24),
                            Releases = reader.GetString(25),
                            Revenue = reader.GetInt64(26),
                            Reviews = reader.GetString(27),
                            Runtime = reader.GetInt32(28),
                            Similar = reader.GetString(29),
                            Recommendations = reader.GetString(30),
                            SpokenLanguages = reader.GetString(31),
                            Status = reader.GetString(32),
                            Tagline = reader.GetString(33),
                            Title = reader.GetString(34),
                            Translations = reader.GetString(35),
                            Video = reader.GetBoolean(36),
                            Videos = reader.GetString(37),
                            VoteAverage = reader.GetDouble(38),
                            VoteCount = reader.GetInt32(39)
                        });
                    }
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return data;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Selects the movie by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Movie.</returns>
        public Movie SelectMovieById(int id)
        {
            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_trending_movie_by_id", conn) {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.Add("@MovieId", SqlDbType.Int);
            cmd.Parameters["@MovieId"].Value = id;
            Movie movie = new Movie();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Movie foundMovie = new Movie()
                        {
                            Id = reader.GetInt32(0),
                            AccountStates = reader.GetString(1),
                            Adult = reader.GetBoolean(2),
                            AlternativeTitles = reader.GetString(3),
                            BackdropPath = reader.GetString(4),
                            BelongsToCollection = reader.GetString(5),
                            Budget = reader.GetInt64(6),
                            Changes = reader.GetString(7),
                            Credits = reader.GetString(8),
                            Genres = reader.GetString(9),
                            Homepage = reader.GetString(10),
                            Images = reader.GetString(11),
                            ImdbId = reader.GetString(12),
                            Keywords = reader.GetString(13),
                            Lists = reader.GetString(14),
                            OriginalLanguage = reader.GetString(15),
                            OriginalTitle = reader.GetString(16),
                            Overview = reader.GetString(17),
                            Popularity = reader.GetDouble(18),
                            PosterPath = reader.GetString(19),
                            ProductionCompanies = reader.GetString(20),
                            ProductionCountries = reader.GetString(21),
                            ReleaseDate = reader.GetDateTime(22),
                            ReleaseDates = reader.GetString(23),
                            ExternalIds = reader.GetString(24),
                            Releases = reader.GetString(25),
                            Revenue = reader.GetInt64(26),
                            Reviews = reader.GetString(27),
                            Runtime = reader.GetInt32(28),
                            Similar = reader.GetString(29),
                            Recommendations = reader.GetString(30),
                            SpokenLanguages = reader.GetString(31),
                            Status = reader.GetString(32),
                            Tagline = reader.GetString(33),
                            Title = reader.GetString(34),
                            Translations = reader.GetString(35),
                            Video = reader.GetBoolean(36),
                            Videos = reader.GetString(37),
                            VoteAverage = reader.GetDouble(38),
                            VoteCount = reader.GetInt32(39)
                        };
                        movie = foundMovie;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return movie;
        }

        public bool CheckForMovieById(int id)
        {
            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_trending_movie_by_id", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@MovieId", SqlDbType.Int);
            cmd.Parameters["@MovieId"].Value = id;
            List<Movie> movie = new List<Movie>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Movie foundMovie = new Movie()
                        {
                            Id = reader.GetInt32(0),
                            AccountStates = reader.GetString(1),
                            Adult = reader.GetBoolean(2),
                            AlternativeTitles = reader.GetString(3),
                            BackdropPath = reader.GetString(4),
                            BelongsToCollection = reader.GetString(5),
                            Budget = reader.GetInt64(6),
                            Changes = reader.GetString(7),
                            Credits = reader.GetString(8),
                            Genres = reader.GetString(9),
                            Homepage = reader.GetString(10),
                            Images = reader.GetString(11),
                            ImdbId = reader.GetString(12),
                            Keywords = reader.GetString(13),
                            Lists = reader.GetString(14),
                            OriginalLanguage = reader.GetString(15),
                            OriginalTitle = reader.GetString(16),
                            Overview = reader.GetString(17),
                            Popularity = reader.GetDouble(18),
                            PosterPath = reader.GetString(19),
                            ProductionCompanies = reader.GetString(20),
                            ProductionCountries = reader.GetString(21),
                            ReleaseDate = reader.GetDateTime(22),
                            ReleaseDates = reader.GetString(23),
                            ExternalIds = reader.GetString(24),
                            Releases = reader.GetString(25),
                            Revenue = reader.GetInt64(26),
                            Reviews = reader.GetString(27),
                            Runtime = reader.GetInt32(28),
                            Similar = reader.GetString(29),
                            Recommendations = reader.GetString(30),
                            SpokenLanguages = reader.GetString(31),
                            Status = reader.GetString(32),
                            Tagline = reader.GetString(33),
                            Title = reader.GetString(34),
                            Translations = reader.GetString(35),
                            Video = reader.GetBoolean(36),
                            Videos = reader.GetString(37),
                            VoteAverage = reader.GetDouble(38),
                            VoteCount = reader.GetInt32(39)
                        };
                        movie.Add(foundMovie);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (movie.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}