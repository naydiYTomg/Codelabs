using Codelabs.Core;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;
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

        public UserDTO GetUserByID(int ID)
        {
            using var context = new Context();
            var user = context.Users.Where(u => u.ID == ID).FirstOrDefault();
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

        public UserDTO? AddUser(UserDTO user)
        {
            using var context = new Context();
            var newUser = context.Users.Add(user).Entity;
            context.SaveChanges();
            return newUser;
        }

        public async Task EditUserByID(int ID, UserDTO changedUser)
        {
            using var context = new Context();
            var user = await context.Users
                                    .SingleAsync(u => u.ID == ID);

            if (user != null)
            {
                user.Name = changedUser.Name ?? user.Name;
                user.Surname = changedUser.Surname ?? user.Surname;
                user.Email = changedUser.Email ?? user.Email;
                await context.SaveChangesAsync();
            }
        }

        public DateTime GetLastTimeVisitedLessonsPage(int ID)
        {
            using var context = new Context();
            var user = context.Users.Single(u => u.ID == ID);
            var lastvisit = (DateTime)user.LastVisitedLessonsPage;

            return lastvisit;
        }

        public async Task UpdateUserVisitedLessonsPageTime(int ID)
        {
            using var context = new Context();
            var user = await context.Users
                                    .SingleAsync(u => u.ID == ID);

            if (user != null)
            {
                user.LastVisitedLessonsPage = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
        }
    }
}