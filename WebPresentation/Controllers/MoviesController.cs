using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModels;
using DataAccessLayer;
using System.Web.Security;

namespace WebPresentation.Controllers
{
    public class MoviesController : Controller
    {
        private MovieAccessor _movieAccessor = new MovieAccessor();
        private UserAccessor _userAccessor = new UserAccessor();
        private FavoriteMovieAccessor _favoriteMovieAccessor = new FavoriteMovieAccessor();

        // GET: Movies/Movies
        public ActionResult Movies()
        {
            Movie movie = new Movie() {Title = "Great Gatsby"};
            List<Movie> movies = _movieAccessor.SelectAllMovies();
            return View(movies);
        }

        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

        /// <summary>
        /// Edits the Trending TV Show.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult Edit(int id)
        {
            var item = _movieAccessor.SelectMovieById(id);
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
                var oldMovie = _movieAccessor.SelectMovieById(newMovie.Id);
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
                _movieAccessor.UpdateMovie(oldMovie, newMovie);
                return RedirectToAction("Movies");
            }
            else
            {
                var oldMovie = _movieAccessor.SelectMovieById(newMovie.Id);
                newMovie.PosterPath = oldMovie.PosterPath;
                return View(newMovie);
            }
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
        /// Deletes the movie.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult DeleteMovie(int id)
        {
            _movieAccessor.DeleteMovie(id);
            return RedirectToAction("Movies");
        }

        /// <summary>
        /// Adds the movie to favorites.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult AddToFavorites(int id)
        {
            var user = _userAccessor.SelectUserByEmail(User.Identity.Name);
            var movie = _movieAccessor.SelectMovieById(id);
            _favoriteMovieAccessor.InsertMovie(movie, user);
            return RedirectToAction("Movies");
        }

        public ActionResult Details(int id)
        {
            var movie = _movieAccessor.SelectMovieById(id);
            return View(movie);
        }
    }
}