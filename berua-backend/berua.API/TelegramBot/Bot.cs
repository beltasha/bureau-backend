using berua.BLL.Actions;
using berua.TelegramBot;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace berua.API.Telegram
{
    public class Bot
    {
        private static ITelegramBotClient botClient;

        public static void Startup()
        {
            botClient = new TelegramBotClient(BotSettings.Token);

            var me = botClient.GetMeAsync().Result;

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

            switch (e.Message.Text) {
                case "/start":
                    //BotSettings.ChatIds.Add(e.Message.Chat.FirstName, e.Message.Chat.Id);

                    if (!BotSettings.ChatIds.ContainsKey(e.Message.Chat.FirstName))
                    {
                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: "Для окончания регистрации нажмите кнопку Подтвердить. ВНИМАНИЕ: подтверждая, вы передаете БЮРО свой номер телефона для регистрации в системе.",
                          disableNotification: true,
                          replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact("Подтвердить")));
                    }
                    break;
                default:
                    if (e.Message.Contact != null && e.Message.Contact != null) {

                        // Здесь сохранение данных пользователя
                        var userDto = UserAction.GetUserByPhone(e.Message.Contact.PhoneNumber);
                        userDto.TelegramChatId = e.Message.Chat.Id;
                        UserAction.AddUpdateUser(userDto);

                        await botClient.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: "Благодарим за регистрацию",
                          replyMarkup: new ReplyKeyboardRemove()
                        );
                    }
                    break;
            }
        }

        //static async void SendMessage(long userId)
        //{

        //    switch (e.Message.Text)
        //    {
        //        case "/start":
        //            //BotSettings.ChatIds.Add(e.Message.Chat.FirstName, e.Message.Chat.Id);

        //            if (!BotSettings.ChatIds.ContainsKey(e.Message.Chat.FirstName))
        //            {
        //                await botClient.SendTextMessageAsync(
        //                  chatId: e.Message.Chat,
        //                  text: "Для окончания регистрации нажмите кнопку Подтвердить. ВНИМАНИЕ: подтверждая, вы передаете БЮРО свой номер телефона для регистрации в системе.",
        //                  disableNotification: true,
        //                  replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact("Подтвердить")));
        //            }
        //            break;
        //        default:
        //            if (e.Message.Contact != null && e.Message.Contact != null)
        //            {

        //                // Здесь сохранение данных пользователя
        //                var userDto = UserAction.GetUserByPhone(e.Message.Contact.PhoneNumber);
        //                userDto.TelegramChatId = e.Message.Chat.Id;
        //                UserAction.AddUpdateUser(userDto);

        //                await botClient.SendTextMessageAsync(
        //                  chatId: e.Message.Chat,
        //                  text: "Благодарим за регистрацию",
        //                  replyMarkup: new ReplyKeyboardRemove()
        //                );
        //            }
        //            break;
        //    }
        //}
    }
}
