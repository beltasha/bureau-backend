using berua.BLL.DTO;
using berua.DAL;
using System;
using System.Linq;

namespace berua.BLL.Actions
{
    public static class UserAction
    {
        public static bool AddUpdateUser(UserDTO user)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.Find(user.Id) is User dbUser)
                    {
                        dbUser.FirstName = user.FirstName;
                        dbUser.LastName = user.LastName;
                        dbUser.Domain = user.Domain;
                        dbUser.ChatId = user.ChatId;
                    }
                    else
                    {
                        ctx.Add(new User
                        {
                            Id = user.Id,
                            DateRegistration = DateTime.Now,
                            Domain = user.Domain,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ChatId = user.ChatId
                        });
                    }

                    if (ctx.ChangeTracker.HasChanges())
                        ctx.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Метод добавляет номер телефона для пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="phone">Телефон без кода страны (10 цифр)</param>
        /// <returns></returns>
        public static bool AddUpdatePhoneUser(long userId, string phone)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.Find(userId) is User dbUser)
                        dbUser.Phone = phone;
                    else
                        throw new Exception("Пользователь не найден");
                    ctx.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Метод добавляет/обновляет ChatId чата с Telegram
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="phone">Телефон без кода страны (10 цифр)</param>
        /// <returns></returns>
        public static bool AddUpdateChatIdUser(string phone, long chatId)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.FirstOrDefault(x => x.Phone == phone) is User dbUser)
                        dbUser.ChatId = chatId;
                    else
                        throw new Exception("Пользователь с таким телеофном не найден");
                    ctx.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Метод возвразщает ChatId пользователя, по его Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static long GetChatIdUser(long userId)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    if (ctx.Users.Find(userId) is User dbUser)
                        return dbUser.ChatId;
                    else
                        throw new Exception("Пользователь не найден");
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Метод возвразщает телефон пользователя, по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPhoneUser(long id)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    var dbUser = ctx.Users.Find(id);
                    if (dbUser == null)
                        throw new Exception("Пользователь не найден");
                    return dbUser.Phone;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Метод возвразщает UserDTO, по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static UserDTO GetUserByPhone(string phone)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    UserDTO user = null;
                    var dbUser = ctx.Users.FirstOrDefault(u => u.Phone.Contains(phone) || u.Phone.Equals(phone));
                    if (dbUser != null)
                    {
                        user = new UserDTO()
                        {
                            Id = dbUser.Id,
                            Domain = dbUser.Domain,
                            Phone = dbUser.Phone,
                            FirstName = dbUser.FirstName,
                            LastName = dbUser.LastName,
                            ChatId = dbUser.ChatId
                        };
                    }
                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод возвразщает UserDTO, по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool? UserAddedTelegram(long chatId)
        {
            try
            {
                using (var ctx = new BeruaContext())
                {
                    UserDTO user = null;
                    var dbUser = ctx.Users.FirstOrDefault(u => u.ChatId.Equals(chatId));
                    if (dbUser != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод возвращает id пользователя в БД
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="psw">Пароль пользователя</param>
        /// <returns></returns>
        //public static int ValidateUser(string login, string psw)
        //{
        //    try
        //    {
        //        using (var ctx = new BeruaContext())
        //        {
        //            var dbUser = ctx.Users.FirstOrDefault(x => x.Login == login);
        //            if (dbUser == null)
        //                return -3;

        //            var salt = Convert.FromBase64String(dbUser.Salt);
        //            var passhash = Hash.GenerateSaltedHash(psw, salt);
        //            var oldHash = Convert.FromBase64String(dbUser.Password);

        //            if (Hash.CompareByteArrays(passhash, oldHash))
        //                return dbUser.Id;
        //            else
        //                return -2;
        //        }
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

    }
}
