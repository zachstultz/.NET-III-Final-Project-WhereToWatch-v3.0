using System;
using System.Collections.Generic;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The TVShow Object class to store everything related to a tv show.
    /// </summary>
    public class Tv
    {
        public int Id { get; set; }
        public string AccountStates { get; set; }
        public string AlternativeTitles { get; set; }
        public string BackdropPath { get; set; }
        public string Changes { get; set; }
        public string ContentRatings { get; set; }
        public string CreatedBy { get; set; }
        public string Credits { get; set; }
        public int EpisodeRunTime { get; set; }
        public string ExternalIds { get; set; }
        [Required(ErrorMessage = "Please enter a date.")]
        public DateTime? FirstAirDate { get; set; }
        public int GenreIds { get; set; }
        [Required(ErrorMessage = "Please enter some genres.")]
        public string Genres { get; set; }
        public string Homepage { get; set; }
        public string Images { get; set; }
        public bool InProduction { get; set; }
        [Required(ErrorMessage = "Please enter some Keywords.")]
        public string Keywords { get; set; }
        public string Languages { get; set; }
        public DateTime? LastAirDate { get; set; }
        public string LastEpisodeToAir { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        public string NextEpisodeToAir { get; set; }
        [Required(ErrorMessage = "Please enter some networks.")]
        public string Networks { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int NumberOfSeasons { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalName { get; set; }
        public string OriginCountry { get; set; }
        [Required(ErrorMessage = "Please enter an overview.")]
        public string Overview { get; set; }
        [Required(ErrorMessage = "Please enter a number.")]
        public double Popularity { get; set; }
        public string PosterPath { get; set; }
        public string ProductionCompanies { get; set; }
        public string Recommendations { get; set; }
        public string Reviews { get; set; }
        public string Seasons { get; set; }
        public string Similar { get; set; }
        public string Status { get; set; }
        public string Translations { get; set; }
        public string Type { get; set; }
        public string Videos { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/04
        ///     Full Constructor
        ///     Initializes a new instance of the <see cref="Tv"/> class.
        /// </summary>
        public Tv(int id, string accountStates, string alternativeTitles, string backdropPath, string changes, string contentRatings, string createdBy, string credits, int episodeRunTime, string externalIds, DateTime? firstAirDate, int genreIds, string genres, string homepage, string images, bool inProduction, string keywords, string languages, DateTime? lastAirDate, string lastEpisodeToAir, string name, string nextEpisodeToAir, string networks, int numberOfEpisodes, int numberOfSeasons, string originalLanguage, string originalName, string originCountry, string overview, double popularity, string posterPath, string productionCompanies, string recommendations, string reviews, string seasons, string similar, string status, string translations, string type, string videos, double voteAverage, int voteCount)
        {
            Id = id;
            AccountStates = accountStates;
            AlternativeTitles = alternativeTitles;
            BackdropPath = backdropPath;
            Changes = changes;
            ContentRatings = contentRatings;
            CreatedBy = createdBy;
            Credits = credits;
            EpisodeRunTime = episodeRunTime;
            ExternalIds = externalIds;
            FirstAirDate = firstAirDate;
            GenreIds = genreIds;
            Genres = genres;
            Homepage = homepage;
            Images = images;
            InProduction = inProduction;
            Keywords = keywords;
            Languages = languages;
            LastAirDate = lastAirDate;
            LastEpisodeToAir = lastEpisodeToAir;
            Name = name;
            NextEpisodeToAir = nextEpisodeToAir;
            Networks = networks;
            NumberOfEpisodes = numberOfEpisodes;
            NumberOfSeasons = numberOfSeasons;
            OriginalLanguage = originalLanguage;
            OriginalName = originalName;
            OriginCountry = originCountry;
            Overview = overview;
            Popularity = popularity;
            PosterPath = posterPath;
            ProductionCompanies = productionCompanies;
            Recommendations = recommendations;
            Reviews = reviews;
            Seasons = seasons;
            Similar = similar;
            Status = status;
            Translations = translations;
            Type = type;
            Videos = videos;
            VoteAverage = voteAverage;
            VoteCount = voteCount;
        }

        public Tv()
        {

        }
    }
}