
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Scheme
{
    // Subject sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateSubjectRequest : BaseRequest
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? isPublic { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
    // Subject sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateSubjectRequest : BaseRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? isPublic { get; set; }
        public DateTime LastActivityDate { get; set; }
    }

    // Subject sınıfı için response gönderilmekte kullanılır.
    public class SubjectResponse : BaseResponse
    {

        public string UserName { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? isPublic { get; set; }
        public DateTime LastActivityDate { get; set; }
    }

}
