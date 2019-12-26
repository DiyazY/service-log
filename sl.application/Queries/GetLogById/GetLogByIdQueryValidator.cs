using FluentValidation;

namespace sl.application.Queries.GetLogById
{
    public sealed class GetLogByIdQueryValidator : AbstractValidator<GetLogByIdQuery>
    {
        public GetLogByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage("Id is required!");
        }
    }
}
