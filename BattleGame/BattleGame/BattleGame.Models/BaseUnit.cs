using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGame.Models
{
    public class BaseUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public virtual Race Race { get; set; }
    }
}