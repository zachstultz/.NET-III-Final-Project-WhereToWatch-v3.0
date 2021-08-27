using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessInterfaces;
using DomainModels;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/05/06
    ///     The tv accessor.
    /// </summary>
    public class TvAccessor : ITvAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Inserts the tv show.
        /// </summary>
        /// <param name="tv">The tv.</param>
        /// <returns>A bool.</returns>
        public bool InsertTvShow(Tv tv)
        {
            bool result = false;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_tv_show", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@TvId", SqlDbType.Int);
            cmd.Parameters.Add("@AccountStates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@AlternativeTitles", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@BackdropPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Changes", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ContentRatings", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Credits", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@EpisodeRunTime", SqlDbType.Int);
            cmd.Parameters.Add("@ExternalIds", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@FirstAirDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@GenreIds", SqlDbType.Int);
            cmd.Parameters.Add("@Genres", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Homepage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Images", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@InProduction", SqlDbType.Bit);
            cmd.Parameters.Add("@Keywords", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Languages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@LastAirDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@LastEpisodeToAir", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NextEpisodeToAir", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Networks", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NumberOfEpisodes", SqlDbType.Int);
            cmd.Parameters.Add("@NumberOfSeasons", SqlDbType.Int);
            cmd.Parameters.Add("@OriginalLanguage", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OriginalName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OriginCountry", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Overview", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Popularity", SqlDbType.Float);
            cmd.Parameters.Add("@PosterPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@ProductionCompanies", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Recommendations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Reviews", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Seasons", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Similar", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Translations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@Videos", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@VoteAverage", SqlDbType.Float);
            cmd.Parameters.Add("@VoteCount", SqlDbType.Int);

            cmd.Parameters["@TvId"].Value = tv.Id;
            cmd.Parameters["@AccountStates"].Value = tv.AccountStates;
            cmd.Parameters["@AlternativeTitles"].Value = tv.AlternativeTitles;
            cmd.Parameters["@BackdropPath"].Value = tv.BackdropPath;
            cmd.Parameters["@Changes"].Value = tv.Changes;
            cmd.Parameters["@ContentRatings"].Value = tv.ContentRatings;
            cmd.Parameters["@CreatedBy"].Value = tv.CreatedBy;
            cmd.Parameters["@Credits"].Value = tv.Credits;
            cmd.Parameters["@EpisodeRunTime"].Value = tv.EpisodeRunTime;
            cmd.Parameters["@ExternalIds"].Value = tv.ExternalIds;
            cmd.Parameters["@FirstAirDate"].Value = tv.FirstAirDate;
            cmd.Parameters["@GenreIds"].Value = tv.GenreIds;
            cmd.Parameters["@Genres"].Value = tv.Genres;
            cmd.Parameters["@Homepage"].Value = tv.Homepage;
            cmd.Parameters["@Images"].Value = tv.Images;
            cmd.Parameters["@InProduction"].Value = tv.InProduction;
            cmd.Parameters["@Keywords"].Value = tv.Keywords;
            cmd.Parameters["@Languages"].Value = tv.Languages;
            cmd.Parameters["@LastAirDate"].Value = tv.LastAirDate;
            cmd.Parameters["@LastEpisodeToAir"].Value = tv.LastEpisodeToAir;
            cmd.Parameters["@Name"].Value = tv.Name;
            cmd.Parameters["@NextEpisodeToAir"].Value = tv.NextEpisodeToAir;
            cmd.Parameters["@Networks"].Value = tv.Networks;
            cmd.Parameters["@NumberOfEpisodes"].Value = tv.NumberOfEpisodes;
            cmd.Parameters["@NumberOfSeasons"].Value = tv.NumberOfSeasons;
            cmd.Parameters["@OriginalLanguage"].Value = tv.NumberOfSeasons;
            cmd.Parameters["@OriginalName"].Value = tv.OriginalName;
            cmd.Parameters["@OriginCountry"].Value = tv.OriginCountry;
            cmd.Parameters["@Overview"].Value = tv.Overview;
            cmd.Parameters["@Popularity"].Value = tv.Popularity;
            cmd.Parameters["@PosterPath"].Value = tv.PosterPath;
            cmd.Parameters["@ProductionCompanies"].Value = tv.ProductionCompanies;
            cmd.Parameters["@Recommendations"].Value = tv.Recommendations;
            cmd.Parameters["@Reviews"].Value = tv.Reviews;
            cmd.Parameters["@Seasons"].Value = tv.Seasons;
            cmd.Parameters["@Similar"].Value = tv.Similar;
            cmd.Parameters["@Status"].Value = tv.Status;
            cmd.Parameters["@Translations"].Value = tv.Translations;
            cmd.Parameters["@Type"].Value = tv.Type;
            cmd.Parameters["@Videos"].Value = tv.Videos;
            cmd.Parameters["@VoteAverage"].Value = tv.VoteAverage;
            cmd.Parameters["@VoteCount"].Value = tv.VoteCount;

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
                    throw new ApplicationException("The tv show could not be added.");
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
        /// Zach Stultz
        /// Created: 2021/03/26
        ///
        /// Deletes the movie.
        /// </summary>
        /// <returns>A bool.</returns>
        public int DeleteTvShow(int id)
        {
            int result;
            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_delete_tv_show", conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.Add("@TvId", SqlDbType.Int);
            cmd.Parameters["@TvId"].Value = id;

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
        ///     Updates the tv show.
        /// </summary>
        /// <param name="oldTv"></param>
        /// <param name="newTv"></param>
        /// <returns>A bool.</returns>
        public int UpdateTvShow(Tv oldTv, Tv newTv)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_tv_show", conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.Add("@TvId", SqlDbType.Int);

            cmd.Parameters.Add("@OldAccountStates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldAlternativeTitles", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldBackdropPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldChanges", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldContentRatings", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldCreatedBy", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldCredits", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldEpisodeRunTime", SqlDbType.Int);
            cmd.Parameters.Add("@OldExternalIds", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldFirstAirDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldGenreIds", SqlDbType.Int);
            cmd.Parameters.Add("@OldGenres", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldHomepage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldImages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldInProduction", SqlDbType.Bit);
            cmd.Parameters.Add("@OldKeywords", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldLanguages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldLastAirDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldLastEpisodeToAir", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OldNextEpisodeToAir", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldNetworks", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldNumberOfEpisodes", SqlDbType.Int);
            cmd.Parameters.Add("@OldNumberOfSeasons", SqlDbType.Int);
            cmd.Parameters.Add("@OldOriginalLanguage", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OldOriginalName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OldOriginCountry", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldOverview", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OldPopularity", SqlDbType.Float);
            cmd.Parameters.Add("@OldPosterPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldProductionCompanies", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldRecommendations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldReviews", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldSeasons", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldSimilar", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldStatus", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldTranslations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldType", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldVideos", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@OldVoteAverage", SqlDbType.Float);
            cmd.Parameters.Add("@OldVoteCount", SqlDbType.Int);

            cmd.Parameters.Add("@NewAccountStates", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewAlternativeTitles", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewBackdropPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewChanges", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewContentRatings", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewCreatedBy", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewCredits", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewEpisodeRunTime", SqlDbType.Int);
            cmd.Parameters.Add("@NewExternalIds", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewFirstAirDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewGenreIds", SqlDbType.Int);
            cmd.Parameters.Add("@NewGenres", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewHomepage", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewImages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewInProduction", SqlDbType.Bit);
            cmd.Parameters.Add("@NewKeywords", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewLanguages", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewLastAirDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewLastEpisodeToAir", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NewNextEpisodeToAir", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewNetworks", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewNumberOfEpisodes", SqlDbType.Int);
            cmd.Parameters.Add("@NewNumberOfSeasons", SqlDbType.Int);
            cmd.Parameters.Add("@NewOriginalLanguage", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NewOriginalName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NewOriginCountry", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewOverview", SqlDbType.NVarChar);
            cmd.Parameters.Add("@NewPopularity", SqlDbType.Float);
            cmd.Parameters.Add("@NewPosterPath", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewProductionCompanies", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewRecommendations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewReviews", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewSeasons", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewSimilar", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewTranslations", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewType", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewVideos", SqlDbType.NVarChar, -1);
            cmd.Parameters.Add("@NewVoteAverage", SqlDbType.Float);
            cmd.Parameters.Add("@NewVoteCount", SqlDbType.Int);

            cmd.Parameters["@TvId"].Value = oldTv.Id;

            cmd.Parameters["@OldAccountStates"].Value = oldTv.AccountStates;
            cmd.Parameters["@OldAlternativeTitles"].Value = oldTv.AlternativeTitles;
            cmd.Parameters["@OldBackdropPath"].Value = oldTv.BackdropPath;
            cmd.Parameters["@OldChanges"].Value = oldTv.Changes;
            cmd.Parameters["@OldContentRatings"].Value = oldTv.ContentRatings;
            cmd.Parameters["@OldCreatedBy"].Value = oldTv.CreatedBy;
            cmd.Parameters["@OldCredits"].Value = oldTv.Credits;
            cmd.Parameters["@OldEpisodeRunTime"].Value = oldTv.EpisodeRunTime;
            cmd.Parameters["@OldExternalIds"].Value = oldTv.ExternalIds;
            cmd.Parameters["@OldFirstAirDate"].Value = oldTv.FirstAirDate;
            cmd.Parameters["@OldGenreIds"].Value = oldTv.GenreIds;
            cmd.Parameters["@OldGenres"].Value = oldTv.Genres;
            cmd.Parameters["@OldHomepage"].Value = oldTv.Homepage;
            cmd.Parameters["@OldImages"].Value = oldTv.Images;
            cmd.Parameters["@OldInProduction"].Value = oldTv.InProduction;
            cmd.Parameters["@OldKeywords"].Value = oldTv.Keywords;
            cmd.Parameters["@OldLanguages"].Value = oldTv.Languages;
            cmd.Parameters["@OldLastAirDate"].Value = oldTv.LastAirDate;
            cmd.Parameters["@OldLastEpisodeToAir"].Value = oldTv.LastEpisodeToAir;
            cmd.Parameters["@OldName"].Value = oldTv.Name;
            cmd.Parameters["@OldNextEpisodeToAir"].Value = oldTv.NextEpisodeToAir;
            cmd.Parameters["@OldNetworks"].Value = oldTv.Networks;
            cmd.Parameters["@OldNumberOfEpisodes"].Value = oldTv.NumberOfEpisodes;
            cmd.Parameters["@OldNumberOfSeasons"].Value = oldTv.NumberOfSeasons;
            cmd.Parameters["@OldOriginalLanguage"].Value = oldTv.NumberOfSeasons;
            cmd.Parameters["@OldOriginalName"].Value = oldTv.OriginalName;
            cmd.Parameters["@OldOriginCountry"].Value = oldTv.OriginCountry;
            cmd.Parameters["@OldOverview"].Value = oldTv.Overview;
            cmd.Parameters["@OldPopularity"].Value = oldTv.Popularity;
            cmd.Parameters["@OldPosterPath"].Value = oldTv.PosterPath;
            cmd.Parameters["@OldProductionCompanies"].Value = oldTv.ProductionCompanies;
            cmd.Parameters["@OldRecommendations"].Value = oldTv.Recommendations;
            cmd.Parameters["@OldReviews"].Value = oldTv.Reviews;
            cmd.Parameters["@OldSeasons"].Value = oldTv.Seasons;
            cmd.Parameters["@OldSimilar"].Value = oldTv.Similar;
            cmd.Parameters["@OldStatus"].Value = oldTv.Status;
            cmd.Parameters["@OldTranslations"].Value = oldTv.Translations;
            cmd.Parameters["@OldType"].Value = oldTv.Type;
            cmd.Parameters["@OldVideos"].Value = oldTv.Videos;
            cmd.Parameters["@OldVoteAverage"].Value = oldTv.VoteAverage;
            cmd.Parameters["@OldVoteCount"].Value = oldTv.VoteCount;

            cmd.Parameters["@NewAccountStates"].Value = newTv.AccountStates;
            cmd.Parameters["@NewAlternativeTitles"].Value = newTv.AlternativeTitles;
            cmd.Parameters["@NewBackdropPath"].Value = newTv.BackdropPath;
            cmd.Parameters["@NewChanges"].Value = newTv.Changes;
            cmd.Parameters["@NewContentRatings"].Value = newTv.ContentRatings;
            cmd.Parameters["@NewCreatedBy"].Value = newTv.CreatedBy;
            cmd.Parameters["@NewCredits"].Value = newTv.Credits;
            cmd.Parameters["@NewEpisodeRunTime"].Value = newTv.EpisodeRunTime;
            cmd.Parameters["@NewExternalIds"].Value = newTv.ExternalIds;
            cmd.Parameters["@NewFirstAirDate"].Value = newTv.FirstAirDate;
            cmd.Parameters["@NewGenreIds"].Value = newTv.GenreIds;
            cmd.Parameters["@NewGenres"].Value = newTv.Genres;
            cmd.Parameters["@NewHomepage"].Value = newTv.Homepage;
            cmd.Parameters["@NewImages"].Value = newTv.Images;
            cmd.Parameters["@NewInProduction"].Value = newTv.InProduction;
            cmd.Parameters["@NewKeywords"].Value = newTv.Keywords;
            cmd.Parameters["@NewLanguages"].Value = newTv.Languages;
            cmd.Parameters["@NewLastAirDate"].Value = newTv.LastAirDate;
            cmd.Parameters["@NewLastEpisodeToAir"].Value = newTv.LastEpisodeToAir;
            cmd.Parameters["@NewName"].Value = newTv.Name;
            cmd.Parameters["@NewNextEpisodeToAir"].Value = newTv.NextEpisodeToAir;
            cmd.Parameters["@NewNetworks"].Value = newTv.Networks;
            cmd.Parameters["@NewNumberOfEpisodes"].Value = newTv.NumberOfEpisodes;
            cmd.Parameters["@NewNumberOfSeasons"].Value = newTv.NumberOfSeasons;
            cmd.Parameters["@NewOriginalLanguage"].Value = newTv.NumberOfSeasons;
            cmd.Parameters["@NewOriginalName"].Value = newTv.OriginalName;
            cmd.Parameters["@NewOriginCountry"].Value = newTv.OriginCountry;
            cmd.Parameters["@NewOverview"].Value = newTv.Overview;
            cmd.Parameters["@NewPopularity"].Value = newTv.Popularity;
            cmd.Parameters["@NewPosterPath"].Value = newTv.PosterPath;
            cmd.Parameters["@NewProductionCompanies"].Value = newTv.ProductionCompanies;
            cmd.Parameters["@NewRecommendations"].Value = newTv.Recommendations;
            cmd.Parameters["@NewReviews"].Value = newTv.Reviews;
            cmd.Parameters["@NewSeasons"].Value = newTv.Seasons;
            cmd.Parameters["@NewSimilar"].Value = newTv.Similar;
            cmd.Parameters["@NewStatus"].Value = newTv.Status;
            cmd.Parameters["@NewTranslations"].Value = newTv.Translations;
            cmd.Parameters["@NewType"].Value = newTv.Type;
            cmd.Parameters["@NewVideos"].Value = newTv.Videos;
            cmd.Parameters["@NewVoteAverage"].Value = newTv.VoteAverage;
            cmd.Parameters["@NewVoteCount"].Value = newTv.VoteCount;

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
        ///     Selects the all tv shows.
        /// </summary>
        /// <returns>A list of TVS.</returns>
        public List<Tv> SelectAllTvShows()
        {
            List<Tv> data = new List<Tv>();

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_select_all_tv_shows", conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new Tv
                        {
                            Id = reader.GetInt32(0),
                            AccountStates = reader.GetString(1),
                            AlternativeTitles = reader.GetString(2),
                            BackdropPath = reader.GetString(3),
                            Changes = reader.GetString(4),
                            ContentRatings = reader.GetString(5),
                            CreatedBy = reader.GetString(6),
                            Credits = reader.GetString(7),
                            EpisodeRunTime = reader.GetInt32(8),
                            ExternalIds = reader.GetString(9),
                            FirstAirDate = reader.GetDateTime(10),
                            GenreIds = reader.GetInt32(11),
                            Genres = reader.GetString(12),
                            Homepage = reader.GetString(13),
                            Images = reader.GetString(14),
                            InProduction = reader.GetBoolean(15),
                            Keywords = reader.GetString(16),
                            Languages = reader.GetString(17),
                            LastAirDate = reader.GetDateTime(18),
                            LastEpisodeToAir = reader.GetString(19),
                            Name = reader.GetString(20),
                            NextEpisodeToAir = reader.GetString(21),
                            Networks = reader.GetString(22),
                            NumberOfEpisodes = reader.GetInt32(23),
                            NumberOfSeasons = reader.GetInt32(24),
                            OriginalLanguage = reader.GetString(25),
                            OriginalName = reader.GetString(26),
                            OriginCountry = reader.GetString(27),
                            Overview = reader.GetString(28),
                            Popularity = reader.GetDouble(29),
                            PosterPath = reader.GetString(30),
                            ProductionCompanies = reader.GetString(31),
                            Recommendations = reader.GetString(32),
                            Reviews = reader.GetString(33),
                            Seasons = reader.GetString(34),
                            Similar = reader.GetString(35),
                            Status = reader.GetString(36),
                            Translations = reader.GetString(37),
                            Type = reader.GetString(38),
                            Videos = reader.GetString(39),
                            VoteAverage = reader.GetDouble(40),
                            VoteCount = reader.GetInt32(41),
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
        ///     Selects the tv show by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A TV.</returns>
        public Tv SelectTvShowById(int id)
        {
            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_tv_show_by_id", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@TvId", SqlDbType.Int);
            cmd.Parameters["@TvId"].Value = id;
            Tv tv = new Tv();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tv foundTvShow = new Tv()
                        {
                            Id = reader.GetInt32(0),
                            AccountStates = reader.GetString(1),
                            AlternativeTitles = reader.GetString(2),
                            BackdropPath = reader.GetString(3),
                            Changes = reader.GetString(4),
                            ContentRatings = reader.GetString(5),
                            CreatedBy = reader.GetString(6),
                            Credits = reader.GetString(7),
                            EpisodeRunTime = reader.GetInt32(8),
                            ExternalIds = reader.GetString(9),
                            FirstAirDate = reader.GetDateTime(10),
                            GenreIds = reader.GetInt32(11),
                            Genres = reader.GetString(12),
                            Homepage = reader.GetString(13),
                            Images = reader.GetString(14),
                            InProduction = reader.GetBoolean(15),
                            Keywords = reader.GetString(16),
                            Languages = reader.GetString(17),
                            LastAirDate = reader.GetDateTime(18),
                            LastEpisodeToAir = reader.GetString(19),
                            Name = reader.GetString(20),
                            NextEpisodeToAir = reader.GetString(21),
                            Networks = reader.GetString(22),
                            NumberOfEpisodes = reader.GetInt32(23),
                            NumberOfSeasons = reader.GetInt32(24),
                            OriginalLanguage = reader.GetString(25),
                            OriginalName = reader.GetString(26),
                            OriginCountry = reader.GetString(27),
                            Overview = reader.GetString(28),
                            Popularity = reader.GetDouble(29),
                            PosterPath = reader.GetString(30),
                            ProductionCompanies = reader.GetString(31),
                            Recommendations = reader.GetString(32),
                            Reviews = reader.GetString(33),
                            Seasons = reader.GetString(34),
                            Similar = reader.GetString(35),
                            Status = reader.GetString(36),
                            Translations = reader.GetString(37),
                            Type = reader.GetString(38),
                            Videos = reader.GetString(39),
                            VoteAverage = reader.GetDouble(40),
                            VoteCount = reader.GetInt32(41),
                        };
                        tv = foundTvShow;
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

            return tv;
        }

        public bool CheckForTvShowById(int id)
        {
            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_tv_show_by_id", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@TvId", SqlDbType.Int);
            cmd.Parameters["@TvId"].Value = id;
            List<Tv> tv = new List<Tv>();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tv foundTvShow = new Tv()
                        {
                            Id = reader.GetInt32(0),
                            AccountStates = reader.GetString(1),
                            AlternativeTitles = reader.GetString(2),
                            BackdropPath = reader.GetString(3),
                            Changes = reader.GetString(4),
                            ContentRatings = reader.GetString(5),
                            CreatedBy = reader.GetString(6),
                            Credits = reader.GetString(7),
                            EpisodeRunTime = reader.GetInt32(8),
                            ExternalIds = reader.GetString(9),
                            FirstAirDate = reader.GetDateTime(10),
                            GenreIds = reader.GetInt32(11),
                            Genres = reader.GetString(12),
                            Homepage = reader.GetString(13),
                            Images = reader.GetString(14),
                            InProduction = reader.GetBoolean(15),
                            Keywords = reader.GetString(16),
                            Languages = reader.GetString(17),
                            LastAirDate = reader.GetDateTime(18),
                            LastEpisodeToAir = reader.GetString(19),
                            Name = reader.GetString(20),
                            NextEpisodeToAir = reader.GetString(21),
                            Networks = reader.GetString(22),
                            NumberOfEpisodes = reader.GetInt32(23),
                            NumberOfSeasons = reader.GetInt32(24),
                            OriginalLanguage = reader.GetString(25),
                            OriginalName = reader.GetString(26),
                            OriginCountry = reader.GetString(27),
                            Overview = reader.GetString(28),
                            Popularity = reader.GetDouble(29),
                            PosterPath = reader.GetString(30),
                            ProductionCompanies = reader.GetString(31),
                            Recommendations = reader.GetString(32),
                            Reviews = reader.GetString(33),
                            Seasons = reader.GetString(34),
                            Similar = reader.GetString(35),
                            Status = reader.GetString(36),
                            Translations = reader.GetString(37),
                            Type = reader.GetString(38),
                            Videos = reader.GetString(39),
                            VoteAverage = reader.GetDouble(40),
                            VoteCount = reader.GetInt32(41),
                        };
                        tv.Add(foundTvShow);
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

            if (tv.Count == 0)
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