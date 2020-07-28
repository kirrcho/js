using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.App.Common
{
    public class MineViewModel
    {
        public MineViewModel()
        {
            this.mines = new List<Mine>();
        }

        public void Add(Mine mine)
        {
            this.mines.Add(mine);
        }

        public List<Mine> mines { get; set; }

        public bool gameEnded { get; set; }
    }
}
