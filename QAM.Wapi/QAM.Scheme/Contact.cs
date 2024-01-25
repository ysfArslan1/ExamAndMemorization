
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Scheme
{
    // Contact sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateContactRequest : BaseRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDefault { get; set; }
    }
    // Contact sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateContactRequest : BaseRequest
    {
        public decimal Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDefault { get; set; }
    }

    // Contact sınıfı için response gönderilmekte kullanılır.
    public class ContactResponse : BaseResponse
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDefault { get; set; }
    }

}
