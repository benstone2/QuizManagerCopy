using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizManager.Core.Enitites;
using QuizManager.Shared.DevelopmentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Infrastructure.Data.Configuration
{
    public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
    {
        public void Configure(EntityTypeBuilder<Organisation> builder)
        {
            builder.HasData(
                new Organisation
                {
                    Id = DevData.OrganisationId,
                    Name = DevData.OrganisationName
                });
        }
    }
}

