using System;
using System.Collections.Generic;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The Movie object class to store everything related to a movie.
    /// </summary>
    public class Movie
    {
        public int Id { get; set; }
        public string AccountStates { get; set; }
        public bool Adult { get; set; }
        public string AlternativeTitles { get; set; }
        public string BackdropPath { get; set; }
        public string BelongsToCollection { get; set; }
        [Required]
        public long Budget { get; set; }
        public string Changes { get; set; }
        public string Credits { get; set; }
        [Required]
        public string Genres { get; set; }
        public string Homepage { get; set; }
        public string Images { get; set; }
        public string ImdbId { get; set; }
        [Required]
        public string Keywords { get; set; }
        public string Lists { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        [Required]
        public string Overview { get; set; }
        [Required]
        public double Popularity { get; set; }
        public string PosterPath { get; set; }
        public string ProductionCompanies { get; set; }
        public string ProductionCountries { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseDates { get; set; }
        public string ExternalIds { get; set; }
        public string Releases { get; set; }
        public long Revenue { get; set; }
        public string Reviews { get; set; }
        [Required]
        public int? Runtime { get; set; }
        public string Similar { get; set; }
        public string Recommendations { get; set; }
        public string SpokenLanguages { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        [Required]
        public string Title { get; set; }
        public string Translations { get; set; }
        public bool Video { get; set; }
        public string Videos { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Full Constructor
        ///     Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        public Movie(int id, string accountStates, bool adult, string alternativeTitles, string backdropPath, string belongsToCollection, long budget, string changes, string credits, string genres, string homepage, string images, string imdbId, string keywords, string lists, string originalLanguage, string originalTitle, string overview, double popularity, string posterPath, string productionCompanies, string productionCountries, DateTime? releaseDate, string releaseDates, string externalIds, string releases, long revenue, string reviews, int? runtime, string similar, string recommendations, string spokenLanguages, string status, string tagline, string title, string translations, bool video, string videos, double voteAverage, int voteCount)
        {
            Id = id;
            AccountStates = accountStates;
            Adult = adult;
            AlternativeTitles = alternativeTitles;
            BackdropPath = backdropPath;
            BelongsToCollection = belongsToCollection;
            Budget = budget;
            Changes = changes;
            Credits = credits;
            Genres = genres;
            Homepage = homepage;
            Images = images;
            ImdbId = imdbId;
            Keywords = keywords;
            Lists = lists;
            OriginalLanguage = originalLanguage;
            OriginalTitle = originalTitle;
            Overview = overview;
            Popularity = popularity;
            PosterPath = posterPath;
            ProductionCompanies = productionCompanies;
            ProductionCountries = productionCountries;
            ReleaseDate = releaseDate;
            ReleaseDates = releaseDates;
            ExternalIds = externalIds;
            Releases = releases;
            Revenue = revenue;
            Reviews = reviews;
            Runtime = runtime;
            Similar = similar;
            Recommendations = recommendations;
            SpokenLanguages = spokenLanguages;
            Status = status;
            Tagline = tagline;
            Title = title;
            Translations = translations;
            Video = video;
            Videos = videos;
            VoteAverage = voteAverage;
            VoteCount = voteCount;
        }

        public Movie()
        {

        }
    }
}