using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using DomainModels;
using LogicInterfaces;

namespace Final_Project
{
    /// <summary>
    ///     Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class FrmRegisterUser
    {
        private readonly IUserManager _userManager;

        public FrmRegisterUser(IUserManager userManager)
        {
            _userManager = userManager;

            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!TxtEmail.Text.IsValidEmail())
            {
                MessageBox.Show("Invalid Email Address.");
                TxtEmail.Clear();
                TxtEmail.Focus();
                return;
            }

            if (!TxtEmail.Text.IsValidFirstName())
            {
                MessageBox.Show("Invalid First Name.");
                TxtEmail.Clear();
                TxtEmail.Focus();
                return;
            }

            if (!TxtPhoneNumber.Text.IsValidPhoneNumber())
            {
                MessageBox.Show("Invalid Phone Number.");
                TxtPhoneNumber.Clear();
                TxtPhoneNumber.Focus();
            }

            try
            {
                List<string> roles = new List<string> {"User"};
                User user = new User(1,TxtFirstName.Text, TxtLastName.Text, TxtPhoneNumber.Text, TxtEmail.Text, roles);
                // invoke a user manager function to change password
                if (_userManager.AddNewUser(user))
                {
                    // if the password was changed successfully, close this
                    // dialog and return true
                    MessageBox.Show("User Created.", "User Creation",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true; // closes window and return true
                    Close();
                }
                else
                {
                    // if the update fails, give an error message
                    throw new ApplicationException("Password not changed.");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}