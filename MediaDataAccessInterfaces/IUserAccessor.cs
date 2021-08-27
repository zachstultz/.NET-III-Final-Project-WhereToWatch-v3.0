using System.Collections.Generic;
using DomainModels;

namespace DataAccessInterfaces
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The user accessor interface.
    /// </summary>
    public interface IUserAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the users by active.
        /// </summary>
        /// <param name="active">If true, active.</param>
        /// <returns>A list of Users.</returns>
        List<User> SelectUsersByActive(bool active = true);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the roles by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>A list of string.</returns>
        List<string> SelectRolesByUserId(int userId);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the all roles.
        /// </summary>
        /// <returns>A list of string.</returns>
        List<string> SelectAllRoles();

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Updates the user profile.
        /// </summary>
        /// <param name="oldUser">The old user.</param>
        /// <param name="newUser">The new user.</param>
        /// <returns>An int.</returns>
        int UpdateUserProfile(User oldUser, User newUser);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Deactivates the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An int.</returns>
        int DeactivateUser(int userId);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Reactivates the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>An int.</returns>
        int ReactivateUser(int userId);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Deletes the user role.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="role">The role.</param>
        /// <returns>An int.</returns>
        int DeleteUserRole(int userId, string role);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Inserts the user role.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="role">The role.</param>
        /// <returns>An int.</returns>
        int InsertUserRole(int userId, string role);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Inserts the new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An int.</returns>
        int InsertNewUser(User user);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Verifies the user name and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>An int.</returns>
        int VerifyUserNameAndPassword(string email, string passwordHash);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>An User.</returns>
        User SelectUserByEmail(string email);

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Updates the password hash.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="newPasswordHash">The new password hash.</param>
        /// <param name="oldPasswordHash">The old password hash.</param>
        /// <returns>An int.</returns>
        int UpdatePasswordHash(string email, string newPasswordHash,
            string oldPasswordHash);
    }
}