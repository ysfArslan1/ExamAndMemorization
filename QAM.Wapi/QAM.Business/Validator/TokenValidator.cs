using FluentValidation;
using QAM.Schema;
using System.Text.RegularExpressions;

namespace QAM.Business.Validator;

public class CreateTokenValidator : AbstractValidator<TokenRequest>
{
    public CreateTokenValidator()
    {
        RuleFor(x => x.Email).NotEmpty().MinimumLength(5).MaximumLength(50).Must(ValidateEmail);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(10);
    }

    // Email doðrulamasý için kullanýlan metot
    private bool ValidateEmail(string text)
    {
        var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return regex.IsMatch(text);
    }
}