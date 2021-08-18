using AppMvcBasic.Models;
using FluentValidation;

namespace DevDe.Business.Models.Validation
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo { PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(1, 50).WithMessage("O campo { PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8).WithMessage("O campo { PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo { PropertyName} precisa ter {MaxLength} caracteres");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo { PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo { PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
