using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berua.API.Model;
using berua.BLL.Actions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace berua.API.Controllers
{
    [Route("api/user")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Registration([FromBody] AuthModel model)
        {
            var res = UserAction.RegistrationUser(model.Email, model.Password);
            switch (res)
            {
                case -1:
                    return BadRequest("Ошибка регистрации");
                case -2:
                    return BadRequest("Пользователь с таким логином уже существует");
                default:
                    return Ok(res);
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] AuthModel model)
        {
            var res = UserAction.RegistrationUser(model.Email, model.Password);
            switch (res)
            {
                case -1:
                    return Unauthorized("Ошибка авторизации");
                case -2:
                    return Unauthorized("Неверный пароль");
                case -3:
                    return Unauthorized("Пользователь с таким логином не найден");
                default:
                    return Ok(res);
            }
        }
    }
}