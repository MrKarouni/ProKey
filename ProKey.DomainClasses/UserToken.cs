using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses
{
    public class UserToken
    {
        public int Id { get; set; }

        public int OwnerUserId { get; set; }

        [ForeignKey("OwnerUserId")]
        public virtual User User { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTime AccessTokenExpirationDateTime { get; set; }

        public string RefreshTokenIdHash { get; set; }

        public string Subject { get; set; }

        public DateTime RefreshTokenExpiresUtc { get; set; }

        public string RefreshToken { get; set; }
    }
}
