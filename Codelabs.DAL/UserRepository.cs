using Codelabs.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.DAL
{
    public class UserRepository
    {

        private Context _context = new Context();

        public AuthorInfoDTO? GetAuthorInfoByTIN(string TIN)
        {
            var authorInfo = _context.AuthorInfos.Where(ai => ai.TIN == TIN).FirstOrDefault();
            return authorInfo;
        }

        public void AddAuthorInfo(AuthorInfoDTO authorInfo)
        {
            _context.AuthorInfos.Add(authorInfo);
            _context.SaveChanges();
        }

        public UserDTO? GetUserByID(int id)
        {
            var user = _context.Users.Where(u => u.ID==id).FirstOrDefault();
            return user;
        }
            
        public UserDTO? GetUserByLogin(string login)
        {
            var user = _context.Users.Where(u => u.Login == login).FirstOrDefault();
            return user;
        }

        public int? AddUser(UserDTO user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            int? id = _context.Users.ToList()[_context.Users.Count() - 1].ID;
            return id;
        }
    }
}
