
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Scheme
{
    // Question sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateQuestionRequest : BaseRequest
    {
        public int SubjectId { get; set; }
        public string? question { get; set; }
        public string? Explanation { get; set; }
        public int Status { get; set; }
    }
    // Question sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateQuestionRequest : BaseRequest
    {
        public int SubjectId { get; set; }
        public string? question { get; set; }
        public string? Explanation { get; set; }
        public int Status { get; set; }
    }

    // Question sınıfı için response gönderilmekte kullanılır.
    public class QuestionResponse : BaseResponse
    {
        public string? SubjectName { get; set; }
        public string? question { get; set; }
        public string? Explanation { get; set; }
        public int Status { get; set; }
    }

}
