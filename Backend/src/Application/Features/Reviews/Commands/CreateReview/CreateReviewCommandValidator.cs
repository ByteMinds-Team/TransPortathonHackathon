using FluentValidation;

namespace Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(p => p.OfferId).NotNull();
        RuleFor(p => p.Comment).NotNull();
        RuleFor(p => p.Point).NotNull().LessThanOrEqualTo(5).GreaterThanOrEqualTo(0);
        RuleFor(p => p.UserId).NotNull();
    }
}