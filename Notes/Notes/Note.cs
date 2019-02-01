using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Runtime.Serialization;

namespace Notes
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public string Theme { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Text { get; set; }
        public override string ToString()
        {
            return Theme;
        }

        //public void SaveNote()
        //{
        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        //    saveFileDialog1.FileName = Theme;
        //    saveFileDialog1.Filter = "Note|*.nt";

        //    if (saveFileDialog1.ShowDialog() == true)
        //    {
        //        using (StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile(), System.Text.Encoding.Default))
        //        {
        //            sw.WriteLine(Theme);
        //            sw.WriteLine(Date);
        //            sw.WriteLine(Text);
        //            sw.Close();
        //        }
        //    }
        //}


        //public void LoadNote()
        //{
        //    OpenFileDialog loadFileDialog1 = new OpenFileDialog();

        //    loadFileDialog1.Filter = "Note|*.nt";

        //    if (loadFileDialog1.ShowDialog() == true)
        //    {
        //        using (StreamReader sw = new StreamReader(loadFileDialog1.OpenFile(), System.Text.Encoding.Default))
        //        {
        //            Theme = sw.ReadLine();
        //            Date = sw.ReadLine();
        //            Text = sw.ReadLine();
        //            sw.Close();
        //        }
        //    }
        //}


    }
}
