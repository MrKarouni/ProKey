using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses.EntityConfiguration
{
    public class UserTokenConfig : EntityTypeConfiguration<UserToken>
    {
        public UserTokenConfig()
        {
            this.HasRequired(x => x.User)
                .WithMany(x => x.UserTokens)
                .WillCascadeOnDelete();
        }
    }
}
