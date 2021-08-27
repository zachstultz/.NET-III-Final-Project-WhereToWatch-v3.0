using System.Collections.Generic;

namespace DomainModels
{
    /// <summary>
    ///     Zach Stultz
    ///     Created: 2021/04/19
    ///     The User Data Object.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Default Constructor
        ///     Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        ///     Zach Stultz
        ///     Created: 2021/04/19
        ///     Full Constructor
        ///     Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="userId">The user i d.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="email">The email.</param>
        /// <param name="roles">The roles.</param>
        public User(int userId, string firstName, string lastName, string phoneNumber, string email, List<string> roles)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Roles = roles;
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public List<string> Roles { get; set; }
    }
}