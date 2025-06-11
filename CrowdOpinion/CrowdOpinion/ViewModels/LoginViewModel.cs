using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Models;
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
        private bool _isLoading;

        [ObservableProperty]
        private string _loginEmail;

        [ObservableProperty]
        private string _loginPassword;

        [ObservableProperty]
        private string _registerResponse;

        [ObservableProperty]
        private string _registerEmail;

        [ObservableProperty]
        private string _registerPassword;

        [ObservableProperty]
        private string _profileUserName;

        [ObservableProperty]
        private string _profileFullName;

        public LoginViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [RelayCommand]
        async private void RegisterNow()
        {

            ProfileObject profileObject = new()
            {
                UserId = "",
                UserName = ProfileUserName,
                FullName = ProfileUserName,
                Bio = "",
                AvatarUrl = "",
            };
            var result = await _dataService.SignUp(RegisterEmail, RegisterPassword, profileObject);

            if (result.IsSuccess)
            {
                RegisterEmail = string.Empty;
                RegisterPassword = string.Empty;
                ProfileUserName = string.Empty;
                ProfileFullName = string.Empty;
            }

            else
            {
                await Application.Current.Windows[0].Page.DisplayAlert("Registration Failed", result.Message, "OK");
            }

        }

        [RelayCommand]
        async private void LoginNow()
        {
            try
            {
                IsLoading = true;

                var result = await _dataService.SignIn(LoginEmail, LoginPassword);

                if (result.IsSuccess)
                {
                    OnLoginSuceeded?.Invoke();
                }
                else
                {
                    // Show error to user
                    await Application.Current.Windows[0].Page.DisplayAlert("Login Failed", result.Message, "OK");
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
