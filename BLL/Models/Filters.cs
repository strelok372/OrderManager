using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Filters
    {
        public int[] OrderNumbersId { get; set; }
        public int[] OrderDatesId { get; set; }
        public string[] ProviderNames { get; set; }
        public string[] OrderItemNames { get; set; }
        public string[] OrderItemUnits { get; set; }
    }
}
