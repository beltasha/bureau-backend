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
        private List<long> _userIdList = new List<long>()
        {

        };
        private int _delayMs = 6000000;
        public delegate void VkHandler(List<VkWallResponse> posts);
        public event VkHandler PostsFound;

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

        private async Task<List<VkWallResponse>> CheckUpdates()
        {
            List<VkWallResponse> postList = new List<VkWallResponse>();
            foreach (var userId in _userIdList)
            {
                var wall = await VkClient.GetUserPosts(userId.ToString());
                if (wall.Response.Items.Any(x => x.Date > DateTime.Now.AddMinutes(-10)))
                {
                    postList.Add(wall);
                }
            }
            return postList;
        }
    }
}
