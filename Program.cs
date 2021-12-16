using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Ascii
{
    class Program {

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.WriteLine("Do you want to continue? y/n");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                Console.Clear();
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead("../../../ascii.txt"))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Console.Write(new string(' ', (Console.WindowWidth - line.Length) / 2));
                        Console.WriteLine(line);
                    }
                }
                Console.ReadKey();
            } else
            {
                Environment.Exit(0);
            }
        }
    }
}