using AutoMapper;
using FunApp.Data.Models;
using FunApp.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.Models.Categories
{
    public class CategoryByNameAndIdViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string NameAndCount =>
           $"{this.Name} ({this.CountOfAllJokes})";

        public int CountOfAllJokes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Category, CategoryByNameAndIdViewModel>()
                .ForMember(x => x.CountOfAllJokes,
                    m => m.MapFrom(c => c.Jokes.Count));
        }
    }
}
