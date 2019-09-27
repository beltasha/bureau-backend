using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berua.API.Model
{
    public partial class VkWallResponse
    {
        public Response Response { get; set; }
    }

    public partial class Response
    {
        public long Count { get; set; }

        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        public long Id { get; set; }
        public long FromId { get; set; }
        public long OwnerId { get; set; }
        public long Date { get; set; }
        public PostType PostType { get; set; }
        public string Text { get; set; }
        public List<Attachment> Attachments { get; set; }
        public PostSource PostSource { get; set; }
        public Comments Comments { get; set; }
        public Likes Likes { get; set; }
        public Reposts Reposts { get; set; }
    }

    public partial class Attachment
    {
        public AttachmentType Type { get; set; }
        public Video Video { get; set; }
        public AttachmentPhoto Photo { get; set; }
        public Link Link { get; set; }
    }

    public partial class Link
    {
        public Uri Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public LinkPhoto Photo { get; set; }
    }

    public partial class LinkPhoto
    {
        public long Id { get; set; }
        public long AlbumId { get; set; }
        public long OwnerId { get; set; }
        public Uri Photo75 { get; set; }
        public Uri Photo130 { get; set; }
        public Uri Photo604 { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public string Text { get; set; }
        public long Date { get; set; }
    }

    public partial class AttachmentPhoto
    {
        public long Id { get; set; }
        public long AlbumId { get; set; }
        public long OwnerId { get; set; }
        public Uri Photo75 { get; set; }
        public Uri Photo130 { get; set; }
        public Uri Photo604 { get; set; }
        public Uri Photo807 { get; set; }
        public Uri Photo1280 { get; set; }
        public Uri Photo2560 { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
        public string Text { get; set; }
        public long Date { get; set; }
        public long? PostId { get; set; }
        public string AccessKey { get; set; }
    }

    public partial class Video
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string Title { get; set; }
        public long Duration { get; set; }
        public string Description { get; set; }
        public long Date { get; set; }
        public long Comments { get; set; }
        public long Views { get; set; }
        public long LocalViews { get; set; }
        public Uri Photo130 { get; set; }
        public Uri Photo320 { get; set; }
        public Uri Photo640 { get; set; }
        public Uri Photo800 { get; set; }
        public string AccessKey { get; set; }
        public string Platform { get; set; }
        public long CanAdd { get; set; }
        public string TrackCode { get; set; }
    }

    public partial class Comments
    {
        public long Count { get; set; }
        public long CanPost { get; set; }
        public bool GroupsCanPost { get; set; }
    }

    public partial class Likes
    {
        public long Count { get; set; }
        public long UserLikes { get; set; }
        public long CanLike { get; set; }
        public long CanPublish { get; set; }
    }

    public partial class PostSource
    {
        public PostSourceType Type { get; set; }
    }

    public partial class Reposts
    {
        public long Count { get; set; }
        public long UserReposted { get; set; }
    }

    public partial class VkServerResponse
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }
    }

    public enum AttachmentType { Link, Photo, Video };

    public enum PostSourceType { Vk };

    public enum PostType { Post };
}
