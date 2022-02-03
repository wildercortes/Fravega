using Core.Request;
using FluentValidation;

namespace Core.Validators
{
    public class GetBranchOfficeByLatAndLogRequestValidator : AbstractValidator<GetBranchOfficeByLatAndLogRequest>
    {
        public GetBranchOfficeByLatAndLogRequestValidator()
        {
            RuleFor(x => x.Latitud)
              .NotEmpty()
              .NotNull()
              .WithMessage("Debe ingresar una latitud valida");

            RuleFor(x => x.Latitud)
              .NotEmpty()
              .NotNull()
              .WithMessage("Debe ingresar una longitud valida");

        }

    }
}
