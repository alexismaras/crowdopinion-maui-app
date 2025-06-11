using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Models;
using System.Collections.ObjectModel;
using CrowdOpinion.Services;

namespace CrowdOpinion.ViewModels
{
    public partial class AnswerViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isRefreshing;

        private readonly IDataService _dataService;
        private Collection<QuestionObjectSupa> QuestionObjects { get; set; } = new();

        [ObservableProperty]
        private QuestionObjectSupa _randomQuestionObject;

        [ObservableProperty]
        private ProfileObject _questionAuthorProfile;

        public AnswerViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Refresh();
        }

        [RelayCommand]
        public async Task GetQuestions()
        {
            QuestionObjects.Clear();

            try
            {
                var questionObjectSupa = await _dataService.GetForeignQuestionObject();

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

        
        private QuestionObjectSupa GetRandomQuestion()
        {
            var random = new Random();
            if (QuestionObjects.Count <= 0) return null;

            int index = random.Next(0, QuestionObjects.Count);

            QuestionObjectSupa newRandomQuestionObject = QuestionObjects[index];

            return newRandomQuestionObject;
        }

        private async Task NewQuestion()
        {
            QuestionObjects.Remove(RandomQuestionObject);
            RandomQuestionObject = GetRandomQuestion();
            if (RandomQuestionObject == null) return;
            QuestionAuthorProfile = await _dataService.GetUserProfileByUuid(RandomQuestionObject.UserId);
        }

        [RelayCommand]
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetQuestions();
            await NewQuestion();
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task ChooseAnswerOne()
        {
            await AddAnswerVote(RandomQuestionObject, 1);

            await NewQuestion();
        }

        [RelayCommand]
        private async Task ChooseAnswerTwo()
        {
            await AddAnswerVote(RandomQuestionObject, 2);

            await NewQuestion();
        }

        private async Task AddAnswerVote(QuestionObjectSupa questionToUpdate, int answer)
        {
            switch (answer)
            {
                case 1:
                    questionToUpdate.AnswerOneCount++;
                    break;
                case 2:
                    questionToUpdate.AnswerTwoCount++;
                    break;
                default:
                    return;

            };
            try
            {
                await _dataService.UpdateQuestionObject(questionToUpdate);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
