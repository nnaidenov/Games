namespace BattleGame.Data.Migrations
{
    using BattleGame.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<GameContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(GameContext context)
        {
            context.Roles.AddOrUpdate(new Role { Name = "user" });
            context.Roles.AddOrUpdate(new Role { Name = "admin" });

            context.Races.AddOrUpdate(new Race { Name = "Race One" });
            context.Races.AddOrUpdate( new Race { Name = "Race two" });
            context.Races.AddOrUpdate(new Race { Name = "Race three" });
            context.Races.AddOrUpdate( new Race { Name = "Race four" });

            context.GameStatuses.AddOrUpdate(new GameStatus { Status = "open" });
            context.GameStatuses.AddOrUpdate(new GameStatus { Status = "active" });
            context.GameStatuses.AddOrUpdate(new GameStatus { Status = "full" });

            context.SaveChanges();
        }
    }
}
