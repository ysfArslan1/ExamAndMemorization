using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Entity
{
    public class Question
    {
        public int SubjectId {  get; set;}
        public virtual Subject Subject { get; set;}
        public string question{get; set;}
        public string Explanation { get; set;}
        public int Status { get; set;}
    }
}
