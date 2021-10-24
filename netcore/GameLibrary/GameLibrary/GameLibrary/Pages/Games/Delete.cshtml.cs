using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GameLibrary.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly IGameData _gameData;

        public Game Game { get; set; }

        public DeleteModel(IGameData gameData)
        {
            _gameData = gameData;
        }
        
        public IActionResult OnGet(int gameId)
        {
            Game = _gameData.GetById(gameId);

            if (Game == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(int gameId)
        {
            var game = _gameData.Delete(gameId);
            _gameData.Commit();

            if (game == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{game.Title} deleted";
            return RedirectToPage("./List");
        }
    }
}