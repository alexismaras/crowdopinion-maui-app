using CrowdOpinion.ViewModels;
using CrowdOpinion.Models;
namespace CrowdOpinion.Pages;

public partial class Profile : ContentPage
{
	public Profile(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
		BindingContext = profileViewModel;
    }
}