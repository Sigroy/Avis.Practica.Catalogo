using AutoMapper;

using Avis.Catalogo.Domain;

namespace Avis.Catalogo.Application
{
    public class DomainMappings : Profile
    {
        public DomainMappings()
        {
            CreateMap<AutoDTO, Auto>().ReverseMap();
            CreateMap<Auto, Auto>().ReverseMap();
        }
    }
}
