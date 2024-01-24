using QAM.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Entity
{
    internal class Contact:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDefault { get; set; }
    }
}
