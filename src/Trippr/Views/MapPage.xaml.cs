namespace Trippr.Views;

using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

public partial class MapPage : ContentPage
{
	public MapPage(MapViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

		var location = new Location(36.1666, - 115.2681);
		map.Pins.Add(new Pin {
			Label = "Tripperist HQ",
			Type = PinType.Place,
			Location = location
		});

		var mapSpan = new MapSpan(location, 0.01, 0.01);
		map.MoveToRegion(mapSpan);

		var circle = new Circle {
			Center = location,
			Radius = new Distance(250),
			StrokeColor = Color.FromArgb("#88FF0000"),
			StrokeWidth = 8,
			FillColor = Color.FromArgb("#88FFC0CB")

		};
		map.MapElements.Add(circle);


#if WINDOWS
		// Note that the map control is not supported on Windows.
		// For more details, see https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/map?view=net-maui-7.0
		// For a possible workaround, see https://github.com/CommunityToolkit/Maui/issues/605
		Content = new Label() { Text = "Windows does not have a map control. 😢" };
#endif
	}
}
