using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirEmptyHackTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dir = textBox1.Text;
            foreach (var item in GetDirRecursive(dir, true))
            {
                Console.WriteLine(item);
                File.WriteAllText(Path.Combine(item, ".gitkeep"), "Ignore Empty dir when commit to github using tortoise");
            }
        }
        private IEnumerable<string> GetDirRecursive(string dir, bool emptyOnlyc = false)
        {
            foreach (string d in Directory.GetDirectories(dir))
            {
                if (IsDirectoryEmpty(d))
                {
                    yield return d;
                }
                foreach (var item in GetDirRecursive(d))
                {
                    yield return item;
                }
            }
        }
        private bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}
