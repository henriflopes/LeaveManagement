using LeaveManagement.Web.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configurations.Entities
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-ladd431ccbbf",
                Name = Roles.Administrator,
                NormalizedName = Roles.Administrator.ToUpper()
            });

            builder.HasData(new IdentityRole
            {
                Id = "cac43a7e-f7cb-4448-baaf-labb431eabbf",
                Name = Roles.User,
                NormalizedName = Roles.User.ToUpper()
			});
        }
    }
}