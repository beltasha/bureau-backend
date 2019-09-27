using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using berau_backend.Model;
using berua.API.Clients;
using berua.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace berau_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("search")]
        public List<AccountDTO> Post([FromBody] SearchDTO search)
        {
            return new List<AccountDTO>(){

                new AccountDTO() {
                    Id = new Guid(),
                    Name = "Test Testerson",
                    PhotoUrl = "https://sun9-52.userapi.com/c638624/v638624418/75575/ocYjIIcMfuY.jpg",
                    AccountUrl = "https://vk.com/liz",
                    Type = SocialNetworkType.vk
                }   
            };
        }

        [HttpPost]
        public string Post([FromBody] Guid accountId)
        {
            return "value";
        }

        [HttpGet]
        public string Get()
        {
            return "Something I'm not sure what";
        }

        [HttpGet]
        [Route("posts")]
        public async void GetInfo()
        {
            //var resp = VkClient.GetUserPosts("210700286");
            VkClient.GetCredentials();
        }
    }
}
