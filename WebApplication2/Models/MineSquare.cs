using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.App.Models
{
    public class MineSquare
    {
        public MineSquare(int row,int col)
        {
            this.HasMine = false;
            this.IsFlagged = false;
            this.Row = row;
            this.Col = col;
        }

        public bool HasMine { get; set; }

        public bool IsFlagged { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
