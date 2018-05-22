using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProKey.DomainClasses.EntityConfiguration
{
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfig()
        {
            this.HasRequired(x => x.User)
                .WithMany(x => x.Roles)
                .WillCascadeOnDelete();
        }
    }
}
