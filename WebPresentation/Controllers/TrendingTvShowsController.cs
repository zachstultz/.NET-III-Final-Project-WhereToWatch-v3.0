using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLayer;
using DomainModels;

namespace WebPresentation.Controllers
{
    public class TrendingTvShowsController : Controller
    {
        private TrendingTvAccessor _trendingTvAccessor = new TrendingTvAccessor();
        private UserAccessor _userAccessor = new UserAccessor();
        private FavoriteTvAccessor _favoriteTvAccessor = new FavoriteTvAccessor();

        public ActionResult TrendingTvShows()
        {
            List<Tv> tvShows = _trendingTvAccessor.SelectAllTvShows();
            return View(tvShows);
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
            var item = _trendingTvAccessor.SelectTvShowById(id);
            return View(item);
        }

        /// <summary>
        /// Our HttpPost for editing the Trending TV Show.
        /// </summary>
        /// <param name="newTv">The new tv.</param>
        /// <returns>A RedirectToRouteResult.</returns>
        [HttpPost]
        public ActionResult Edit(Tv newTv)
        {
            if (ModelState.IsValid)
            {
                var oldTv = _trendingTvAccessor.SelectTvShowById(newTv.Id);
                newTv.AccountStates = oldTv.AccountStates;
                newTv.AlternativeTitles = oldTv.AlternativeTitles;
                newTv.BackdropPath = oldTv.BackdropPath;
                newTv.Changes = oldTv.Changes;
                newTv.ContentRatings = oldTv.ContentRatings;
                newTv.CreatedBy = oldTv.CreatedBy;
                newTv.Credits = oldTv.Credits;
                newTv.EpisodeRunTime = oldTv.EpisodeRunTime;
                newTv.ExternalIds = oldTv.ExternalIds;
                newTv.GenreIds = oldTv.GenreIds;
                newTv.Homepage = oldTv.Homepage;
                newTv.Images = oldTv.Images;
                newTv.InProduction = oldTv.InProduction;
                newTv.Languages = oldTv.Languages;
                newTv.LastAirDate = oldTv.LastAirDate;
                newTv.LastEpisodeToAir = oldTv.LastEpisodeToAir;
                newTv.NextEpisodeToAir = oldTv.NextEpisodeToAir;
                newTv.NumberOfEpisodes = oldTv.NumberOfEpisodes;
                newTv.NumberOfSeasons = oldTv.NumberOfSeasons;
                newTv.OriginalLanguage = oldTv.OriginalLanguage;
                newTv.OriginalName = oldTv.OriginalName;
                newTv.OriginCountry = oldTv.OriginCountry;
                newTv.PosterPath = oldTv.PosterPath;
                newTv.ProductionCompanies = oldTv.ProductionCompanies;
                newTv.Recommendations = oldTv.Recommendations;
                newTv.Reviews = oldTv.Reviews;
                newTv.Seasons = oldTv.Seasons;
                newTv.Similar = oldTv.Similar;
                newTv.Status = oldTv.Status;
                newTv.Translations = oldTv.Translations;
                newTv.Type = oldTv.Type;
                newTv.Videos = oldTv.Videos;
                newTv.VoteAverage = oldTv.VoteAverage;
                newTv.VoteCount = oldTv.VoteCount;
                _trendingTvAccessor.UpdateTvShow(oldTv, newTv);
                return RedirectToAction("TrendingTvShows");
            }
            else
            {
                var oldTv = _trendingTvAccessor.SelectTvShowById(newTv.Id);
                newTv.PosterPath = oldTv.PosterPath;
                return View(newTv);
            }
        }

        /// <summary>
        /// Returns the Detailed View for the Trending TV Show.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult Details(int id)
        {
            var tvShow = _trendingTvAccessor.SelectTvShowById(id);
            return View(tvShow);
        }

        public ActionResult AddToFavorites(int id)
        {
            var user = _userAccessor.SelectUserByEmail(User.Identity.Name);
            var movie = _trendingTvAccessor.SelectTvShowById(id);
            _favoriteTvAccessor.InsertTvShow(movie, user);
            return RedirectToAction("TrendingTvShows");
        }

    }
}