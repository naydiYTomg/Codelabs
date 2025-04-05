﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.Core.OutputModels
{
    public class UserOutputModel
    {
        public int ID { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public RoleType Role { get; set; }
    }
}
