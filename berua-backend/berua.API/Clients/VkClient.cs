﻿using berua.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Newtonsoft.Json;
using berau_backend.Model;

namespace berua.API.Clients
{
    public class VkClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static ulong _appId = 7142991;
        private static readonly string serviceKey = "bfbe3904bfbe3904bfbe390428bfd3251abbfbebfbe3904e2307d5ce4623daf95da58a6";
        private static readonly string apiVersion = "5.64";
        private static VkApi _api = new VkApi();
        

        static VkClient()
        {
            //_api.Authorize(new ApiAuthParams
            //{
            //    ApplicationId = _appId,
            //    Login = "89319675217",
            //    Password = "KPLOsbligq9514#",
            //    Settings = Settings.All
            //});
            
        }

        public static async Task<VkWallResponse> GetUserPosts(PostDTOModel postModel)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                if(postModel.AccountId == 0)
                {
                    response = await HttpClient.GetAsync("http://api.vk.com/method/wall.get?access_token=" + serviceKey + "&owner_id=&v=" + apiVersion);

                }
                else
                {
                    response = await HttpClient.GetAsync("http://api.vk.com/method/wall.get?access_token=" + serviceKey + "&owner_id=" + postModel.AccountId + "&v=" + apiVersion + "&count=5");

                }

                response.EnsureSuccessStatusCode();
                var srjha = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsAsync<VkWallResponse>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<VkNet.Model.Attachments.Post> GetPosts(PostDTOModel postModel)
        {
            var api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                AccessToken = postModel.Token
            });
            var posts = api.Wall.Get(new WallGetParams
            {
                OwnerId = Convert.ToInt64(postModel.AccountId),
                Filter = WallFilter.Owner
            });
            return posts.WallPosts.ToList();
        }

        public static AccountModel GetUser(string token,long id)
        {
            var api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                AccessToken = token
            });

            var user = api.Users.Get(new long[] { id }).FirstOrDefault();
            var accUser = new AccountModel();
            accUser.Id = user.Id.ToString();
            accUser.AccountUrl = user.Domain;
            accUser.FirstName = user.FirstName;
            accUser.LastName = user.LastName;
            accUser.PhotoUrl = "";
            return accUser;
        }

        public static async Task<VkServerResponse> GetCredentials() {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await HttpClient.GetAsync("https://api.vk.com/method/streaming.getServerUrl?access_token=" + serviceKey + "&v=" + apiVersion);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsAsync<VkServerResponse>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public static void GetUserWall(long userId)
        //{           
        //  var wall = _api.Wall.Get(new WallGetParams
        //    {
        //        OwnerId = userId,
        //        Filter = WallFilter.Owner
        //    });
        //}

        //public static void GetCurrentUserFriends()
        //{

        //    var friends = _api.Friends.Get(new FriendsGetParams
        //    {

        //    });
        //    var p = _api.Users.Get(new long[] { }).FirstOrDefault();
            
        //}
        
        public async Task<User> GetUser(TokenModel token)
        {
            VkApi api = new VkApi();
            HttpResponseMessage response = new HttpResponseMessage();
            response = await HttpClient.GetAsync("https://oauth.vk.com/access_token?client_id="+token.ClientId+ "&client_secret=0jHZfOgGCSz3HBs1iMgH&redirect_uri="+token.RedirectId+"&code="+token.Code);
                      
            var account = api.Users.Get(new List<long>());
            return account.FirstOrDefault();
        }

        public async Task<User> GetUserByCode(string token)
        {           
            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                AccessToken = token
            });

            var a = api.Users.Get(new long[] { });
            return a.FirstOrDefault();
        }

        public async Task<string> GetToken(TokenModel token)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = await HttpClient.GetAsync("https://oauth.vk.com/access_token?client_id=" + token.ClientId + "&client_secret=0jHZfOgGCSz3HBs1iMgH&redirect_uri=" + token.RedirectId + "&code=" + token.Code);
            var json = await response.Content.ReadAsStringAsync();
            TokenResponse tokenReponse = JsonConvert.DeserializeObject<TokenResponse>(json);
            return tokenReponse.Access_token;
        }

        public static User Search(SearchDTO search)
        {
            var api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                AccessToken = search.Token
            });
            var users = api.Users.Search(new UserSearchParams
            {
                Query = search.Text,
                Sort = 0,
                Count = 1,
                Fields = ProfileFields.All
            }).ToList();
            return users.FirstOrDefault();
        }

        private class TokenResponse
        {
            public string Access_token { get; set; }
            public string Expires_in { get; set; }
            public string User_id { get; set; }
        }

    }
}
