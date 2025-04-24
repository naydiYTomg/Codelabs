using AutoMapper;
using Codelabs.BLL.MapperProfiles;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL;

public class LessonManager
{
    private readonly LessonRepository _repository = new();
    private readonly Mapper _mapper;

    public LessonManager()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new LessonMapperProfile()));
        _mapper = new Mapper(config);
    }

    public async Task<List<LessonForShowcaseOutputModel>> GetAllExistingLessons()
    {
        var got = await _repository.GetAllExistingLessons();
        var output = _mapper.Map<List<LessonForShowcaseOutputModel>>(got);
        return output;
    }

    public async Task<List<LessonForShowcaseOutputModel>> GetAllExistingLessonsByAuthor(int authorID)
    {
        var got = await _repository.GetAllExistingLessonsByAuthor(authorID);
        var output = _mapper.Map<List<LessonForShowcaseOutputModel>>(got);
        return output;
    }

    public async Task<bool> GetIfThisLessonBelongsToAuthor(int authorID, int lessonID)
    {
        bool belongs = false;
        var got = await _repository.GetAllExistingLessonsByAuthor(authorID);
        foreach (var lesson in got)
        {
            if (lessonID == lesson.ID)
            {
                belongs = true;
                break;
            }
        }
        Console.WriteLine(belongs);
        return belongs;
    }

    public async Task<LessonForShowcaseOutputModel> GetLessonByID(int lessonID)
    {
        var got = await _repository.GetLessonByID(lessonID);
        var output = _mapper.Map<LessonForShowcaseOutputModel>(got);
        return output;
    }

    public async Task<List<LessonForShowcaseOutputModel>> GetAllExistingLessonsFromPurchasesByUser(int userID)
    {
        var got = await _repository.GetAllExistingLessonsFromPurchasesByUser(userID);
        var output = _mapper.Map<List<LessonForShowcaseOutputModel>>(got);
        return output;
    }
    
}