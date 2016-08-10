using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualEnglishDict.Business.Api;
using VisualEnglishDict.Business.Helper;

namespace VisualEnglishDict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GoogleApi api = new GoogleApi();
            CoVietApi api2 = new CoVietApi();
            var suggested = Task.Run(async () => api.GetPredictedTextByKeyword(textBox1.Text).Result);
            suggested.Wait();
            textBox1.Text = suggested.Result;
            
            var t1 = Task.Run(async () => api.GetImageByKeyword(textBox1.Text.Trim()).Result);
            var t2 = Task.Run(async () => api2.GetDictHtmlbyKeyWord(textBox1.Text.Trim(), CoVietApi.DICT_EN_VI_API).Result);
            Task.WaitAll(t1, t2);
            var imgList = t1.Result;
            var transText = t2.Result;
            {
                webBrowser1.DocumentText = transText.TranslatedDoc;
            }
            {
                HttpHelper helper = new HttpHelper();
                var img1Task = Task.Run(async () => helper.GetImageFromUrl(imgList[0].Url).Result).ContinueWith((img1) => {
                    pictureBox1.Image = img1.Result;    
                });
                var img2Task = Task.Run(async () => helper.GetImageFromUrl(imgList[1].Url).Result).ContinueWith((img2) =>
                {
                    pictureBox2.Image = img2.Result;
                });
                var img3Task = Task.Run(async () => helper.GetImageFromUrl(imgList[2].Url).Result).ContinueWith((img3) =>
                {
                    pictureBox3.Image = img3.Result;
                });
                var img4Task = Task.Run(async () => helper.GetImageFromUrl(imgList[2].Url).Result).ContinueWith((img4) =>
                {
                    pictureBox4.Image = img4.Result;
                });
                var img5Task = Task.Run(async () => helper.GetImageFromUrl(imgList[2].Url).Result).ContinueWith((img5) =>
                {
                    pictureBox5.Image = img5.Result;
                });
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
