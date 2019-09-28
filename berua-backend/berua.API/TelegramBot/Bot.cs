﻿using berua.API.Model;
using berua.BLL.Actions;
using System;
using System.Collections.Generic;
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
            botClient.OnMessage += OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void OnMessage(object sender, MessageEventArgs e)
        {

            switch (e.Message.Text) {
                case "/start":
                    await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Для окончания регистрации нажмите кнопку Подтвердить. ВНИМАНИЕ: подтверждая, вы передаете БЮРО свой номер телефона для регистрации в системе.",
                        disableNotification: true,
                        replyMarkup: new ReplyKeyboardMarkup(KeyboardButton.WithRequestContact("Подтвердить")));
                    break;
                default:
                    if (e.Message.Contact != null && e.Message.Contact != null) {

                        // Здесь сохранение данных пользователя
                        var userDto = UserAction.GetUserByPhone(e.Message.Contact.PhoneNumber);
                        if (userDto == null)
                        {
                            userDto = new BLL.DTO.UserDTO()
                            {
                                FirstName = e.Message.Contact.FirstName,
                                LastName = e.Message.Contact.LastName,
                                Phone = e.Message.Contact.PhoneNumber,
                                ChatId = e.Message.Chat.Id
                            };
                            UserAction.AddUpdateUser(userDto);

                            await botClient.SendTextMessageAsync(
                              chatId: e.Message.Chat,
                              text: "Благодарим за регистрацию",
                              replyMarkup: new ReplyKeyboardRemove()
                            );
                        }
                        else
                        {
                            userDto.ChatId = e.Message.Chat.Id;
                            UserAction.AddUpdateUser(userDto);

                            await botClient.SendTextMessageAsync(
                              chatId: e.Message.Chat,
                              text: "Благодарим за регистрацию",
                              replyMarkup: new ReplyKeyboardRemove()
                            );
                        }
                        // Здесь нужно подписывать пользователя на уведомления
                    }
                    break;
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
                    await botClient.SendTextMessageAsync(
                        chatId: post.Key,
                        text: item.Text
                    );
                }
            }
        }
    }
}