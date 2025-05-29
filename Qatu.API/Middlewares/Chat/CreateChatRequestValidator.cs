using FluentValidation;

using Qatu.Application.DTOs.Chat;

namespace Qatu.API.Middlewares.Chat
{
    public class CreateChatRequestValidator : AbstractValidator<CreateChatRequestDto>
    {
        public CreateChatRequestValidator()
        {
            RuleFor(x => x.BuyerId)
                .NotEmpty().WithMessage("Buyer ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Buyer ID must be a valid GUID.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Product ID must be a valid GUID.");
        }
    }
}
