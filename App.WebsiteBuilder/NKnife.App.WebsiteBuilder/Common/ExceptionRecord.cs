using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu
{
    public class ExceptionRecord
    {
        static string path = Path.Combine(Application.StartupPath, "Log");
        
        public static void Record(string message)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DateTime dt = DateTime.Now.Date;
            string date = dt.ToShortDateString() + ".txt";
            string filePath = Path.Combine(path, date);
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            string time = DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString();

            string writeStr = time + "       " + message + "\r\n";

            File.SetAttributes(filePath, FileAttributes.Normal);
            StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8);
            writer.WriteLine(writeStr);
            writer.Close();
        }
        
    }
}
