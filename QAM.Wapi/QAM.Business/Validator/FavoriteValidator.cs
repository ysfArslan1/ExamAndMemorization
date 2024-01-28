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
    // CreateFavoriteRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateFavoriteRequestValidator : AbstractValidator<CreateFavoriteRequest>
    {
        public CreateFavoriteRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.SubjectId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
    // UpdateFavoriteRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateFavoriteRequestValidator : AbstractValidator<UpdateFavoriteRequest>
    {
        public UpdateFavoriteRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.SubjectId).NotNull().NotEmpty().GreaterThan(0);
        }
    }

}
