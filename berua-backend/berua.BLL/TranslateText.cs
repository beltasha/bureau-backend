using Google.Cloud.Translation.V2;
using System.Collections.Generic;

namespace berua.BLL
{
    public static class TranslateText
    {
        private static string ru = "ru";
        private static string en = "en";

        public static ICollection<string> TranslateEnToRu(string[] text)
        {
            var res = new List<string>();
            
            using (var client = TranslationClient.Create())
            {
                foreach(var elem in text)
                {
                    var response = client.TranslateText(elem, ru, en);
                    res.Add(response.TranslatedText);
                }
            }
            return res;
        }

        public static ICollection<string> TranslateRuToEn(string[] text)
        {
            var res = new List<string>();

            using (var client = TranslationClient.Create())
            {
                foreach (var elem in text)
                {
                    var response = client.TranslateText(elem, en, ru);
                    res.Add(response.TranslatedText);
                }
            }
            return res;
        }

    }
}
