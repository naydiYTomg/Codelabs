namespace Codelabs.Core.DTOs;

public class ExerciseDTO
{
    public int ID { get; set; }

    public string Name { get; set; }
    
    public string RequirementsPath { get; set; }

    public string DesiredOutput { get; set; }

    public string? ProgramInput { get; set; }

    public bool IsDeleted { get; set; }

    public LessonDTO Lesson { get; set; }
    
    public List<SolutionDTO>? Solutions { get; set; }

    public bool IsEqual(ExerciseDTO ex)
    {
        return ex.Name == this.Name
            && ex.DesiredOutput == this.DesiredOutput
            && ex.ProgramInput == this.ProgramInput;
    }

    public static bool operator ==(ExerciseDTO left, ExerciseDTO right) => left.Name == right.Name
                                                                           && left.DesiredOutput == right.DesiredOutput
                                                                           && left.ProgramInput == right.ProgramInput;
    public static bool operator !=(ExerciseDTO left, ExerciseDTO right) => left.Name != right.Name
                                                                           || left.DesiredOutput != right.DesiredOutput
                                                                           || left.ProgramInput != right.ProgramInput;
}