using BattleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BattleGame.Services.Models
{
    [DataContract]
    public class CreateHeroeModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "race")]
        public int Race { get; set; }
    }
}