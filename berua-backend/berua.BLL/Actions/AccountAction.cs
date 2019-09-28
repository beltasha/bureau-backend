using berua.BLL.DTO;
using berua.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace berua.BLL.Actions
{
    public static class AccountAction
    {

        //public static void AddAccount(AccountDTO acc, SubscriptionDTO sub)
        //{
        //    try
        //    {
        //        using (var ctx = new BeruaContext())
        //        {
        //            var key = new AccountKey
        //            {
                        
        //                SocialNetworkType = ()sub.Type,
        //            };


        //            var newAcc = new Account
        //            {
        //                AvatarUrl = acc.PhotoUrl,
        //                Name = acc.Fullname.Contains(" ") ? acc.Fullname.Split(" ")[0] : acc.Fullname,
        //                Surname = acc.Fullname.Contains(" ") ? acc.Fullname.Split(" ")[1] : "",
        //            };

        //            if(ctx.Accounts.FirstOrDefault(x => x.AccountKeys.Contains()))

        //           var dbAccountKeys = ctx.AccountKeys.Where(x => x.SocialNetworkType == account.).Select(x => x.)

        //            if (ctx.Acc.Any(x => login == x.Login))
        //                return -2;

        //            var salt = Hash.CreateSalt(4);
        //            var passHash = Hash.GenerateSaltedHash(psw, salt);

        //            ctx.Add(new User
        //            {
        //                DateRegistration = DateTime.Now,
        //                Login = login,
        //                Salt = Convert.ToBase64String(salt),
        //                Password = Convert.ToBase64String(passHash),
        //            });

        //            ctx.SaveChanges();

                    
        //        }
        //    }
        //    catch
        //    {
                
        //    }




        //}



    }
}
