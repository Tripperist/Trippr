namespace Trippr.Views;

public partial class TripPage : ContentPage
{
    private readonly TripViewModel tripViewModel;

    public TripPage(TripViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel ;
        tripViewModel = viewModel ;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) {
        base.OnNavigatedTo(args);

        await tripViewModel.LoadDataAsync();
    }
}