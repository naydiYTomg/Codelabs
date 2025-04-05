using Codelabs.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.Core.InputModels
{
    public class UserInputModel
    {
        public int? ID { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public RoleType Role { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
    }
}
