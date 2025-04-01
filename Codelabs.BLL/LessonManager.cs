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

    public async Task<List<LessonForShowcaseOutputModel>> GetAllExistingLessonsByAuthor(int authorId)
    {
        var got = await _repository.GetAllExistingLessonsByAuthor(authorId);
        var output = _mapper.Map<List<LessonForShowcaseOutputModel>>(got);
        return output;
    }
}