using Codelabs.Core;
using Codelabs.Core.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public UserDTO? GetAuthorByID(int UserID)
        {
            using var context = new Context();
            var author = context.Users
                .Where(u => u.ID == UserID && u.Role == RoleType.Author)
                .FirstOrDefault();
            return author;

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

        public async Task<UserDTO> GetFirstUserById(int id)
        {
            await using var context = new Context();
            var user = await context.Users.SingleAsync(x => x.ID == id);
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
            int? id = context.Users.ToList().Last().ID;
            return id;
        }
    }
}