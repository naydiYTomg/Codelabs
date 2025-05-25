using Codelabs.Core.OutputModels;

namespace Codelabs.UI.Components.InternalTypes
{
    public class LessonFilter
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public FilterGroup Group { get; set; }
        public FilterType Type { get; set; }
        public Func<LessonForShowcaseOutputModel, bool> Func { get; set; }
    }
}
