using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEnglishDict.Core.Models.WebApi.Dict
{
    public class DictPredictWordRequest
    {
        public string Keyword { get; set; }
    }
    public class DictPredictWordResult
    {
        public string Result { get; set; }
    }
}
