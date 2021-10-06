using System;
using DemoSeven.WebApi.Contracts;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;

namespace DemoSeven.WebApi.Services
{
    public sealed class UserService : IUserService
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}