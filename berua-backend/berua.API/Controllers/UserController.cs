using berua.API.Model;
using berua.BLL.Actions;
using berua.BLL.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace berua.API.Controllers
{
    [Route("api/user")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddUpadateVKUser([FromBody] AuthVKModel model)
        {
            var user = new UserDTO
            {
                Id = model.Id,
                FirstName = model.First_name,
                LastName = model.Last_name,
                Domain = model.Domain
            };

            if (UserAction.AddUpdateUser(user))
                return Ok();
            else
                return BadRequest("Ошибка при добавлении пользоватиеля");

        }

        [HttpPost]
        public IActionResult UpdatePhone(long userId, string phone)
        {
            //phone = phone.Substring(phone.Length - 10);
            if (UserAction.AddUpdatePhoneUser(userId, phone))
                return Ok();
            else
                return BadRequest("Ошибка при обновлении номера телефона");
        }

        [HttpPost]
        public IActionResult GetPhone(long userId)
        {
            return Ok(UserAction.GetPhoneUser(userId));
        }
    }
}