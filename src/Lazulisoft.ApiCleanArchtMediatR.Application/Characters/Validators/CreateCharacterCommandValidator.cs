using FluentValidation;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands;

namespace Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Validators
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FullName).MaximumLength(100);
            RuleFor(x => x.Homeworld).MaximumLength(50);
            RuleFor(x => x.Species).MaximumLength(50);
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.Occupation).MaximumLength(100);
        }
    }
}
