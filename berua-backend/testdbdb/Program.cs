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

            string[] coll = new string[]
            {

                @"The White House is reportedly considering de-listing Chinese companies from US stock exchanges as part of a broader effort to curb US investment in China.",
                @"The US is also said to be looking at limiting the ability of government pension funds to participate in Chinese markets.",
                @"The discussion comes amid a tense trade stand-off between the two countries.",
                @"US stock exchanges dropped on the news, which Bloomberg first reported.",
                @"Shares in Chinese companies listed in the US, which include big names such as Alibaba and JD.com, also fell sharply after the reports.",
                @"Alibaba fell 5% while JD.com dropped more than 6 %."
            };

            var res = TranslateText.TranslateEnToRu(coll);
            foreach(var el in res)
            {
                Console.WriteLine(el);
            }
            Console.ReadLine();

        }
    }
}
