using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berua.API.Model
{
    public class PostDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string Text { get; set; }
        public string[] Tags { get; set; }
        public long Likes { get; set; }
        public string PostUrl { get; set; }
        public string[] Images { get; set; }
    }
}
