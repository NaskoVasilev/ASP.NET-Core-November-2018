using System;

namespace Eventures.ViewModels.Order
{
    public class OrderViewModel
    {
        public string Event { get; set; }

        public string Customer { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
