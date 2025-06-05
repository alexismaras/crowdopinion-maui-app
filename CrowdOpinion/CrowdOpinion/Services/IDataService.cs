using CrowdOpinion.Models;
namespace CrowdOpinion.Services
{
    public interface IDataService
    {
        Task<bool> IsUserLoggedIn();
        Task<IEnumerable<QuestionObjectSupa>> GetQuestionObject();
        Task<IEnumerable<QuestionObjectSupa>> GetUserQuestionObject();
        Task CreateQuestionObject(QuestionObjectSupa questionObjectSupa);
        Task DeleteQuestionObject(int id);
        Task UpdateQuestionObject(QuestionObjectSupa questionObjectSupa);
        Task SignUp(string email, string password);
        Task SignIn(string email, string password);
        Task SignOut();

    }
}
