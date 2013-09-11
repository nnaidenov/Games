using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleGame.Models;

namespace BattleGame.Models
{
    public class Heroe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Points { get; set; }
        public int Money { get; set; }
        public int NumberOfWins { get; set; }
        public int NumberOfLoses { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public virtual Race Race { get; set; }
        public virtual User User { get; set; }
    }
}