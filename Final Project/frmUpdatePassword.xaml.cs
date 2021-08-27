using System;
using System.Windows;
using DomainModels;
using LogicInterfaces;

namespace Final_Project
{
    /// <summary>
    ///     Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class FrmUpdatePassword
    {
        private readonly bool _isNewUser;
        private readonly User _user;
        private readonly IUserManager _userManager;

        public FrmUpdatePassword(User user, IUserManager userManager,
            bool isNewUser = false)
        {
            _user = user;
            _userManager = userManager;
            _isNewUser = isNewUser;

            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!TxtEmail.Text.IsValidEmail()
                || TxtEmail.Text != _user.Email)
            {
                MessageBox.Show("Invalid Email Address.");
                TxtEmail.Clear();
                TxtEmail.Focus();
                return;
            }

            if (!PwdOldPassword.Password.IsValidPassword())
            {
                MessageBox.Show("Invalid Password.");
                PwdOldPassword.Clear();
                PwdOldPassword.Focus();
                return;
            }

            // error check for missing input
            if (!PwdNewPassword.Password.IsValidPassword()
                || PwdNewPassword.Password == "newuser")
            {
                MessageBox.Show("Invalid Password.");
                PwdNewPassword.Clear();
                PwdNewPassword.Focus();
                return;
            }

            // error check for new password and retype password match
            if (PwdNewPassword.Password != PwdRetypePassword.Password)
            {
                MessageBox.Show("Passwords must match.");
                PwdRetypePassword.Clear();
                PwdRetypePassword.Focus();
                return;
            }

            try
            {
                // invoke a user manager function to change password
                if (_userManager.UpdatePassword(_user, PwdOldPassword.Password,
                    PwdNewPassword.Password))
                {
                    // if the password was changed successfully, close this
                    // dialog and return true
                    MessageBox.Show("Password Changed.", "Profile Updated",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true; // closes window and return true
                }
                else
                {
                    // if the update fails, give an error message
                    throw new ApplicationException("Password not changed.");
                }
            }
            catch (Exception ex)
            {
                PwdNewPassword.Clear();
                PwdRetypePassword.Clear();
                if (ex.InnerException != null) MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                PwdNewPassword.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isNewUser)
            {
                TbkMessage.Text = "On First Login " + TbkMessage.Text;
                TxtEmail.Text = _user.Email;
                TxtEmail.IsEnabled = false;
                PwdOldPassword.Password = "newuser";
                PwdOldPassword.IsEnabled = false;
                PwdNewPassword.Focus();
            }
        }
    }
}