using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataAccessLayer;
using DataAccessInterfaces;
using DomainModels;
using LogicInterfaces;
using MediaLogicLayer;

namespace DomainModels
{
    /// <summary>
    ///     <para>
    ///         Zach Stultz
    ///         Created: 2021/04/19
    ///     </para>
    ///     <para>The user manager.</para>
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IUserAccessor _userAccessor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserManager" /> class.
        /// </summary>
        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserManager" /> class.
        /// </summary>
        /// <param name="suppliedUserAccessor">The supplied user accessor.</param>
        public UserManager(IUserAccessor suppliedUserAccessor)
        {
            _userAccessor = suppliedUserAccessor;
        }

        /// <summary>
        ///     <para>
        ///         Zach Stultz
        ///         Created: 2021/04/19
        ///     </para>
        ///     <para>Authenticates the user.</para>
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>An User.</returns>
        public User AuthenticateUser(string email, string password)
        {
            // method returns a user object or null
            User user;

            // hash the password
            password = HashSha256(password);

            var isNewUser = password == "newuser";

            // this calls a method that throws exceptions
            try
            {
                // was the user found?
                if (1 == _userAccessor.VerifyUserNameAndPassword(email, password))
                    // if so, we need to get a user object
                    user = _userAccessor.SelectUserByEmail(email);
                else
                    throw new ApplicationException("Bad Username or Password");

                // return the user object
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login Failed.", ex);
            }

            return user;
        }

        /// <summary>
        ///     <para>
        ///         Zach Stultz
        ///         Created: 2021/04/19
        ///     </para>
        ///     <para>Updates the password.</para>
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A bool.</returns>
        public bool UpdatePassword(User user, string oldPassword, string newPassword)
        {
            oldPassword = oldPassword.Sha256Value();
            newPassword = newPassword.Sha256Value();

            bool result;
            try
            {
                result = 1 == _userAccessor.UpdatePasswordHash(
                             user.Email, newPassword, oldPassword);

                if (result == false) throw new ApplicationException("Update Failed.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bad username or password.", ex);
            }

            return result;
        }

        /// <summary>
        ///     <para>
        ///         Zach Stultz
        ///         Created: 2021/04/19
        ///     </para>
        ///     <para>Adds the new user.</para>
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns>A bool.</returns>
        public bool AddNewUser(User newUser)
        {
            bool result;
            try
            {
                var newUserId = _userAccessor.InsertNewUser(newUser);
                if (newUserId == 0) throw new ApplicationException("New user was not added.");

                // add newly assigned roles
                foreach (var role in newUser.Roles) _userAccessor.InsertUserRole(newUserId, role);

                result = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate key"))
                {
                    throw new ApplicationException("User already exists in Database", ex);
                }
                throw new ApplicationException("Add User Failed.", ex);
            }

            return result;
        }

        /// <summary>
        ///     <para>
        ///         Zach Stultz
        ///         Created: 2021/04/19
        ///     </para>
        ///     <para>Edits the user profile.</para>
        /// </summary>
        /// <param name="oldUser">The old user.</param>
        /// <param name="newUser">The new user.</param>
        /// <param name="oldUnassignedRoles">The old unassigned roles.</param>
        /// <param name="newUnassignedRoles">The new unassigned roles.</param>
        /// <returns>A bool.</returns>
        public bool EditUserProfile(User oldUser, User newUser, List<string> oldUnassignedRoles,
            List<string> newUnassignedRoles)
        {
            bool result;
            try
            {
                result = 1 == _userAccessor.UpdateUserProfile(oldUser, newUser);
                if (result == false) throw new ApplicationException("Profile data not changed.");

                // remove newly removed roles
                foreach (var role in newUnassignedRoles)
                    if (!oldUnassignedRoles.Contains(role))
                        _userAccessor.DeleteUserRole(oldUser.UserId, role);

                // add newly assigned roles
                foreach (var role in newUser.Roles)
                    if (!oldUser.Roles.Contains(role))
                        _userAccessor.InsertUserRole(oldUser.UserId, role);


                if (oldUser.Active != newUser.Active)
                {
                    if (newUser.Active)
                        _userAccessor.ReactivateUser(oldUser.UserId);
                    else
                        _userAccessor.DeactivateUser(oldUser.UserId);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update Failed.", ex);
            }

            return result;
        }

        /// <summary>
        ///     <para>
        ///         Zach Stultz
        ///         Created: 2021/04/19
        ///     </para>
        ///     <para>Retrieves the all roles.</para>
        /// </summary>
        /// <returns>A list of string.</returns>
        public List<string> RetrieveAllRoles()
        {
            List<string> roles;

            try
            {
                roles = _userAccessor.SelectAllRoles() ?? new List<string>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }

            return roles;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves the users by active.
        /// </summary>
        /// <param name="active">If true, active.</param>
        /// <returns>A list of Users.</returns>
        public List<User> RetrieveUsersByActive(bool active = true)
        {
            List<User> users;
            try
            {
                users = _userAccessor.SelectUsersByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User list not available.", ex);
            }

            return users;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves the roles by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of string.</returns>
        public List<string> RetrieveRolesByUserId(int userId)
        {
            List<string> roles;
            try
            {
                roles = _userAccessor.SelectRolesByUserId(userId) ?? new List<string>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }

            return roles;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Synchronizes the roles.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roles">The roles.</param>
        public void SynchronizeRoles(int userId, List<string> roles)
        {
            var existingRoles = _userAccessor.SelectRolesByUserId(userId);
            var rolesToRemove = existingRoles.Except(roles);

            // remove unassigned roles
            foreach (var role in rolesToRemove) _userAccessor.DeleteUserRole(userId, role);

            var rolesToAdd = roles.Except(existingRoles);

            // add missing roles
            foreach (var role in rolesToAdd) _userAccessor.InsertUserRole(userId, role);
        }

        /// <summary>
        ///     <para>
        ///         Zach Stultz
        ///         Created: 2021/04/19
        ///     </para>
        ///     <para>Hashes the passwords.</para>
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A string.</returns>
        private string HashSha256(string source)
        {
            // create a byte array - cryptography is byte oriented
            byte[] data;

            // create a .NET hash provider object
            using (var sha256Hash = SHA256.Create())
            {
                // hash the source
                data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now to build the result string
            var s = new StringBuilder();

            // loop through the byte array
            foreach (var t in data) s.Append(t.ToString("x2"));

            // convert the string builder to a string
            var result = s.ToString();

            return result;
        }
    }
}