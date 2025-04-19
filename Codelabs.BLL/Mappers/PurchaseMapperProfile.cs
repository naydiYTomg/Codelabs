using AutoMapper;
using Codelabs.Core.DTOs;
using Codelabs.Core.OutputModels;

namespace Codelabs.BLL.Mappers;

public class PurchaseMapperProfile : Profile
{
    public PurchaseMapperProfile()
    {
        CreateMap<PurchaseDTO, PurchaseForBuyingOutputModel>();
    }
    
}