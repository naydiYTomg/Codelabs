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
            cfg.AddProfile(new UserMapperProfile());
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

    public void UpdateLessonByID(int ID, LessonInputModel changedLesson)
    {
        var lessonDTO = _mapper.Map<LessonDTO>(changedLesson);
        var lessonBD = _repository.GetLessonByID(ID);

        if (lessonBD != null)
        {
            _repository.UpdateLessonByID(ID, lessonDTO, changedLesson.LanguageID);

            using (var writer = new StreamWriter(lessonBD.ContentPath, false))
            {
                writer.Write(changedLesson.Content);
            }

            for (int i = 0;i < lessonBD.Exercises.Count; i++)
            {
                var exerciseDTO = _mapper.Map<ExerciseDTO>(changedLesson.Exercises[i]);

                _repository.UpdateExerciseByID(lessonBD.Exercises[i].ID, exerciseDTO);

                using (var writer = new StreamWriter(lessonBD.Exercises[i].RequirementsPath, false))
                {
                    writer.WriteLine(changedLesson.Exercises[i].Requirements);
                }
            }
        }
    }

    public void AddLesson(LessonInputModel lesson) 
    {
        var lessonDTO = _mapper.Map<LessonDTO>(lesson);

        string lessonGuid = Guid.NewGuid().ToString();
        string lessonDirPath = $"wwwroot/data/u#{lesson.AuthorID}/l#{lessonGuid}";

        if (!Directory.Exists(Path.GetFullPath(lessonDirPath)))
        {
            Directory.CreateDirectory(Path.GetFullPath(lessonDirPath));
            Directory.CreateDirectory(Path.GetFullPath($"{lessonDirPath}/exercises"));
        }

        using (var writer = new StreamWriter($"{Path.GetFullPath(lessonDirPath)}/content.md", false))
        {
            writer.Write(lesson.Content);
        }

        lessonDTO.ContentPath = $"{lessonDirPath}/content.md";
        var newLesson = _repository.AddLesson(lessonDTO, lesson.AuthorID, lesson.LanguageID);

        foreach (ExerciseInputModel exercise in lesson.Exercises)
        {

            string exerciseGuid = Guid.NewGuid().ToString();

            using (var writer = new StreamWriter($"{Path.GetFullPath(lessonDirPath)}/exercises/e#{exerciseGuid}.md", false))
            {
                writer.Write(exercise.Requirements);
            }

            var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);
            exerciseDTO.RequirementsPath = $"{lessonDirPath}/exercises/e#{exerciseGuid}.md";
            _repository.AddExercise(exerciseDTO, newLesson.ID);
        }
    }

    public LessonOutputModel? GetLessonByID(int ID)
    {
        var DTO = _repository.GetLessonByID(ID);

        if (DTO != null)
        {
            var outputModel = _mapper.Map<LessonOutputModel>(DTO);

            using (var reader = new StreamReader(DTO.ContentPath, false))
            {
                outputModel.Content = reader.ReadToEnd();
            }

            for (int i = 0; i < DTO.Exercises.Count; i++)
            {
                using (var reader = new StreamReader(DTO.Exercises[i].RequirementsPath, false))
                {
                    outputModel.Exercises[i].Requirements = reader.ReadToEnd();
                }
            }

            return outputModel;
        }
        else
        {
            return null;
        }
    }

    public List<ExerciseInputModel> MapExercisesToInputModels(List<ExerciseOutputModel> exercises)
    {
        var result = _mapper.Map<List<ExerciseInputModel>>(exercises);
        return result;
    }
}