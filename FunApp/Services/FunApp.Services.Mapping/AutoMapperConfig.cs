using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace FunApp.Services.Mapping
{
    public static class AutoMapperConfig
    {
        private static bool initialized;

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToList();
            Mapper.Initialize(configuration =>
            {
                // IMapFrom<>
                foreach (var map in GetFromMaps(types))
                {
                    configuration.CreateMap(map.Source, map.Destination);
                }

                // IMapTo<>
                foreach (var map in GetToMaps(types))
                {
                    configuration.CreateMap(map.Source, map.Destination);
                }

                // IHaveCustomMappings
                foreach (var map in GetCustomMappings(types))
                {
                    map.CreateMappings(configuration);
                }
            });
        }
        private static IEnumerable<TypesMap> GetFromMaps(
            IEnumerable<Type> types)
        {
            List<TypesMap> maps = new List<TypesMap>();

            foreach (var t in types.Where(t => !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsInterface))
            {
                foreach (var i in t.GetTypeInfo().GetInterfaces()
                    .Where(it => it.GetTypeInfo().IsGenericType && it.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                {
                    maps.Add(new TypesMap
                    {
                        Source = i.GetGenericArguments()[0],
                        Destination = t
                    });
                }

            }

            return maps;

            //var fromMaps = from t in types
            //               from i in t.GetTypeInfo().GetInterfaces()
            //               where i.GetTypeInfo().IsGenericType &&
            //                     i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
            //                     !t.GetTypeInfo().IsAbstract &&
            //                     !t.GetTypeInfo().IsInterface
            //               select new TypesMap
            //               {
            //                   Source = i.GetTypeInfo().GetGenericArguments()[0],
            //                   Destination = t,
            //               };

            //return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            List<TypesMap> maps = new List<TypesMap>();

            foreach (var t in types.Where(t => !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsInterface))
            {
                foreach (var i in t.GetTypeInfo().GetInterfaces()
                    .Where(it => it.GetTypeInfo().IsGenericType && it.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                {
                    maps.Add(new TypesMap
                    {
                        Source = t,
                        Destination = i.GetGenericArguments()[0]
                    });
                }

            }

            return maps;


            //var toMaps = from t in types
            //             from i in t.GetTypeInfo().GetInterfaces()
            //             where i.GetTypeInfo().IsGenericType &&
            //                   i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
            //                   !t.GetTypeInfo().IsAbstract &&
            //                   !t.GetTypeInfo().IsInterface
            //             select new TypesMap
            //             {
            //                 Source = t,
            //                 Destination = i.GetTypeInfo().GetGenericArguments()[0],
            //             };

            //return toMaps;
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            var maps = types
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IHaveCustomMappings).IsAssignableFrom(t))
                .Select(t => (IHaveCustomMappings)Activator.CreateInstance(t))
                .ToArray();

            return maps;

            //var customMaps = from t in types
            //                 from i in t.GetTypeInfo().GetInterfaces()
            //                 where typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(t) &&
            //                       !t.GetTypeInfo().IsAbstract &&
            //                       !t.GetTypeInfo().IsInterface
            //                 select (IHaveCustomMappings)Activator.CreateInstance(t);

            //return customMaps;
        }

        private class TypesMap
        {
            public Type Source { get; set; }

            public Type Destination { get; set; }
        }
    }
}
