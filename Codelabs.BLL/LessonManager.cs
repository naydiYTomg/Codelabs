using AutoMapper;
using Codelabs.BLL.Mappers;
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

    public async Task<LessonForShowcaseOutputModel> GetLessonForShowcaseByID(int lessonID)
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

    public List<LanguageOutputModel> GetAllLanguages()
    {
        var DTOs = _repository.GetAllLanguages();
        var outputModels = _mapper.Map<List<LanguageOutputModel>>(DTOs);
        return outputModels;
    }

    public async Task UpdateLessonByID(int ID, LessonInputModel changedLesson)
    {
        var lessonDTO = _mapper.Map<LessonDTO>(changedLesson);
        var lessonBD = await _repository.GetLessonByID(ID);

        if (lessonBD != null)
        {
            _repository.UpdateLessonByID(ID, lessonDTO, changedLesson.LanguageID);

            using (var writer = new StreamWriter(lessonBD.ContentPath, false))
            {
                writer.Write(changedLesson.Content);
            }

            for (int i = 0;i < changedLesson.Exercises.Count; i++)
            {
                var exercise = changedLesson.Exercises[i];

                if (exercise.ID != null)
                {
                    var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);

                    _repository.UpdateExerciseByID((int)exercise.ID, exerciseDTO);

                    using (var writer = new StreamWriter(lessonBD.Exercises[i].RequirementsPath, false))
                    {
                        writer.WriteLine(exercise.Requirements);
                    }
                }
                else
                {
                    List<string> listLessonDirPath = lessonBD.ContentPath.Split("/").ToList();
                    listLessonDirPath.RemoveAt(listLessonDirPath.Count - 1);
                    string lessonDirPath = String.Join("/", listLessonDirPath);

                    AddExercise(exercise, lessonDirPath, (int)lessonBD.ID);
                }
            }
        }
    }

    public void AddExercise(ExerciseInputModel exercise, string lessonDirPath, int lessonID)
    {
        if (!Directory.Exists(Path.GetFullPath($"{lessonDirPath}/exercises")))
        {
            Directory.CreateDirectory(Path.GetFullPath($"{lessonDirPath}/exercises"));
        }

        string exerciseGuid = Guid.NewGuid().ToString();

        using (var writer = new StreamWriter($"{Path.GetFullPath(lessonDirPath)}/exercises/e#{exerciseGuid}.md", false))
        {
            writer.Write(exercise.Requirements);
        }

        var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);
        exerciseDTO.RequirementsPath = $"{lessonDirPath}/exercises/e#{exerciseGuid}.md";
        _repository.AddExercise(exerciseDTO, lessonID);
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
            AddExercise(exercise, lessonDirPath, (int)newLesson.ID);
        }
    }

    public async Task<LessonOutputModel?> GetLessonByID(int ID)
    {
        var DTO = await _repository.GetLessonByID(ID);

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