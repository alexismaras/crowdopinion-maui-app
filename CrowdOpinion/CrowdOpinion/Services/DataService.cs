using CrowdOpinion.Models;
using CrowdOpinion.ViewModels;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Storage;
using Supabase;
using Supabase.Gotrue;
using Supabase.Gotrue.Exceptions;

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

        public async Task<IEnumerable<QuestionObjectSupa>> GetForeignQuestionObject()
        {
            var session = _supabaseClient.Auth.CurrentSession;

            var user = await _supabaseClient.Auth.GetUser(session.AccessToken);
            if (user == null) throw new UnauthorizedAccessException("User not authenticated");

            // Filter by the user's UID
            var response = await _supabaseClient
                .From<QuestionObjectSupa>()
                .Where(x => x.UserId != user.Id)  // Assuming your model has UserId property
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

        public async Task<AuthResult> SignUp(string email, string password, ProfileObject profileObject)
        {

            try
            {
                var session = await _supabaseClient.Auth.SignUp(email, password);

                if (session?.User == null)
                {
                    return AuthResult.Failure("Registration failed");
                }

                var user = session.User;
                Console.WriteLine(user.Id.ToString());

                profileObject.UserId = user.Id;
                await CreateUserProfile(profileObject);

                return AuthResult.Success("Registration successful", session);
            }
            catch (Exception ex)
            {
                // Generic error handling
                return AuthResult.Failure($"Registration error: {ex.Message}");
            }
        }

        public async Task CreateUserProfile(ProfileObject profileObject)
        {
            await _supabaseClient.From<ProfileObject>().Insert(profileObject);
        }

        public async Task<AuthResult> SignIn(string email, string password)
        {
            try
            {
                var session = await _supabaseClient.Auth.SignIn(email, password);

                if (session?.User == null)
                {
                    return AuthResult.Failure("Authentication failed");
                }
                
                await SecureStorage.Default.SetAsync("session_refresh_token", session.RefreshToken);
                await SecureStorage.Default.SetAsync("session_access_token", session.AccessToken);

                return AuthResult.Success("Login successful", session);
            }
            catch (GotrueException ex) when (ex.Message.Contains("Invalid login credentials"))
            {
                // Specific handling for wrong password/email
                return AuthResult.Failure("Incorrect email or password");
            }
            catch (GotrueException ex) when (ex.Message.Contains("Email not confirmed"))
            {
                return AuthResult.Failure("Please verify your email first");
            }
            catch (Exception ex)
            {
                // Generic error handling
                return AuthResult.Failure($"Login error: {ex.Message}");
            }
        }

        public async Task SignOut()
        {
            await _supabaseClient.Auth.SignOut();
        }

        public async Task<bool> RestoreLastSession()
        {
            try
            {
                var refreshToken = await SecureStorage.Default.GetAsync("session_refresh_token");
                var accessToken = await SecureStorage.Default.GetAsync("session_access_token");

                if (refreshToken == null || accessToken == null) return false;
                var session = await _supabaseClient.Auth.SetSession(accessToken, refreshToken);

                if (session == null) return false;

                await SecureStorage.Default.SetAsync("session_refresh_token", session.RefreshToken);
                await SecureStorage.Default.SetAsync("session_access_token", session.AccessToken);

                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task<IEnumerable<ProfileObject>> GetUserProfilesByUsername(string searchString)
        {
            string lowerCaseSearchString = searchString.ToLower();
            var response = await _supabaseClient.From<ProfileObject>()
                .Select("username")
                .Where(x => x.UserName.Contains(lowerCaseSearchString))
                .Get();
            return response.Models.OrderByDescending(b => b.CreatedAt);
        }

        public async Task<ProfileObject> GetUserProfileByUuid(string uuid)
        {
            try
            {
                var response = await _supabaseClient.From<ProfileObject>()
                    .Where(x => x.UserId == uuid)
                    .Single();

                Console.WriteLine(response.UserName);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}