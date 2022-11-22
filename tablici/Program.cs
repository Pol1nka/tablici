using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace tablici
{
    class program
    {
        private const int minItemSize = 1;
        private const int maxItemSize = 999;
        // константы для определения чисел внутри ячеек таблицы

        private static int[,] table;
        private static string[,] readyTable;        

        private static int width;
        private static int height;

        static void Main()
        {
            itemSizes();
            initTable();
            fullTable();
            NormalizeTable();
            drawTable();
            Thread.Sleep(10000);
            // continueorno();
        }

        static void continueorno()
        {
            Console.Clear();
            Console.WriteLine("Хотите ли вы начать программу заново?");
            Console.ReadKey();
            
        }


        static void itemSizes()
        {
            Console.Write("Введите ширину: ");
            while (width <= 0 || width>=25)
            {
                try
                {
                    width = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    if (width <= 0 || width >= 25) throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Неверный размер, введите еще раз: ");
                }
            }
            
            Console.Write("Введите высоту: ");
            while (height <= 0 || height>= 25)
            {
                try
                {
                    height = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    if (height <= 0 || height>=25) throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Неверный размер, введите еще раз: ");
                }
            }
        }

        static void initTable()
        {
            table = new int [height, width];
            readyTable = new string[height, width];
        }

        static void fullTable()
        {
            Random num = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    table[i, j] = num.Next(minItemSize, maxItemSize);
                    readyTable[i, j] = table[i, j].ToString();
                }
            }
        }

        private static void NormalizeTable()
        {
            for (int i = 0; i < width; i++)
            {
                int[] column = new int[height];
                for (int j = 0; j < height; j++)
                {
                    column[j] = table[j, i];
                }

                int maxLength = column.Max().ToString().Length;
                for (int j = 0; j < height; j++)
                {
                    int currentLength = readyTable[j, i].Length;
                    int diff = maxLength - currentLength;
                    if (diff > 0)
                    {
                        for (int k = 0; k < diff; k++)
                        {
                            readyTable[j, i] = "E" + readyTable[j, i];
                        }
                    }
                }
            }
        }
        
        private static int GetRowMaxWidth()
        {
            StringBuilder builder = new StringBuilder();
            
            for (int i = 0; i < height;)
            {
                for (int j = 0; j < width; j++)
                {
                    builder.Append("| " + readyTable[i, j].Replace('E', ' ') + " ");
                }
                builder.Append("|");
                
                break;
            }

            return builder.ToString().Length;
        }
        private static void DrawVerticalBorder(bool realVertical = false, bool realDown = false)
        {
            int maxRowWidth = GetRowMaxWidth();
            char startSymbol = '├';
            char endSymbol = '┤';
            if (realVertical)
            {
                startSymbol = '┌';
                endSymbol = '┐';
            }

            if (realDown)
            {
                startSymbol = '└';
                endSymbol = '┘';
            }
            for (int j = 0; j < maxRowWidth; j++)
            {
                if(j == 0) Console.Write(startSymbol);
                if(j == maxRowWidth - 1) Console.Write(endSymbol);
                if(j > 0 && j < maxRowWidth - 1) Console.Write('─');
            }
        }

        static void drawTable()
        {
            DrawVerticalBorder(true);
            for (int i = 0; i < height; i++)
            {
                if (i!=0) DrawVerticalBorder();
                Console.WriteLine();
                for (int j = 0; j < width; j++)
                {
                    Console.Write("│ " + readyTable[i, j].Replace('E', ' ') + " ");
                }
                Console.Write("│");
                Console.WriteLine();
            }
            DrawVerticalBorder(false,true);
        }
    }
}