using Supabase.Gotrue;


namespace CrowdOpinion.Models
{
    public class AuthResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public Session? Session { get; }

        private AuthResult(bool success, string message, Session? session)
        {
            IsSuccess = success;
            Message = message;
            Session = session;
        }

        public static AuthResult Success(string message, Session? session)
            => new(true, message, session);

        public static AuthResult Failure(string message)
            => new(false, message, null);
    }
}
