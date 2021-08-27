using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using MediaLogicLayer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;

namespace Final_Project
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private User _user;
        private readonly IUserManager _userManager = new UserManager();
        public ImageSource PosterPath { get; set; }
        private MovieAccessor _movieAccessor = new MovieAccessor();
        private TvAccessor _tvAccessor = new TvAccessor();

        private TrendingMovieAccessor _trendingMovieAccessor = new TrendingMovieAccessor();
        private TrendingTvAccessor _trendingTvAccessor = new TrendingTvAccessor();
        private FavoriteTvAccessor _favoriteTvAccessor = new FavoriteTvAccessor();
        private FavoriteMovieAccessor _favoriteMovieAccessor = new FavoriteMovieAccessor();

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            TxtSearchBox.Focus();
            var test = new MediaApi();
            CheckTrending(test.GetTrendingLists());
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Sends the user's query off to the MediaSearch method in the MediaAPI class
        ///     to scrape search results for the user. It then adds that to the ListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ResetDataGrid();
            var test = new MediaApi();
            var t = test.MediaSearch(TxtSearchBox.Text);
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i] is Movie movie)
                {
                    if (_movieAccessor.CheckForMovieById(movie.Id) && (movie == _movieAccessor.SelectMovieById(movie.Id)))
                    {
                        DgMediaResults.Items.Add(_movieAccessor.SelectMovieById(movie.Id));
                    }
                    else if (_movieAccessor.CheckForMovieById(movie.Id))
                    {
                        _movieAccessor.UpdateMovie(_movieAccessor.SelectMovieById(movie.Id), movie);
                        DgMediaResults.Items.Add(_movieAccessor.SelectMovieById(movie.Id));
                    }
                    else
                    {
                        _movieAccessor.InsertMovie(movie);
                        DgMediaResults.Items.Add(_movieAccessor.SelectMovieById(movie.Id));
                    }
                }
                else if (t[i] is Tv tv)
                {
                    if (_tvAccessor.CheckForTvShowById(tv.Id) && (tv == _tvAccessor.SelectTvShowById(tv.Id)))
                    {
                        DgMediaResults.Items.Add(_tvAccessor.SelectTvShowById(tv.Id));
                    }
                    else if (_tvAccessor.CheckForTvShowById(tv.Id))
                    {
                        _tvAccessor.UpdateTvShow(_tvAccessor.SelectTvShowById(tv.Id), tv);
                        DgMediaResults.Items.Add(_tvAccessor.SelectTvShowById(tv.Id));
                    }
                    else
                    {
                        _tvAccessor.InsertTvShow(tv);
                        DgMediaResults.Items.Add(_tvAccessor.SelectTvShowById(tv.Id));
                    }
                }
            }
            LblResults.Content = "Loaded " + DgMediaResults.Items.Count + " Results";
        }

        /// <summary>
        /// Checks the trending movies and tv shows. Updates items in the DB if they've changed.
        /// </summary>
        /// <param name="trendingList">The trending list.</param>
        private void CheckTrending(List<object> trendingList)
        {
            for (int i = 0; i < trendingList.Count; i++)
            {
                if (trendingList[i] is Movie movie)
                {
                    if (_trendingMovieAccessor.CheckForMovieById(movie.Id) &&
                        (movie == _trendingMovieAccessor.SelectMovieById(movie.Id)))
                    {
                        //DgMediaResults.Items.Add(_trendingMovieAccessor.SelectAllMoviesByUserId(movie.Id));
                    }
                    else if (_trendingMovieAccessor.CheckForMovieById(movie.Id))
                    {
                        _trendingMovieAccessor.UpdateMovie(_trendingMovieAccessor.SelectMovieById(movie.Id), movie);
                        //DgMediaResults.Items.Add(_trendingMovieAccessor.SelectAllMoviesByUserId(movie.Id));
                    }
                    else
                    {
                        _trendingMovieAccessor.InsertMovie(movie);
                        //DgMediaResults.Items.Add(_trendingMovieAccessor.SelectAllMoviesByUserId(movie.Id));
                    }
                }
                else if (trendingList[i] is Tv tv)
                {
                    if (_trendingTvAccessor.CheckForTvShowById(tv.Id) && (tv == _trendingTvAccessor.SelectTvShowById(tv.Id)))
                    {
                        //DgMediaResults.Items.Add(_trendingTvAccessor.SelectAllTvShowsByUserId(tv.Id));
                    }
                    else if (_trendingTvAccessor.CheckForTvShowById(tv.Id))
                    {
                        _trendingTvAccessor.UpdateTvShow(_trendingTvAccessor.SelectTvShowById(tv.Id), tv);
                        //DgMediaResults.Items.Add(_trendingTvAccessor.SelectAllTvShowsByUserId(tv.Id));
                    }
                    else
                    {
                        _trendingTvAccessor.InsertTvShow(tv);
                        //DgMediaResults.Items.Add(_trendingTvAccessor.SelectAllTvShowsByUserId(tv.Id));
                    }
                }
            }
        }

        private void ResetDataGrid()
        {
            DgMediaResults.ScrollIntoView(0);
            DgMediaResults.HeadersVisibility = DataGridHeadersVisibility.Column;
            DgMediaResults.Items.Clear();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Calls the Search Function when the user Returns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TxtSearchBox.Text.Length != 0
            ) // if the key they press is enter, then it activates the Search, so they don't
            {
                // have to drag the mouse and click it.
                LblResults.Visibility = Visibility.Visible;
                BtnSearch_Click(this, new EventArgs());
            }
        }

        private void PwdPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && (!string.IsNullOrEmpty(TxtUsername.Text) && (!string.IsNullOrEmpty(PwdPassword.Password))))
            {
                BtnLogin_Click(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     What executes when an item within the DataGrid is double clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgMediaResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mediaResultsForm = new MediaMovieResultPage(DgMediaResults.SelectedItem, _user);
            mediaResultsForm.Show();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Opens the About Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            var media = new MediaAboutPage();
            media.Show();
        }


        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Logs in the user.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // logic to check whether this is a login or logout
            if ((string) BtnLogin.Content == "Login")
            {
                try
                {
                    _user = _userManager.AuthenticateUser(TxtUsername.Text, PwdPassword.Password);
                    MessageBox.Show(_user.FirstName + " logged in successfully.", "Successful Login");

                    BtnLogin.Content = "Logout";
                    BtnRegister.Visibility = Visibility.Hidden;
                    TxtUsername.Text = "";

                    if (PwdPassword.Password == "newuser")
                    {
                        var updatePassword = new FrmUpdatePassword(_user, _userManager, true);

                        // if the person doesn't change the password, log them out
                        if (!updatePassword.ShowDialog() == true)
                        {
                            ResetWindow();
                            _user = null;
                            return;
                        }
                    }

                    BtnLogin.IsDefault = false;
                    PwdPassword.Password = "";
                    TxtUsername.Visibility = Visibility.Hidden;
                    LblUsername.Visibility = Visibility.Hidden;
                    PwdPassword.Visibility = Visibility.Hidden;
                    LblPassword.Visibility = Visibility.Hidden;
                    SbItemMessage.Content = "";


                    MnuMain.IsEnabled = true;

                    if (_user.Roles.Count > 0)
                    {
                        var roleString = _user.Roles[0];
                        for (var i = 1; i < _user.Roles.Count; i++) roleString += ", " + _user.Roles[i];

                        //lblRoles.Content = "You are logged in as: " + roleString;
                    }
                }
                catch (Exception ex)
                {
                    PwdPassword.Clear();
                    TxtUsername.Clear();
                    if (ex.InnerException != null)
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    TxtUsername.Focus();
                }
            }
            else // button says Logout
            {
                _user = null;
                BtnRegister.Visibility = Visibility.Visible;
                ResetWindow();
                BtnLogin.IsDefault = true;
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Resets the window.
        /// </summary>
        private void ResetWindow()
        {
            BtnLogin.IsDefault = true;
            MnuMain.IsEnabled = false;
            TxtUsername.Text = "";
            PwdPassword.Password = "";
            BtnLogin.Content = "Login";
            TxtUsername.Visibility = Visibility.Visible;
            LblUsername.Visibility = Visibility.Visible;
            PwdPassword.Visibility = Visibility.Visible;
            LblPassword.Visibility = Visibility.Visible;
            SbItemMessage.Content = "Please login to continue.";

            // new closeout code
            //dgUserList.ItemsSource = null;

            TxtUsername.Focus();
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///      Populates the box with the user's favorites.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void BtnShowFavorites_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                ClearCurrentlyLoadedResults();
                List<Movie> usersFavoriteMovies = _favoriteMovieAccessor.SelectAllMoviesByUserId(_user);
                List<Tv> usersFavoriteTvShows = _favoriteTvAccessor.SelectAllTvShowsByUserId(_user);
                if (usersFavoriteMovies.Count + usersFavoriteTvShows.Count == 0)
                {
                    MessageBox.Show("No favorites found.");
                }
                else
                {
                    LblResults.Visibility = Visibility.Visible;
                    LblResults.Content = "Loading " + (usersFavoriteMovies.Count + usersFavoriteTvShows.Count) + " Results";
                    foreach (var t in usersFavoriteMovies)
                    {
                        DgMediaResults.Items.Add(t);
                    }

                    foreach (var t in usersFavoriteTvShows)
                    {
                        DgMediaResults.Items.Add(t);
                    }
                }
            }
            if(_user == null)
            {
                MessageBox.Show("User must be logged in to load favorites list.");
            }

            LblResults.Visibility = Visibility.Visible;
            LblResults.Content = "Loaded " + DgMediaResults.Items.Count + " Results";
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/05/09
        ///     Clears the currently loaded results.
        /// </summary>
        private void ClearCurrentlyLoadedResults()
        {
            DgMediaResults.Items.Clear();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerUser = new FrmRegisterUser(_userManager);
            registerUser.Show();
        }
    }
}