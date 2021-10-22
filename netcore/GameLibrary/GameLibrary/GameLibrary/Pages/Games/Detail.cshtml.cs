using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameLibrary.Pages.Games
{
    public class DetailModel : PageModel
    {
        private readonly IGameData _gameData;

        [TempData]
        public string Message { get; set; }
        
        public Game Game { get; set; }

        public DetailModel(IGameData gameData)
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
    }
}