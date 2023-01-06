using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = UserRoles.Editor,
                    NormalizedName = UserRoles.Editor.ToUpper()
                },
                new IdentityRole
                {
                    Name = UserRoles.Viewer,
                    NormalizedName = UserRoles.Viewer.ToUpper()
                },
                new IdentityRole
                {
                    Name = UserRoles.Restricted,
                    NormalizedName = UserRoles.Restricted.ToUpper()
                }
            ); 
        }
    }
}
