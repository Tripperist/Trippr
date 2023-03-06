namespace Trippr.Views;

public partial class ListDetailPage : ContentPage
{
	private readonly ListDetailViewModel listDetailViewModel;

	public ListDetailPage(ListDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		listDetailViewModel = viewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		await listDetailViewModel.LoadDataAsync();
	}
}
