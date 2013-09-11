using BattleGame.Data;
using BattleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleGame.Services.Persisters
{
    public class BasePersister
    {
        public static User GetUserBySessionKey(string sessionKey, GameContext context)
        {
            return context.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
        }
    }
}