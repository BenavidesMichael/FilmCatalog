using AutoMapper;
using FilmCatalog.API.DAL;
using FilmCatalog.API.Models;

namespace FilmCatalog.API.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreateDTO, Genre>();
        }
    }
}
