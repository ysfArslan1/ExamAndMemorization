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
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
