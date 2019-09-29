using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berua.API.Clients;
using berua.API.Model;

namespace berua.API.Notification
{
    public class VkNotificationListener
    {
        private List<long> _userIdList = new List<long>();
        private int _delayMs = 6000000;
        public delegate void VkHandler(Dictionary<long, List<VkWallResponse>> posts);
        public event VkHandler PostsFound;

        public void AddUser(long userId)
        {
            _userIdList.Add(userId);
        }

        public async Task Process()
        {
            while (true)
            {
                var postList = await CheckUpdates();
                if(postList.Count() > 0 && PostsFound != null)
                {
                    PostsFound(postList);
                }
                await Task.Delay(_delayMs);
            }
        }

        private async Task<Dictionary<long, List<VkWallResponse>>> CheckUpdates()
        {
            Dictionary<long, List<VkWallResponse>> usersPosts = new Dictionary<long, List<VkWallResponse>>();
            foreach (var userId in _userIdList)
            {
                var postDto = new PostDTOModel()
                {
                    AccountId = userId
                };
                var wall = await VkClient.GetUserPosts(postDto);
                //if (wall.Response.Items.Any(x => x.Date > DateTime.Now.AddMinutes(-10)))
                //{
                //    if (usersPosts.ContainsKey(userId))
                //    {
                //        usersPosts[userId].Add(wall);
                //    }
                //    else
                //    {
                //        usersPosts.Add(userId, new List<VkWallResponse>() { wall });
                //    }
                //}
            }
            return usersPosts;
        }
    }
}
