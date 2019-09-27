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
using berua.API.Model;

namespace berau_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {       
        private List<Guid> _savedUsers = new List<Guid>();
        
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
        [Route("posts")]
        public IEnumerable<PostDTO> GetUserPosts([FromBody] string accountId)
        {                     
            var post1 = new PostDTO()
            {
                Id = new Guid().ToString(),
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                AvatarUrl = "https://sun9-12.userapi.com/c851016/v851016587/119cab/ai0uN_RKSXc.jpg?ava=1",
                Text = "This is my Very big post text" +
                "This is my Very big post text" +
                "This is my Very big post text" +
                "This is my Very big post text",
                Tags = new string[5] { "красота", "здоровье", "секс", "шмекс", "хакатон" },
                Likes = 2500,
                PostUrl = "https://vk.com/mudakoff?w=wall-57846937_32571674",
                Images = new string[2] { "https://sun9-12.userapi.com/c543105/v543105145/653c8/CO5L_Xf8-OI.jpg", "https://sun9-24.userapi.com/c543105/v543105158/4c299/Z3ge6QQsH0g.jpg" }
            };

            var postList = new List<PostDTO>();

            for (int i = 0; i < 6; i++)
            {
                var currentPost = post1;
                currentPost.FirstName += i.ToString();
                currentPost.LastName += i.ToString();
                postList.Add(currentPost);
            }

            return postList;
            
        }

        [HttpPost]
        [Route("save")]
        public IActionResult SaveUser([FromBody] Guid account)
        {
            _savedUsers.Add(account);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<AccountDTO> GetSavedUsers()
        {
            var userList = new List<AccountDTO>();
            foreach(var guid in _savedUsers)
            {
                var user = new AccountDTO();
                user.Id = guid;
                userList.Add(user);
            }

            return userList;
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
