using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoWebAPIWinForms.Model
{
    public class PW_Teams
    {
        public System.Guid TeamID { get; set; }
        public string TeamDesc { get; set; }
        public bool IsActive { get; set; }

    }
}
