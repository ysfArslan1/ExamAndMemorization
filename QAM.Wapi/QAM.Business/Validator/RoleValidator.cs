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
    // CreateRoleRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }
    // UpdateRoleRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
