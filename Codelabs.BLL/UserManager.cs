using AutoMapper;
using Codelabs.BLL.MapperProfiles;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UserOutputModel? GetUserByID(int id)
        {
            var DTO = _userRepository.GetUserByID(id);
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
    }
}
