using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CrowdOpinion.Models;
using CrowdOpinion.Pages;
using CrowdOpinion.Services;

namespace CrowdOpinion.ViewModels
{
    // ProfileViewModel.cs
    public partial class ProfileViewModel : ObservableObject
    {

        private readonly IDataService _dataService;

        public ObservableCollection<QuestionObjectSupa> QuestionObjects { get; set; } = new();
        public ProfileViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [RelayCommand]
        public async Task GetQuestions()
        {
            QuestionObjects.Clear();

            try
            {
                var questionObjectSupa = await _dataService.GetQuestionObject();

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