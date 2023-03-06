namespace Trippr.ViewModels;

public partial class TripViewModel : BaseViewModel
{
    readonly TripDataService dataService;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    ObservableCollection<Trip> trips;

    public TripViewModel(TripDataService service) 
    {
        dataService = service;
    }

    [RelayCommand]
    private async void OnRefreshing() {
        IsRefreshing = true;

        try {
            await LoadDataAsync();
        }
        finally {
            IsRefreshing = false;
        }
    }

    public async Task LoadDataAsync() {
        Trips = new ObservableCollection<Trip>(await dataService.GetItems(25.0d));
    }

    [RelayCommand]
    private void GoToDetails(Trip trip) 
    { 
        // Get Point of Interests for Trip
        // Go to ListDetailPage passing PointOfInterests
    }

}

