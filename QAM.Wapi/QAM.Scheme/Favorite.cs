
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Scheme
{
    // Favorite sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateFavoriteRequest : BaseRequest
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
    }
    // Favorite sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateFavoriteRequest : BaseRequest
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
    }

    // Favorite sınıfı için response gönderilmekte kullanılır.
    public class FavoriteResponse : BaseResponse
    {

        public string UserName { get; set; }
        public string SubjectName { get; set; }
    }

}
