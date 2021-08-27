using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLayer;
using DomainModels;

namespace WebPresentation.Controllers
{
    public class TrendingMoviesController : Controller
    {
        private TrendingMovieAccessor _trendingMovieAccessor = new TrendingMovieAccessor();
        private FavoriteMovieAccessor _favoriteMovieAccessor = new FavoriteMovieAccessor();
        private MovieAccessor _movieAccessor = new MovieAccessor();
        private UserAccessor _userAccessor = new UserAccessor();

        public ActionResult TrendingMovies()
        {
            List<Movie> movies = _trendingMovieAccessor.SelectAllMovies();
            return View(movies);
        }

        public ContentResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace((sortBy)))
            {
                sortBy = "Title";
            }

            return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        }

        /// <summary>
        /// Edits the Trending TV Show.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult Edit(int id)
        {
            var item = _trendingMovieAccessor.SelectMovieById(id);
            return View(item);
        }

        /// <summary>
        /// Our HttpPost for editing the Movie.
        /// </summary>
        /// <param name="newMovie">The new movie.</param>
        /// <returns>A RedirectToRouteResult.</returns>
        [HttpPost]
        public ActionResult Edit(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                var oldMovie = _trendingMovieAccessor.SelectMovieById(newMovie.Id);
                newMovie.AccountStates = oldMovie.AccountStates;
                newMovie.Adult = oldMovie.Adult;
                newMovie.AlternativeTitles = oldMovie.AlternativeTitles;
                newMovie.BackdropPath = oldMovie.BackdropPath;
                newMovie.BelongsToCollection = oldMovie.BelongsToCollection;
                newMovie.Changes = oldMovie.Changes;
                newMovie.Credits = oldMovie.Credits;
                newMovie.Homepage = oldMovie.Homepage;
                newMovie.Images = oldMovie.Images;
                newMovie.ImdbId = oldMovie.ImdbId;
                newMovie.Lists = oldMovie.Lists;
                newMovie.OriginalLanguage = oldMovie.OriginalLanguage;
                newMovie.OriginalTitle = oldMovie.OriginalTitle;
                newMovie.PosterPath = oldMovie.PosterPath;
                newMovie.ProductionCompanies = oldMovie.ProductionCompanies;
                newMovie.ProductionCountries = oldMovie.ProductionCountries;
                newMovie.ReleaseDates = oldMovie.ReleaseDates;
                newMovie.ExternalIds = oldMovie.ExternalIds;
                newMovie.Releases = oldMovie.Releases;
                newMovie.Revenue = oldMovie.Revenue;
                newMovie.Reviews = oldMovie.Reviews;
                newMovie.Similar = oldMovie.Similar;
                newMovie.Recommendations = oldMovie.Recommendations;
                newMovie.SpokenLanguages = oldMovie.SpokenLanguages;
                newMovie.Status = oldMovie.Status;
                newMovie.Tagline = oldMovie.Tagline;
                newMovie.Translations = oldMovie.Translations;
                newMovie.Video = oldMovie.Video;
                newMovie.Videos = oldMovie.Videos;
                newMovie.VoteAverage = oldMovie.VoteAverage;
                newMovie.VoteCount = oldMovie.VoteCount;
                _trendingMovieAccessor.UpdateMovie(oldMovie, newMovie);
                return RedirectToAction("TrendingMovies");
            }
            else
            {
                var oldMovie = _trendingMovieAccessor.SelectMovieById(newMovie.Id);
                newMovie.PosterPath = oldMovie.PosterPath;
                return View(newMovie);
            }
        }

        public ActionResult Details(int id)
        {
            var movie = _trendingMovieAccessor.SelectMovieById(id);
            return View(movie);
        }

        public ActionResult AddToFavorites(int id)
        {
            var user = _userAccessor.SelectUserByEmail(User.Identity.Name);
            var movie = _trendingMovieAccessor.SelectMovieById(id);
            _favoriteMovieAccessor.InsertMovie(movie, user);
            return RedirectToAction("TrendingMovies");
        }
    }
}