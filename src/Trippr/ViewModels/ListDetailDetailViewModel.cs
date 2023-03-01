namespace Trippr.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailViewModel : BaseViewModel
{
	[ObservableProperty]
	PointOfInterest item;

    [RelayCommand]
    private async void MapClicked() 
    {
         await Shell.Current.GoToAsync(nameof(MapPage));
    }
}
