using FluentValidation;

namespace sl.application.Queries.GetLogsByTerm
{
    public class GetLogsByTermQueryValidator : AbstractValidator<GetLogsByTermQuery>
    {
        public GetLogsByTermQueryValidator()
        {
            RuleFor(p => p.SystemId)
                .NotEmpty()
                    .WithMessage("System id is required!");

            RuleFor(p => p.Page)
                .NotEmpty()
                    .WithMessage("Page is required!");

            RuleFor(p => p.Count)
                .NotEmpty()
                    .WithMessage("Page count is required!");
        }
    }
}
