using CrowdOpinion.ViewModels;

namespace CrowdOpinion.Pages;

public partial class Settings : ContentPage
{
	public Settings(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
	}
}