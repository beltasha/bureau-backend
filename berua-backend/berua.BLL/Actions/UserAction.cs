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
        public static int RegistrationUser(string login, string psw)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var sef = ctx.Accounts;
                    var sd = ctx.Users.ToList();

                    if (ctx.Users.Any(x => login == x.Login))
                        return -2;

                    var salt = Hash.CreateSalt(4);
                    var passHash = Hash.GenerateSaltedHash(psw, salt);

                    ctx.Add(new User
                    {
                        DateRegistration = DateTime.Now,
                        Login = login,
                        Salt = Convert.ToBase64String(salt),
                        Password = Convert.ToBase64String(passHash),
                    });

                    ctx.SaveChanges();

                    return ctx.Users.First(x => x.Login == login).Id ;
                }
            }
            catch
            {
                return -1;
            }
        }



        /// <summary>
        /// Метод возвразщает инфу о пользователе, по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserDTO GetUser(int id)
        {
            var user = new UserDTO();
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var dbUser = ctx.Users.Find(id);
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


        /// <summary>
        /// Метод возвращает id пользователя в БД
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="psw">Пароль пользователя</param>
        /// <returns></returns>
        public static int ValidateUser(string login, string psw)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.Login == login);
                    if (dbUser == null)
                        return -3;

                    var salt = Convert.FromBase64String(dbUser.Salt);
                    var passhash = Hash.GenerateSaltedHash(psw, salt);
                    var oldHash = Convert.FromBase64String(dbUser.Password);

                    if (Hash.CompareByteArrays(passhash, oldHash))
                        return dbUser.Id;
                    else
                        return -2;
                }
            }
            catch
            {
                return -1;
            }
        }

    }
}
