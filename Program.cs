using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTracker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int id = 1;
            var thread = new TrackedThread(() => Test(id));
            thread.Thread.Name = "goc" + id.ToString();
            thread.Thread.Start();
        }
        private static void ThemDong(string newline, string path)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(@"" + path + "", "");
            }
            try
            {
                string[] lines = File.ReadAllLines(path);
                string uid = newline.Split('|')[0];
                bool exit = false;
                int exit_line = 0;
                int i = 0;
                foreach (var l in lines)
                {
                    var arr = l.Split('|');
                    string id = arr[0];
                    if (uid == id)
                    {
                        exit = true;
                        i = exit_line;
                        break;
                    }
                    exit_line++;
                }
                if (!exit)
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(newline);
                        sw.Flush();
                    }
                }
                else
                {
                    lines[i] = newline;
                    File.WriteAllLines(path, lines);
                }
            }
            catch (Exception) { }
        }
        private static void Test(int id)
        {
            List<int> lst = new List<int>() { 1, 2 };
            while (true)
            {
                foreach (var i in lst)
                {
                    var thread = new TrackedThread(() => DongBoTest(i));
                    thread.Thread.Name = i.ToString();
                    thread.Thread.Start();
                }
                Task delay =  Task.Delay(5*60000);
                delay.Wait();
            }
        }
        public static void DongBoTest(int i)
        {
            for (int j = 1; j <= 10; j++)
            {
                while (true)
                {
                    try
                    {
                        //ThemDong(j + "." + DateTime.Now, "E:\\" + i + ".txt");
                        Console.WriteLine(j + "." + Thread.CurrentThread.ThreadState + "." + DateTime.Now);
                        Thread.Sleep(100);
                        break;
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

    }
}