using berua.BLL.DTO;
using berua.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace berua.BLL.Actions
{
    public static class UserAction
    {

        public static bool RegistrationUser(UserDTO user)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var sef = ctx.Accounts;
                    var sd = ctx.Users.ToList();

                    if (ctx.Users.Any(x => user.Login == x.Login))
                        return false;

                    var salt = Hash.CreateSalt(4);
                    var passHash = Hash.GenerateSaltedHash(user.Password, salt);

                    ctx.Add(new User
                    {
                        DateRegistration = DateTime.Now,
                        Login = user.Login,
                        Phone = user.Phone,
                        Salt = Convert.ToBase64String(salt),
                        Password = Convert.ToBase64String(passHash),
                    });

                    ctx.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;

            }
        }


        public static UserDTO GetUser(string login)
        {
            var user = new UserDTO();
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.Login == login);
                    if (dbUser == null)
                        return user;

                    user.Id = dbUser.Id;
                    user.Login = dbUser.Login;

                }
                return user;
            }
            catch
            {
                return user;

            }
        }

        public static bool ValidateUser(string login, string psw)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.Login == login);
                    if (dbUser == null)
                        return false;

                    var salt = Convert.FromBase64String(dbUser.Salt);
                    var passhash = Hash.GenerateSaltedHash(psw, salt);
                    var oldHash = Convert.FromBase64String(dbUser.Password);

                    return Hash.CompareByteArrays(passhash, oldHash);
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
