using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGame.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string AuthCode { get; set; }
        public string SessionKey { get; set; }
        public string Avatar { get; set; }
        public virtual Role Role { get; set; }
    }
}
