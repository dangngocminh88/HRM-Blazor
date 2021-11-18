using DB_CSharp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_CSharp.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Set_Project");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProjectName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.ChangeDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ChangeCount).IsRequired().HasDefaultValue(1);
            //builder.Property(x => x.ChangeBy).IsRequired();
        }
    }
}
