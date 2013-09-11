using BattleGame.Data;
using BattleGame.Models;
using BattleGame.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleGame.Services.Persisters
{
    public class HeroePersister : BasePersister
    {
        private const string ValidHeroeNameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinHeroeNameChars = 1;
        private const int MaxHeroeNameChars = 30;

        public static void ValidateCreateHeroe(CreateHeroeModel model, GameContext context)
        {
            ValidateName(model.Name);
            ValidateRace(model.Race.Name, context);
        }

        private static void ValidateName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Nickname cannot be null");
            }
            else if (name.Length < MinHeroeNameChars)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Nickname must be more than {0} characters long", MinHeroeNameChars));
            }
            else if (name.Length > MaxHeroeNameChars)
            {
                throw new ArgumentOutOfRangeException(
                 string.Format("Nickname must be less than {0} characters long", MaxHeroeNameChars));
            }
            else if (name.Any(ch => !ValidHeroeNameChars.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Nickname must contains Latin letters, digits, ., and _");
            }
        }

        private static void ValidateRace(string raceName, GameContext context)
        {
            var result = context.Races.FirstOrDefault(r => r.Name == raceName);
            if (result == null)
            {
                throw new ArgumentNullException("The Race is not valid!");
            }
        }
    }
}