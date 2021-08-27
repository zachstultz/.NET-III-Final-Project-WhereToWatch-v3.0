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
    ///     The tv accessor interface.
    /// </summary>
    public interface IFavoriteTvAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Inserts the tv show.
        /// </summary>
        /// <param name="tv">The tv.</param>
        /// <param name="user"></param>
        /// <returns>A bool.</returns>
        bool InsertTvShow(Tv tv, User user);


        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Updates the tv show.
        /// </summary>
        /// <param name="oldTv">The old tv.</param>
        /// <param name="newTv">The new tv.</param>
        /// <param name="user"></param>
        /// <returns>A bool.</returns>
        int UpdateTvShow(Tv oldTv, Tv newTv, User user);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Selects the all tv shows.
        /// </summary>
        /// <returns>A list of TVS.</returns>
        List<Tv> SelectAllTvShows();

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Selects the tv show by id.
        /// </summary>
        /// <returns>A TV.</returns>
        /// <param name="user"></param>
        List<Tv> SelectAllTvShowsByUserId(User user);

        bool CheckForExistanceCheckForTvShowByIdAndUserId(int id, User user);
    }
}