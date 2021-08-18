using AppMvcBasic.Models;
using FluentValidation;
using FluentValidation.Results;

namespace DevDe.Business.Services
{
    public abstract class BaseService
    {
        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string msg)
        {
            // Send this erro untill View
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }
       
    }

}
