using API.Code;
using Core.Security;
using Entity.SystemUsers;
using Model.Base;
using Model.Request.Users;
using Model.Response.Users;
using Repository.SystemUser.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController<LoginController>
    {
        private readonly IUserRepository _userRepository;
        private readonly IForgatPasswordRepository _forgatPasswordRepository;
        private readonly IHttpContextAccessor _accessor;


        public UserController(
         IUserRepository userRepository,
         IForgatPasswordRepository forgatPasswordRepository,
         IHttpContextAccessor accessor
         )
        {
            _userRepository = userRepository;
            _forgatPasswordRepository = forgatPasswordRepository;
            _accessor = accessor;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser([FromBody] SignUpRequest request)
        {
            var response = new BaseResponse<SignUpResponse>();
            response.Data = new SignUpResponse();


            if (_userRepository.Any(x => x.Email == request.Email))
            {
                response.SetMessage("This email address is registered");
            }
            else if (_userRepository.Any(x => x.Username == request.Username))
            {
                response.SetMessage("This username address is registered");
            }
            else
            {
                var user = new User
                {
                    Username = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = new Cryptography().EncryptString(request.Password),
                    Role = request.Role
                };
                _userRepository.Add(user);

                response.Data.Email = request.Email;
                response.Data.UserName = request.Username;
            }

            return Ok(response);
        }
    }
}
