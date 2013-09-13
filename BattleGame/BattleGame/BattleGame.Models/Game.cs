using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleGame.Models;

namespace BattleGame.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual GameStatus Status { get; set; }
        public virtual Map Map { get; set; }
        public virtual Hero FirstPlayer { get; set; }
        public virtual Hero SecondPlayer { get; set; }
        public virtual Hero UserInTurn { get; set; }
    }
}