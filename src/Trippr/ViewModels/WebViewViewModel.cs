namespace Trippr.ViewModels;

[QueryProperty(nameof(PointOfInterest), nameof(PointOfInterest))]
public partial class WebViewViewModel : BaseViewModel
{
	[ObservableProperty]
	public string source;

	[ObservableProperty]
	public bool isLoading;

    [ObservableProperty]
    PointOfInterest pointOfInterest;

    public WebViewViewModel()
	{
		// TODO: Update the default URL
		Source = "https://mikelor.github.io";
		IsLoading = true;
	}

	[RelayCommand]
	private async void WebViewNavigated(WebNavigatedEventArgs e)
	{
		Source = PointOfInterest.Link.ToString();
		IsLoading = false;

		if (e.Result != WebNavigationResult.Success)
		{
			// TODO: handle failed navigation in an appropriate way
			await Shell.Current.DisplayAlert("Navigation failed", e.Result.ToString(), "OK");
		}
	}

	[RelayCommand]
	private void NavigateBack(WebView webView)
	{
		if (webView.CanGoBack)
		{
			webView.GoBack();
		}
	}

	[RelayCommand]
	private void NavigateForward(WebView webView)
	{
		if (webView.CanGoForward)
		{
			webView.GoForward();
		}
	}

	[RelayCommand]
	private void RefreshPage(WebView webView)
	{
		webView.Reload();
	}

	[RelayCommand]
	private async void OpenInBrowser()
	{
		await Launcher.OpenAsync(Source);
	}
}
