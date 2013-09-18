using BattleGame.Data;
using BattleGame.Models;
using BattleGame.Services.Attributes;
using BattleGame.Services.Models;
using BattleGame.Services.Persisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace BattleGame.Services.Controllers
{
    public class HeroesController : BaseApiController
    {
        private const int StartHeroeLevel = 0;
        private const int StartHeroePoints = 0;
        private const int StartHeroeMoney = 10000;

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage CreateHeroe(CreateHeroeModel model,
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
             string sessionKey)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var context = new GameContext();
                var user = BasePersister.GetUserBySessionKey(sessionKey, context);

                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password!");
                }

                HeroePersister.ValidateCreateHeroe(model, context);

                Hero newHeroe = new Hero()
                 {
                     Name = model.Name,
                     Race = context.Races.First(r => r.Id == model.Race),
                     Level = StartHeroeLevel,
                     Points = StartHeroePoints,
                     Money = StartHeroeMoney,
                     NumberOfLoses = 0,
                     NumberOfWins = 0,
                     User = BasePersister.GetUserBySessionKey(sessionKey, context),
                     Units = new HashSet<Unit>()
                 };
                context.Heroes.Add(newHeroe);

                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            });
        }

        [HttpGet]
        [ActionName("all")]
        public HttpResponseMessage GetAllHeros(
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
             string sessionKey)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var context = new GameContext();
                var user = BasePersister.GetUserBySessionKey(sessionKey, context);

                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password!");
                }

                var heroes = user.Heroes;

                var models = (
                    from h in heroes
                    select new ViewHeroModel
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Image = h.Image,
                        Money = h.Money,
                        NumberOfWins = h.NumberOfWins,
                        NumberOfLoses = h.NumberOfLoses,
                        Level = h.Level,
                        Race = h.Race.Name,
                        Points = h.Points
                    });

                var response = this.Request.CreateResponse(HttpStatusCode.OK, models);

                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage GetHeroUnits(int id,
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
             string sessionKey)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var context = new GameContext();
                var user = BasePersister.GetUserBySessionKey(sessionKey, context);

                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password!");
                }

                var units = context.Units.Where(h => h.Hero.Id == id);

                var models = (
                    from u in units
                    select new ViewUnitModel
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Image = u.Image,
                        Health = u.Health,
                        Attack = u.Attack,
                        Defense = u.Defense,
                        Damage = u.Damage,
                        Speed = u.Speed
                    });

                var response = this.Request.CreateResponse(HttpStatusCode.OK, models);

                return response;
            });
        }

    }
}