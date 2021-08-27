using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLayer;
using System.Web.Security;
using DomainModels;

namespace WebPresentation.Controllers
{
    public class FavoritesController : Controller
    {
        private FavoriteMovieAccessor _favoriteMovieAccessor = new FavoriteMovieAccessor();
        private FavoriteTvAccessor _favoriteTvAccessor = new FavoriteTvAccessor();
        private UserAccessor _userAccessor = new UserAccessor();
        private List<object> _favoritesList = new List<object>();
        private string _currentUserEmail;
        private User _currentUser;
        private int _currentUserId;
        private bool loggedIn;

        public ActionResult Favorites()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                try
                {
                    _currentUserEmail = User.Identity.Name;
                    _currentUser = _userAccessor.SelectUserByEmail(_currentUserEmail);
                    _currentUserId = _currentUser.UserId;
                    loggedIn = true;
                    List<Movie> movies = _favoriteMovieAccessor.SelectAllMoviesByUserId(_currentUser);
                    List<Tv> tvShows = _favoriteTvAccessor.SelectAllTvShowsByUserId(_currentUser);
                    foreach (var movie in movies)
                    {
                        _favoritesList.Add(movie);
                    }
                    foreach (var tvShow in tvShows)
                    {
                        _favoritesList.Add(tvShow);
                    }
                    return View(_favoritesList);
                }
                catch (Exception e)
                {
                }
            }
            return HttpNotFound();
        }

        /// <summary>
        /// Deletes the movie.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult DeleteMovie(int id)
        {
            _currentUserEmail = User.Identity.Name;
            _currentUser = _userAccessor.SelectUserByEmail(_currentUserEmail);
            _favoriteMovieAccessor.DeleteMovie(id, _currentUser);
            return RedirectToAction("Favorites");
        }

        /// <summary>
        /// Deletes the tv show.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An ActionResult.</returns>
        public ActionResult DeleteTvShow(int id)
        {
            _currentUserEmail = User.Identity.Name;
            _currentUser = _userAccessor.SelectUserByEmail(_currentUserEmail);
            _favoriteTvAccessor.DeleteTvShow(id, _currentUser);
            return RedirectToAction("Favorites");
        }
    }
}