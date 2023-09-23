using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using BookStoreWebApi.Services;

namespace BookStoreWebApi.Services
{
    public class UserService : IUserService
    {
        private List<User> UserList;

        public UserService ()
        {
            if (UserList == null)
            {
                UserList = new Faker<User>().RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .Generate(100);
            }

        }


        [HttpGet]
        public List<User> GetUsers()
        {
            return UserList.OrderBy(x => x.Id).ToList();
        }
        [HttpGet("{id}")] //by route
        public void GetById(int id)
        {
            var User = UserList.Where(u => u.Id == id).FirstOrDefault();

        }
        [HttpGet("GetByQuery")]
        public User GetByIdQuery([FromQuery] int id)
        {
            return UserList.Where(u => u.Id == id).FirstOrDefault();
        }
        [HttpPost]
        public void AddUser([FromBody] User model)
        {
            var User = UserList.Where(u => u.Id == model.Id).FirstOrDefault();
            UserList.Add(model);

        }
        [HttpPut]
        public void UpdateUser([FromBody] User model)
        {
            var User = UserList.Where(u => u.Id == model.Id).FirstOrDefault();

            User.FirstName = model.FirstName;
            User.LastName = model.LastName;
            User.Address = model.Address;

        }
        [HttpDelete]
        public void DeleteUser(int id)
        {
            var User = UserList.Where(u => u.Id == id).FirstOrDefault();
           
            UserList.Remove(User);
        }
    }
}
