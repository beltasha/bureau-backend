using Google.Cloud.Translation.V2;
using System.Collections.Generic;

namespace berua.BLL
{
    public static class TranslateText
    {

        private static readonly string ru = "ru";
        private static readonly string en = "en";


        public static ICollection<string> TranslateTextRuToEn(string[] couplets)
        {
            var result = new List<string>();
            using (var client = TranslationClient.Create())
            {
                foreach(var elem in couplets)
                {
                    var response = client.TranslateText(elem, en, ru);
                    result.Add(response.TranslatedText);
                }
                
            }
            return result;
        }

        public static ICollection<string> TranslateTextEnToRu(string[] couplets)
        {
            var result = new List<string>();
            using (var client = TranslationClient.Create())
            {
                foreach (var elem in couplets)
                {
                    var response = client.TranslateText(elem, ru, en);
                    result.Add(response.TranslatedText);
                }

            }
            return result;
        }

    }
}
