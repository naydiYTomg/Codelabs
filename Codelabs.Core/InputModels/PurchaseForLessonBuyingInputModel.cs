namespace Codelabs.Core.InputModels;

public class PurchaseForLessonBuyingInputModel
{
    public required int UserID { get; set; }

    public required int LessonID { get; set; }

    public required DateTime Date { get; set; }
}