using AutoMapper;
using Lab1_bobrivnyk.Rest.Configurations;
using Lab1_bobrivnyk.Rest.Context;
using Lab1_bobrivnyk.Rest.DTOs;
using Lab1_bobrivnyk.Rest.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_bobrivnyk.Rest.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
                {
                    new User { Id = 1, Email = "test", Password = "test" }
                };

        private readonly AppSettings _appSettings;
        private AzureDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IOptions<AppSettings> appSettings/*, AzureDbContext context, IMapper mapper*/)
        {
            _appSettings = appSettings.Value;
            /*_context = context;
            _mapper = mapper;*/
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest DTO)
        {
            var user = _users.SingleOrDefault(x => x.Email == DTO.Email && x.Password == DTO.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 hrs
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
