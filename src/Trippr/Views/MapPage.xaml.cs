namespace Trippr.Views;

using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

public partial class MapPage : ContentPage
{
    PointOfInterest pointOfInterest;

	public MapPage(MapViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

#if WINDOWS
		// Note that the map control is not supported on Windows.
		// For more details, see https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/map?view=net-maui-7.0
		// For a possible workaround, see https://github.com/CommunityToolkit/Maui/issues/605
		Content = new Label() { Text = "Windows does not have a map control. 😢" };
#endif
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args) 
	{
        base.OnNavigatedTo(args);
        MapViewModel vm = (MapViewModel)BindingContext;
        pointOfInterest = vm.PointOfInterest;
        var location = pointOfInterest.Placemark.Location;


#if WINDOWS
        // Launch the Bing Maps app
        // For more information see: https://learn.microsoft.com/en-us/windows/uwp/launch-resume/launch-maps-app
        var uri = new Uri($"bingmaps:?collection=point.{location.Latitude}_{location.Longitude}_{Uri.EscapeDataString(pointOfInterest.Name)}&lvl=16");
        var launcherOptions = new Windows.System.LauncherOptions();
        launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
        var success = Windows.System.Launcher.LaunchUriAsync(uri, launcherOptions);
#else
        map.Pins.Add(new Pin {
            Label = $"{pointOfInterest.Name}",
            Address = $"{pointOfInterest.Description}",
            Type = PinType.Place,
            Location = location
        });

        var mapSpan = new MapSpan(location, 0.01, 0.01);
        map.MoveToRegion(mapSpan);
#endif

    }
}
