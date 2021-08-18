using AppMvcBasic.Models;
using DevDe.Business.Models.Validation.Documents;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevDe.Business.Models.Validation
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("Este campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.TypeProvider == TypeProvider.Person, () => 
            {
                RuleFor(f => f.Document.Length).Equal(CpfValidation.SizeCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi forneceido {PropertyValue}");
                RuleFor(f => CpfValidation.Validator(f.Document)).Equal(true)
                    .WithMessage("O dcumento fornedico não é válido");
            });
            When(f => f.TypeProvider == TypeProvider.Company, () => {
                RuleFor(f => f.Document.Length).Equal(CnpjValidation.SizeCnpj)
                       .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi forneceido {PropertyValue}");
                RuleFor(f => CnpjValidation.Validator(f.Document)).Equal(true)
                    .WithMessage("O dcumento fornedico não é válido");
            });
        }
    }
}
