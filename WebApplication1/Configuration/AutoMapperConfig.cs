using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
