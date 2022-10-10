using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : LaboteControllerBase
    {
        private readonly UserManager<LaboteUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly LaboteContext _context;

        public AuthController(UserManager<LaboteUser> userManager, LaboteContext context, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<dynamic>> Login(LoginRequestModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var userFromMail = await _userManager.FindByEmailAsync(model.UserName);

                if (user == null && userFromMail == null)
                {
                    return Ok(new
                    {
                        token = "",
                        expiration = "",
                        error = true
                    });
                }
                user = user == null ? userFromMail : user;
                if ((user != null || userFromMail != null) && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id.ToString()),

                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Enums.SecretKey));
                    var token = new JwtSecurityToken(
                    //  issuer: _configuration[“JWT: ValidIssuer”],
                    //audience: _configuration[“JWT: ValidAudience”],

                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        error = false
                    });
                }
            }
            catch (Exception e)
            {
            }

            return Ok(new
            {
                token = "",
                expiration = "",
                error = true
            });
        }

        [AllowAnonymous]
        [HttpGet("CheckLogin")]
        public async Task<ActionResult<dynamic>> CheckLogin()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Ok(new
                {

                    UserExist = false,
                    Auth = false,
                    PhoneConfirmed = false
                });

            }
            var userId = User.Identity?.UserId();
            if (userId == null)
            {
                return Ok(new
                {

                    UserExist = false,
                    Auth = false,
                    PhoneConfirmed = false
                });
            }
            var userExist = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (userExist == null)
            {
                return Ok(new
                {

                    UserExist = false,
                    Auth = false,
                    PhoneConfirmed = false
                });
            }
            if (!userExist.PhoneNumberConfirmed)
            {
                return Ok(new
                {

                    UserExist = true,
                    Auth = true,
                    PhoneConfirmed = false
                });
            }
            return Ok(new
            {

                UserExist = userExist,
                Auth = User.Identity.IsAuthenticated,
                PhoneConfirmed = true
            });



            return Unauthorized();
        }

  
        [HttpGet("TokenCheck")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> TokenCheck()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId=User.Identity.UserId();
                if (userId!=null)
                {
                   var data= _context.Users.Where(x => x.Id == userId).Select(x => new {
                    x.FirstName,
                    x.Lastname,
                    x.Email,
                    
                    });
                    return Ok(data);
                }
                
            }
           return Unauthorized();
            
        }


        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<ActionResult<dynamic>> SignUp(UserCreateRequestModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var userTopic = new UserTopic
                    {
                    };
                    using (LaboteContext context = new LaboteContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {

                            context.UserTopics.Add(userTopic);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                    }

                    var role = new UserRole();
                    using (LaboteContext context = new LaboteContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            LaboteUser user = new LaboteUser();
                            if (!_userManager.Users.Any(x=>x.UserName==model.UserName || x.Email==model.Email))
                            {
                                user = new LaboteUser()
                                {
                                    Email = model.Email,
                                    SecurityStamp = Guid.NewGuid().ToString(),
                                    UserName = model.UserName,
                                    FirstName = model.FirstName,
                                    Lastname = model.LastName,
                                    NotDelete = false,
                                    UserTopicId=userTopic.Id,

                                };
                                var usr = _userManager.CreateAsync(user, model.Password).Result;

                                role = new UserRole
                                {
                                    Name = "Admin-" + userTopic.TopicCode,
                                    NormalizedName = "ADMİN-" + userTopic.TopicCode.ToUpper(),
                                    IsHidden = false,
                                    UserTopicId = userTopic.Id,
                                    NotDelete=true,
                                };
                                var userRol = await _roleManager.CreateAsync(role);

                                var RoleAdd = context.UserRoles.Add(new IdentityUserRole<Guid> { RoleId = role.Id, UserId = user.Id });
                                context.SaveChanges();
                                transaction.Commit();

                            }
                            else
                            {
                                PageResponse.IsError = true;
                                PageResponse.Message = "Mail adresi yada kullanıcı adı sistemde zaten mevcut";
                                return PageResponse;
                            }


                        };
                    }


                    using (LaboteContext context = new LaboteContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            var data = context.MenuModules.Where(x=>x.IsSuperAdmin==false).ToList();

                            foreach (var item in data)
                            {
                                context.UserMenuModules.Add(new UserMenuModule
                                {
                                    UserRoleId = role.Id,
                                    MenuModelId = item.Id
                                });
                            }
                            context.SaveChanges();
                            transaction.Commit();
                        };
                    }

                }
            }
            catch (Exception e)
            {
                PageResponse.IsError = true;
                PageResponse.Message = "Hatalı giriş";
            }

            return PageResponse;


        }

    }
}
