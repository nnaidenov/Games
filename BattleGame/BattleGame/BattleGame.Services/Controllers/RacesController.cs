using BattleGame.Data;
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
    public class RacesController : BaseApiController
    {
        [HttpGet]
        [ActionName("all")]
        public IQueryable<ReturnRaceModel> GetAll(
             [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var context = new GameContext();
                var user = BasePersister.GetUserBySessionKey(sessionKey, context);

                if (user == null)
                {
                    throw new NullReferenceException();
                }
                var races = context.Races;

                var models =
                    (from r in races
                     select new ReturnRaceModel
                     {
                        Id = r.Id,
                        Name = r.Name
                     });

                return models;
            });
        }
    }
}