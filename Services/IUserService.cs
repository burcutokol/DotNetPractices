using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreWebApi.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        void GetById(int id);
        User GetByIdQuery([FromQuery] int id);
        void AddUser([FromBody] User model);
        void UpdateUser([FromBody] User model);
        void DeleteUser(int id);
    }
}
