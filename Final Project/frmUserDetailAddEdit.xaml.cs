using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DomainModels;
using LogicInterfaces;

namespace Final_Project
{
    /// <summary>
    ///     Interaction logic for frmUserDetailAddEdit.xaml
    /// </summary>
    public partial class FrmUserDetailAddEdit
    {
        private readonly bool _addUser;

        // these two fields hold changes from the original state
        private List<string> _assignedRoles;
        private readonly List<string> _originalUnassignedRoles = new List<string>();

        private List<string> _unassignedRoles;

        // these two fields preserve the User's original state
        private readonly User _user;
        private readonly IUserManager _userManager = new UserManager();

        public FrmUserDetailAddEdit()
        {
            _user = new User();
            _addUser = true;
            InitializeComponent();
        }

        public FrmUserDetailAddEdit(User user)
        {
            _user = user;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addUser)
            {
                // display blank user
                TxtUserId.Text = "Assigned Automatically";
                TxtUserId.IsEnabled = false;

                TxtFirstName.Text = "";
                TxtLastName.Text = "";
                TxtEmail.Text = "";
                TxtPhoneNumber.Text = "";
                ChkActive.IsChecked = true;

                try // set up the roles boxes
                {
                    // this sets the roles for the listbox items source (starts with none)
                    _assignedRoles = new List<string>();
                    // this holds the unassigned roles (starts with all roles)
                    _unassignedRoles = _userManager.RetrieveAllRoles();

                    LstAssignedRoles.ItemsSource = _assignedRoles;
                    LstUnassignedRoles.ItemsSource = _unassignedRoles;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }

                SetupEdit();
                ChkActive.IsEnabled = false;
            }
            else // display existing user
            {
                TxtUserId.Text = _user.UserId.ToString();
                TxtFirstName.Text = _user.FirstName;
                TxtLastName.Text = _user.LastName;
                TxtEmail.Text = _user.Email;
                TxtPhoneNumber.Text = _user.PhoneNumber;
                ChkActive.IsChecked = _user.Active;

                ResetRoles();
            }
        }

