using Supabase;
using CrowdOpinion.Models;
namespace CrowdOpinion.Services
{
    public class DataService : IDataService
    {
        private readonly Client _supabaseClient;

        public DataService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }
        public async Task<IEnumerable<QuestionObjectSupa>> GetQuestionObject()
        {
            var response = await _supabaseClient.From<QuestionObjectSupa>().Get();
            return response.Models.OrderByDescending(b => b.CreatedAt);
        }

        public async Task CreateQuestionObject(QuestionObjectSupa questionObjectSupa)
        {
            await _supabaseClient.From<QuestionObjectSupa>().Insert(questionObjectSupa);
        }

        public async Task DeleteQuestionObject(int id)
        {
            await _supabaseClient.From<QuestionObjectSupa>()
                .Where(b => b.Id == id).Delete();
        }

        public async Task UpdateQuestionObject(QuestionObjectSupa questionObjectSupa)
        {
            await _supabaseClient.From<QuestionObjectSupa>().Where(b => b.Id == questionObjectSupa.Id)
                .Set(b => b.Question, questionObjectSupa.Question)
                .Set(b => b.AnswerOne, questionObjectSupa.AnswerOne)
                .Set(b => b.AnswerTwo, questionObjectSupa.AnswerTwo)
                .Set(b => b.AnswerOneCount, questionObjectSupa.AnswerOneCount)
                .Set(b => b.AnswerTwoCount, questionObjectSupa.AnswerTwoCount)
                .Update();
        }
    }
}