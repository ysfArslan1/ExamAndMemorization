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
    public class Question:BaseEntity
    {
        public int SubjectId {  get; set;}
        public virtual Subject? Subject { get; set;}
        public string? question {get; set;}
        public string? Explanation { get; set;}
        public int Status { get; set;}
    }
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.question).IsRequired(true).HasMaxLength(500);
            builder.Property(x => x.Explanation).IsRequired(true).HasMaxLength(600);
            builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(0);

            builder.HasKey(x => x.Id);
        }
    }
}
