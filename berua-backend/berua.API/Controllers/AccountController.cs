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
        private static List<long> _idList = new List<long> { 32707600, 68038156, 55325758 };

        [HttpPost]
        [Route("search")]       
        public List<AccountModel> Post([FromBody] SearchDTO search)
        {
            var user = VkClient.Search(search);
            var dtoUser = new List<AccountModel>();
            dtoUser.Add(new AccountModel()
            {              
                Id = user.Id.ToString(),
                AccountUrl = user.Domain,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhotoUrl = user.Photo400Orig.ToString(),
                Type = SocialNetworkType.VK
            });               
            return dtoUser;
            
        }

        [HttpPost]
        [Route("get-posts")]
        public async Task<IEnumerable<PostDTO>> GetUserPosts([FromBody] PostDTOModel postModel)
        {                             
            var postList = new List<PostDTO>();

            var vkPosts = await VkClient.GetUserPosts(new PostDTOModel  { AccountId = 35340109, Token = postModel.Token });
            //var vkPosts2 = await VkClient.GetUserPosts(new PostDTOModel { AccountId = 68038156, Token = postModel.Token});
            var vkPosts3 = await VkClient.GetUserPosts(new PostDTOModel { AccountId = 55325758, Token = postModel.Token });
            var usr = VkClient.GetUser(postModel.Token, 32707600);
            var usr2 = VkClient.GetUser(postModel.Token, 68038156);
            var usr3 = VkClient.GetUser(postModel.Token, 55325758);
            List<Item> postList3 = new List<Item>();
            postList3 = postList3.Concat(vkPosts.Response.Items).ToList();
           // postList3.Concat(vkPosts2.Response.Items);
            postList3 = postList3.Concat(vkPosts3.Response.Items).ToList();
            //vkPosts.Response.Items.Add(vkPosts2.Response.Items);
            string[] arr = new string[] { };
            foreach (var pst in postList3)
            {
                postList.Add(new PostDTO
                {
                    Id = pst.Id.ToString(),
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    AvatarUrl = usr.PhotoUrl,
                    Text = pst.Text,
                    Likes = pst.Likes.Count,
                    Images = arr
                });
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
