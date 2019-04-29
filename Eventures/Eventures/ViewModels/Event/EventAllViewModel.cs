using Eventures.MappingConfiguration.Contracts;
using System;

namespace Eventures.ViewModels.Event
{
    public class EventAllViewModel : IMapFrom<Models.Event>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
