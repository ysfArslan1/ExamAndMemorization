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
    // CreateTagRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(400);
        }
    }
    // UpdateTagRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
    {
        public UpdateTagRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(400);
        }
    }

}
