using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QAM.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Entity
{
    public class Contact:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool isDefault { get; set; }
    }
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.PhoneNumber).IsRequired(true).HasMaxLength(15);
            builder.Property(x => x.isDefault).IsRequired(true).HasDefaultValue(false);

            builder.HasKey(x => x.Id);
        }
    }
}
