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
        public AuthorInfoDTO? GetAuthorInfoByTIN(string TIN)
        {
            using var context = new Context();
            var authorInfo = context.AuthorInfos.Where(ai => ai.TIN == TIN).FirstOrDefault();
            return authorInfo;
        }

        public void AddAuthorInfo(AuthorInfoDTO authorInfo)
        {
            using var context = new Context();
            context.AuthorInfos.Add(authorInfo);
            context.SaveChanges();
        }

        public UserDTO? GetUserByID(int id)
        {
            using var context = new Context();
            var user = context.Users.Where(u => u.ID==id).FirstOrDefault();
            return user;
        }
            
        public UserDTO? GetUserByLogin(string login)
        {
            using var context = new Context();
            var user = context.Users.Where(u => u.Login == login).FirstOrDefault();
            return user;
        }

        public int? AddUser(UserDTO user)
        {
            using var context = new Context();
            context.Users.Add(user);
            context.SaveChanges();
            int? id = context.Users.ToList()[context.Users.Count() - 1].ID;
            return id;
        }
    }
}
