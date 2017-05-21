using Domain.Model;
using System.Data.Entity.ModelConfiguration;

namespace Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(p => p.FirstName).HasMaxLength(30);
            Property(p => p.LastName).HasMaxLength(30);
            Property(p => p.UserName).HasMaxLength(30);
            Property(p => p.PhoneNumber).HasMaxLength(50);

            Property(p => p.FirstName).IsRequired();
            Property(p => p.LastName).IsRequired();
            Property(p => p.UserName).IsRequired();
            Property(p => p.PhoneNumber).IsOptional();
            Property(p => p.Email).IsRequired();
        }
    }
}
