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
        RuleFor(x => x.Marca);
        RuleFor(x => x.Modelo);
        RuleFor(x => x.Color);
        RuleFor(x => x.Tipo);
    }
}