using System;

namespace Eventures.ViewModels.Event
{
    public class UserEventViewModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Tickets { get; set; }
    }
}
