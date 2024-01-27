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
    // CreateSubjectRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateSubjectRequestValidator : AbstractValidator<CreateSubjectRequest>
    {
        public CreateSubjectRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(400);
            RuleFor(x => x.isPublic).NotNull().NotEmpty();
            RuleFor(x => x.LastActivityDate).NotNull().NotEmpty().LessThan(DateTime.Now.AddMinutes(1));
        }
    }
    // UpdateSubjectRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateSubjectRequestValidator : AbstractValidator<UpdateSubjectRequest>
    {
        public UpdateSubjectRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(400);
            RuleFor(x => x.isPublic).NotNull().NotEmpty();
            RuleFor(x => x.LastActivityDate).NotNull().NotEmpty().LessThan(DateTime.Now.AddMinutes(1));
        }
    }

}
