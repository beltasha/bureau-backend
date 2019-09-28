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

        public static int AddUpdateAccount(AccountDTO acc)
        {
            int resId = 0;
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Accounts.Find(acc.Id) is Account dbAcc)
                    {
                        resId = acc.Id;
                        dbAcc.AvatarUrl = acc.AvatarUrl;
                        dbAcc.KeyFacebook = string.IsNullOrEmpty(acc.KeyFacebook) ? dbAcc.KeyFacebook : acc.KeyFacebook;
                        dbAcc.KeyInstagram = string.IsNullOrEmpty(acc.KeyInstagram) ? dbAcc.KeyInstagram : acc.KeyInstagram;
                        dbAcc.KeyVK = string.IsNullOrEmpty(acc.KeyVK) ? dbAcc.KeyVK : acc.KeyVK;
                        dbAcc.Fullname = acc.Fullname;
                    }
                    else
                    {
                        ctx.Accounts.Add(new Account
                        {
                            Fullname = acc.Fullname,
                            AvatarUrl = acc.AvatarUrl,
                            KeyFacebook = acc.KeyFacebook ?? string.Empty,
                            KeyInstagram = acc.KeyInstagram ?? string.Empty,
                            KeyVK = acc.KeyVK ?? string.Empty,
                        });
                    }

                    if (ctx.ChangeTracker.HasChanges())
                    {
                        ctx.SaveChanges();
                        resId = ctx.Accounts.Last().Id;
                    }
                }
                return resId;
            }
            catch
            {
                return resId;
            }
        }

    }
}
