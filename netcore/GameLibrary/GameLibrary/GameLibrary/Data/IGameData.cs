using System.Collections.Generic;
using System.Linq;
using GameLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Data
{
    public interface IGameData
    {
        Game GetById(int gameId);
        IEnumerable<Game> GetGamesByName(string name);
        Game Update(Game game);
        Game Add(Game game);
        Game Delete(int gameId);
        int Commit();
    }

    public class SqliteGameData : IGameData
    {
        private readonly GameLibraryDbContext _dbContext;

        public SqliteGameData(GameLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Game GetById(int gameId)
        {
            return _dbContext.Games.Find(gameId);
        }

        public IEnumerable<Game> GetGamesByName(string name)
        {
            var query = from g in _dbContext.Games
                        where g.Title.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby g.Title
                        select g;

            return query;
        }

        public Game Update(Game game)
        {
            var entity = _dbContext.Games.Attach(game);
            entity.State = EntityState.Modified;
            return game;
        }

        public Game Add(Game game)
        {
            _dbContext.Add(game);
            return game;
        }

        public Game Delete(int gameId)
        {
            var game = GetById(gameId);

            if (game != null)
            {
                _dbContext.Games.Remove(game);
            }

            return game;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
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

        public Game Delete(int gameId)
        {
            var game = games.FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                games.Remove(game);
            }

            return game;
        }

        public int Commit()
        {
            return 0;
        }
    }
}