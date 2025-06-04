using CrowdOpinion.Pages;
using CrowdOpinion.Services;
using CommunityToolkit.Mvvm.Input;
namespace CrowdOpinion
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
        }
    }
}