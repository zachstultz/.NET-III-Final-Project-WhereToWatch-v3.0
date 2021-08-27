using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessInterfaces;
using DomainModels;

namespace DataAccessLayer
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The User accessor.
    /// </summary>
    public class UserAccessor : IUserAccessor
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>An User.</returns>
        public User SelectUserByEmail(string email)
        {
            User user;

            // get a connection
            var conn = DbConnection.GetDbConnection();

            // create a command
            var cmd = new SqlCommand("sp_select_user_by_email", conn) {CommandType = CommandType.StoredProcedure};

            // create parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    var userId = reader.GetInt32(0);
                    var firstName = reader.GetString(2);
                    var lastName = reader.GetString(3);
                    var phoneNumber = reader.GetString(4);
                    var active = reader.GetBoolean(5);
                    reader.Close();

                    // get the user roles
                    var userAccessor = new UserAccessor();
                    var roles = userAccessor.SelectRolesByUserId(userId);

                    // add to a user object
                    user = new User(userId, firstName, lastName, phoneNumber, email, roles);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>An User.</returns>
        public bool CheckForExistanceOfUserByEmail(string email)
        {
            bool result;

            // get a connection
            var conn = DbConnection.GetDbConnection();

            // create a command
            var cmd = new SqlCommand("sp_select_user_by_email", conn) { CommandType = CommandType.StoredProcedure };

            // create parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // set parameter values
            cmd.Parameters["@Email"].Value = email;

            List<User> users = new List<User>();

            // execute the command
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        User foundUser = new User()
                        {
                            UserId = reader.GetInt32(0),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Active = reader.GetBoolean(5)
                    };
                        users.Add(foundUser);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (users.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Verifies the user name and password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>An int.</returns>
        public int VerifyUserNameAndPassword(string email, string passwordHash)
        {
            int result; // verification will falsify this

            // we need to start with a connection
            var conn = DbConnection.GetDbConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_authenticate_user", conn) {CommandType = CommandType.StoredProcedure};

            // add any needed parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash.ToUpper();

            // now that the operation is set up, we need to execute it
            // this is unsafe code, so it always goes in a try block
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Updates the password hash.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="newPasswordHash">The new password hash.</param>
        /// <param name="oldPasswordHash">The old password hash.</param>
        /// <returns>An int.</returns>
        public int UpdatePasswordHash(string email, string newPasswordHash, string oldPasswordHash)
        {
            int result;

            // connection
            var conn = DbConnection.GetDbConnection();
            // command
            var cmd = new SqlCommand("sp_update_passwordhash", conn) {CommandType = CommandType.StoredProcedure};
            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            // values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            // execute
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Deactivates the user.
        /// </summary>
        /// <param name="userId">The user i d.</param>
        /// <returns>An int.</returns>
        public int DeactivateUser(int userId)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_safely_deactivate_user", conn) {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@UserID", userId);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1) throw new ApplicationException("User could not be deactivated.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Deletes the user role.
        /// </summary>
        /// <param name="userId">The user i d.</param>
        /// <param name="role">The role.</param>
        /// <returns>An int.</returns>
        public int DeleteUserRole(int userId, string role)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_safely_remove_userrole", conn) {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@RoleID"].Value = role;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1) throw new ApplicationException(role + " role could not be removed.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Inserts the user role.
        /// </summary>
        /// <param name="userId">The user i d.</param>
        /// <param name="role">The role.</param>
        /// <returns>An int.</returns>
        public int InsertUserRole(int userId, string role)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_add_userrole", conn) {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@RoleID"].Value = role;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1) throw new ApplicationException(role + " role could not be added.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Inserts the new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>An int.</returns>
        public int InsertNewUser(User user)
        {
            int userId;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_new_user", conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@Email"].Value = user.Email;
            cmd.Parameters["@FirstName"].Value = user.FirstName;
            cmd.Parameters["@LastName"].Value = user.LastName;
            cmd.Parameters["@PhoneNumber"].Value = user.PhoneNumber;

            try
            {
                conn.Open();
                userId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return userId;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Reactivates the user.
        /// </summary>
        /// <param name="userId">The user i d.</param>
        /// <returns>An int.</returns>
        public int ReactivateUser(int userId)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_reactivate_user", conn) {CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@UserID", userId);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects all the roles.
        /// </summary>
        /// <returns>A list of string.</returns>
        public List<string> SelectAllRoles()
        {
            var roles = new List<string>();

            // get a connection
            var conn = DbConnection.GetDbConnection();

            // get a command
            var cmd = new SqlCommand("sp_select_all_roles", conn) {CommandType = CommandType.StoredProcedure};

            // command type

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                        roles.Add(reader.GetString(0));

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects users by active.
        /// </summary>
        /// <param name="active">If true, active.</param>
        /// <returns>A list of Users.</returns>
        public List<User> SelectUsersByActive(bool active = true)
        {
            var users = new List<User>();

            // connection
            var conn = DbConnection.GetDbConnection();
            // command
            var cmd = new SqlCommand("sp_select_users_by_active", conn) {CommandType = CommandType.StoredProcedure};
            // command type
            // parameters with value the shortcut way
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UserId = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            Roles = null // lazy instantiation - wait until needed
                        };
                        users.Add(user);
                    }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return users;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Selects the roles by user id.
        /// </summary>
        /// <param name="userId">The user i d.</param>
        /// <returns>A list of string.</returns>
        public List<string> SelectRolesByUserId(int userId)
        {
            var roles = new List<string>();

            // get a connection
            var conn = DbConnection.GetDbConnection();

            // get a command
            var cmd = new SqlCommand("sp_select_roles_by_userID", conn) {CommandType = CommandType.StoredProcedure};

            // command type

            // parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // values
            cmd.Parameters["@UserID"].Value = userId;

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                        roles.Add(reader.GetString(0));

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Updates the user profile.
        /// </summary>
        /// <param name="oldUser">The old user.</param>
        /// <param name="newUser">The new user.</param>
        /// <returns>An int.</returns>
        public int UpdateUserProfile(User oldUser, User newUser)
        {
            int result;

            var conn = DbConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_user_profile_data", conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.NVarChar, 15);

            cmd.Parameters["@UserID"].Value = oldUser.UserId;
            cmd.Parameters["@NewEmail"].Value = newUser.Email;
            cmd.Parameters["@NewFirstName"].Value = newUser.FirstName;
            cmd.Parameters["@NewLastName"].Value = newUser.LastName;
            cmd.Parameters["@NewPhoneNumber"].Value = newUser.PhoneNumber;
            cmd.Parameters["@OldEmail"].Value = oldUser.Email;
            cmd.Parameters["@OldFirstName"].Value = oldUser.FirstName;
            cmd.Parameters["@OldLastName"].Value = oldUser.LastName;
            cmd.Parameters["@OldPhoneNumber"].Value = oldUser.PhoneNumber;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}