using berua.API.Model;
using berua.BLL.Actions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace berua.API.Telegram
{
    public class Bot
    {
        private static ITelegramBotClient botClient;

        public async static Task Startup()
        {
            botClient = new TelegramBotClient(BotSettings.Token);
            botClient.OnMessage += OnMessage;
            botClient.StartReceiving();
            //Task.Delay(int.MaxValue);
            Thread.Sleep(int.MaxValue);
        }

        static async void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                switch (e.Message.Text)
                {
                    case "/start":
                        var isUserRegistred = (bool)UserAction.UserAddedTelegram(e.Message.Chat.Id);
                        if (!isUserRegistred)
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: e.Message.Chat.Id,
                                text: "Нажмите Подтвердить, чтобы получать уведомления. ВНИМАНИЕ: подтверждая, вы передаете БЮРО свой номер телефона для регистрации в системе.",
                                disableNotification: true,
                                replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact("Подтвердить")));
                        }
                        break;
                    //case "Test":
                    //    Dictionary<long, List<PostDTO>> posts = new Dictionary<long, List<PostDTO>>();
                    //    posts.Add(127354174, new List<PostDTO> {
                    //    new PostDTO() {
                    //        FirstName = "Ольга",
                    //        LastName = "Бузова",
                    //        AvatarUrl = "https://sun9-60.userapi.com/c850324/v850324431/1dffa8/EfHhV4TGJHc.jpg?ava=1",
                    //        Text = "Такой потрясающий день🙏🏻 Просто самая лучшая перезарядка🤤🥰 В кругу близких, детишек, на свежем воздухе....природа, устрицы, гребешки 🤤🤤🤤 провожали закат 🌅 Наслаждаюсь каждой секундой 🥰 я люблю тебя жизнь ❤",
                    //        Tags = new string[2]{ "#отдых", "#тревл" },
                    //        Likes = 8861,
                    //        PostUrl = "https://vk.com/olgabuzova?z=photo32707600_457250122%2Falbum32707600_00%2Frev",
                    //        Images = new string[1]{ "https://sun9-71.userapi.com/c543105/v543105535/50cb6/Nxirt3LCSes.jpg" }
                    //    },
                    //    //new PostDTO() {
                    //    //    Text = "222 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    //    //},
                    //    //new PostDTO() {
                    //    //    Text = "333 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    //    //    Images = new string[3]{"https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Telegram_2019_Logo.svg/1200px-Telegram_2019_Logo.svg.png",
                    //    //    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Telegram_2019_Logo.svg/1200px-Telegram_2019_Logo.svg.png",
                    //    //    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Telegram_2019_Logo.svg/1200px-Telegram_2019_Logo.svg.png"}
                    //    //}
                    //     });
                    //    SendMessages(posts);
                    //    break;
                    default:
                        if (e.Message.Contact != null && e.Message.Contact != null)
                        {

                            string replyText = "Благодарим за регистрацию";
                            var userDto = UserAction.GetUserByPhone(e.Message.Contact.PhoneNumber);
                            if (userDto.ChatId != e.Message.Chat.Id)
                            {
                                if (userDto == null)
                                {
                                    userDto = new BLL.DTO.UserDTO()
                                    {
                                        FirstName = e.Message.Contact.FirstName,
                                        LastName = e.Message.Contact.LastName,
                                        Phone = e.Message.Contact.PhoneNumber,
                                        ChatId = e.Message.Chat.Id
                                    };
                                }
                                else
                                {
                                    userDto.ChatId = e.Message.Chat.Id;
                                }
                                UserAction.AddUpdateUser(userDto);
                            }
                            else
                            {
                                replyText = "Вы уже зарегистрированы";
                            }
                            await botClient.SendTextMessageAsync(
                                    chatId: e.Message.Chat.Id,
                                    text: replyText,
                                    replyMarkup: new ReplyKeyboardRemove()
                                );
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async void SendMessage(long chatId, PostDTO post)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: post.Text
            );
        }

        public static async void SendMessages(Dictionary<long, List<PostDTO>> postList)
        {
            foreach (var post in postList)
            {
                foreach (var item in post.Value)
                {
                    // Первое сообщение: Аватарка и Имя
                    //var name = item.FirstName + " " + item.LastName;
                    //if (!string.IsNullOrEmpty(name))
                    //{
                    //    if (!string.IsNullOrEmpty(item.AvatarUrl))
                    //    {
                    //        await botClient.SendPhotoAsync(
                    //            chatId: post.Key,
                    //            photo: item.AvatarUrl,
                    //            caption: "Посты на тему: " + name
                    //        );
                    //    }
                    //    else
                    //    {
                    //        await botClient.SendTextMessageAsync(
                    //            chatId: post.Key,
                    //            text: "Посты на тему: " + name
                    //        );
                    //    }
                    //}

                    // Копоновка текста поста
                    string replyText = item.Text;
                    // Добавляем теги
                    if (item.Tags != null && item.Tags.Length > 0)
                    {
                        replyText += "\n";
                        for (int i = 0; i < item.Tags.Length; i++)
                        {
                            if (i + 1 != item.Tags.Length)
                                replyText += item.Tags[i] + " ";
                            else
                                replyText += item.Tags[i];
                        }
                    }
                    // Добавляем лайки
                    if (item.Likes > 0)
                    {
                        replyText += "\n👍 " + item.Likes.ToString();
                    }
                    // Добавляем урлу
                    if (!string.IsNullOrEmpty(item.PostUrl))
                    {
                        replyText += "\n" + item.PostUrl;
                    }

                    // Отправка всех картинок поста
                    if (item.Images != null)
                    {
                        // Картинок много -> отсылаем их по отдельности, пост с последней
                        if (item.Images.Length > 1)
                        {
                            for (int i = 0; i < item.Images.Length - 1; i++)
                            {
                                await botClient.SendPhotoAsync(
                                    chatId: post.Key,
                                    photo: item.Images[i]
                                );
                            }
                            await botClient.SendPhotoAsync(
                                chatId: post.Key,
                                photo: item.Images[item.Images.Length - 1],
                                caption: replyText
                            );
                        }
                        // Картинка одна -> отсылаем все одним сообщением
                        else 
                        {
                            await botClient.SendPhotoAsync(
                                chatId: post.Key,
                                photo: item.Images[0],
                                caption: replyText
                            );
                        }
                    }
                    // Картинок нет -> шлем просто сообщение
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: post.Key,
                            text: replyText
                        );
                    }
                    
                }
            }
        }
    }
}
