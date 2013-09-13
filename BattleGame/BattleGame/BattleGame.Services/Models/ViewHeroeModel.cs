using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BattleGame.Services.Models
{
    [DataContract]
    public class ViewHeroeModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "money")]
        public int Money { get; set; }

        [DataMember(Name = "wins")]
        public int NumberOfWins { get; set; }

        [DataMember(Name = "loses")]
        public int NumberOfLoses { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "race")]
        public BattleGame.Models.Race Race { get; set; }

        [DataMember(Name = "points")]
        public int Points { get; set; }

        [DataMember(Name = "units")]
        public IEnumerable<ViewUnitModel> Units { get; set; }
    }
}
