namespace Trippr;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ListDetailDetailPage), typeof(ListDetailDetailPage));

		// Experimenting with routing to MapPage from somewhere other than the TabBar
		Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
	}
}
