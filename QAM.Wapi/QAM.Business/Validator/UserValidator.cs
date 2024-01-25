using FluentValidation;
using QAM.Scheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QAM.Business.Validator
{
    // CreateUserRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().Length(11);
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100).Must(ValidateEmail);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0);
        }

        // Email doğrulaması için kullanılan metot
        private bool ValidateEmail(string text)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(text);
        }
    }
    // UpdateUserRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100).Must(ValidateEmail);
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0);
        }

        // Email doğrulaması için kullanılan metot
        private bool ValidateEmail(string text)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(text);
        }
    }

}
