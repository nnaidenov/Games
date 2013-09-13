using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleGame.Models;

namespace BattleGame.Models
{
    public class Unit : BaseUnit
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public virtual Hero Hero { get; set; }
    }
}
