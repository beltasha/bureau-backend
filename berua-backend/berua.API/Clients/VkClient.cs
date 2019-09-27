﻿using berua.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace berua.API.Clients
{
    public class VkClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static readonly string serviceKey = "bfbe3904bfbe3904bfbe390428bfd3251abbfbebfbe3904e2307d5ce4623daf95da58a6";
        private static readonly string apiVersion = "5.64";

        public static async Task<VkWallResponse> GetUserPosts(string ownerId)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await HttpClient.GetAsync("http://api.vk.com/method/wall.get?access_token=" + serviceKey + "&owner_id=" + ownerId + "&v=" + apiVersion);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsAsync<VkWallResponse>();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
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

    }
}