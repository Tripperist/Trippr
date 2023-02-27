namespace Trippr.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
	readonly PointOfInterestService dataService;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	ObservableCollection<PointOfInterest> items;

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

	[RelayCommand]
	public async Task LoadMore()
	{
		/*
		 * This does nothing unless we need to load more
		
		var items = await dataService.GetItems(25.0d);

		foreach (var item in items)
		{
			Items.Add(item);
		}
		*/
	}

	public async Task LoadDataAsync()
	{
		Items = new ObservableCollection<PointOfInterest>(await dataService.GetItems(25.0d));
	}

	[RelayCommand]
	private async void GoToDetails(PointOfInterest item)
	{
		await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
		{
			{ "Item", item }
		});
	}
}
