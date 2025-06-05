using CrowdOpinion.ViewModels;

namespace CrowdOpinion.Pages;

public partial class Login : TabbedPage
{
    public Login(LoginViewModel loginViewModel)
    {
        InitializeComponent();
		BindingContext = loginViewModel;
	}
}