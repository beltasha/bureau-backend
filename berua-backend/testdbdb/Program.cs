using berua.BLL;
using berua.BLL.Actions;
using berua.BLL.DTO;
using System;
using System.IO;

namespace testdbdb
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] gg = new string[]
            {
                @"In a race that started at midnight local time, Briton Charlotte Purdue was among the athletes to pull out in temperatures of 32C and with humidity reaching over 70%.",
                @"Organisers decided to go ahead with the event in its scheduled slot despite fears that the conditions might not be conducive for marathon running.",
                @"Ethiopia's marathon coach Haji Adillo Roba witnessed his trio of athletes stop, including Tokyo Marathon winner Ruti Aga.",
                @"We never would have run a marathon in these conditions in our own country, he told BBC Sport during the race.",
            };
            var res = TranslateText.TranslateTextEnToRu(gg);

            foreach(var i in res)
            {
                Console.WriteLine(i);

            }

        }
    }
}
