using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL;

public class PurchaseManager
{
    private readonly PurchaseRepository _repository = new();
    private readonly Mapper _mapper;

    public PurchaseManager()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new PurchaseMapperProfile()));
        _mapper = new Mapper(config);
    }


    public async Task<bool> IsUserBoughtLesson(int lessonID, int userID)
    {
        var got = await _repository.GetAllUserPurchases(userID);
        var models = _mapper.Map<List<PurchaseForBuyingOutputModel>>(got);
        return models.Any(model => model.Lesson.ID == lessonID);
    }


    public async Task CreatePurchase(PurchaseForLessonBuyingInputModel model)
    {
        var dto = new PurchaseDTO
        {
            IsVisited = false,
            Date = model.Date,
        };
        await _repository.CreatePurchase(dto, model.UserID, model.LessonID);
    }
}