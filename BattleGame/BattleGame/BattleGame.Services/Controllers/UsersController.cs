using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;
using BattleGame.Data;
using BattleGame.Models;
using BattleGame.Services.Attributes;
using BattleGame.Services.Models;
using BattleGame.Services.Persisters;

namespace BattleGame.Services.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(RegisterUserModel model)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                UserPersister.ValidateRegisterUser(model);

                var context = new GameContext();
                var dbUser = UserPersister.GetUserByUsernameAndDisplayName(model.Username, model.Nickname, context);
                if (dbUser != null)
                {
                    throw new InvalidOperationException("This user already exists in the database");
                }

                dbUser = new User()
                {
                    Username = model.Username.ToLower(),
                    Nickname = model.Nickname,
                    AuthCode = model.AuthCode,
                    Avatar = model.Avatar,
                    Role = context.Roles.Where(r => r.Name == "user").FirstOrDefault()
                };
                context.Users.Add(dbUser);
                dbUser.SessionKey = UserPersister.GenerateSessionKey(dbUser.Id);

                context.SaveChanges();

                var responseModel = new UserResponseModel()
                {
                    Nickname = dbUser.Nickname,
                    SesionKey = dbUser.SessionKey
                };

                var response = this.Request.CreateResponse(HttpStatusCode.Created, responseModel);
                return response;
            });
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(LoginUserModel model)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                if (model == null)
                {
                    throw new FormatException("invalid username and/or password");
                }

                UserPersister.ValidateLoginUser(model);

                var context = new GameContext();

                var user = context.Users.FirstOrDefault(u => u.Username == model.Username.ToLower()
                    && u.AuthCode == model.AuthCode);

                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password");
                }

                if (user.SessionKey == null)
                {
                    user.SessionKey = UserPersister.GenerateSessionKey(user.Id);
                    context.SaveChanges();
                }

                var responseModel = new UserResponseModel()
                {
                    Nickname = user.Nickname,
                    SesionKey = user.SessionKey
                };

                var response = this.Request.CreateResponse(HttpStatusCode.OK, responseModel);
                return response;
            });
        }

        [HttpPut]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string sessionKey)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var context = new GameContext();
                var user = UserPersister.GetUserBySessionKey(sessionKey, context);
                user.SessionKey = null;
                context.SaveChanges();

                var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            });
        }

        [HttpGet]
        [ActionName("details")]
        public HttpResponseMessage UserDetails(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))]
            string sessionKey)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                var context = new GameContext();
                var user = UserPersister.GetUserBySessionKey(sessionKey, context);

                var model = new UserDetails
                {
                    Avatar = user.Avatar,
                    Nickname = user.Nickname,
                    Username = user.Username,
                    Heroes = (
                    from h in user.Heroes
                    select new ViewHeroeModel
                    {
                        Id = h.Id,
                        Name = h.Name
                    })
                };

                var response = this.Request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}