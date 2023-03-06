namespace Trippr.ViewModels;

[QueryProperty(nameof(PointOfInterest), nameof(PointOfInterest))]
public partial class ListDetailDetailViewModel : BaseViewModel
{
	[ObservableProperty]
	PointOfInterest pointOfInterest;

    [RelayCommand]
    private async void OpenLink() 
    {
            await Launcher.OpenAsync(PointOfInterest.Link);
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
