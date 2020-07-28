using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.App.Common
{
    public class Mine
    {
        public Mine(string currentId,int numberToShow)
        {
            this.currentId = currentId;
            this.numberToShow = numberToShow;
        }

        public string currentId { get; set; }

        public int numberToShow { get; set; }
    }
}
