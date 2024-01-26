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
    // CreateQuestionRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
    {
        public CreateQuestionRequestValidator()
        {
            RuleFor(x => x.SubjectId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.question).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Explanation).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }
    }
    // UpdateQuestionRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
    {
        public UpdateQuestionRequestValidator()
        {
            RuleFor(x => x.SubjectId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.question).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Explanation).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }
    }

}
