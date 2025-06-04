using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Pages;
using System.Diagnostics;

namespace CrowdOpinion.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        public SettingsViewModel()
        {
        }

        [RelayCommand]
        Task CloseSettings() => Shell.Current.GoToAsync("..", true);
    }
}
