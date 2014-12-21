using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFArrivals.API;
using WPFArrivals.Demos.ListControls;
using WPFArrivals.Singletons;

//http://api.tfl.gov.uk/#Line
//http://stackoverflow.com/questions/1405739/mvvm-tutorial-from-start-to-finish
namespace WPFArrivals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitDTO();

            // dtos.ModeOptions = checkBoxes;
            //dto.Routes = des;
            this.DataContext = StaticDTOs.Arrivals;

        }

        private void InitDTO()
        {
            var fs = new FileStream(@"c:\Line.json", FileMode.Open);
            var serialiser = new DataContractJsonSerializer(typeof(Route[]));
            var routes = (Route[])serialiser.ReadObject(fs);

            var checkBoxes = BuildCheckBoxes(routes);
            StaticDTOs.Arrivals = new DTOArrivals() { ModeOptions = checkBoxes, Routes = routes };
        }

        private IEnumerable<CheckBox> BuildCheckBoxes(Route[] des)
        {
            var routes = des.Distinct(new DistinctModes());
            var list = new List<CheckBox>();
            foreach (var route in routes)
            {
                var checkbox = new CheckBox() { Content = route.ModeName };
                checkbox.Click += checkbox_Click;
                list.Add(checkbox);
            }
            return list;
        }

        void checkbox_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btnItemsControl_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = new ItemsControlDemo();
            window.Show();
        }
    }

    public class DistinctModes : IEqualityComparer<Route>
    {
        public bool Equals(Route x, Route y)
        {
            return GetValue(x).Equals(GetValue(y), StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Route obj)
        {
            return GetValue(obj).GetHashCode();
        }

        private string GetValue(Route obj)
        {
            return obj.ModeName.ToLower();
            
        }
    }

    public class IAMADTO
    {
    }
}
