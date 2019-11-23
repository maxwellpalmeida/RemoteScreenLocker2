using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenLocker.Models
{
    public class Instructions
    {
        public class RootInstructions
        {
            public string target { get; set; }
            public bool status { get; set; }
            public string message { get; set; }
            public bool IsCloseAllowed { get; set; }
        }


    }
}
