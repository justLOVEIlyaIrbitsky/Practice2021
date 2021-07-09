using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace prac2
{
    class Loger : IDisposable
    {
        private static StreamWriter fs;
        internal enum Severity
        {
            race, debug,
            information,
            warning, error, critical
        }
        public Loger(string path)
        {
            try
            {
                FileInfo fInfo = new FileInfo(path);
                fs = new StreamWriter(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                throw ex;
            }
        }

        ~Loger()
        {
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public bool Log(string data, Severity severity)
        {
            try
            {
                fs.WriteLine($"[{DateTime.Now}][{severity}]: {data} {Environment.NewLine} ");
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
    }

    class Program
    {
        static Random random = new Random();
        static Array values = Enum.GetValues(typeof(Loger.Severity));
        static int Main(string[] args)
        {
            Console.WriteLine("Enter path to file: ");
            string path = Console.ReadLine();
            Console.WriteLine($"Entered path: {path}");

            if (!File.Exists(path))
            {
                Console.WriteLine($"No file on the entered path: {path}\nNew file will be created!");
                try
                {
                    FileStream ffs = File.Create(path);
                    ffs.Close();
                    Console.WriteLine("created successfully");
                    
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine("[ERROR]: ", ex.Message);
                    return -1;
                }
            }

            Loger lg = new Loger(path);
            for (int i = 0; i < 100; i++)
            {
                lg.Log($"Some Data number s{i+1}", (Loger.Severity)values.GetValue(random.Next(values.Length))); 
            }
            lg.Dispose();
            return 0;
        }
    }
}
