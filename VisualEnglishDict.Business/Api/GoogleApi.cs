using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using VisualEnglishDict.Business.Helper;
using VisualEnglishDict.Core.Models;
using Newtonsoft.Json;

namespace VisualEnglishDict.Business.Api
{
    public class GoogleApi
    {
        public const string IMAGE_SEARCH_API =
            "https://www.google.com.vn/search?q={0}&safe=off&biw=1280&bih=310&tbm=isch&ijn=1";

        public const string EN_VI_DICT_API =
            "https://www.google.com.vn/complete/search?sclient=psy-ab&site=&source=hp&q={0}&oq=&gs_l=&pbx=1&bav=on.2,or.&bvm=bv.129391328,d.dGo&fp=1&biw=1280&bih=288&dpr=1&pf=p&gs_rn=64&gs_ri=psy-ab&cp=13&gs_id=1k&xhr=t&tch=1&ech=13&psi=mX-lV73RG8Sy0ASkm7ywBg.1470463904951.1";
        public async Task<List<HtmlImage>> GetImageByKeyword(string keyword)
        {
            List<HtmlImage> result = new List<HtmlImage>();
            {
                string url = string.Format(IMAGE_SEARCH_API, keyword);

                HttpHelper helper = new HttpHelper();

                var rawHtml = await helper.GetRawHtmlFromUrl(url, null);

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(rawHtml);

                var imgsNode = htmlDocument.DocumentNode.Descendants("img").ToList();
                result.AddRange(imgsNode.Select(x => new HtmlImage { Url = x.Attributes["data-src"].Value }));
            }

            return result;
        }
        public async Task<string> GetPredictedTextByKeyword(string keyword)
        {
            string url = string.Format(EN_VI_DICT_API, keyword);
            HttpHelper helper = new HttpHelper();
            var rawHtml = await helper.GetRawHtmlFromUrl(url, null);

            var obj = JsonConvert.DeserializeObject<GoogleSuggestObj>(rawHtml);
            var obj2 = JsonConvert.DeserializeObject<List<object>>(obj.d);
            dynamic obj3 = obj2[2];
            string obj4 = obj3.o;
            //Right word at all
            if (obj4 == null)
            {
                return keyword;
            }
            //Wrong word need suggestion
            var obj5 = obj4.Replace("<sc>", string.Empty).Replace("</sc>", string.Empty).Trim();
            obj5 = WebUtility.HtmlDecode(obj5);
            return obj5;
        }
        private class GoogleSuggestObj
        {
            public string d { get; set; }
        }
    }
}
