using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codelabs.Core.InputModels
{
    public class AuthorInfoInputModel
    {
        public int UserID { get; set; }

        public string? TIN { get; set; }

        public string? BankName { get; set; }

        public string? BIC { get; set; }

        public string? SettlementAccount { get; set; }

        public string? CorrespondentAccount { get; set; }
    }
}
