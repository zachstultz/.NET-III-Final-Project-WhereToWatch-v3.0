using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using DataAccessLayer;
using DomainModels;

namespace Final_Project
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     Interaction logic for MediaMovieResultPage.xaml
    /// </summary>
    public partial class MediaMovieResultPage
    {
        private static object _currentObject = new object();
        private static FavoriteMovieAccessor _favoriteMovieAccessor = new FavoriteMovieAccessor();
        private static FavoriteTvAccessor _favoriteTvAccessor = new FavoriteTvAccessor();
        private static string _tempUrl;
        private static User _loggedInUser;

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/04
        ///     Empty Constructor
        /// </summary>
        public MediaMovieResultPage(object sender, User user)
        {
            InitializeComponent();
            _loggedInUser = user;
            _currentObject = sender;
            if (user != null)
            {
                if (_currentObject is Movie movieCheck)
                {
                    if (_favoriteMovieAccessor.CheckForExistanceOfMovieByIdAndUserId(movieCheck.Id, _loggedInUser)) BtnAddToFavorites.IsEnabled = false;
                }
                if (_currentObject is Tv tvCheck)
                {
                    if (_favoriteTvAccessor.CheckForExistanceCheckForTvShowByIdAndUserId(tvCheck.Id, _loggedInUser)) BtnAddToFavorites.IsEnabled = false;
                }
            }

            _tempUrl = "";
            if (sender is Movie movie)
            {
                WindowMediaResultPage.Title = "WhereToWatch v2.0 - Result Page - " + movie.Title;
                SetPoster(movie);
                TbRunTime.Text = ConvertRuntime(movie.Runtime);
                TbRating.Text = (movie.VoteAverage * 10) + "%";
                TbRanking.Text = "#" + movie.VoteCount;
                TbTitle.Text = movie.Title;
                TbReleaseDate.Text = Convert.ToDateTime(movie.ReleaseDate).Year.ToString();
                TbOverviewContent.Text = movie.Overview;
                TbStudiosContent.Text = movie.Credits;
                TbGenresContent.Text = movie.Genres;
                _tempUrl = movie.Videos;
                TbTypeContent.Text = "Movie";
                TbStatusContent.Text = movie.Status;
                HideTvEntities();
                if (string.IsNullOrEmpty(movie.Homepage))
                {
                    TbStatusContent.Visibility = Visibility.Hidden;
                }
                else
                {
                    TbStreamOnContent.Text = movie.Homepage;
                    TbStreamOnContent.TextAlignment = TextAlignment.Center;
                }
                TbTagsContent.Text = movie.Keywords;
            }

            if (sender is Tv tv)
            {
                WindowMediaResultPage.Title = "WhereToWatch v2.0 - Result Page - " + tv.Name;
                SetPoster(tv);
                ConvertRuntime();
                TbRating.Text = (tv.VoteAverage * 10) + "%";
                TbRanking.Text = "#" + tv.VoteCount;
                TbTitle.Text = tv.Name;
                TbReleaseDate.Text = Convert.ToDateTime(tv.FirstAirDate).Year.ToString();
                TbOverviewContent.Text = tv.Overview;
                TbStudiosContent.Text = tv.Networks;
                TbGenresContent.Text = tv.Genres;
                _tempUrl = tv.Videos;
                TbTypeContent.Text = "TV";
                TbStatusContent.Text = tv.Status;
                if (tv.FirstAirDate != null) TbBroadcastContent.Text = "Every " + tv.FirstAirDate.Value.DayOfWeek;
                TbSeasonsContent.Text = tv.NumberOfSeasons.ToString();
                TbEpisodesContent.Text = tv.NumberOfEpisodes.ToString();
                if (string.IsNullOrEmpty(tv.Homepage))
                {
                    TbStatusContent.Visibility = Visibility.Hidden;
                }
                else
                {
                    TbStreamOnContent.Text = tv.Homepage;
                    TbStreamOnContent.TextAlignment = TextAlignment.Center;
                }
                TbTagsContent.Text = tv.Keywords;
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/05
        ///     Capitalizes the first letter.
        /// </summary>
        /// <param name="s">The passed string.</param>
        /// <returns>A string.</returns>
        public string CapitalizeFirstLetter(string s)
        {
            char[] a = s.ToLower().ToCharArray();

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];
            }

            return new string(a);
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/04
        ///     Sets the poster.
        /// </summary>
        /// <param name="item">The item.</param>
        private void SetPoster(object item)
        {
            string baseUrl = "http://image.tmdb.org/t/p/w500";
            if (item is Movie movie)
            {
                ImgPosterArt.Source = !string.IsNullOrEmpty(movie.PosterPath)
                    ? new BitmapImage(new Uri(baseUrl + movie.PosterPath, UriKind.Absolute))
                    : new BitmapImage(new Uri("~/Resources/Images/Image-not-found.png"));
            }

            if (item is Tv tv)
            {
                if (!string.IsNullOrEmpty(tv.PosterPath))
                {
                    ImgPosterArt.Source = new BitmapImage(new Uri(baseUrl + tv.PosterPath, UriKind.Absolute));
                }
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/04
        ///     Hides the tv entities.
        /// </summary>
        private void HideTvEntities()
        {
            TbSeasonsTitle.Visibility = Visibility.Hidden;
            TbSeasonsContent.Visibility = Visibility.Hidden;
            TbEpisodesTitle.Visibility = Visibility.Hidden;
            TbEpisodesContent.Visibility = Visibility.Hidden;
            TbBroadcastContent.Visibility = Visibility.Hidden;
            TbBroadcastTitle.Visibility = Visibility.Hidden;
            RdSeasons.Height = new GridLength(0);
            RdEpisodes.Height = new GridLength(0);
            RdBroadcast.Height = new GridLength(0);
        }

        /// <summary>
        /// Adds the piece of media to the user's favorites list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTrailer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_tempUrl))
            {
                try
                {
                    var process = Process.Start(_tempUrl);
                }
                catch (Win32Exception w)
                {
                    Console.WriteLine(w.Message);
                }
                catch (InvalidOperationException ie)
                {
                    MessageBox.Show(ie.Message, "No Trailer Error");
                }
            }
            else
            {
                MessageBox.Show("No Trailer Available!", "No Trailer Found");
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/04
        ///     Converts the runtime to a h:mm format.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <returns>An object.</returns>
        private string ConvertRuntime(int? runtime = 0)
        {
            string convertedRuntime = null;

            if (runtime != 0) // if our runtime is empty or null, we hide the runtime label.
            {
                int? hours = (runtime / 60); // set the hours of runtime.
                int? minutes = (runtime - (60 * hours)); // sets our minutes of runtime.
                if (hours < 1) // if hours is <1, then we only use minutes.
                {
                    convertedRuntime = minutes + "m";
                }
                else // else then we use both.
                {
                    convertedRuntime = hours + "h " + minutes + "m";
                }
            }
            else
            {
                HideRuntime();
            }

            return convertedRuntime;
        }

        /// <summary>
        /// Hides the runtime.
        /// </summary>
        private void HideRuntime()
        {
            TbRunTime.IsEnabled = false;
            TbRunTime.Visibility = Visibility.Hidden;
            RdLength.Height = new GridLength(0);
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Adds the piece of media to the user's favorites list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            if (_currentObject is Movie movie && _loggedInUser != null)
            {
                _favoriteMovieAccessor.InsertMovie(movie, _loggedInUser);
                MessageBox.Show("Media added to favorites!", "Item Favorites");
                BtnAddToFavorites.IsEnabled = false;
            }
            if (_currentObject is Tv tv && _loggedInUser != null)
            {
                _favoriteTvAccessor.InsertTvShow(tv, _loggedInUser);
                MessageBox.Show("Media added to favorites!", "Item Favorites");
                BtnAddToFavorites.IsEnabled = false;
            }
            else if(_loggedInUser == null)
            {
                MessageBox.Show("User must be logged in to add item to favorites.", "User Logged Out");
            }
        }
    }
}