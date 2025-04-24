using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.Core.OutputModels
{
    public class ExerciseOutputModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }

        public string DesiredOutput { get; set; }

        public string? ProgramInput { get; set; }

        public string Requirements { get; set; }
    }
}
