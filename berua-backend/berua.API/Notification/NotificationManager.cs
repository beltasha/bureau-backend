using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using berua.API.Clients;
using berua.API.Model;
using berua.API.Telegram;

namespace berua.API.Notification
{
    public class NotificationManager
    {
        private static NotificationManager _instance;
        public static NotificationManager Instance {
            get
            {
                if (_instance == null)
                    _instance = new NotificationManager();
                return _instance;
            }
        }

        private VkNotificationListener _vkListener = new VkNotificationListener();
        private NotificationProcessor _ntfProcessor = new NotificationProcessor();
        public async void Start()
        {
            _vkListener.PostsFound += ProcessNewVkPosts;
            await Task.Run(() => _vkListener.Process());
            //то же самое на инсту и фейсбук, должно создаться три потока которые будут чекать обновления
        }

        public void AddUser(long userId)
        {
            _vkListener.AddUser(userId);
        } 
        
        public void ProcessNewVkPosts(Dictionary<long, List<VkWallResponse>> postList)
        {
            Dictionary<long, List<PostDTO>> postDtos = new Dictionary<long, List<PostDTO>>();

            foreach (var post in postList)
            {
                postDtos.Add(post.Key, new List<PostDTO>());
                foreach (var vkWallResponse in post.Value)
                {
                    foreach (var item in vkWallResponse.Response.Items)
                    {
                        postDtos[post.Key].Add(new PostDTO()
                        {
                            Text = item.Text
                        });
                    }
                }
            }

            Bot.SendMessages(postDtos);
        }
    }
}
