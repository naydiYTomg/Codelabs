namespace Codelabs.Core.DTOs;

public class PurchaseDTO
{
    public int ID { get; set; }

    public bool IsVisited { get; set; }

    public UserDTO User { get; set; }

    public LessonDTO Lesson { get; set; }
    //������ SolutionDTO
    //���� ���������� ������� ������� DateTime.Now
}