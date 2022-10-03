using AutoMapper;

//Dependencia Arquitectura
using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application;

public class DomainMapping : Profile
{
    
    public DomainMapping()
    {
        CreateMap<AutoDTO, Auto>().ReverseMap();
    }
}