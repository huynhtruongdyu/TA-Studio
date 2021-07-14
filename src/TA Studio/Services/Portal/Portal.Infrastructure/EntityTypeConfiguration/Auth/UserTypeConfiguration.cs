using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.AggregatesModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.EntityTypeConfiguration.Auth
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Username).IsRequired(true);
            builder.HasIndex(user => user.Username).IsUnique();
            builder.Property(user => user.Password).IsRequired(true);
        }
    }
}
