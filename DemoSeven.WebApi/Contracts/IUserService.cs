using System;
using DemoSeven.WebApi.Models.Dtos;
using DemoSeven.WebApi.Models.Entities;

namespace DemoSeven.WebApi.Contracts
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        User GetById(Guid id);
    }
}