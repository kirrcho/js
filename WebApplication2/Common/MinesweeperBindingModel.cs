using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.App.Common
{
    public class MinesweeperBindingModel
    {
        public string encodedBoard { get; set; }

        public string currentId { get; set; }

        public List<string> flagged { get; set; }
    }
}
