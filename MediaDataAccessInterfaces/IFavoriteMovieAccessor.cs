using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace DataAccessInterfaces
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/05/06
    ///     The movie accessor interface.
    /// </summary>
    public interface IFavoriteMovieAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Initializes a new instance of the <see cref="InsertMovie"/> class.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <param name="user"></param>
        bool InsertMovie(Movie movie, User user);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Updates the movie.
        /// </summary>
        /// <param name="oldMovie">The old movie.</param>
        /// <param name="newMovie">The new movie.</param>
        /// <param name="user"></param>
        /// <returns>A bool.</returns>
        int UpdateMovie(Movie oldMovie, Movie newMovie, User user);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Selects the all movies.
        /// </summary>
        /// <returns>A list of Movies.</returns>
        List<Movie> SelectAllMovies();

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Selects the movie by id.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A list of Movies.</returns>
        List<Movie> SelectAllMoviesByUserId(User user);

        bool CheckForExistanceOfMovieByIdAndUserId(int id, User user);
    }
}