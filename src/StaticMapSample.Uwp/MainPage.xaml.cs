using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using JeffWilcox.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StaticMapSample.Uwp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly string path = "47.6479289610064,-122.140453672473" +
                                       "|47.647269370278,-122.140418705562" +
                                       "|47.6467511145767,-122.140383738651" +
                                       "|47.6459972789224,-122.14031380483" +
                                       "|47.6459737213829,-122.140278837919" +
                                       "|47.6460443939695,-122.141502679795" +
                                       "|47.6459972789224,-122.142376852563" +
                                       "|47.645926606272,-122.143076190777";


        public MainPage()
        {
            InitializeComponent();

            _bingMap.MapCenter =
                new Geopoint(new BasicGeoposition
                {
                    Latitude = 47.64508819794251,
                    Longitude = -122.14200973510742
                });


            _googleMap.MapCenter =
                new Geopoint(new BasicGeoposition
                {
                    Latitude = 47.64508819794251,
                    Longitude = -122.14200973510742
                });
            _googleMap.MapCenterVisibility = Visibility.Collapsed;
            _googleMap.Path = path;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var map = button.Content as StaticMap;
                if (map != null)
                {
                    map.NavigateToMapsApplication();
                }
            }
        }
    }
}