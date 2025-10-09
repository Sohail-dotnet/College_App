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

            //config for custom value transformation
            /*If you need to apply custom transformations to property values during mapping, 
             *you can use the ConvertUsing method or the MapFrom option within ForMember.
             */
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(src => string.IsNullOrEmpty(src)?"No Address Found":src);
        }
    }
}
