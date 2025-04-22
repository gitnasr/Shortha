using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Shortha.DTO
{
    // Data annotations are used for validation but it makes the code missy
    // Using the Validation Fluent library would be better
    public class RegisterRequest
    {
      
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(6, 10).WithMessage("Username must be between 6 and 10 characters long.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Username can only contain letters and numbers.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
        }
    }

}
