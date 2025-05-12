using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL
{
    public class UserManager
    {
        private UserRepository _userRepository = new();
        private Mapper _mapper;

        public UserManager() 
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
                cfg.AddProfile(new AuthorInfoMapperProfile());
            });
            _mapper = new Mapper(configuration);
        }

        public void AddAuthorInfo(AuthorInfoInputModel authorInfo)
        {
            var DTO = _mapper.Map<AuthorInfoDTO>(authorInfo);
            _userRepository.AddAuthorInfo(DTO);
        }

        public AuthorInfoOutputModel? GetAuthorInfoByTIN(string TIN)
        {
            var DTO = _userRepository.GetAuthorInfoByTIN(TIN);
            var outputModel = _mapper.Map<AuthorInfoOutputModel>(DTO);
            return outputModel;
        }

        public UserOutputModel? GetAuthorByID(int UserID)
        {
            var DTO = _userRepository.GetAuthorByID(UserID);
            var outputModel = _mapper.Map<UserOutputModel>(DTO);
            return outputModel;
        }

        public UserOutputModel GetUserByID(int ID)
        {
            var DTO = _userRepository.GetUserByID(ID);
            var outputModel = _mapper.Map<UserOutputModel>(DTO);
            return outputModel;
        }

        public UserOutputModel? GetUserByLogin(string login)
        {
            var DTO = _userRepository.GetUserByLogin(login);
            var outputModel = _mapper.Map<UserOutputModel>(DTO);
            return outputModel;
        }

        public int? AddUser(UserInputModel user)
        {
            var DTO = _mapper.Map<UserDTO>(user);
            var newUserID = _userRepository.AddUser(DTO);
            return newUserID;
        }

        public void EditUserByID(int ID, UserInputModel changedUser)
        {
            var userDTO = _mapper.Map<UserDTO>(changedUser);
            var userBD = _userRepository.GetUserByID(ID);

            if (userBD != null)
            {
                _userRepository.EditUserByID((int)userBD.ID, userDTO);
            }
        }

        public void UpdateUserVisitedLessonsPageTime(int ID)
        {
            var userBD = _userRepository.GetUserByID(ID);

            if (userBD != null)
            {
                _ = _userRepository.UpdateUserVisitedLessonsPageTime((int)userBD.ID);
            }
        }
    }
}