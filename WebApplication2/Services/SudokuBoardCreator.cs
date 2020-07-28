using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.App.Common;
using WebApplication2.App.Common.Constants;
using WebApplication2.App.Models;

namespace WebApplication2.App.Services
{
    public class SudokuBoard
    {
        public SudokuBoard()
        {
            CreateBoard();
        }

        public SudokuBoard(List<SudokuBindingModel> boxes)
        {
            foreach (var box in boxes)
            {
                this.board.Add(new SudokuBox(box.Row, box.Col, box.Value));
            }
        }

        private List<SudokuBox> board = new List<SudokuBox>();
        public List<SudokuBox> Board
        {
            get
            {
                return this.board;
            }
        }

        private List<SudokuBox> solvedBoard = new List<SudokuBox>();

        public static bool IsSudokuCorrect(List<SudokuBindingModel> boxes)
        {
            foreach (var box in boxes)
            {
                if (box.Value == 0)
                {
                    return false;
                }
                var boxesInRow = boxes.Where(p => p.Row == box.Row);
                var boxesInCol = boxes.Where(p => p.Col == box.Col);
                var boxesInSquare = boxes.Where(p => SudokuBox.DetermineSquare(p.Row, p.Col) == SudokuBox.DetermineSquare(box.Row, box.Col));
                if (boxesInRow.Where(p => p.Value == box.Value).Count() != 1 ||
                    boxesInCol.Where(p => p.Value == box.Value).Count() != 1 ||
                    boxesInSquare.Where(p => p.Value == box.Value).Count() != 1)
                {
                    return false;
                }
            }
            return true;
        }

