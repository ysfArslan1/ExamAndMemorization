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
    public class Subject:BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? isPublic { get; set; }
        public DateTime LastActivityDate { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<TagSubject>? Tags { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
    }
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(400);
            builder.Property(x => x.isPublic).IsRequired(true).HasDefaultValue(false);
            builder.Property(x => x.LastActivityDate).IsRequired(true);

            builder.HasKey(x => x.Id);
        }
    }
}
