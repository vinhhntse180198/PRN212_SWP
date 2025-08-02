using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private DnaTestingContext _context;

        public void Add(User x)
        {
            _context = new();
            x.Role = "CUSTOMER";
            _context.Users.Add(x);
            _context.SaveChanges();
        }

        public User? GetAuthen(string username, string password)
        {
            _context = new();
            return _context.Users.FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()) && u.Password == password);
        }

        public bool checkEmailExist(string email)
        {
            _context = new();
            return _context.Users.Any(u => u.Email.ToLower().Equals(email.ToLower()));
        }

        public bool CheckUsernameExist(string username)
        {
            _context = new();
            return _context.Users.Any(u => u.Username.ToLower().Equals(username.ToLower()));
        }
    }
}