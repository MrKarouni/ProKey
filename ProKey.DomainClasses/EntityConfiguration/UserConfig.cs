using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses.EntityConfiguration
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            //one-to-one
            this.HasOptional(x => x.UserProfile)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete();
        }
    }
}
