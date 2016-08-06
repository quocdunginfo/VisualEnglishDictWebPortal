using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace VisualEnglishDict.Business.Helper
{
    public class HttpHelper
    {
        public async Task<Image> GetImageFromUrl(string url)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);
                req.Proxy = new WebProxy();
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();
                if (stream != null)
                {
                    var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    Image img = Image.FromStream(ms);
                    stream.Close();
                    ms.Close();
                    return img;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetRawHtmlFromUrl(string url, Encoding encode)
        {
            WebClient client = new WebClient();
            if (encode != null)
            {
                client.Encoding = encode;
            }
            client.Proxy = new WebProxy();
            var rawHtml = await client.DownloadStringTaskAsync(url);
            return rawHtml;
        }
    }
}
