using System;
using System.IO;
using System.Drawing;

namespace BadApple
{
    class Program
    {
        public static void Main()
        {
            Console.Title = "Bad Apple: Generating";
            ConsoleHelper.SetCurrentFont("Impact", 1);
            Console.Clear();
            int frameWidth = 0, frameHeight = 0, i=0;
            DirectoryInfo folder = new DirectoryInfo(@"");//Здесь должен быть путь до папки с фреймами
            FileInfo[] files = folder.GetFiles("*.png");
            Bitmap sample = new Bitmap(files[0].ToString());
            frameHeight = sample.Height;
            frameWidth = sample.Width*2;//Для корректного рендера нужно увеличить ширину в два раза
            string[,] video = new string[files.Length, frameHeight];
            Console.SetWindowSize(frameWidth, frameHeight);
            Console.SetBufferSize(frameWidth, frameHeight);
            foreach (FileInfo file in files)
            {
                Console.Title = "Generating "+i.ToString();
                Bitmap frame = new Bitmap(file.ToString());
                for (int y = 0; y < frame.Height; y++)
                {
                    for (int x = 0; x < frame.Width; x++)
                    {
                        if (Int32.Parse(HexConverter(frame.GetPixel(x, y)), System.Globalization.NumberStyles.HexNumber) < 8553090)
                            video[i, y] += "  ";
                        else
                            video[i, y] += "▮▮";
                    }
                }
                frame.Dispose();
                i++;
            }
            Console.ReadKey();
            int totalSize = i;
            for (i=0;i<totalSize;i++)
            {
                Console.Title = "Rendering " + i;
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                for (int j = 0; j < frameHeight; j++)
                {
                    Console.WriteLine(video[i, j]);
                }
                Thread.Sleep(70);
            }
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}