using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Http = System.Web.Http;
using VisualEnglishDict.Business.Api;
using VisualEnglishDict.Core.Models.WebApi.Dict;
using System.Threading;
using System.Diagnostics;

namespace VisualEnglishDict.Web.WebApi
{
    public class DictController : ApiController
    {
        [Http.AllowAnonymous]
        [Http.HttpPost]
        public DictTransTextResult GetTransText([FromBody] DictTransTextRequest request)
        {
            CoVietApi coVietApi = new CoVietApi();
            var t = Task.Run(async () => coVietApi.GetDictHtmlbyKeyWord(request.Keyword, CoVietApi.DICT_EN_VI_API).Result);
            t.Wait();
            return new DictTransTextResult
            {
                Result = t.Result
            };
        }
        [Http.AllowAnonymous]
        [Http.HttpPost]
        public DictImgResult GetDictImg([FromBody] DictImgRequest request)
        {
            GoogleApi googleApi = new GoogleApi();
            var t = Task.Run(async () => googleApi.GetImageByKeyword(request.Keyword).Result);
            t.Wait();
            return new DictImgResult
            {
                Images = t.Result
            };
        }
        [Http.AllowAnonymous]
        [Http.HttpPost]
        public DictPredictWordResult GetPredictWord([FromBody] DictPredictWordRequest request)
        {
            GoogleApi googleApi = new GoogleApi();
            var t = Task.Run(async () => googleApi.GetPredictedTextByKeyword(request.Keyword).Result);
            t.Wait();
            return new DictPredictWordResult
            {
                Result = string.IsNullOrEmpty(t.Result) || t.Result.Equals(request.Keyword) ? string.Empty : t.Result
            };
        }
    }
}