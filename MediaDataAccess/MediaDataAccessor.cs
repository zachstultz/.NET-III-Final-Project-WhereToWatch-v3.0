using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     Handles access to the Object Lists that we store Movie and TV Show Objects in
    /// </summary>
    public class MediaDataAccessor
    {
        /// <summary>
        ///     the media list, it includes movies, TV shows, and people.
        /// </summary>
        private static readonly List<object> Media = new List<object>();

        /// <summary>
        ///     The list of movies and TV shows favorites by the user.
        /// </summary>
        private static readonly List<object> UsersFavorites = new List<object>();

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves a media list depending on the value passed, then returns it.
        /// </summary>
        /// <returns></returns>
        public static List<object> RetrieveMediaList(int i)
        {
            if (i == 0) // 0=default list, anything else is the favorite list.
                return Media;

            return UsersFavorites;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves the media list count.
        /// </summary>
        /// <returns>An int.</returns>
        public static int RetrieveMediaListCount()
        {
            return Media.Count;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Clears the media list of the list of the corresponding value passed.
        /// </summary>
        public static void ClearMediaList(int i)
        {
            if (i == 0) // clears the list, does _media if it's 0, otherwise the favorites list.
                Media.Clear();
            else
                UsersFavorites.Clear();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Adds whatever object is passed-in to the corresponding List, based on the value that was passed in.
        /// </summary>
        public static void AddMedia(object mediaObj, int i)
        {
            if (i == 0) // if the value is 0, it's for the regular List, otherwise it's for the favorites list.
                Media.Add(mediaObj);
            else
                UsersFavorites.Add(mediaObj);
        }
    }
}