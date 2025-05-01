using AutoMapper;
using Codelabs.BLL.Mappers;
using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using Codelabs.Core.OutputModels;
using Codelabs.DAL;

namespace Codelabs.BLL;
public class ExerciseManager
{
    private readonly LessonRepository _lessonRepository = new();
    private readonly ExerciseRepository _repository = new();
    private readonly Mapper _mapper;

    public ExerciseManager()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new ExerciseMapperProfile()));
        _mapper = new Mapper(config);
    }

    public async Task<ExerciseForCompletingOutputModel> GetExerciseByID(int exerciseID)
    {
        var got = await _repository.GetExerciseByID(exerciseID);
        var model = _mapper.Map<ExerciseForCompletingOutputModel>(got);
        return model;
    }

    public async Task AddExercise(ExerciseInputModel exercise, string lessonDirPath, int lessonID)
    {
        var exerciseDTO = _mapper.Map<ExerciseDTO>(exercise);

        string guid = Guid.NewGuid().ToString();
        string requirementsPath = $"{lessonDirPath}/exercises/e#{guid}.md";

        await FileService.WriteToFile(requirementsPath, exercise.Requirements);

        exerciseDTO.RequirementsPath = requirementsPath;
        await _repository.AddExercise(exerciseDTO, lessonID);
    }

    public async Task EditExercisesForLesson(int lessonID, List<ExerciseInputModel> changedExercises)
    {
        var lessonBD = await _lessonRepository.GetLessonByID(lessonID);

        var newExercises = changedExercises.Where(e => e.ID == null && !e.IsDeleted).ToList();
        var deletedExercises = changedExercises.Where(e => e.IsDeleted && e.ID != null).ToList();
        var editedExercises = changedExercises.Where(e =>
                        lessonBD.Exercises.Any(eBD => eBD.ID == e.ID && !eBD.IsEqual(_mapper.Map<ExerciseDTO>(e)))).ToList();

        foreach (var newExercise in newExercises)
        {
            Console.WriteLine($"CREATED: {newExercise.Name}");
            await AddExercise(newExercise, Path.GetDirectoryName(lessonBD.ContentPath), (int)lessonBD.ID);
        }

        foreach (var deletedExercise in deletedExercises)
        {
            Console.WriteLine($"DELETED: {deletedExercise.Name}");
            await _repository.DeleteExerciseByID((int)deletedExercise.ID);
        }

        foreach (var editedExercise in editedExercises)
        {
            Console.WriteLine($"EDITED: {editedExercise.Name}");
            await EditExerciseByID((int)editedExercise.ID, editedExercise);
        }
    }

    public async Task EditExerciseByID(int ID, ExerciseInputModel changedExercise)
    {
        var exerciseBD = await _repository.GetExerciseByID(ID);
        var exerciseDTO = _mapper.Map<ExerciseDTO>(changedExercise);

        await FileService.WriteToFile(exerciseBD.RequirementsPath, changedExercise.Requirements);
        await _repository.EditExerciseByID((int)changedExercise.ID, exerciseDTO);
    }

    public List<ExerciseInputModel> MapExercisesToInputModels(List<ExerciseOutputModel> exercises)
    {
        var result = _mapper.Map<List<ExerciseInputModel>>(exercises);
        return result;
    }
}