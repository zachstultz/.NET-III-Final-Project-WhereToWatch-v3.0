using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using DomainModels;
using Newtonsoft.Json.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Trending;
using TMDbLib.Objects.TvShows;
using Movie = DomainModels.Movie;

namespace MediaLogicLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     Handles interaction with TheMovieDB's API.
    /// </summary>
    public class MediaApi
    {
        private static readonly string ApiKey = "API KEY HERE!"; // API Key for TheMovieDB
        private readonly TMDbClient _client = new TMDbClient(ApiKey); // TheMovieDB API Access Object.
        private readonly List<object> _mediaObjects = new List<object>();
        private readonly List<object> _trendingMediaObjects = new List<object>();

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Scraps the initial list-box results.
        ///     This includes the name, id, and media type.
        /// </summary>
        /// <param name="userInput"></param>
        public List<object> MediaSearch(string userInput)
        {
            var results = _client.SearchMultiAsync(userInput).Result; // passes the user's query to a MultiAsyncSearch
            foreach (var result in results.Results) // loops through the results
            {
                var mediaType = result.MediaType.ToString().ToUpper(); // converts it to upper for the sake of style
                switch (mediaType)
                {
                    case "MOVIE":
                        var movie = _client.GetMovieAsync(result.Id, MovieMethods.Videos | MovieMethods.Keywords)
                            .Result; // passes the movie ID gained from the SearchMultiAsync result
                        //movie.
                        if (movie.ReleaseDate != null
                        ) // and we pass it to GetMovieAsync method that pulls the items for the partial constructor.
                        {
                            var movieObj = new Movie(movie.Id, "None", movie.Adult, "None",
                                SetMovieBackdropPath(movie), "None",
                                movie.Budget,
                                "None", "None", GetGenresForMedia(movie), SetStreamOn(movie), "None",
                                SetImdb(movie), GetTags(movie),
                                "None", "None", movie.OriginalTitle, movie.Overview,
                                movie.Popularity,
                                SetPoster(movie),
                                "None", "None", movie.ReleaseDate,
                                "None",
                                "None",
                                "None", movie.Revenue, "None", SetMovieRuntime(movie), "None",
                                "None",
                                "None",
                                movie.Status, movie.Tagline, movie.Title, "None", movie.Video, GetTrailerLink(movie),
                                movie.VoteAverage, movie.VoteCount);
                            _mediaObjects.Add(movieObj);
                        }

                        break;
                    case "TV":
                        var tv = _client.GetTvShowAsync(result.Id, TvShowMethods.Keywords | TvShowMethods.Videos)
                            .Result;
                        if (tv.FirstAirDate != null)
                        {
                            var tvShowObj = new Tv(tv.Id, "None", "None", SetBackdropPath(tv), "None",
                                "None", SetCreatedBy(tv), "None", 0, "None",
                                tv.FirstAirDate,
                                0, GetGenresForMedia(tv), tv.Homepage, "None", tv.InProduction, GetTags(tv),
                                "None", SetTvLastAirDate(tv), "None", tv.Name, "None",
                                SetStreamOn(tv),
                                tv.NumberOfEpisodes, tv.NumberOfSeasons, tv.OriginalLanguage, tv.OriginalName,
                                tv.OriginCountry.ToString(),
                                tv.Overview, tv.Popularity, SetPoster(tv), "None", "None",
                                "None",
                                tv.Seasons.ToString(), "None", tv.Status, "None", tv.Type, GetTrailerLink(tv), tv.VoteAverage,
                                tv.VoteCount);
                            _mediaObjects.Add(tvShowObj);
                        }

                        break;
                }
            }

            return _mediaObjects;
        }

        private static DateTime? SetTvLastAirDate(TvShow tv)
        {
            if (tv.LastAirDate != null)
            {
                return tv.LastAirDate;
            }
            else
            {
                DateTime dateTime = new DateTime(9999, 12, 31);
                return dateTime;
            }
        }

        public List<object> GetTrendingLists()
        {
            var movieResults = _client.GetTrendingMoviesAsync(TimeWindow.Week).Result;
            var tvResults = _client.GetTrendingTvAsync(TimeWindow.Week).Result;
            foreach (var result in movieResults.Results ) // loops through the results
            {
                var movie = _client.GetMovieAsync(result.Id, MovieMethods.Videos | MovieMethods.Keywords)
                    .Result;
                if (movie.ReleaseDate != null)
                {
                    var movieObj = new Movie(movie.Id, "None", movie.Adult, "None",
                        SetMovieBackdropPath(movie), "None",
                        movie.Budget,
                        "None", "None", GetGenresForMedia(movie), SetStreamOn(movie), "None",
                        SetImdb(movie), GetTags(movie),
                        "None", "None", movie.OriginalTitle, movie.Overview,
                        movie.Popularity,
                        SetPoster(movie),
                        "None", "None", movie.ReleaseDate,
                        "None",
                        "None",
                        "None", movie.Revenue, "None", SetMovieRuntime(movie), "None",
                        "None",
                        "None",
                        movie.Status, movie.Tagline, movie.Title, "None", movie.Video, GetTrailerLink(movie),
                        movie.VoteAverage, movie.VoteCount);
                    _trendingMediaObjects.Add(movieObj);
                }
            }
            foreach (var result in tvResults.Results) // loops through the results
            {
                var tv = _client.GetTvShowAsync(result.Id, TvShowMethods.Keywords | TvShowMethods.Videos)
                    .Result;
                if (tv.FirstAirDate != null)
                {
                    var tvShowObj = new Tv(tv.Id, "None", "None", SetBackdropPath(tv), "None",
                        "None", SetCreatedBy(tv), "None", 0, "None",
                        tv.FirstAirDate,
                        0, GetGenresForMedia(tv), tv.Homepage, "None", tv.InProduction, GetTags(tv),
                        "None", tv.LastAirDate, "None", tv.Name, "None",
                        SetStreamOn(tv),
                        tv.NumberOfEpisodes, tv.NumberOfSeasons, tv.OriginalLanguage, tv.OriginalName,
                        tv.OriginCountry.ToString(),
                        tv.Overview, tv.Popularity, SetPoster(tv), "None", "None",
                        "None",
                        tv.Seasons.ToString(), "None", tv.Status, "None", tv.Type, GetTrailerLink(tv), tv.VoteAverage,
                        tv.VoteCount);
                    _trendingMediaObjects.Add(tvShowObj);
                }

            }
            return _trendingMediaObjects;
        }

        /// <summary>
        /// Sets the movie runtime.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>An int? .</returns>
        private static int? SetMovieRuntime(TMDbLib.Objects.Movies.Movie movie)
        {
            if (movie.Runtime != null)
            {
                return movie.Runtime;
            }
            return 0;
        }

        /// <summary>
        /// Sets the backdrop path.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A string.</returns>
        private static string SetBackdropPath(object obj)
        {
            string baseUrl = "http://image.tmdb.org/t/p/w1280";

            if (obj is TMDbLib.Objects.Movies.Movie movie)
            {
                if (movie.BackdropPath != null)
                {
                    return baseUrl + movie.BackdropPath;
                }
                else
                {
                    return "None";
                }
            }

            if (obj is TvShow tv)
            {
                if (tv.BackdropPath != null)
                {
                    return baseUrl + tv.BackdropPath;
                }
                else
                {
                    return "";
                }
            }

            return "";
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/08
        ///     Sets the created by.
        /// </summary>
        /// <param name="tv">The tv.</param>
        /// <returns>A string.</returns>
        private static string SetCreatedBy(TvShow tv)
        {
            if (tv.CreatedBy.Count != 0 && tv.CreatedBy != null)
            {
                try
                {
                    return tv.CreatedBy[0].Name;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/08
        ///     Sets the movie backdrop path.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>A string.</returns>
        private static string SetMovieBackdropPath(TMDbLib.Objects.Movies.Movie movie)
        {
            if (movie.BackdropPath != null)
            {
                return "https://image.tmdb.org/t/p/w1280" + movie.BackdropPath;
            }
            else
            {
                return "None";
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/08
        ///     Sets the imdb.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>A string.</returns>
        private static string SetImdb(TMDbLib.Objects.Movies.Movie movie)
        {
            if (!string.IsNullOrEmpty(movie.ImdbId))
            {
                return movie.ImdbId;
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/05
        ///     Sets the stream on.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public string SetStreamOn(object sender)
        {
            StringBuilder result = new StringBuilder();
            if (sender is TMDbLib.Objects.Movies.Movie movie)
            {
                if (!string.IsNullOrEmpty(movie.Homepage))
                {
                    result.Append(movie.Homepage + "\n");
                }
            }

            if (sender is TvShow tv)
            {
                if (tv.Networks != null)
                {
                    foreach (var network in tv.Networks)
                    {
                        if (!string.IsNullOrEmpty(network.Name))
                        {
                            result.Append(network.Name + "\n");
                        }
                    }
                }
            }

            return result.ToString();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/05
        ///     Sets the tags.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public String GetTags(object sender)
        {
            StringBuilder combinedTags = new StringBuilder();
            switch (sender)
            {
                case TMDbLib.Objects.Movies.Movie movie1:
                    {
                        TMDbLib.Objects.Movies.Movie movie = movie1;
                        if (movie.Keywords.Keywords != null && movie.Keywords.Keywords.Count != 0)
                        {
                            foreach (var t in movie.Keywords.Keywords)
                            {
                                if (t == movie.Keywords.Keywords[movie.Keywords.Keywords.Count - 1])
                                {
                                    string result = CapitalizeFirstLetter(t.Name);
                                    combinedTags.Append(result);
                                }
                                else
                                {
                                    string result = CapitalizeFirstLetter(t.Name);
                                    combinedTags.Append(result + ", ");
                                }
                            }
                        }

                        break;
                    }

                case TvShow tv1:
                    {
                        TvShow tv = tv1;
                        if (tv.Keywords.Results != null && tv.Keywords.Results.Count != 0)
                        {
                            foreach (var t in tv.Keywords.Results)
                            {
                                if (t == tv.Keywords.Results[tv.Keywords.Results.Count - 1])
                                {
                                    string result = CapitalizeFirstLetter(t.Name);
                                    combinedTags.Append(result);
                                }
                                else
                                {
                                    string result = CapitalizeFirstLetter(t.Name);
                                    combinedTags.Append(result + ", ");
                                }
                            }
                        }

                        break;
                    }
            }

            return combinedTags.ToString();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/05
        ///     Capitalizes the first letter.
        /// </summary>
        /// <param name="s">The passed string.</param>
        /// <returns>A string.</returns>
        public string CapitalizeFirstLetter(string s)
        {
            char[] a = s.ToLower().ToCharArray();

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];
            }

            return new string(a);
        }

        public string GetTrailerLink(object sender)
        {
            string youtubeBaseUrl = "https://www.youtube.com/watch?v=";
            string result = "";
            if (sender is TMDbLib.Objects.Movies.Movie movie && movie.Videos != null)
            {
                foreach (var video in movie.Videos.Results)
                {
                    if ((movie.Videos.Results.Count != 0 && video.Type == "Trailer") &&
                        video.Site == "YouTube")
                    {
                        result = youtubeBaseUrl + video.Key;
                        break;
                    }
                }
            }

            if (sender is TvShow tv && tv.Videos != null)
            {
                foreach (var video in tv.Videos.Results)
                {
                    if ((tv.Videos.Results.Count != 0 && video.Type == "Trailer") &&
                        video.Site == "YouTube")
                    {
                        result = youtubeBaseUrl + video.Key;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/04
        ///     Sets the poster.
        /// </summary>
        /// <param name="item">The item.</param>
        private string SetPoster(object item)
        {
            string baseUrl = "http://image.tmdb.org/t/p/w500";
            if (item is TMDbLib.Objects.Movies.Movie movie)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    return baseUrl + movie.PosterPath;
                }
            }

            if (item is TvShow tv)
            {
                if (!string.IsNullOrEmpty(tv.PosterPath))
                {
                    return baseUrl + tv.PosterPath;
                }
            }

            return "";
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Loops our Genres Object contained within our Object and concatenates it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetGenresForMedia(object obj)
        {
            StringBuilder genres = new StringBuilder();
            if (obj is TMDbLib.Objects.Movies.Movie movie)
            {
                for (int i = 0; i < movie.Genres.Count; i++)
                {
                    if (i == movie.Genres.Count - 1)
                    {
                        genres.Append(movie.Genres[i].Name);
                    }
                    else
                    {
                        genres.Append(movie.Genres[i].Name + ", ");
                    }
                }
            }
            else if (obj is TvShow tvShow)
            {
                genres = new StringBuilder();
                for (int i = 0; i < tvShow.Genres.Count; i++)
                {
                    if (i == tvShow.Genres.Count - 1)
                    {
                        genres.Append(tvShow.Genres[i].Name);
                    }
                    else
                    {
                        genres.Append(tvShow.Genres[i].Name + ", ");
                    }
                }
            }

            return genres.ToString();
        }


        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Checks for a valid poster path,
        ///     otherwise it gets set to a default.
        /// </summary>
        /// <param name="posterPath">The poster path.</param>
        private string IsValidPosterPath(string posterPath)
        {
            if (string.IsNullOrEmpty(posterPath))
                posterPath =
                    "https://josselyn.org/wp-content/themes/qube/assets/images/no-image/No-Image-Found-400x264.png";
            else
                posterPath = "http://image.tmdb.org/t/p/w500" + posterPath;
            return posterPath;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Gets the genres in string form.
        /// </summary>
        /// <param name="genres">The genres.</param>
        /// <returns>A string.</returns>
        public string GetGenresInStringForm(List<Genre> genres)
        {
            var build = new StringBuilder();
            for (var i = 0; i < genres.Count; i++)
                if (i == genres.Count - 1)
                    build.Append(genres[i].Name);
                else
                    build.Append(genres[i].Name + ", ");

            return build.ToString();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Takes a trailer URL and checks if the URL has a KEY appended
        ///     on the end of it, if it does, it's returned, if it doesn't, then we return set it's value to
        ///     null and return it.
        /// </summary>
        /// <param name="trailerUrl"></param>
        /// <returns></returns>
        public string TrailerKeyEmptyCheck(string trailerUrl)
        {
            if (trailerUrl == "https://www.youtube.com/watch?v="
                ) // if the youtube link URL doesn't have the key on the end.
                //then set null, which I have a check for in the MediaResultsForm
                return null; // otherwise return the URL as-is.

            return trailerUrl;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Scraps the JSON page for the variable KEY,
        ///     KEY is the value that we append to the end of a youtube link, which links to the trailer.
        /// </summary>
        /// <returns></returns>
        public string ScrapJsonPage(object obj, int id, string apiKey)
        {
            var mediaType = "";
            if (obj is Movie)
                mediaType = "movie";
            else if (obj is Tv) mediaType = "tv";

            var url = "https://api.themoviedb.org/3/" + mediaType.ToLower() + "/" + id +
                      "/videos?api_key=" + apiKey + "&language=en-us"; // the API page to parse the JSON from.
            using (var wc = new WebClient()
            ) // use WebClient to download it, and the using to automatically close it.
            {
                var rawJson = wc.DownloadString(url); // the JSON downloaded and put into a string variable.
                var o = JObject.Parse(rawJson); // Parses it into a JObject.
                var token = o.SelectToken("$..results[0].key"); // Parses the JSON and gets the KEY attribute.
                return "https://www.youtube.com/watch?v=" +
                       token; //the returned youtube link with the token/key appended
            }
        }


        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Scraps the HTML page for the streaming service text that the media is available on,
        ///     and the matching logo for the streaming service.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string[] ScrapHtmlPage(string url)
        {
            var html = url; // the URL to scrape.
            var web = new HtmlWeb();
            var htmlDoc = web.Load(html); // we load the HTML and put it into a HtmlDocument
            htmlDoc.OptionEmptyCollection = true; // gets rid of error.
            var availableOn = htmlDoc.DocumentNode?.SelectNodes("//div[contains(@class, 'provider')]")
                ?.Descendants("img")
                ?.FirstOrDefault()
                ?.GetAttributeValue("alt", null); // the text for what streaming service it's available on.
            var logo = htmlDoc.DocumentNode?.SelectNodes("//div[contains(@class, 'provider')]")
                ?.Descendants("img")?.FirstOrDefault()
                ?.GetAttributeValue("src", null); // the corresponding logo image URL for the service.

            if (availableOn == null) // if we can't find a service to stream/purchase from, we set that string.
                availableOn = "No Available Streaming Service/Purchase Option Found.";
            if (logo == null
                ) // if there's no logo, we set the URL for the logo to an empty string, which is checked for in the
                // Results Form.
                logo = "";
            string[]
                scrappedHtml =
                    {availableOn, logo}; // we put the two in an array so I can return both values from the method.
            return scrappedHtml; // we return the array for use above in the ScrapMetaData method's.
        }
    }
}