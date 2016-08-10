using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEnglishDict.Core.Models.WebApi.Dict
{
    public class DictImgRequest
    {
        public string Keyword { get; set; }
    }
    public class DictImgResult
    {
        public List<HtmlImage> Images { get; set; }
    }
}
