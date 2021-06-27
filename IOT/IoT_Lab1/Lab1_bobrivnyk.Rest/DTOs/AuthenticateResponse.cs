using Lab1_bobrivnyk.Rest.Models;


namespace Lab1_bobrivnyk.Rest.DTOs
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Token = token;

        }
    }
}
