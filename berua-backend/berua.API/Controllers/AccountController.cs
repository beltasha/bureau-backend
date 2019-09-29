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
            var user = VkClient.Search(search);
            var dtoUser = new AccountModel()
            {
                Id = user.Id.ToString(),
                AccountUrl = user.Domain,
                FirstName = user.FirstName,
                LastName = user.LastName,   
                PhotoUrl = user.Photo400Orig.ToString(),
                Type = SocialNetworkType.VK
            };
            return dtoUser;
            
        }

        [HttpPost]
        [Route("get-posts")]
        public async Task<IEnumerable<PostDTO>> GetUserPosts([FromBody] PostDTOModel postModel)
        {                             
            var postList = new List<PostDTO>();
          
            var vkPosts = await VkClient.GetUserPosts(postModel);
            var usr = VkClient.GetUser(postModel.Token, postModel.AccountId);
            foreach (var pst in vkPosts.Response.Items){
                postList.Add(new PostDTO
                {
                    Id = pst.Id.ToString(),
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    AvatarUrl = usr.PhotoUrl,
                    Text = pst.Text,
                    Likes = pst.Likes.Count,
                    Images = pst.Attachments.Select(x => x.Photo.Photo1280.ToString()).ToArray()
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
