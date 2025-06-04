using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CrowdOpinion.Services;
using CrowdOpinion.Models;

namespace CrowdOpinion.ViewModels
{
    public partial class AskViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        public AskViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [ObservableProperty]
        private string _questionText;

        [ObservableProperty]
        private string _answerOneText;

        [ObservableProperty]
        private string _answerTwoText;

        //public ObservableCollection<QuestionObject> Questions => _questionStore.Questions;

        [RelayCommand]

        private async Task AskQuestion()
        {
            try
            {
                if (!string.IsNullOrEmpty(QuestionText) &&
                    !string.IsNullOrEmpty(AnswerOneText) &&
                    !string.IsNullOrEmpty(AnswerTwoText))
                {
                    QuestionObjectSupa questionObjectSupa = new()
                    {
                        Question = QuestionText,
                        AnswerOne = AnswerOneText,
                        AnswerTwo = AnswerTwoText,
                        AnswerOneCount = 0,
                        AnswerTwoCount = 0,
                    };
                    await _dataService.CreateQuestionObject(questionObjectSupa);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Fill All", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }
        //private void AskQuestion()
        //{
        //    _questionStore.AddQuestion(QuestionText, AnswerOneText, AnswerTwoText);
        //    QuestionText = string.Empty;
        //    AnswerOneText = string.Empty;
        //    AnswerTwoText = string.Empty;
        //}
    }
}

