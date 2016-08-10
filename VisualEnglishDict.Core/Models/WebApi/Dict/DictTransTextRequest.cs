using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEnglishDict.Core.Models.WebApi.Dict
{
    public class DictTransTextRequest
    {
        public string Keyword { get; set; }
        public string Dict { get; set; }
    }
    public class DictTransTextResult
    {
        public DictResult Result { get; set; }
    }
}
