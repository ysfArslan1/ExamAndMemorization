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
    // CreateTagSubjectRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateTagSubjectRequestValidator : AbstractValidator<CreateTagSubjectRequest>
    {
        public CreateTagSubjectRequestValidator()
        {
            RuleFor(x => x.TagId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.SubjectId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
    // UpdateTagSubjectRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateTagSubjectRequestValidator : AbstractValidator<UpdateTagSubjectRequest>
    {
        public UpdateTagSubjectRequestValidator()
        {
            RuleFor(x => x.TagId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.SubjectId).NotNull().NotEmpty().GreaterThan(0);
        }
    }

}
