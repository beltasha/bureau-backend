using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berua.API.Clients;
using berua.API.Model;

namespace berua.API.Notification
{
    public class NotificationManager
    {
        private VkNotificationListener _vkListener = new VkNotificationListener();
        private NotificationProcessor _ntfProcessor = new NotificationProcessor();
        public async void Start()
        {
            _vkListener.PostsFound += ProcessNewVkPosts;
            await Task.Run(() => _vkListener.Process());
            //то же самое на инсту и фейсбук, должно создаться три потока которые будут чекать обновления
        }
        private async Task GetNewsNotification()
        {
            
        }    
        
        public void ProcessNewVkPosts(List<VkWallResponse> postList)
        {
            
        }
    }
}
