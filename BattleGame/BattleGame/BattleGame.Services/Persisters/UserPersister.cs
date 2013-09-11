using System;
using System.Linq;
using System.Text;
using BattleGame.Data;
using BattleGame.Models;
using BattleGame.Services.Models;

namespace BattleGame.Services.Persisters
{
    public class UserPersister : BasePersister
    {
        private const string SessionKeyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int SessionKeyLength = 50;

        private const int Sha1CodeLength = 40;
        private const string ValidUsernameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890";
        private const string ValidNicknameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinUsernameNicknameChars = 6;
        private const int MaxUsernameNicknameChars = 30;

        public static string GenerateSessionKey(int userId)
        {
            StringBuilder sessionKey = new StringBuilder();
            sessionKey.Append(userId);
            Random rand = new Random();
            while (sessionKey.Length < SessionKeyLength)
            {
                int index = rand.Next(SessionKeyLength);
                sessionKey.Append(SessionKeyChars[index]);
            }

            return sessionKey.ToString();
        }

        public static void ValidateRegisterUser(RegisterUserModel model)
        {
            ValidateAuthCode(model.AuthCode);
            ValidateNickname(model.Nickname);
            ValidateUsername(model.Username);
        }

        public static void ValidateLoginUser(LoginUserModel model)
        {
            ValidateAuthCode(model.AuthCode);
            ValidateUsername(model.Username);
        }

        private static void ValidateAuthCode(string authCode)
        {
            if (authCode == null)
            {
                throw new ArgumentNullException("AuthCode cannot be null");
            }
            else if (authCode.Length < Sha1CodeLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("AuthCode must be exact {0} characters long", Sha1CodeLength));
            }
            //else if (authCode.Any(ch => !.Contains(ch)))
            //{
            //    throw new ArgumentOutOfRangeException("AuthCode must contains Latin letters and digits");
            //}
        }

        private static void ValidateNickname(string nickname)
        {
            if (nickname == null)
            {
                throw new ArgumentNullException("Nickname cannot be null");
            }
            else if (nickname.Length < MinUsernameNicknameChars)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Nickname must be more than {0} characters long", MinUsernameNicknameChars));
            }
            else if (nickname.Length > MaxUsernameNicknameChars)
            {
                throw new ArgumentOutOfRangeException(
                 string.Format("Nickname must be less than {0} characters long", MaxUsernameNicknameChars));
            }
            else if (nickname.Any(ch => !ValidNicknameChars.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Nickname must contains Latin letters, digits, ., and _");
            }
        }

        private static void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null");
            }
            else if (username.Length < MinUsernameNicknameChars)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be more than {0} characters long", MinUsernameNicknameChars));
            }
            else if (username.Length > MaxUsernameNicknameChars)
            {
                throw new ArgumentOutOfRangeException(
                 string.Format("Username must be less than {0} characters long", MaxUsernameNicknameChars));
            }
            else if (username.Any(ch => !ValidUsernameChars.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Username must contains Latin letters, digits, ., and _");
            }
        }

        public static User GetUserByUsernameAndDisplayName(string username, string nickname, GameContext context)
        {
            return context.Users.FirstOrDefault(u => u.Username == username.ToLower() && u.Nickname == nickname);
        }
    }
}