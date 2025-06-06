using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Services;
using Microsoft.Maui.Controls.Platform;
using System.Threading.Channels;

namespace CrowdOpinion.ViewModels
{

    public partial class LoginViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        public static event Action OnLoginSuceeded;

        [ObservableProperty]
        private string _loginEmail;

        [ObservableProperty]
        private string _loginPassword;

        [ObservableProperty]
        private string _registerEmail;

        [ObservableProperty]
        private string _registerPassword;


        public LoginViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [RelayCommand]
        async private void RegisterNow()
        {
            try
            {
                await _dataService.SignUp(RegisterEmail, RegisterPassword);
                await _dataService.SignIn(RegisterEmail, RegisterPassword);
                OnLoginSuceeded?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //Application.Current.Windows[0].DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async private void LoginNow()
        {
            try
            {
                await _dataService.SignIn(LoginEmail, LoginPassword);
                OnLoginSuceeded?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //Application.Current.Windows[0].DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
