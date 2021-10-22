using System.Collections.Generic;
using System.Linq;
using GameLibrary.Models;

namespace GameLibrary.Data
{
    public interface IGameData
    {
        Game GetById(int gameId);
        IEnumerable<Game> GetGamesByName(string name);
        Game Update(Game game);
        Game Add(Game game);
        int Commit();
    }

    public class InMemoryGameData : IGameData
    {
        private readonly List<Game> games;
        
        public InMemoryGameData()
        {
            games = new List<Game>()
            {
                new Game { GameId = 1, Genre = Genre.Shooter,  Publisher = "Bungie",    Title = "Halo",        PublishYear = 2001 },
                new Game { GameId = 2, Genre = Genre.Puzzle,   Publisher = "Russia",    Title = "Tetris",      PublishYear = 1985 },
                new Game { GameId = 3, Genre = Genre.Rpg,      Publisher = "Gamefreak", Title = "Pokemon",     PublishYear = 1995 },
                new Game { GameId = 4, Genre = Genre.Strategy, Publisher = "EA",        Title = "Red Alert",   PublishYear = 1997 },
                new Game { GameId = 5, Genre = Genre.Action,   Publisher = "Edios",     Title = "Tomb Raider", PublishYear = 1994 },
            };
        }

        public Game GetById(int gameId)
        {
            return games.SingleOrDefault(game => game.GameId == gameId);
        }

        public IEnumerable<Game> GetGamesByName(string name = null)
        {
            return from game in games
                   where string.IsNullOrEmpty(name) || game.Title.StartsWith(name)
                   orderby game.Title
                   select game;
        }

        public Game Update(Game game)
        {
            var currentRecord = games.SingleOrDefault(g => g.GameId == game.GameId);

            if (currentRecord != null)
            {
                currentRecord.Genre = game.Genre;
                currentRecord.Publisher = game.Publisher;
                currentRecord.PublishYear = game.PublishYear;
                currentRecord.Title = game.Title;
            }

            return currentRecord;
        }

        public Game Add(Game game)
        {
            games.Add(game);
            game.GameId = games.Max(m => m.GameId) + 1;
            return game;
        }

        public int Commit()
        {
            return 0;
        }
    }
}