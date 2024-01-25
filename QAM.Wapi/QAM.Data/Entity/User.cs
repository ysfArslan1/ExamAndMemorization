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
    public class User:BaseEntity
    {
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int PasswordRetryCount { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public virtual ICollection<Contact>? Contacts { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.IdentityNumber).IsRequired(true).HasMaxLength(11);
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.DateOfBirth).IsRequired(true);
            builder.Property(x => x.PasswordRetryCount).HasDefaultValue(0);

            builder.HasKey(x => x.Id);
        }
    }
}
