using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public int UserTypeIndex { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `TokenSerialNumber` GUID and store it in the DB.
        /// </summary>
        public string TokenSerialNumber { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<Production> Productions { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
