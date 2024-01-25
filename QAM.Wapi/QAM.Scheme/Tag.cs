
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Scheme
{
    // Tag sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateTagRequest : BaseRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    // Tag sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateTagRequest : BaseRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    // Tag sınıfı için response gönderilmekte kullanılır.
    public class TagResponse : BaseResponse
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

}
