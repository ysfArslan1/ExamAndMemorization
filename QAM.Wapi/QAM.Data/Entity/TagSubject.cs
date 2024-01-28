using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QAM.Base.Entity;

namespace QAM.Data.Entity
{
    public class TagSubject : BaseEntity
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public  virtual Tag? Tag { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
    }
    public class TagSubjectConfiguration : IEntityTypeConfiguration<TagSubject>
    {
        public void Configure(EntityTypeBuilder<TagSubject> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
            builder.HasKey(x => x.Id);
        }
    }
}
