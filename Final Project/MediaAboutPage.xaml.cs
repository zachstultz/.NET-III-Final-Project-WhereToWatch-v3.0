using System.Diagnostics;
using System.Windows;

namespace Final_Project
{
    /// <summary>
    ///     Interaction logic for MediaAboutPage.xaml
    /// </summary>
    public partial class MediaAboutPage
    {
        public MediaAboutPage()
        {
            InitializeComponent();
        }

        private void hlGithub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/zachstultz");
        }

        private void hlEmail_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("zach-stultz@student.kirkwood.edu");
            MessageBox.Show("Email copied to Clipboard.", "Email Copied");
        }
    }
}