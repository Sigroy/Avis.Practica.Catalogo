using FluentValidation;

using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application;

public class AutoDTOValidator : AbstractValidator<AutoDTO>
{
    public AutoDTOValidator()
    {
        RuleFor(x => x.AutoPkey)
            .NotNull().WithMessage("La llave no puede ser nula.")
            .NotEmpty().WithMessage("La llave no puede estar vacía.");
    }
}