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
using Microsoft.AspNetCore.Cors;
using berua.BLL;

namespace berau_backend.Controllers
{
    [Route("api/users")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class AccountController : ControllerBase
    {       
        private List<long> _savedUsers = new List<long>();
        
        [HttpPost]
        [Route("search")]       
        public AccountModel Post([FromBody] SearchDTO search)
        {
            var user = VkClient.Search(search.Text);
            var dtoUser = new AccountModel()
            {
                Id = user.Id.ToString(),
                AccountUrl = user.Domain,
                Name = user.FirstName + " " + user.LastName,
                PhotoUrl = user.Photo400Orig.ToString(),
                Type = SocialNetworkType.VK
            };
            return dtoUser;
            
        }

        [HttpPost]
        [Route("get-posts")]
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
        public IActionResult SaveUser([FromBody] long account)
        {
            _savedUsers.Add(account);
            return Ok();
        }

        [HttpGet]
        [Route("saved")]
        public IEnumerable<AccountModel> GetSavedUsers()
        {
            var userList = new List<AccountModel>();
            foreach(var guid in _savedUsers)
            {
                var user = new AccountModel();
                user.Id = guid.ToString();
                userList.Add(user);
            }

            return userList;
        }
    }
}
