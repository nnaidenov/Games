using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleGame.Models;

namespace BattleGame.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public Role()
        {
            this.Users = new HashSet<User>();
        }
    }
}
