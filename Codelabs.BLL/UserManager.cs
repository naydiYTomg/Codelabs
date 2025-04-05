using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.BLL
{
    public class UserManager
    {
        private UserRepository _userRepository = new UserRepository();
        private Mapper _mapper;

        public UserManager() 
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
            });
            _mapper = new Mapper(configuration);
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
