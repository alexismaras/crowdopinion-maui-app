using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrowdOpinion.Models
{
    public class QuestionStore
    {
        public ObservableCollection<QuestionObject> Questions  { get; } =
            new ObservableCollection<QuestionObject>();
        public void AddQuestion(string question, string answerOne, string answerTwo, int answerOneCount = 0, int answerTwoCount = 0)
        {
            if (!string.IsNullOrWhiteSpace(question) &&
                !string.IsNullOrWhiteSpace(answerOne) &&
                !string.IsNullOrWhiteSpace(answerTwo))
            {

                QuestionObject _questionObject = new QuestionObject(question, answerOne, answerTwo, answerOneCount, answerTwoCount);
                Questions.Add(_questionObject);
            }            
        }

        public void RemoveQuestion(QuestionObject questionObject)
        {
            if (Questions.Contains(questionObject))
            {
                Questions.Remove(questionObject);
            }
        }

        public void AddAnswerVote(QuestionObject questionObject, int choice)
        {
            if (!Questions.Contains(questionObject))
            {
                return;
            }
            int index = Questions.IndexOf(questionObject);
            if (index != -1)
            {
                switch (choice)
                {
                    case 1:
                        Questions[index].AnswerOneCount++;
                        break;
                    case 2:
                        Questions[index].AnswerTwoCount++;
                        break;
                }
            }
        }
    }
}
