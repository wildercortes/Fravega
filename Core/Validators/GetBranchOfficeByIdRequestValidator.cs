using Core.Request;
using FluentValidation;

namespace Core.Validators
{
    public class GetBranchOfficeByIdRequestValidator : AbstractValidator<GetBranchOfficeByIdRequest>
    {
        public GetBranchOfficeByIdRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .NotNull()
               .WithMessage("Debe ingresar un Id");

        }
    }
}
