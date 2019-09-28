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
        public static bool IsSubscriptionYet(int userId, string accountKey, SocialNetworkType type)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var dbSubscriptions = ctx.Users.Find(userId).Subscriptions;

                    if (dbSubscriptions.FirstOrDefault(x =>
                         x.AccountKey.SocialNetworkType == (byte)type &&
                         x.AccountKeyId == accountKey) is Subscription sub)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }


        //public static bool AddSubscription(long userId, SubscriptionDTO sub)
        //{
        //    try
        //    {
        //        using (var ctx = new BeruaContext())
        //        {
        //            if (ctx.Users.Find(userId) is User dbUser)
        //            {
        //                dbUser.Subscriptions.Where(x => x.)




        //            }




        //            var dbAccKey = ctx.AccountKeys.Where(x => x.SocialNetworkType == (byte)type);
        //            if (dbAccKey.FirstOrDefault(x => x.Id == accountKey) is AccountKey key)
        //                key.Subscriptions.Add(new Subscription
        //                {
        //                    AccountKeyId = accountKey,
        //                    UserId
        //                });

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


        public static ICollection<AccountDTO> GetSubSubscriptionsByUser(long userId)
        {
            var listRes = new List<AccountDTO>();

            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.Find(userId) is User dbUser)
                    {




                        //var dbSubs = dbUser.Subscriptions.Select(x => x.AccountKeyId).ToArray();

                        //foreach (var sub in dbSubs.GroupBy(x => x.AccountKey.AccountId))
                        //{
                        //    var dbAcc = ctx.Accounts.Find(sub.Key);

                        //    var acc = new AccountDTO
                        //    {
                        //        Fullname = $"{dbAcc.Name} {dbAcc.Surname}",
                        //        PhotoUrl = dbAcc.AvatarUrl,
                        //        Id = dbAcc.Id,
                        //        AccountIds = new Dictionary<SocialNetworkType, string>()
                        //    };
                        //    foreach (var url in sub)
                        //    {
                        //        acc.AccountIds.Add({ url.AccountKey.AccountId })
                        //    }


                        //    listRes.Add(acc);
                        //}
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
