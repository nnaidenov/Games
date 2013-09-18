using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BattleGame.Services.Models
{
    [DataContract]
    public class UserDetails
    {
        [DataMember(Name="username")]
        public string Username { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "avatar")]
        public string Avatar { get; set; }

        [DataMember(Name = "heroes")]
        public IEnumerable<ViewHeroModel> Heroes { get; set; }
    }
}