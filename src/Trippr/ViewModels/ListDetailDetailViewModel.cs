namespace Trippr.ViewModels;

[QueryProperty(nameof(PointOfInterest), nameof(PointOfInterest))]
public partial class ListDetailDetailViewModel : BaseViewModel
{
	[ObservableProperty]
	PointOfInterest pointOfInterest;

    [RelayCommand]
    private async void OpenLink() 
    {
        UriBuilder newUri = new UriBuilder(PointOfInterest.Link.ToString());
        newUri.Scheme = Uri.UriSchemeHttps;
        newUri.Port = 443;
        PointOfInterest.Link = newUri.Uri;
        //await Launcher.OpenAsync(PointOfInterest.Link);
        await Shell.Current.GoToAsync(nameof(WebViewPage),
            new Dictionary<string, object>
            {
                ["PointOfInterest"] = PointOfInterest

            });
    }

    [RelayCommand]
    private async void MapClicked() 
    {
        await Shell.Current.GoToAsync(nameof(MapPage),
            new Dictionary<string, object> {
                ["PointOfInterest"] = PointOfInterest
            }) ;
    }
}
