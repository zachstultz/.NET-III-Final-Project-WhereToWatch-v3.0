using System.Collections.Generic;
using DomainModels;

namespace LogicInterfaces
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The user manager interface.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Synchronizes the roles.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roles">The roles.</param>
        void SynchronizeRoles(int userId, List<string> roles);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves the users by active.
        /// </summary>
        /// <param name="active">If true, active.</param>
        /// <returns>A list of Users.</returns>
        List<User> RetrieveUsersByActive(bool active = true);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves the roles by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of string.</returns>
        List<string> RetrieveRolesByUserId(int userId);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Retrieves the all roles.
        /// </summary>
        /// <returns>A list of string.</returns>
        List<string> RetrieveAllRoles();

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Edits the user profile.
        /// </summary>
        /// <param name="oldUser">The old user.</param>
        /// <param name="newUser">The new user.</param>
        /// <param name="oldUnassignedRoles">The old unassigned roles.</param>
        /// <param name="newUnassignedRoles">The new unassigned roles.</param>
        /// <returns>A bool.</returns>
        bool EditUserProfile(User oldUser, User newUser, List<string> oldUnassignedRoles,
            List<string> newUnassignedRoles);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Adds the new user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns>A bool.</returns>
        bool AddNewUser(User newUser);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Authenticates the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>An User.</returns>
        User AuthenticateUser(string email, string password);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Updates the password.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A bool.</returns>
        bool UpdatePassword(User user, string oldPassword, string newPassword);
    }
}