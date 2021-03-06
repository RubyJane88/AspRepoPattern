using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DemoSeven.WebApi.Contracts;
using DemoSeven.WebApi.Helpers;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DemoSeven.WebApi.Services
{
    //Cannot be INTERNAL
    public sealed class UserService : IUserService
    {
         private readonly List<User> _users = new()
        {
            new User
             {
                 Id = new Guid("b01e8a73-3b16-4a3b-bb3a-cc6974281447"),
                FirstName = "Devlin",
                LastName = "Duldulao",
                Email = "webmasterdevlin@gmail.com",
                Password = "Pass123!"
             },
            
            new User
            {
                Id = new Guid("b01e8a73-3b16-4a3b-bb3a-cc6974281448"),
                FirstName = "Buby",
                LastName = "Bee",
                Email = "arjaycee_8@yahoo.com",
                Password = "Pass123!"
            },
            new User
            {
                Id = new Guid("b01e8a73-3b16-4a3b-bb3a-cc6974281449"),
                FirstName = "Kairo",
                LastName = "Juan",
                Email = "kairo@yahoo.com",
                Password = "Pass123!"
            },
            
            
        };

        private readonly AuthSettings _authSettings;

        public UserService(IOptions<AuthSettings> appSettings)
        {
            _authSettings = appSettings.Value;
        }
        
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
                return null;

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public User GetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);
        private string GenerateJwtToken(User user)
        {
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sub", user.Id.ToString()), new Claim("email", user.Email) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

       
       
    }
}