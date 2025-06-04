using CrowdOpinion.Helper;
using CrowdOpinion.Pages;
using CrowdOpinion.ViewModels;

namespace CrowdOpinion
{
    public partial class App : Application
    {
        private LoginViewModel loginViewModel = new LoginViewModel();
        public App()
        {
            InitializeComponent();

            LoginViewModel.OnLoginSuceeded += HandleLoginSucceeded;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Login(loginViewModel));
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
    }
}