using FluentValidation;

using Qatu.Application.DTOs.Chat;

namespace Qatu.API.Middlewares.Chat
{
    public class CreateMessageRequestValidator : AbstractValidator<CreateMessageRequestDto>
    {
        public CreateMessageRequestValidator()
        {
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Sender ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Sender ID must be a valid GUID.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Message content is required.")
                .MaximumLength(1000).WithMessage("Message content cannot exceed 1000 characters.");
        }
    }
}