        private void ResetRoles()
        {
            try
            {
                // this sets the roles for the listbox items source
                _assignedRoles = _userManager.RetrieveRolesByUserId(_user.UserId);
                // this preserves the original roles for the User being edited
                _user.Roles = new List<string>();
                foreach (var r in _assignedRoles) _user.Roles.Add(r);

                _unassignedRoles = _userManager.RetrieveAllRoles();

                // this preserves the unassigned roles for convenience
                foreach (var role in _assignedRoles) _unassignedRoles.Remove(role);

                foreach (var r in _unassignedRoles) _originalUnassignedRoles.Add(r);

                LstAssignedRoles.ItemsSource = _assignedRoles;
                LstUnassignedRoles.ItemsSource = _unassignedRoles;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if ((string) BtnEditSave.Content == "Edit")
            {
                SetupEdit();
            }
            else // save
            {
                // are we saving an edited User, or a new one
                if (_addUser == false)
                {
                    if (!TxtFirstName.Text.IsValidFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        TxtFirstName.Focus();
                        TxtFirstName.SelectAll();
                        return;
                    }

                    if (!TxtLastName.Text.IsValidLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        TxtLastName.Focus();
                        TxtLastName.SelectAll();
                        return;
                    }

                    if (!TxtEmail.Text.IsValidEmail())
                    {
                        MessageBox.Show("Bad email address.");
                        TxtEmail.Focus();
                        TxtEmail.SelectAll();
                        return;
                    }

                    if (!TxtPhoneNumber.Text.IsValidPhoneNumber())
                    {
                        MessageBox.Show("Invalid Phone Number.");
                        TxtPhoneNumber.Focus();
                        TxtPhoneNumber.SelectAll();
                        return;
                    }

                    var newUser = new User
                    {
                        Email = TxtEmail.Text,
                        FirstName = TxtFirstName.Text,
                        LastName = TxtLastName.Text,
                        PhoneNumber = TxtPhoneNumber.Text,
                        Active = ChkActive.IsChecked != null && (bool) ChkActive.IsChecked
                    };
                    var roles = new List<string>();
                    foreach (var item in LstAssignedRoles.Items) roles.Add((string) item);

                    newUser.Roles = roles;

                    try
                    {
                        _userManager.EditUserProfile(_user, newUser,
                            _originalUnassignedRoles, _unassignedRoles);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        ResetRoles();
                        if (ex.InnerException != null && ex.InnerException.Message.Contains("deactivated"))
                            ChkActive.IsChecked = true;

                        if (ex.InnerException != null) MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
                else // _addUser == true, so this needs to invoke an add method
                {
                    if (!TxtFirstName.Text.IsValidFirstName())
                    {
                        MessageBox.Show("Invalid First Name.");
                        TxtFirstName.Focus();
                        TxtFirstName.SelectAll();
                        return;
                    }

                    if (!TxtLastName.Text.IsValidLastName())
                    {
                        MessageBox.Show("Invalid Last Name.");
                        TxtLastName.Focus();
                        TxtLastName.SelectAll();
                        return;
                    }

                    if (!TxtEmail.Text.IsValidEmail())
                    {
                        MessageBox.Show("Bad email address.");
                        TxtEmail.Focus();
                        TxtEmail.SelectAll();
                        return;
                    }

                    if (!TxtPhoneNumber.Text.IsValidPhoneNumber())
                    {
                        MessageBox.Show("Invalid Phone Number.");
                        TxtPhoneNumber.Focus();
                        TxtPhoneNumber.SelectAll();
                        return;
                    }

                    var newUser = new User
                    {
                        Email = TxtEmail.Text,
                        FirstName = TxtFirstName.Text,
                        LastName = TxtLastName.Text,
                        PhoneNumber = TxtPhoneNumber.Text,
                        Active = ChkActive.IsChecked != null && (bool) ChkActive.IsChecked
                    };
                    var roles = new List<string>();
                    foreach (var item in LstAssignedRoles.Items) roles.Add((string) item);

                    newUser.Roles = roles;

                    try
                    {
                        _userManager.AddNewUser(newUser);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                            MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }

        private void SetupEdit()
        {
            BtnEditSave.Content = "Save";
            TxtFirstName.IsReadOnly = false;
            TxtLastName.IsReadOnly = false;
            TxtEmail.IsReadOnly = false;
            TxtPhoneNumber.IsReadOnly = false;
            ChkActive.IsEnabled = true;
            LstAssignedRoles.IsEnabled = true;
            LstUnassignedRoles.IsEnabled = true;
            TxtFirstName.BorderBrush = Brushes.Black;
            TxtLastName.BorderBrush = Brushes.Black;
            TxtEmail.BorderBrush = Brushes.Black;
            TxtPhoneNumber.BorderBrush = Brushes.Black;
            TxtFirstName.Focus();
        }

        private void lstAssignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = LstAssignedRoles.SelectedItem;
            _assignedRoles.Remove((string) selectedRole);
            _unassignedRoles.Add((string) selectedRole);

            LstAssignedRoles.ItemsSource = null;
            LstUnassignedRoles.ItemsSource = null;

            LstAssignedRoles.ItemsSource = _assignedRoles;
            LstUnassignedRoles.ItemsSource = _unassignedRoles;
        }

        private void lstUnassignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRole = LstUnassignedRoles.SelectedItem;
            _unassignedRoles.Remove((string) selectedRole);
            _assignedRoles.Add((string) selectedRole);

            LstAssignedRoles.ItemsSource = null;
            LstUnassignedRoles.ItemsSource = null;

            LstAssignedRoles.ItemsSource = _assignedRoles;
            LstUnassignedRoles.ItemsSource = _unassignedRoles;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}