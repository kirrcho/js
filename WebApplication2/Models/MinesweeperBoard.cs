using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.App.Common;

namespace WebApplication2.App.Models
{
    public class MinesweeperBoard
    {
        //Change with js constants
        public const int Mines_In_Game = 99;
        public const int Rows_In_Game = 16;
        public const int Cols_In_Game = 30;

        public MinesweeperBoard()
        {
            this.board = new List<List<MineSquare>>();
            CreateBoard();
            this.EncodedBoard = StringifyMines();
        }

        private List<List<MineSquare>> board;

        public string EncodedBoard { get; private set; }

        public int MineCount { get; private set; }

        private void CreateBoard()
        {
            this.FillBoard();
            this.FillMines();
        }

        private void FillBoard()
        {
            for (int i = 0; i < Rows_In_Game; i++)
            {
                this.board.Add(new List<MineSquare>());
                for (int k = 0; k < Cols_In_Game; k++)
                {
                    this.board[i].Add(new MineSquare(i,k));
                }
            }
        }

        private void FillMines()
        {
            while (this.MineCount < Mines_In_Game)
            {
                var random = new Random();
                var row = random.Next(0, Rows_In_Game);
                var col = random.Next(0, Cols_In_Game);
                if (!this.board[row][col].HasMine)
                {
                    this.board[row][col].HasMine = true;
                    this.MineCount++;
                }
            }
        }

        public void Flag(int row,int col)
        {
            this.board[row][col].IsFlagged = true;
        }

        public void UnFlag(int row,int col)
        {
            this.board[row][col].IsFlagged = false;
        }

        public static bool CheckIfGameEndedSuccessfully(List<string> decodedMines, List<string> flaggedSquares)
        {
            if (flaggedSquares.Count > Mines_In_Game)
            {
                return false;
            }

            foreach (var mine in decodedMines)
            {
                if (!flaggedSquares.Contains(mine))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckIfMineIsHit(List<string> decodedMines,string id)
        {
            if (decodedMines.Contains(id))
            {
                return true;
            }

            return false;
        }

        private string StringifyMines()
        {
            var stringified = "";

            for (int row = 0; row < this.board.Count; row++)
            {
                var line = this.board[row];
                for (int col = 0; col < line.Count; col++)
                {
                    if (this.board[row][col].HasMine)
                    {
                        stringified += (this.board[row][col].Col + 1) + "_" + (this.board[row][col].Row + 1) + ";";
                    }
                }
            }

            stringified.Remove(stringified.Length - 1);

            var output = MinesweeperBoard.Encrypt(stringified);

            return output;
        }

        private static string Encrypt(string szPlainText)
        {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            int[] numbers = { 2, 5, 7, 3, 1, 6, 8, 4, 3, 6, 3, 2, 6, 2, 9 };
            var randomNumbers = new List<int>(numbers);
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch + randomNumbers[iCount % randomNumbers.Count]);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }

        public static string Decrypt(string szPlainText)
        {
            Dictionary<string, string> replaceStrings = new Dictionary<string, string>();

            replaceStrings.Add("&amp;","&");
            replaceStrings.Add("&lt;","<");
            replaceStrings.Add("&gt;",">");
            replaceStrings.Add("&quot;","\"");
            replaceStrings.Add("&#39;","'");
            replaceStrings.Add("&#x2F;","/");

            foreach (var htmlBullshit in replaceStrings)
            {
                szPlainText = szPlainText.Replace(htmlBullshit.Key, htmlBullshit.Value);
            }

            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            int[] numbers = { 2, 5, 7, 3, 1, 6, 8, 4, 3, 6, 3, 2, 6, 2, 9 };
            var randomNumbers = new List<int>(numbers);
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch - randomNumbers[iCount % randomNumbers.Count]);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }

        public static MineViewModel TilesToReveal(string currentId,List<string> mines,MineViewModel minesToReturn)
        {
            if (minesToReturn.mines.FirstOrDefault(p => p.currentId == currentId) != null)
            {
                return minesToReturn;
            }
            if (mines.Contains(currentId))
            {
                return minesToReturn;
            }
            int numberToShow = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    string id = $"{int.Parse(currentId.Split('_')[0]) - 1 + i}_{int.Parse(currentId.Split('_')[1]) - 1 + k}";
                    if (mines.Contains(id))
                    {
                        numberToShow++;
                    }
                }
            }

            minesToReturn.Add(new Mine(currentId, numberToShow));

            if (numberToShow == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        var rowAndCol = currentId.Split('_').Select(int.Parse).ToList();
                        if (rowAndCol[0] - 1 + i < 1 || 
                            rowAndCol[0] - 1 + i > Cols_In_Game || 
                            rowAndCol[1] - 1 + k < 1 || 
                            rowAndCol[1] - 1 + k > Rows_In_Game)
                        {
                            continue;
                        }
                        string id = $"{int.Parse(currentId.Split('_')[0]) - 1 + i}_{int.Parse(currentId.Split('_')[1]) - 1 + k}";
                        TilesToReveal(id, mines, minesToReturn);
                    }
                }
            }

            return minesToReturn;
        }

        //function RevealTiles(current, mines)
        //{
        //    if (current == undefined || current.className == 'empty')
        //    {
        //        return;
        //    }
        //    current.className = "empty";
        //    current.innerHTML = "";
        //    let currentId = current.id.split('_');
        //    let numberToShow = 0;
        //    for (let i = 0; i < 3; i++)
        //    {
        //        for (let k = 0; k < 3; k++)
        //        {
        //            let id = `${ +currentId[0] + i - 1}
        //            _${ +currentId[1] + k - 1}`;
        //            if (mines.find(p => p == id))
        //            {
        //                numberToShow++;
        //            }
        //        }
        //    }
        //    if (numberToShow == 0)
        //    {
        //        for (let i = 0; i < 3; i++)
        //        {
        //            for (let k = 0; k < 3; k++)
        //            {
        //                let element = $(`#${+currentId[0] + i - 1}_${+currentId[1] + k - 1}`)[0];
        //                RevealTiles(element, mines);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        current.textContent = numberToShow;
        //    }
        //}
    }
}
