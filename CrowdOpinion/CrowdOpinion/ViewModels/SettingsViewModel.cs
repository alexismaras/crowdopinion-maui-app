using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Pages;
using CrowdOpinion.Services;
using System.Diagnostics;

namespace CrowdOpinion.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        public static event Action OnLogout;
        public SettingsViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [RelayCommand]
        async void LogOutNow()
        {
            try
            {
                await _dataService.SignOut();
                OnLogout?.Invoke();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        Task CloseSettings() => Shell.Current.GoToAsync("..", true);
    }
}
