using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFArrivals.Singletons
{
    public  class StaticDTOs
    {
        public static DTOArrivals Arrivals { get; set; }


    }
    public class DTOArrivals
    {
        public IEnumerable<CheckBox> ModeOptions { get; set; }
        public IEnumerable<WPFArrivals.API.Route> Routes { get; set; }
    }
}
