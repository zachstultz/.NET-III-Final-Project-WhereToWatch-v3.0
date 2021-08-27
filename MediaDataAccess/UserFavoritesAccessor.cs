using System.Data;
using System.Data.SqlClient;
using DataAccessInterfaces;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The user favorites accessor.
    /// </summary>
    public class UserFavoritesAccessor : IUserFavoritesAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Adds the movie to user's favorite movies list.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="movie">The movie.</param>
        /// <returns>An int.</returns>
        public int AddMovieToUsersFavoriteMoviesList(string email, Movie movie)
        {
            var result = 0;
            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_add_movie_to_users_favorite_movies_list", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldMovies", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            return result;
        }
    }
}