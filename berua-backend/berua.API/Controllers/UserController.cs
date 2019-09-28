﻿using berua.API.Model;
using berua.BLL.Actions;
using berua.BLL.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using berua.API.Clients;

namespace berua.API.Controllers
{
    [Route("api/user")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]    
        [Route("addupdatevkuser")]
        public IActionResult AddUpdateVKUser([FromBody] TokenModel token)
        {
            var vkuser = VkClient.GetUser(token.Token);
            var user = new UserDTO
            {
                Id = vkuser.Id,
                FirstName = vkuser.FirstName,
                LastName = vkuser.LastName,
                Domain = vkuser.Domain,
            };

            if (UserAction.AddUpdateUser(user))
                return Ok();
            else
                return BadRequest("Ошибка при добавлении пользоватиеля");           
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
        public IActionResult GetPhone(long userId)
        {
            return Ok(UserAction.GetPhoneUser(userId));
        }
    }
}