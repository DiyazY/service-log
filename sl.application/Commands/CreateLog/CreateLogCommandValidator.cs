using FluentValidation;

namespace sl.application.Commands.CreateLog
{
    public sealed class CreateLogCommandValidator : AbstractValidator<CreateLogCommand>
    {
        public CreateLogCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage("Id can't be empty!!!");
            RuleFor(p => p.Message)
                .NotEmpty()
                    .WithMessage("Message can't be empty!!!");
            RuleFor(p => p.SystemId)
                .NotEmpty()
                    .WithMessage("System id can't be empty!!!");
            RuleFor(p => p.Level)
                .NotEmpty()
                    .WithMessage("Level can't be empty!!!");
        }
    }
}
