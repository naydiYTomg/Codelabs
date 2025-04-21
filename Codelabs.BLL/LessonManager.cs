using AutoMapper;
using Codelabs.BLL.MapperProfiles;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL;

public class LessonManager
{
    private readonly UserRepository _userRepository = new();
    private readonly LessonRepository _repository = new();
    private readonly Mapper _mapper;

    public LessonManager()
    {
        var config = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new LessonMapperProfile());
            cfg.AddProfile(new LanguageMapperProfile());
            cfg.AddProfile(new ExerciseMapperProfile());
        });
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

    public List<LanguageOutputModel> GetAllLanguages()
    {
        var DTOs = _repository.GetAllLanguages();
        var outputModels = _mapper.Map<List<LanguageOutputModel>>(DTOs);
        return outputModels;
    }

    public void AddLesson(LessonInputModel lesson) 
    {
        var lessonDTO = _mapper.Map<LessonDTO>(lesson);
        lessonDTO.Author = _userRepository.GetUserByID((int)lesson.AuthorID);
        lessonDTO.Language = _repository.GetLanguageByID((int)lesson.LanguageID);
        var newLesson = _repository.AddLesson(lessonDTO);

        foreach (ExerciseInputModel exercise in lesson.Exercises)
        {
            var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);
            exerciseDTO.Lesson = newLesson;
            _repository.AddExercise(exerciseDTO);
        }

        string lessonGuid = Guid.NewGuid().ToString();
        string dirPath = Path.GetFullPath($"data/u#{lesson.AuthorID}");
        string filePath = $"{dirPath}/{lessonGuid}.md";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        using var writer = new StreamWriter(filePath, false);
        writer.Write(lesson.Content);
    }
}