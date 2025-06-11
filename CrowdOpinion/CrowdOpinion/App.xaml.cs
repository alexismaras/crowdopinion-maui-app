using CrowdOpinion.Helper;
using CrowdOpinion.Pages;
using CrowdOpinion.Services;
using CrowdOpinion.ViewModels;
using System.Diagnostics;

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
            // Create a temporary window to show immediately
            var loadingWindow = new Window(new ContentPage
            {
                Content = new ActivityIndicator { IsRunning = true, Scale = 0.1 }
            });

            // Start the auth check without blocking
            _ = CheckAuthAndNavigate(loadingWindow);

            return loadingWindow;
        }

        private async Task CheckAuthAndNavigate(Window loadingWindow)
        {
            try
            {
                bool isUserLoggedIn = await _dataService.IsUserLoggedIn();

                if (!isUserLoggedIn)
                {
                    isUserLoggedIn = await _dataService.RestoreLastSession();
                }

                loadingWindow.Page = isUserLoggedIn
                        ? new AppShell()
                        : new Login(_loginViewModel);
            }
            catch (Exception ex)
            {
                loadingWindow.Page = new Login(_loginViewModel);
                Debug.WriteLine($"Auth check failed: {ex}");
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