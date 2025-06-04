using CrowdOpinion.ViewModels;

namespace CrowdOpinion.Pages;

public partial class Login : ContentPage
{
    public Login(LoginViewModel loginViewModel)
    {
        InitializeComponent();
		BindingContext = loginViewModel;
	}
}