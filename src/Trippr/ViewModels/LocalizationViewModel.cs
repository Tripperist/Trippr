namespace Trippr.ViewModels;

public partial class LocalizationViewModel : BaseViewModel
{
	public string LocalizedText => Trippr.Resources.Strings.AppResources.HelloMessage;
}
