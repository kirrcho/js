using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.App.Common;
using WebApplication2.App.Models;
using WebApplication2.App.Services;

namespace WebApplication2.App.Controllers
{
    public class GamesController : Controller
    {
        //FOR REFACTORING
        public IActionResult Minesweeper()
        {
            MinesweeperBoard board = new MinesweeperBoard();

            return View(board);
        }
        
        [HttpPost]
        [Route("/Games/Minesweeper")]
        public JsonResult Minesweeper([FromBody]MinesweeperBindingModel model)
        {
            var board = MinesweeperBoard.Decrypt(model.encodedBoard).Split(';').ToList();

            var gameEndedSuccessfully = MinesweeperBoard.CheckIfGameEndedSuccessfully(board,model.flagged);
            var gameEndedUnsuccessfully = MinesweeperBoard.CheckIfMineIsHit(board, model.currentId);

            MineViewModel output = MinesweeperBoard.TilesToReveal(model.currentId, board, new MineViewModel());

            output.gameEnded = gameEndedSuccessfully || gameEndedUnsuccessfully;

            if (gameEndedUnsuccessfully)
            {
                output.mines = new List<Mine>();
                foreach (var mine in board)
                {
                    output.mines.Add(new Mine(mine, 0));
                }
            }

            return new JsonResult(output);
        }

        public IActionResult Sudoku()
        {
            var board = new SudokuBoard();

            return View(board.Board);
        }

        [HttpPost]
        [Route("/Games/Sudoku")]
        public JsonResult Sudoku([FromBody]List<SudokuBindingModel> json)
        {
            var result = SudokuBoard.IsSudokuCorrect(json);

            var board = new SudokuBoard(json);

            var error = board.CheckIfSudokuIsCorrect();

            var output = new List<bool>();

            output.Add(result);
            output.Add(error);

            return new JsonResult(output);
        }

        public IActionResult Tetris()
        {
            return this.View();
        }
    }
}