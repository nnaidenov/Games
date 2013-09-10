using System;
using System.Data.Entity;
using System.Linq;
using BattleGame.Models;

namespace BattleGame.Data
{
    public class GameContext : DbContext
    {
        public GameContext()
            : base("GameDb")
        { }

        public DbSet<BaseUnit> BaseUnits { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Heroe> Heroes { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<GameStatus> GameStatuses { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
