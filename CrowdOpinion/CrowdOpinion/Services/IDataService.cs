using CrowdOpinion.Models;

namespace CrowdOpinion.Services
{
    public interface IDataService
    {
        Task<IEnumerable<QuestionObjectSupa>> GetQuestionObject();
        Task CreateQuestionObject(QuestionObjectSupa questionObjectSupa);
        Task DeleteQuestionObject(int id);
        Task UpdateQuestionObject(QuestionObjectSupa questionObjectSupa);
    }
}
