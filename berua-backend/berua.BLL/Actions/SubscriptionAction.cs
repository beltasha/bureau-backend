using berua.BLL.DTO;
using berua.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace berua.BLL.Actions
{
    public static class SubscriptionAction
    {
        /// <summary>
        /// Метод для проверки является ли данная пара пользователь - аккаунт подпиской
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <param name="accountKey">id аккаунта</param>
        /// <param name="type">тип аккаунта</param>
        /// <returns></returns>
        /// 
        //public static bool IsSubscriptionYet(int userId, string accountKey, SocialNetworkType type)
        //{
        //    try
        //    {
        //        using (var ctx = new BeruaContext())
        //        {
        //            var dbSubscriptions = ctx.Users.Find(userId).Subscriptions;

        //            if (dbSubscriptions.FirstOrDefault(x =>
        //                 x.AccountKey.SocialNetworkType == (byte)type &&
        //                 x.AccountKeyId == accountKey) is Subscription sub)
        //                return true;
        //            else
        //                return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        public static bool AddSubscription(long userId, int accId)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.Find(userId) is User dbUser)
                    {
                        if (dbUser.Subscriptions
                            .FirstOrDefault(x => x.AccountId == accId) is Subscription dbSub)
                            return false;
                        else
                            dbUser.Subscriptions.Add(new Subscription
                            {
                                AccountId = accId,
                                UserId = userId
                            });
                    }
                    if (ctx.ChangeTracker.HasChanges())
                        ctx.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static ICollection<AccountDTO> GetSubscriptionsByUser(long userId)
        {
            var listRes = new List<AccountDTO>();
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.Find(userId) is User dbUser)
                    {
                        var col = dbUser.Subscriptions.ToList();
                        var acc = ctx.Accounts.ToList();

                        listRes = dbUser.Subscriptions
                            .Join(ctx.Accounts,
                            sb => sb.Account,
                            ac => ac,
                            (sb, ac) => new AccountDTO
                            {
                                Id = ac.Id,
                                AvatarUrl = ac.AvatarUrl,
                                Fullname = ac.Fullname,
                                KeyVK = ac.KeyVK,
                                KeyFacebook = ac.KeyFacebook,
                                KeyInstagram = ac.KeyInstagram
                            })
                            .ToList();
                    }
                }
                return listRes;
            }
            catch
            {
                return listRes;
            }
        }


    }
}
