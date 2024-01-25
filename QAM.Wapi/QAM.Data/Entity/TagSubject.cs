﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Entity
{
    public class TagSubject
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
            builder.HasKey(x => x.Id);
        }
    }
}
