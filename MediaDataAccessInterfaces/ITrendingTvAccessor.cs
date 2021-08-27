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
    public interface ITrendingTvAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Inserts the tv show.
        /// </summary>
        /// <param name="tv">The tv.</param>
        /// <returns>A bool.</returns>
        bool InsertTvShow(Tv tv);


        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/06
        ///     Updates the tv show.
        /// </summary>
        /// <param name="oldTv">The old t v.</param>
        /// <param name="newTv">The new t v.</param>
        /// <returns>A bool.</returns>
        int UpdateTvShow(Tv oldTv, Tv newTv);

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
        /// <param name="id">The id.</param>
        /// <returns>A TV.</returns>
        Tv SelectTvShowById(int id);
    }
}