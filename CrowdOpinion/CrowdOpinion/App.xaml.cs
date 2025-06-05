using CrowdOpinion.Helper;
using CrowdOpinion.Pages;
using CrowdOpinion.ViewModels;
using CrowdOpinion.Services;
namespace CrowdOpinion
{
    public partial class App : Application
    {
        private readonly IDataService _dataService;
        private LoginViewModel _loginViewModel;
        public App(IDataService dataService)
        {
            _dataService = dataService;
            _loginViewModel = new LoginViewModel(_dataService);

            Application.Current.UserAppTheme = AppTheme.Dark;

            InitializeComponent();

            LoginViewModel.OnLoginSuceeded += HandleLoginSucceeded;
            SettingsViewModel.OnLogout+= HandleLogout;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            bool isUserLoggedIn = _dataService.IsUserLoggedIn().GetAwaiter().GetResult();
            if (isUserLoggedIn)
            {
                return new Window(new AppShell());
            }
            else
            {
                return new Window(new Login(_loginViewModel));
            }
        }

        private void HandleLoginSucceeded()
        {
            if (Application.Current != null)
            {
                foreach (var window in Application.Current.Windows.ToList())
                {
                    Application.Current.CloseWindow(window);
                }

                Window shellWindow = new Window(new AppShell());

                App.Current?.OpenWindow(shellWindow);

            } 
        }

        private void HandleLogout()
        {
            if (Application.Current != null)
            {
                foreach (var window in Application.Current.Windows.ToList())
                {
                    Application.Current.CloseWindow(window);
                }

                Window loginWindow = new Window(new Login(_loginViewModel));

                App.Current?.OpenWindow(loginWindow);

            }
        }
    }
}