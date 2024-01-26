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
    // CreateContactRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100).Must(ValidateEmail);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(x => x.isDefault).NotNull().NotEmpty();
        }
        // Email doğrulaması için kullanılan metot
        private bool ValidateEmail(string text)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(text);
        }
    }
    // UpdateContactRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100).Must(ValidateEmail);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(x => x.isDefault).NotNull().NotEmpty();
        }
        // Email doğrulaması için kullanılan metot
        private bool ValidateEmail(string text)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(text);
        }
    }

}
