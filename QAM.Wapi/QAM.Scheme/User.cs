
using QAM.Base.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAM.Data.Scheme
{
    // User sınıfı için gelen create requestleri almakta kullanılır.
    public class CreateUserRequest : BaseRequest
    {
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
    // User sınıfı için gelen update requestleri almakta kullanılır.
    public class UpdateUserRequest : BaseRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int RoleId { get; set; }
    }

    // User sınıfı için response gönderilmekte kullanılır.
    public class UserResponse : BaseResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int PasswordRetryCount { get; set; }
        public string RoleName { get; set; }
    }

}
