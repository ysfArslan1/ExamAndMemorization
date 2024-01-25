
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Scheme
{
    // Role sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateRoleRequest : BaseRequest
    {
        public string? Name { get; set; }
    }
    // Role sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateRoleRequest : BaseRequest
    {
        public string? Name { get; set; }
    }

    // Role sınıfı için response gönderilmekte kullanılır.
    public class RoleResponse : BaseResponse
    {
        public string? Name { get; set; }
    }

}
