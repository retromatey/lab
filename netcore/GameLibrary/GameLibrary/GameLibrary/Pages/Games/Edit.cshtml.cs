using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameLibrary.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly IGameData _gameData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Game Game { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        
        public EditModel(IGameData gameData, IHtmlHelper htmlHelper)
        {
            _gameData = gameData;
            _htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? gameId)
        {
            Genres = _htmlHelper.GetEnumSelectList<Genre>();

            Game = gameId.HasValue ? _gameData.GetById(gameId.Value) : new Game();

            if (Game == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Genres = _htmlHelper.GetEnumSelectList<Genre>();
                return Page();
            }
            
            if (Game.GameId > 0)
            {
                _gameData.Update(Game);
            }
            else
            {
                _gameData.Add(Game);
            }
                
            _gameData.Commit();
            TempData["Message"] = "Record saved";
            return RedirectToPage("./Detail", new {gameId = Game.GameId});
        }
    }
}