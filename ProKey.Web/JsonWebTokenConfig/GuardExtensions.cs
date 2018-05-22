using System;

namespace ProKey.Web.JsonWebTokenConfig
{
    public static class GuardExtensions
    {
        /// <summary>
        /// Checks if the argument is null.
        /// </summary>
        public static void CheckArgumentNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }
    }
}