        private List<SudokuBox> CreateBoard()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var k = 0; k < 9; k++)
                {
                    this.board.Add(new SudokuBox(i, k, 0));
                }
            }

            for (var i = 0; i < 9; i++)
            {
                var failureCount = 0;
                var numbersToAppend = new List<int>();
                for (var k = 0; k < 9; k++)
                {
                    var currentBox = this.board.FirstOrDefault(p => p.Row == i && p.Col == k);
                    int potentialNumber = CreateValidNumberOrReturnNull(currentBox.Row,currentBox.Col,currentBox.Square, failureCount);
                    if (potentialNumber == 0)
                    {
                        for (var c = 0; c < 9; c++)
                        {
                            this.board.FirstOrDefault(p => p.Row == i && p.Col == c).SetNumber(0);
                        }
                        i--;
                        numbersToAppend = new List<int>();
                        break;
                    }
                    board.FirstOrDefault(p => p.Row == i && p.Col == k).SetNumber(potentialNumber);
                    numbersToAppend.Add(potentialNumber);
                }
            }

            foreach (var box in this.board)
            {
                solvedBoard.Add(new SudokuBox(box.Row,box.Col,box.Number));
            }

            var failedCounter = 0;
            while(failedCounter < 20)
            {
                var numberGenerator = new Random();
                var randomRow = numberGenerator.Next(0, 9);
                var randomCol = numberGenerator.Next(0, 9);
                var box = board.FirstOrDefault(p => p.Row == randomRow && p.Col == randomCol);
                if (box.Number == 0)
                {
                    continue;
                }
                var number = box.Number;
                box.SetNumber(0);
                var isSudokuSolved = TryToSolveSudoku();
                if (!isSudokuSolved)
                {
                    box.SetNumber(number);
                    failedCounter++;
                    continue;
                }
                failedCounter = 0;
            }

            return board;
        }

        private bool TryToSolveSudoku()
        {
            List<SudokuBox> dummyBoard = new List<SudokuBox>();

            foreach (var box in this.board)
            {
                dummyBoard.Add(new SudokuBox(box.Row,box.Col,box.Number));
            }

            var changeHappened = true;
            while (changeHappened)
            {
                changeHappened = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        var box = dummyBoard.FirstOrDefault(p => p.Row == row && p.Col == col);
                        if (box.Number == 0)
                        {
                            var number = CheckIfNumberCanBePlacedInPosition(box.Row, box.Col, box.Square,dummyBoard);
                            if (number != 0)
                            {
                                box.SetNumber(number);
                                changeHappened = true;
                            }
                        }
                    }
                }
            }

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var rightNumber = this.solvedBoard.FirstOrDefault(p => p.Row == row && p.Col == col).Number;
                    var number = dummyBoard.FirstOrDefault(p => p.Row == row && p.Col == col).Number;
                    if (rightNumber != number)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckIfSudokuIsCorrect()
        {
            List<SudokuBox> dummyBoard = new List<SudokuBox>();

            foreach (var box in this.board)
            {
                dummyBoard.Add(new SudokuBox(box.Row, box.Col, box.Number));
            }

            var changeHappened = true;
            while (changeHappened)
            {
                changeHappened = false;
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        var box = dummyBoard.FirstOrDefault(p => p.Row == row && p.Col == col);
                        if (box.Number == 0)
                        {
                            var number = CheckIfNumberCanBePlacedInPosition(box.Row, box.Col, box.Square, dummyBoard);
                            if (number != 0)
                            {
                                box.SetNumber(number);
                                changeHappened = true;
                            }
                        }
                    }
                }
            }

            foreach (var box in dummyBoard)
            {
                if (box.Number == 0)
                {
                    return false;
                }
                if (dummyBoard.FirstOrDefault(p => p.Row == box.Row && p.Col != box.Col && p.Number == box.Number) != null)
                {
                    return false;
                }
                if (dummyBoard.FirstOrDefault(p => p.Row != box.Row && p.Col == box.Col && p.Number == box.Number) != null)
                {
                    return false;
                }
            }
            return true;
        }

        private int CheckIfNumberCanBePlacedInPosition(int row,int col,int square,List<SudokuBox> currentBoard)
        {
            var numbersThatCanBePlaced = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                var rowCheck = (CheckIfNumberCanBeInRow(row, col, i,currentBoard));
                var colCheck = (CheckIfNumberCanBeInCol(row, col, i,currentBoard));
                var squareCheck = (CheckIfNumberCanBeInSquare(row, col, square,i,currentBoard));
                if (rowCheck && colCheck && squareCheck)
                {
                    numbersThatCanBePlaced.Add(i);
                }
            }
            if (numbersThatCanBePlaced.Count == 1)
            {
                return numbersThatCanBePlaced[0];
            }
            return 0;
        }

        private int CreateValidNumberOrReturnNull(int row,int col,int square, int failureCount)
        {
            if (failureCount > 50)
            {
                return 0;
            }
            var numberGenerator = new Random();
            var randomNumber = numberGenerator.Next(1,10);
            if (!CheckIfNumberCanBeInRow(row,col,randomNumber, this.board))
            {
                failureCount++;
                randomNumber = CreateValidNumberOrReturnNull(row,col,square, failureCount);
            }
            else if (!CheckIfNumberCanBeInCol(row,col,randomNumber, this.board))
            {
                failureCount++;
                randomNumber = CreateValidNumberOrReturnNull(row,col,square, failureCount);
            }
            else if (!CheckIfNumberCanBeInSquare(row,col,square,randomNumber,this.board))
            {
                failureCount++;
                randomNumber = CreateValidNumberOrReturnNull(row,col,square, failureCount);
            }
            return randomNumber;
        }

        private bool CheckIfNumberCanBeInSquare(int row,int col,int square,int number,List<SudokuBox> currentBoard)
        {
            var numbersInSquare = currentBoard.Where(p => p.Square == square);
            if (numbersInSquare.FirstOrDefault(p => p.Number == number && p.Row != row && p.Col != col) != null)
            {
                return false;
            }
            return true;
        }

        private bool CheckIfNumberCanBeInRow(int row,int col,int number, List<SudokuBox> currentBoard)
        {
            var numbersInRow = currentBoard.Where(p => p.Row == row);
            if (numbersInRow.FirstOrDefault(p => p.Number == number && p.Col != col) != null)
            {
                return false;
            }
            return true;
        }

        private bool CheckIfNumberCanBeInCol(int row,int col, int number, List<SudokuBox> currentBoard)
        {
            var numbersInRow = currentBoard.Where(p => p.Col == col);
            if (numbersInRow.FirstOrDefault(p => p.Number == number && p.Row != row) != null)
            {
                return false;
            }
            return true;
        }
    }
}
