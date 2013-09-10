using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BattleGame.Services.Models
{
    [DataContract]
    public class UserResponseModel
    {
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "sessionKey")]
        public string SesionKey { get; set; }
    }
}
