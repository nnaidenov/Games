namespace BattleGame.Data.Migrations
{
    using BattleGame.Models;
    using System;
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
            context.Races.AddOrUpdate(new Race { Name = "Race two" });
            //context.Races.AddOrUpdate(new Race { Name = "Race three" });
            //context.Races.AddOrUpdate(new Race { Name = "Race four" });

            context.GameStatuses.AddOrUpdate(new GameStatus { Status = "open" });
            context.GameStatuses.AddOrUpdate(new GameStatus { Status = "active" });
            context.GameStatuses.AddOrUpdate(new GameStatus { Status = "full" });

            context.SaveChanges();

            context.BaseUnits.AddOrUpdate(new BaseUnit { Attack = 100, Damage = 50, Defense = 150, Health = 252, Name = "Soldier", Price = 150, Race = context.Races.FirstOrDefault(r => r.Name == "Race One"), Speed = 10 });
            context.BaseUnits.AddOrUpdate(new BaseUnit { Attack = 1200, Damage = 50, Defense = 150, Health = 252, Name = "Soldier 2", Price = 150, Race = context.Races.FirstOrDefault(r => r.Name == "Race One"), Speed = 23 });
            context.BaseUnits.AddOrUpdate(new BaseUnit { Attack = 10, Damage = 50, Defense = 150, Health = 252, Name = "Soldier 3", Price = 150, Race = context.Races.FirstOrDefault(r => r.Name == "Race two"), Speed = 130 });
            context.BaseUnits.AddOrUpdate(new BaseUnit { Attack = 150, Damage = 50, Defense = 150, Health = 252, Name = "Soldier 4", Price = 150, Race = context.Races.FirstOrDefault(r => r.Name == "Race two"), Speed = 12 });
            context.BaseUnits.AddOrUpdate(new BaseUnit { Attack = 140, Damage = 50, Defense = 150, Health = 252, Name = "Soldier 5", Price = 150, Race = context.Races.FirstOrDefault(r => r.Name == "Race One"), Speed = 32 });

            context.SaveChanges();
        }
    }
}
