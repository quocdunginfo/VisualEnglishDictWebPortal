using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using VisualEnglishDict.Business.Helper;
using System.Globalization;
using VisualEnglishDict.Core.Models;
using System.Text.RegularExpressions;

namespace VisualEnglishDict.Business.Api
{
    public class CoVietApi
    {
        public const string DICT_API = "http://tratu.coviet.vn/hoc-tieng-anh/tu-dien/lac-viet/A-V/{0}.html";
        public const string DICT_VI_EN_API = "http://tratu.coviet.vn/hoc-tieng-anh/tu-dien/lac-viet/V-A/{0}.html";
        public async Task<DictResult> GetDictHtmlbyKeyWord(string keyword, string api)
        {
            var transText = string.Empty;
            var soundUrl = string.Empty;
            {
                HtmlNode dictNode;
                // Trans text
                {
                    HttpHelper helper = new HttpHelper();
                    var url = string.Format(api, keyword);
                    string rawHtml = await helper.GetRawHtmlFromUrl(url, Encoding.UTF8);

                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(rawHtml);

                    dictNode = htmlDocument.GetElementbyId("ctl00_ContentPlaceHolderMain_ctl00");
                    transText = dictNode.InnerHtml;
                }
                // Sound
                {
                    var soundPattern = "file=(.*?\\.mp3)";
                    var tmp = Regex.Matches(dictNode.InnerHtml, soundPattern);
                    if (tmp.Count > 0)
                    {
                        var tmp2 = tmp[0];
                        soundUrl = tmp2.Value.Replace("file=", string.Empty);
                    }
                }
            }
            return new DictResult
            {
                TranslatedDoc = transText,
                SoundUrl = soundUrl
            };
        }
    }
}
