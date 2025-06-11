using CrowdOpinion.Models;
using Supabase;
using Supabase.Gotrue;
namespace CrowdOpinion.Services
{
    public interface IDataService
    {
        Task<IEnumerable<QuestionObjectSupa>> GetQuestionObject();
        Task<IEnumerable<QuestionObjectSupa>> GetUserQuestionObject();
        Task<IEnumerable<QuestionObjectSupa>> GetForeignQuestionObject();
        Task CreateQuestionObject(QuestionObjectSupa questionObjectSupa,
            string imageOneUrl,
            string imageTwoUrl);
        Task DeleteQuestionObject(int id);
        Task UpdateQuestionObject(QuestionObjectSupa questionObjectSupa);
        Task<AuthResult> SignUp(string email, string password, ProfileObject profileObject);
        Task<AuthResult> SignIn(string email, string password);
        Task SignOut();
        Task<bool> RestoreLastSession();
        Task<bool> IsUserLoggedIn();
        Task<IEnumerable<ProfileObject>> GetUserProfilesByUsername(string searchString);
        Task<ProfileObject> GetUserProfileByUuid(string uuid);

    }
}
