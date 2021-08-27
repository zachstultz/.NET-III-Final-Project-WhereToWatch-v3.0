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
    public interface ITrendingMovieAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Initializes a new instance of the <see cref="InsertMovie"/> class.
        /// </summary>
        /// <param name="movie">The movie.</param>
        bool InsertMovie(Movie movie);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Updates the movie.
        /// </summary>
        /// <param name="oldMovie">The old movie.</param>
        /// <param name="newMovie">The new movie.</param>
        /// <returns>A bool.</returns>
        int UpdateMovie(Movie oldMovie, Movie newMovie);

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
        /// <param name="id">The id.</param>
        /// <returns>A Movie.</returns>
        Movie SelectMovieById(int id);
    }
}