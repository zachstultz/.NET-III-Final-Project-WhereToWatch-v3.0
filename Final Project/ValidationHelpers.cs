namespace Final_Project
{
    /// <summary>
    ///     The validation helpers.
    /// </summary>
    public static class ValidationHelpers
    {
        public static bool IsValidEmail(this string email)
        {
            var result = email.Contains("@") && email.Length >= 7 &&
                         email.Length <= 100;

            return result;
        }

        public static bool IsValidPassword(this string password)
        {
            var result = password.Length >= 7;
            // we need to add complexity checks

            return result;
        }

        public static bool IsValidFirstName(this string firstName)
        {
            var result = firstName.Length >= 1 && firstName.Length <= 50;

            return result;
        }

        public static bool IsValidLastName(this string lastName)
        {
            var result = lastName.Length >= 1 && lastName.Length <= 50;

            return result;
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            var result = phoneNumber.Length >= 7 && phoneNumber.Length <= 15;

            return result;
        }
    }
}