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
                HeroePersister.ValidateCreateHeroe(model, context);

                Heroe newHeroe = new Heroe()
                 {
                     Name = model.Name,
                     Race = context.Races.First(r=>r.Id == model.Race),
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

                var responseModel = new ViewHeroeModel()
                {
                    Id = newHeroe.Id,
                    Name = newHeroe.Name
                };

                var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            });
        }
    }
}