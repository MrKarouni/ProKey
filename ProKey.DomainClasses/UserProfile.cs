using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses
{
    public class UserProfile
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int Id { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
