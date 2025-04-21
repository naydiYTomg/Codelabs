using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.BLL.MapperProfiles
{
    public class ExerciseMapperProfile : Profile
    {
        public ExerciseMapperProfile()
        {
            CreateMap<ExerciseInputModel, ExerciseDTO>();
        }
    }
}
