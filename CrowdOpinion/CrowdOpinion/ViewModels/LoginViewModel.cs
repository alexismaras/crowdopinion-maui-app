using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace CrowdOpinion.ViewModels
{

    public partial class LoginViewModel : ObservableObject
    {
        public static event Action OnLoginSuceeded;
        public LoginViewModel()
        {
        }

        [RelayCommand]
        private void LoginNow()
        {
            OnLoginSuceeded?.Invoke();
        }
    }
}
