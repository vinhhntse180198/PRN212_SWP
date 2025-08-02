using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class UserService
    {
        private UserRepository _userRepository = new();

        public void AddUser(User x)
        {
            _userRepository.Add(x);
        }

        public User? CheckAuthen(string username, string password)
        {
            return _userRepository.GetAuthen(username, password);
        }

        public bool CheckEmailExist(string email)
        {
            return _userRepository.checkEmailExist(email);
        }

        public bool CheckUsernameExist(string username)
        {
            return _userRepository.CheckUsernameExist(username);
        }
    }
}