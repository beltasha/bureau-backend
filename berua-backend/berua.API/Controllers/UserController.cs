using berua.API.Model;
using berua.BLL.Actions;
using berua.BLL.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using berua.API.Clients;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;

namespace berua.API.Controllers
{
    [Route("api/user")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("addupdatevkuser")]
        public async Task<IActionResult> AddUpdateVKUser([FromBody] TokenModel token)
        {
            var vk = new VkClient();
            var tokenString = await vk.GetToken(token);
            var vkuser = await vk.GetUserByCode(tokenString);           
            var user = new UserDTO();
            if(vkuser != null)
            {
                user = new UserDTO
                {
                    Id = vkuser.Id,
                    FirstName = vkuser.FirstName,
                    LastName = vkuser.LastName,
                    Domain = vkuser.Domain,
                };               
            }
            
            try
            {
                UserAction.AddUpdateUser(user);
                var tknbck = new TokenBack()
                {
                    Token = tokenString,
                    Id = user.Id
                };
                return Ok(tknbck);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }                         

        }

        private class TokenBack
        {
            public string Token { get; set; }
            public long Id { get; set; }
        }

        [HttpPost]
        [Route("updatephone")]
        public IActionResult UpdatePhone(long userId, string phone)
        {
            //phone = phone.Substring(phone.Length - 10);
            if (UserAction.AddUpdatePhoneUser(userId, phone))
                return Ok();
            else
                return BadRequest("Ошибка при обновлении номера телефона");
        }

        [HttpPost]
        [Route("getphone")]
        public IActionResult GetPhone([FromBody] long userId)
        {
            return Ok(UserAction.GetPhoneUser(userId));
        }


        [HttpPost]
        [Route("addtest")]
        public IActionResult AddUpdateVKUser([FromBody] AuthVKModel model)
        {
            var user = new UserDTO
            {
                Id = model.Id,
                Domain = model.Domain,
            };

            if (UserAction.AddUpdateUser(user))
                return Ok();
            else
                return BadRequest("Ошибка при добавлении пользоватиеля");
        }
    }
}