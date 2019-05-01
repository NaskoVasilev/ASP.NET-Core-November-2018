using AutoMapper;
using FunApp.Data.Models;
using FunApp.Services.Mapping;

namespace FunApp.Services.Models.Home
{
    public class JokeViewModel : IMapFrom<Joke>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string HtmlContent => this.Content.Replace("\n", "<br />\n");

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<Joke, JokeViewModel>()
            //    .ForMember(x => x.CategoryName, y => y.MapFrom(s => s.Category.Name));
        }
    }
}
