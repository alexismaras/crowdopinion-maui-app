using CrowdOpinion.ViewModels;

namespace CrowdOpinion.Pages;

public partial class Ask : ContentPage
{
	public Ask(AskViewModel askViewModel)
	{
		InitializeComponent();
		BindingContext = askViewModel;
	}
}