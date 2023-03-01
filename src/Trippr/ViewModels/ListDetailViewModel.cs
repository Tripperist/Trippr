namespace Trippr.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
	readonly PointOfInterestService dataService;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	ObservableCollection<PointOfInterest> pointOfInterests;

	public ListDetailViewModel(PointOfInterestService service)
	{
		dataService = service;
	}

	[RelayCommand]
	private async void OnRefreshing()
	{
		IsRefreshing = true;

		try
		{
			await LoadDataAsync();
		}
		finally
		{
			IsRefreshing = false;
		}
	}

	public async Task LoadDataAsync()
	{
        PointOfInterests = new ObservableCollection<PointOfInterest>(await dataService.GetItems(25.0d));
	}

	[RelayCommand]
	private async void GoToDetails(PointOfInterest pointOfInterest)
	{
		await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
		{
			{ "PointOfInterest", pointOfInterest }
		});
	}
}
