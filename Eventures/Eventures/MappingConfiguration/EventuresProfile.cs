using AutoMapper;
using System.Reflection;
using System.Linq;
using Eventures.MappingConfiguration.Contracts;
using System;

namespace Eventures.MappingConfiguration
{
    public class EventuresProfile : Profile
    {
        public EventuresProfile()
        {
            Type[] types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>) || i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                .ToArray();

            foreach (var type in types)
            {
                Type[] interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .ToArray();

                foreach (var currentInterface in interfaces)
                {
                    if(currentInterface.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                    {
                        Type interfaceGenericType = currentInterface.GetGenericArguments()[0];
                        this.CreateMap(interfaceGenericType, type);
                    }
                    else if(currentInterface.GetGenericTypeDefinition() == typeof(IMapTo<>))
                    {
                        Type interfaceGenericType = currentInterface.GetGenericArguments()[0];
                        this.CreateMap(type, interfaceGenericType);
                    }
                }
            }
        }
    }
}
