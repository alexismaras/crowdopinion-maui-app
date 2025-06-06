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

        
        private QuestionObjectSupa GetRandomQuestion()
        {
            var random = new Random();
            if (QuestionObjects.Count <= 0) return new QuestionObjectSupa()
            {
                Question = "Foo",
                AnswerOne = "Nothing",
                AnswerTwo = "Nothing",
                AnswerOneCount = 0,
                AnswerTwoCount = 0
            };

            int index = random.Next(0, QuestionObjects.Count);
            return QuestionObjects[index];
        }

        [RelayCommand]
        async private void Refresh()
        {
            IsRefreshing = true;
            await GetQuestions();
            RandomQuestionObject = GetRandomQuestion();
            IsRefreshing = false;
        }

        [RelayCommand]
        private void ChooseAnswerOne()
        {
            AddAnswerVote(RandomQuestionObject, 1);
            RandomQuestionObject = GetRandomQuestion();
        }

        [RelayCommand]
        private void ChooseAnswerTwo()
        {
            AddAnswerVote(RandomQuestionObject, 2);
            RandomQuestionObject = GetRandomQuestion();
        }

        async private void AddAnswerVote(QuestionObjectSupa questionToUpdate, int answer)
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
