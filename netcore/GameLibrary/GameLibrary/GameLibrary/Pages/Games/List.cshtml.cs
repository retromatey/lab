using System.Collections.Generic;
using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace GameLibrary.Pages.Games
{
    public class ListModel : PageModel
    {
        private readonly IGameData _gameData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Game> Games { get; set; }


        public ListModel(IGameData gameData)
        {
            _gameData = gameData;
        }
        
        public void OnGet()
        {
            Games = _gameData.GetGamesByName(SearchTerm);
        }
    }
}