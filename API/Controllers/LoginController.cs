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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Services.MailManager;

namespace API.Controllers
{
    public class LoginController : BaseController<LoginController>
    {
        private readonly IUserRepository _userRepository;
        private readonly IForgatPasswordRepository _forgatPasswordRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMailManager _mailManager;

        public LoginController(
            IUserRepository userRepository,
            IForgatPasswordRepository forgatPasswordRepository,
            IHttpContextAccessor accessor,
            IMailManager mailManager
            )
        {
            _userRepository = userRepository;
            _forgatPasswordRepository = forgatPasswordRepository;
            _accessor = accessor;
            _mailManager = mailManager;
        }

        [HttpPost]
        public IActionResult Get()
        {
            var response = new BaseResponse<UserGetResponse>();
            var user = _userRepository.GetById(CurrentUserID);

            response.Data = new UserGetResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<BaseResponse<SignInResponse>> SignIn([FromBody] SignInRequest request)
        {
            var response = new BaseResponse<SignInResponse>();
            response.Data = new SignInResponse();

            try
            {
                request.Password = new Cryptography().EncryptString(request.Password);
                var user = _userRepository.FirstOrDefaultBy(x => x.Email == request.Email && x.Password == request.Password);

                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authnetication");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("userID",user.Id.ToString()),
                    new Claim("email",user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("firstName",user.FirstName),
                    new Claim("lastName",user.LastName)
                        }),
                        Expires = DateTime.UtcNow.AddDays(365),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    response.Data.Token = tokenHandler.WriteToken(token);
                }
                else
                {
                    response.SetMessage("You entered an incorrect username or password.");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
      //  [Authorize(Roles = "Admin,Agent")]  
        public ActionResult SignUp([FromBody] SignUpRequest request)
        {
            var response = new BaseResponse<SignUpResponse>();
            response.Data = new SignUpResponse();


            try
            {
                if (_userRepository.Any(x => x.Email == request.Email))
                {
                    response.SetMessage("This email address is registered");
                }
                else if (_userRepository.Any(x => x.Username == request.Username))
                {
                    response.SetMessage("This username is registered");
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
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }


        [HttpPost]
        public ActionResult ForgetPassword([FromBody] ForgetPasswordRequest request)
        {

            string Email = request.Email;
            var response = new BaseResponse<bool>();

            try
            {
                var control = _userRepository.FirstOrDefaultBy(x => x.Username == request.Username && x.Email == request.Email);


                if (control == null)
                {
                    response.SetMessage("Such a person cannot be found in the system.");
                    return Ok(response);
                }
                var forgotPassword = new ForgatPassword();
                forgotPassword.UserID = control.Id;
                forgotPassword.Key = Cryptography.GenerateKey(32);
                _forgatPasswordRepository.Add(forgotPassword);

                var Recipients = new Dictionary<string, string>();
                Recipients.Add("recipient.Email", Email);
                Recipients.Add("recipient.FullName", control.FirstName + " " + control.LastName);
                Recipients.Add("recipient.IPAddress", _accessor.HttpContext.Connection.RemoteIpAddress.ToString());
                Recipients.Add("recipient.Date", DateTime.Now.ToString("dd MMM yyyy, dddd HH:mm"));
                Recipients.Add("recipient.Link", $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/Login/RePassword?q=" + forgotPassword.Key);

                _mailManager.Send("reset your password", control.Email, "forgot-password-en.html", Recipients, null);
                response.Data = true;

                response.Message = "Email sended";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }  

            return Ok(response);
        }

        [HttpPost]
        public ActionResult RePassword([FromBody] RePassword request)
        {
            var response = new BaseResponse<Boolean>();
            try
            {
                var control = _forgatPasswordRepository.FirstOrDefaultBy(x => x.Key == request.Code);

                if (control == null)
                {
                    response.SetMessage("Invalid password reset code.");
                    return Ok(response);
                }

                var user = _userRepository.FirstOrDefaultBy(y => y.Id == control.UserID);

                if (user == null)
                {
                    response.SetMessage("User record not found.");
                    return NotFound(response);
                }

                user.Password = new Cryptography().EncryptString(request.Password);
                _userRepository.Update(user);

                response.Message = "Your password has been successfully changed, you can login using your new password.";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
