using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace BattleGame.Services.Models
{
    [DataContract]
    public class RegisterUserModel
    {
        [DataMember(Name = "authCode")]
        public string AuthCode { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}