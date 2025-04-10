using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.Core.InputModels
{
    public class LessonInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Cost { get; set; }

        public int? LanguageID { get; set; }

        public int? AuthorID { get; set; }

        public string? Content { get; set; }
    }
}
