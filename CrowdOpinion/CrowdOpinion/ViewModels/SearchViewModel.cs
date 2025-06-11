using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Models;
using CrowdOpinion.Services;
using System.Collections.ObjectModel;

namespace CrowdOpinion.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        public SearchViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [ObservableProperty]
        private string _searchString;

        public ObservableCollection<ProfileObject> SearchProfileObjects { get; set; } = new();

        [RelayCommand]
        private async Task SearchUsers()
        {

            SearchProfileObjects.Clear();

            try
            {
                var profileObjects = await _dataService.GetUserProfilesByUsername(SearchString);

                if (profileObjects.Any())
                {
                    foreach (var profile in profileObjects)
                    {
                        SearchProfileObjects.Add(profile);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}