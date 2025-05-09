using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
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

    public bool IsUserBoughtLesson(int userID, int lessonID)
    {
        bool result = _repository.IsUserBoughtLesson(userID, lessonID);
        return result;
    }

    public async Task<int> GetPurchaseIDWhereLessonHasExercise(int exerciseID, int userID)
    {
        var got = await _repository.GetPurchaseIDWhereLessonHasExercise(exerciseID, userID);
        return got;
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

    public async Task MarkTruePurchaseByUserAndLessonID(int userID, int lessonID)
    { 
        await _repository.MarkTruePurchaseByUserAndLessonID(userID, lessonID);
    }
}