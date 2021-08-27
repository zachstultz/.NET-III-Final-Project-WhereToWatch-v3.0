using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModels;
using DataAccessLayer;

namespace WebPresentation.Controllers
{
    public class TvController : Controller
    {
        private TvAccessor _tvAccessor = new TvAccessor();
        private FavoriteTvAccessor _favoriteTvAccessor = new FavoriteTvAccessor();
        private UserAccessor _userAccessor = new UserAccessor();
        public ActionResult TvShows()
        {
            List<Tv> tvShowsList = _tvAccessor.SelectAllTvShows();
            return View(tvShowsList);
        }

        // GET: TV
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

        public ActionResult DeleteTvShow(int id)
        {
            _tvAccessor.DeleteTvShow(id);
            return RedirectToAction("TvShows");
        }

        public ActionResult Edit(int id)
        {
            var item = _tvAccessor.SelectTvShowById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Tv newTv)
        {
            if (ModelState.IsValid)
            {
                var oldTv = _tvAccessor.SelectTvShowById(newTv.Id);
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
                _tvAccessor.UpdateTvShow(oldTv, newTv);
                return RedirectToAction("TvShows");
            }
            else
            {
                var oldTv = _tvAccessor.SelectTvShowById(newTv.Id);
                newTv.PosterPath = oldTv.PosterPath;
                return View(newTv);
            }
        }

        /// <summary>
        /// Adds the tv show to favorites.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult AddToFavorites(int id)
        {
            var user = _userAccessor.SelectUserByEmail(User.Identity.Name);
            var movie = _tvAccessor.SelectTvShowById(id);
            _favoriteTvAccessor.InsertTvShow(movie, user);
            return RedirectToAction("TvShows");
        }

        public ActionResult Details(int id)
        {
            var tvShow = _tvAccessor.SelectTvShowById(id);
            return View(tvShow);
        }
    }
}