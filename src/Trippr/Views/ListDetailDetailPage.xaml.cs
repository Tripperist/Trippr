namespace Trippr.Views;

public partial class ListDetailDetailPage : ContentPage
{
	public ListDetailDetailPage(ListDetailDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e) {
        var pointOfInterest = ((VisualElement)sender).BindingContext as ListDetailDetailViewModel;
        await Launcher.OpenAsync(pointOfInterest.Item.Link);
    }
}
