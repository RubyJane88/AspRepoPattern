using AutoMapper;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;

namespace DemoSeven.WebApi.Mappings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}