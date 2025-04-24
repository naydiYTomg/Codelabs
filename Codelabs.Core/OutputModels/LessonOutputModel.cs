using Codelabs.Core.DTOs;
using Codelabs.Core.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.Core.OutputModels
{
    public class LessonOutputModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Cost { get; set; }

        public string? Content { get; set; }

        public LanguageOutputModel? Language { get; set; }

        public UserOutputModel? Author { get; set; }

        public List<ExerciseOutputModel> Exercises { get; set; } = new();
    }
}
