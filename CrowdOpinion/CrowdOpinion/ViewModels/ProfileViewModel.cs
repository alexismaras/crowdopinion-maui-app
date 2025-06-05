using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CrowdOpinion.Models;
using CrowdOpinion.Pages;
using CrowdOpinion.Services;

namespace CrowdOpinion.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isRefreshing;
        private readonly IDataService _dataService;

        public ObservableCollection<QuestionObjectSupa> QuestionObjects { get; set; } = new();
        public ProfileViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [RelayCommand]
        private async void Refresh()
        {
            IsRefreshing = true;
            await GetQuestions();
            IsRefreshing = false;
        }
        private async Task GetQuestions()
        {

            QuestionObjects.Clear();

            try
            {
                var questionObjectSupa = await _dataService.GetUserQuestionObject();

                if (questionObjectSupa.Any())
                {
                    foreach (var question in questionObjectSupa)
                    {
                        QuestionObjects.Add(question);
                        Console.WriteLine(question.Question);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }

        [RelayCommand]
        public async Task DeleteQuestion(QuestionObjectSupa questionObjectSupa)
        {
            try
            {
                await _dataService.DeleteQuestionObject(questionObjectSupa.Id);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        Task GoToSettings() => Shell.Current.GoToAsync(nameof(Settings));

    }
}