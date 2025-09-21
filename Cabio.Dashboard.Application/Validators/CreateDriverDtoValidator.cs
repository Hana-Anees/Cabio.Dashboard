using Cabio.Dashboard.Application.Dtos.Drivers;
using FluentValidation;

namespace Cabio.Dashboard.Application.Validators.Drivers
{
    public class CreateDriverDtoValidator : AbstractValidator<CreateDriverDto>
    {
        public CreateDriverDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Driver name is required.")
                .MaximumLength(100);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License number is required.")
                .Matches(@"^[A-Z0-9]+$").WithMessage("License number must be alphanumeric.");

            RuleFor(x => x.Contact)
                .NotEmpty().WithMessage("Contact is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid contact number.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
