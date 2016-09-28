namespace DiReCTUI.Controls
{
    /// <summary>
    /// A class for login security challenge
    /// </summary>
    class SigninChallenge
    {
        /// <summary>
        /// Stores user name
        /// </summary>
        private string Name;

        /// <summary>
        /// Stores password
        /// </summary>
        private string Password;

        /// <summary>
        /// Constructor method
        /// Obtains data for the challenge
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="password">Password</param>
        public SigninChallenge(string name,string password)
        {
            Name=name;
            Password=password;
        }

        /// <summary>
        /// Security challenge
        /// WIP: for demo only, always returns true, should become a real method on a real tablet
        /// </summary>
        /// <returns>
        /// True if passed
        /// False if failed
        /// </returns>
        public bool Verify()
        {
            return true;
        }
    }
}
