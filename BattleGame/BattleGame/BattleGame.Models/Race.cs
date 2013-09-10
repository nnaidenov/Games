using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleGame.Models;

namespace BattleGame.Models
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BaseUnit> BaseUnits { get; set; }
    }
}