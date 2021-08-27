using DomainModels;

namespace DataAccessInterfaces
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The user favorites accessor interface.
    /// </summary>
    public interface IUserFavoritesAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Adds the movie to the users favorite movies list.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="movie">The movie.</param>
        /// <returns>An int.</returns>
        int AddMovieToUsersFavoriteMoviesList(string email, Movie movie);
    }
}