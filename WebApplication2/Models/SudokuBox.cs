using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.App.Common.Constants;

namespace WebApplication2.App.Models
{
    public class SudokuBox
    {
        public SudokuBox(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            this.DetermineSquare();
        }

        public SudokuBox(int row, int col, int number)
            : this(row, col)
        {
            this.Number = number;
        }

        private int row;
        public int Row
        {
            get
            {
                return row;
            }
            private set
            {
                if (value < 0 || value > 8)
                {
                    throw new ArgumentException(ErrorConstants.Sudoku_InvalidRow);
                }
                row = value;
            }
        }

        private int col;
        public int Col
        {
            get
            {
                return col;
            }
            private set
            {
                if (value < 0 || value > 8)
                {
                    throw new ArgumentException(ErrorConstants.Sudoku_InvalidCol);
                }
                col = value;
            }
        }


        private int number;
        public int Number
        {
            get
            {
                return number;
            }
            private set
            {
                if (this.number < 0 || this.number > 9)
                {
                    throw new ArgumentException(ErrorConstants.Sudoku_InvalidNumber);
                }
                number = value;
            }
        }

        public int Square { get; private set; }

        public void SetNumber(int number)
        {
            this.Number = number;
        }

        private void DetermineSquare()
        {
            for (var i = 0; i < 9; i += 3)
            {
                for (var k = 0; k < 9; k += 3)
                {
                    this.Square++;
                    if (this.Row >= i && this.Row < i + 3 && this.Col >= k && this.Col < k + 3)
                    {
                        return;
                    }
                }
            }
        }

        public static int DetermineSquare(int row,int col)
        {
            var square = 0;

            for (var i = 0; i < 9; i += 3)
            {
                for (var k = 0; k < 9; k += 3)
                {
                    square++;
                    if (row >= i && row < i + 3 && col >= k && col < k + 3)
                    {
                        return square;
                    }
                }
            }

            return square;
        }
    }
}
