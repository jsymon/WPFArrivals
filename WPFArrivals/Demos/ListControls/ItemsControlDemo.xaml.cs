using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFArrivals.API;
using WPFArrivals.Singletons;
using WPFArrivals.Properties;

namespace WPFArrivals.Demos.ListControls
{
    /// <summary>
    /// Interaction logic for ItemsControlDemo.xaml
    /// </summary>
    public partial class ItemsControlDemo : Window
    {
        public ItemsControlDemo()
        {
            InitializeComponent();
            dgRoutes.SelectionChanged += dgRoutes_SelectionChanged;
            SetData();
        }

        private void SetData()
        {
            DataContext = new
            {
                ModeOptions = StaticDTOs.Arrivals.ModeOptions.Select(a => (a.Content as string).ToTitleCase()).ToArray(),
                Routes = GetRoutes()
            };
        }

        private IEnumerable<Route> GetRoutes()
        {
            IEnumerable<Route> routes = null;
            if (icOptions.Items.Count > 0)
            {
                var selections = GetSelections();
                routes = StaticDTOs.Arrivals.Routes.Where(a => selections.Any(b => b.Equals(a.ModeName, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                routes = new Route[0];
            }
            return routes;
        }


        private IEnumerable<string> GetSelections()
        {
            var rootControls =
                icOptions
                .Items
                .Cast<object>()
                .SelectMany(item => GetRootControls<CheckBox>(item))
                .OfType<CheckBox>();
            var selections = rootControls
                .Where(a => a.IsChecked.HasValue && a.IsChecked.Value)
                .Select(a => a.Content as string);
            return selections;
        }

        private IEnumerable<DependencyObject> GetRootControls<T>(object item) where T : DependencyObject
        {
            var itemContainer = (ContentPresenter)icOptions.ItemContainerGenerator.ContainerFromItem(item);
            var childCount = VisualTreeHelper.GetChildrenCount(itemContainer);

            var children = Enumerable.Range(0, childCount)
                 .Select(i => VisualTreeHelper.GetChild(itemContainer, i));

            if (!children.Any(a => a is T))
            {
                //check one level deeper
                //this could be recursive in the future 
                children = children
                    .OfType<Panel>()
                    .SelectMany(a => a.Children.Cast<UIElement>())
                    .Where(a => a is T);
            }
            return children;
        }

        private void chkMode_Click(object sender, RoutedEventArgs e)
        {
            var routes = GetRoutes();
            dgRoutes.ItemsSource = routes;
        }

        private void dgRoutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dgRoutes.SelectedItem as Route;
            if (selectedItem != null)
                dgRouteSections.ItemsSource = selectedItem.RouteSections;
        }
    }
}
