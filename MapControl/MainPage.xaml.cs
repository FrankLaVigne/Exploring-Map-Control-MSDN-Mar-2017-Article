using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MapControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadMapMarkers();
        }

        private void LoadMapMarkers()
        {
            MapIcon landmarkMapIcon = new MapIcon();
            landmarkMapIcon.Location = new Geopoint(new BasicGeoposition() { Latitude = 38.8895, Longitude = -77.0353 } );
            landmarkMapIcon.Title = "Washington Monument";
            mapControl.MapElements.Add(landmarkMapIcon);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var senderButton = sender as Button;
            var buttonTest = senderButton.Content.ToString();

            switch (buttonTest)
            {
                case "Aerial":
                    mapControl.Style = MapStyle.Aerial;
                    break;
                case "Aerial With Roads":
                    mapControl.Style = MapStyle.AerialWithRoads;
                    break;
                case "Terrain":
                    mapControl.Style = MapStyle.Terrain;
                    break;
                case "3D Aerial":
                    mapControl.Style = MapStyle.Aerial3D;
                    break;
                case "3D Aerial With Roads":
                    mapControl.Style = MapStyle.Aerial3DWithRoads;
                    break;
                default:
                    mapControl.Style = MapStyle.Road;
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mapControl.TrafficFlowVisible = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            mapControl.TrafficFlowVisible = false;
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            mapControl.ZoomLevel = mapControl.ZoomLevel + 1;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            mapControl.ZoomLevel = mapControl.ZoomLevel - 1;

        }

        private async void btnCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            var locationAccessStatus = await Geolocator.RequestAccessAsync();

            if (locationAccessStatus == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator();
                Geoposition currentPosition = await geolocator.GetGeopositionAsync();

                mapControl.Center = new Geopoint(new BasicGeoposition()
                {
                    Latitude = currentPosition.Coordinate.Latitude,
                    Longitude = currentPosition.Coordinate.Longitude
                });

                mapControl.ZoomLevel = 12;

            }
        }



        private async void btnWhatZoom_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog($"ZoomLevel: {mapControl.ZoomLevel}");
            await dialog.ShowAsync();
        }
    }
}
