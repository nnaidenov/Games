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
            Role user = new Role { Name = "user" };
            Role admin = new Role { Name = "admin" };
            context.Roles.AddOrUpdate(user);
            context.Roles.AddOrUpdate(admin);

            context.SaveChanges();
        }
    }
}
