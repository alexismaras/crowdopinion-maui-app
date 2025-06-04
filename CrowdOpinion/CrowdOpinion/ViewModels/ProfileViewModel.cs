using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CrowdOpinion.Models;
using CrowdOpinion.Pages;

namespace CrowdOpinion.ViewModels
{
    // ProfileViewModel.cs
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly QuestionStore _questionStore;

        [ObservableProperty]
        private ObservableCollection<QuestionObject> _questions;
        public ProfileViewModel(QuestionStore questionStore)
        {
            _questionStore = questionStore;
            Questions = _questionStore.Questions;
        }

        //public ObservableCollection<QuestionObject> Questions => QuestionStore.Questions;

        [RelayCommand]
        void DeleteQuestion(QuestionObject questionObject)
        {
            _questionStore.RemoveQuestion(questionObject);
        }

        [RelayCommand]
        Task GoToSettings() => Shell.Current.GoToAsync(nameof(Settings));

    }
}