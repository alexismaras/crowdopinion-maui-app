using CrowdOpinion.Models;
using CrowdOpinion.ViewModels;
using Microsoft.Maui.ApplicationModel.Communication;
using Supabase;
using Microsoft.Maui.Storage;

namespace CrowdOpinion.Services
{
    public class DataService : IDataService
    {
        public readonly Supabase.Client _supabaseClient;
        
        public DataService(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }
        public async Task<IEnumerable<QuestionObjectSupa>> GetQuestionObject()
        {
            var response = await _supabaseClient.From<QuestionObjectSupa>().Get();
            return response.Models.OrderByDescending(b => b.CreatedAt);
        }

        public async Task<IEnumerable<QuestionObjectSupa>> GetUserQuestionObject()
        {
            var session = _supabaseClient.Auth.CurrentSession;

            var user = await _supabaseClient.Auth.GetUser(session.AccessToken);
            if (user == null) throw new UnauthorizedAccessException("User not authenticated");

            // Filter by the user's UID
            var response = await _supabaseClient
                .From<QuestionObjectSupa>()
                .Where(x => x.UserId == user.Id)  // Assuming your model has UserId property
                .Get();

            return response.Models.OrderByDescending(b => b.CreatedAt);
        }

        public async Task CreateQuestionObject(
            QuestionObjectSupa questionObjectSupa,
            string imageOneUrl,
            string imageTwoUrl)
        {

            var session = _supabaseClient.Auth.CurrentSession;

            var user = await _supabaseClient.Auth.GetUser(session.AccessToken);
            if (user == null) throw new Exception("Not authenticated");

            Guid myuuidOne = Guid.NewGuid();
            string myuuidOneString = user.Id + "/" + myuuidOne.ToString();

            Guid myuuidTwo = Guid.NewGuid();
            string myuuidTwoString = user.Id + "/" + myuuidTwo.ToString();


            await _supabaseClient.Storage.From("images").Upload(imageOneUrl, myuuidOneString);
            await _supabaseClient.Storage.From("images").Upload(imageTwoUrl, myuuidTwoString);

            // Set the user_id
            questionObjectSupa.UserId = user.Id;
            questionObjectSupa.AnswerOneImgUrl = myuuidOneString;
            questionObjectSupa.AnswerTwoImgUrl = myuuidTwoString;

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
                .Set(b => b.UserId, questionObjectSupa.UserId)
                .Update();
        }

        public async Task SignUp(string email, string password)
        {
            var user = await _supabaseClient.Auth.SignUp(email, password);
        }

        public async Task SignIn(string email, string password)
        {
            var session = await _supabaseClient.Auth.SignIn(email, password);
            await SecureStorage.SetAsync("supabase_refresh_token", session.RefreshToken);
            await SecureStorage.SetAsync("supabase_access_token", session.AccessToken);
        }

        public async Task SignOut()
        {
            await _supabaseClient.Auth.SignOut();
        }

        public async Task<bool> IsUserLoggedIn()
        {
            try
            {
                // Check if we have a current session
                var session = _supabaseClient.Auth.CurrentSession;
                if (session == null) // || session.ExpiresAt < DateTime.Now)
                return false;

                // Optionally verify with server
                var user = await _supabaseClient.Auth.GetUser(session.AccessToken);
                return user != null;
            }
            catch
            {
                return false;
            }
        }


    }
}