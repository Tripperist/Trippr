namespace Trippr.ViewModels;

[QueryProperty(nameof(PointOfInterest), nameof(PointOfInterest))]
public partial class MapViewModel : BaseViewModel
{
    [ObservableProperty]
    PointOfInterest pointOfInterest;
}
