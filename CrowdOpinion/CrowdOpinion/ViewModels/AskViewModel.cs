using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CrowdOpinion.Models;

namespace CrowdOpinion.ViewModels
{
    public partial class AskViewModel : ObservableObject
    {
        private readonly QuestionStore _questionStore;

        public AskViewModel(QuestionStore questionStore)
        {
            _questionStore = questionStore;
        }

        [ObservableProperty]
        private string _questionText;

        [ObservableProperty]
        private string _answerOneText;

        [ObservableProperty]
        private string _answerTwoText;

        public ObservableCollection<QuestionObject> Questions => _questionStore.Questions;

        [RelayCommand]
        private void AskQuestion()
        {
            _questionStore.AddQuestion(QuestionText, AnswerOneText, AnswerTwoText);
            QuestionText = string.Empty;
            AnswerOneText = string.Empty;
            AnswerTwoText = string.Empty;
        }
    }
}

