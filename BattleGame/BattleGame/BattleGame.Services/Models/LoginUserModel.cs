using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BattleGame.Services.Models
{
    [DataContract]
    public class LoginUserModel
    {
        [DataMember(Name = "authCode")]
        public string AuthCode { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}