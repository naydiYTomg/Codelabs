using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;
using System;
using System.Runtime.CompilerServices;

namespace Codelabs.BLL;
public class LessonManager
{
    private readonly ExerciseRepository _exerciseRepository = new();
    private readonly ExerciseManager _exerciseManager = new();
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

    public async Task DeleteLessonByID(int ID)
    {
        await _repository.DeleteLessonByID(ID);
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

    public async Task EditLessonByID(int ID, LessonInputModel changedLesson)
    {
        var lessonDTO = _mapper.Map<LessonDTO>(changedLesson);
        var lessonBD = await _repository.GetLessonByID(ID);

        if (lessonBD != null)
        {
            await FileService.WriteToFile(lessonBD.ContentPath, changedLesson.Requirements);
            lessonDTO.ContentPath = lessonBD.ContentPath;

            await _repository.EditLessonByID((int)lessonBD.ID, lessonDTO, changedLesson.LanguageID);
            await _exerciseManager.EditExercisesForLesson((int)lessonBD.ID, changedLesson.Exercises);
        }
    }

    public async Task AddLesson(LessonInputModel lesson) 
    {
        var lessonDTO = _mapper.Map<LessonDTO>(lesson);

        string guid = Guid.NewGuid().ToString();
        string contentPath = $"wwwroot/data/u#{lesson.AuthorID}/l#{guid}/content.md";

        await FileService.WriteToFile(contentPath, lesson.Requirements);

        lessonDTO.ContentPath = contentPath;
        var newLesson = await _repository.AddLesson(lessonDTO, lesson.AuthorID, lesson.LanguageID);

        foreach (ExerciseInputModel exercise in lesson.Exercises)
        {
            await _exerciseManager.AddExercise(exercise, Path.GetDirectoryName(contentPath), (int)newLesson.ID);
        }
    }

    public async Task<LessonOutputModel?> GetLessonByID(int ID)
    {
        var DTO = await _repository.GetLessonByID(ID);

        if (DTO != null)
        {
            var outputModel = _mapper.Map<LessonOutputModel>(DTO);

            outputModel.Content = await FileService.ReadFile(DTO.ContentPath);

            for (int i = 0; i < DTO.Exercises.Count; i++)
            {
                outputModel.Exercises[i].Requirements = await FileService.ReadFile(DTO.Exercises[i].RequirementsPath);
            }

            return outputModel;
        }
        else
        {
            return null;
        }
    }
}