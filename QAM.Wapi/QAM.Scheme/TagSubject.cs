
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Scheme
{
    // TagSubject sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateTagSubjectRequest : BaseRequest
    {
        public int TagId { get; set; }
        public int SubjectId { get; set; }
    }
    // TagSubject sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateTagSubjectRequest : BaseRequest
    {
        public int TagId { get; set; }
        public int SubjectId { get; set; }
    }

    // TagSubject sınıfı için response gönderilmekte kullanılır.
    public class TagSubjectResponse : BaseResponse
    {
        public int TagName { get; set; }
        public int SubjectName { get; set; }
    }

}
