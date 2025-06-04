using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Models;

namespace CrowdOpinion.ViewModels
{
    public partial class AnswerViewModel : ObservableObject
    {
        private readonly QuestionStore _questionStore;

        [ObservableProperty]
        private QuestionObject _randomQuestionObject;
        public AnswerViewModel(QuestionStore questionStore)
        {
            _questionStore = questionStore;
            RandomQuestionObject = GetRandomQuestion();
        }
        private QuestionObject GetRandomQuestion()
        {
            var random = new Random();
            if (_questionStore.Questions.Count <= 0) return new QuestionObject("Nothing", "Nothing", "Nothing", 0, 0);
            int index = random.Next(0, _questionStore.Questions.Count);
            return _questionStore.Questions[index];
        }

        [RelayCommand]
        private void ChooseAnswerOne()
        {
            _questionStore.AddAnswerVote(RandomQuestionObject, 1);
            RandomQuestionObject = GetRandomQuestion();
        }

        [RelayCommand]
        private void ChooseAnswerTwo()
        {
            _questionStore.AddAnswerVote(RandomQuestionObject, 2);
            RandomQuestionObject = GetRandomQuestion();
        }
    }
}
