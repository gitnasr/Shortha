using FluentValidation;

namespace Shortha.DTO
{
    public class LoginRequestPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
   class LoginRequestPayloadValidation : AbstractValidator<LoginRequestPayload> {


        public LoginRequestPayloadValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(6, 10).WithMessage("Username must be between 6 and 10 characters long.")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Username can only contain letters and numbers.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
        }


    }
}
