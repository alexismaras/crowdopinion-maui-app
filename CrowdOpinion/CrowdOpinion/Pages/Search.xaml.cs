using CrowdOpinion.ViewModels;

namespace CrowdOpinion.Pages;

public partial class Search : ContentPage
{
	public Search(SearchViewModel searchViewModel)
	{
		InitializeComponent();
        BindingContext = searchViewModel;
    }
}