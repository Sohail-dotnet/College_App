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

            //config for different property names 
            /*If there is a change in property names between source and destination classes, 
             *we can use ForMember method to map the properties explicitly.
             */
                     //CreateMap<StudentDTO, Student>().ForMember(n=> n.Name, opt=> opt.MapFrom(x=>x.StudentName)).ReverseMap();

            //config for ignoring specific properties
            /*If there are properties in the destination class that you want to ignore during mapping, 
             *you can use the ForMember method with the Ignore option.
             */
                     //CreateMap<StudentDTO, Student>().ForMember(n => n.PhoneNumber, opt => opt.Ignore()).ReverseMap();

            // Config for a empty or null property
            /*If there are properties that might be null or empty in the source class, 
             *you can use the ForMember method with a condition to provide a default value during mapping.
             */
                     CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "No Address Found" : n.Address));

        }
    }
}

  