using AVIS.CoreBase.Clases;
using AVIS.CoreBase.Extensions;
using FluentValidation.Results;
using FluentValidation;
using Avis.Catalogo.Application;
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Infrastructure;

public class AutoAggregate : IAutoAggregate
{
    private Auto _auto;

    private readonly IValidator<AutoDTO> _validator;

    private readonly IDapperUnitofWork _unitofWork;

    public IList<InternalException> Errores { get; } = new List<InternalException>();

    public bool Success { get; private set; } = false;

    public AutoAggregate(IValidator<AutoDTO> validator,
        IDapperUnitofWork unitofWork)
    {
        _unitofWork = unitofWork;
        _validator = validator;
    }

    public async Task<int> CreateAsync(AutoDTO auto)
    {
        Success = false;
        int id = 0;
        try
        {
            ValidationResult result = await _validator.ValidateAsync(auto);

            if (!result.IsValid)
            {
                Errores.ToList().AddRange(result.ToErrorList(this.GetType().ToString(), "CreateAsync"));
                Success = false;
            }
            else
            {
                _auto = auto.ToModelorVM<Auto>();
                var key = Guid.NewGuid().ToString();
                _auto.AutoPkey = key;

                #region TRANSACCION DAPPER

                try
                {
                    id = await _unitofWork.AutoCmdRepository.AddAsync(_auto);

                    if (_unitofWork.AutoCmdRepository.Success)
                    {
                        if (_unitofWork.AutoCmdRepository.Success)
                        {
                            Success = true;
                        }
                    }

                    if (Success)
                    {
                        _unitofWork.Commit();
                    }
                    else
                    {
                        id = 0;
                        Success = false;

                        if (_unitofWork.AutoCmdRepository.Errores.Count > 0)
                        {
                            _unitofWork.AutoCmdRepository.Errores.ToCurrentList(Errores);
                        }

                        _unitofWork.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    Success = false;
                    string extra = "";
                    if (ex.InnerException != null)
                    {
                        extra = ex.InnerException.Message;
                    }

                    InternalException error = new InternalException()
                    {
                        ClassName = this.GetType().ToString(),
                        MethodName = "Transaccion Dapper",
                        ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
                        Ex = ex
                    };
                    Errores.Add(error);
                }

                #endregion
            }
        }
        catch (Exception ex)
        {
            Success = false;
            string extra = "";
            if (ex.InnerException != null)
            {
                extra = ex.InnerException.Message;
            }

            InternalException error = new InternalException()
            {
                ClassName = this.GetType().ToString(),
                MethodName = "Add",
                ErrorMessage = "Inner:" + extra + " Exception:" + ex.Message,
                Ex = ex
            };
            Errores.Add(error);
        }

        return id;
    }
}