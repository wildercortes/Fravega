using Core.Request;
using FluentValidation;

namespace Core.Validators
{
    public class AddBranchOfficeRequestValidator : AbstractValidator<AddBranchOfficeRequest>
    {
        public AddBranchOfficeRequestValidator()
        {
            RuleFor(x => x.Direccion)
               .NotEmpty()
               .NotNull()
               .WithMessage("Debe ingresar una direccion")
               .NotEqual("string")
               .WithMessage("Debe ingresar una direccion valida");

        }

    }
}
