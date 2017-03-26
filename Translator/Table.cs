using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    class Table
    {
        private static string GetSpaces(int kol)
        {
            string s = "";
            for (int i = 0; i < kol; i++)
            {
                s += " ";
            }
            return s;
        }
        private static void ShowTableHeader(string Header)
        {
            var Color = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(Header);
            Console.BackgroundColor = Color;
        }
        private static void ShowHeader(string[] Header)
        {
            var Color = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("  ");
            for (int j = 0; j < Header.Length-1; j++)
            {
                Console.Write(Header[j] + "   ");
            }
            Console.Write(Header[Header.Length - 1] + "  ");
            Console.BackgroundColor = Color;
            Console.WriteLine();           
        }
        public static void ShowTable(List<string[]> ShowList,string TableHeader = "")
        {
            int Deep = ShowList[0].Length;
            int[] MaxSymbols = new int[Deep];
            for (int i = 0; i < Deep; i++)
            {
                var tempMax = ShowList[0][i].Length;
                for (int j = 1; j < ShowList.Count; j++)
                {
                    tempMax = tempMax < ShowList[j][i].Length ? ShowList[j][i].Length : tempMax;
                }
                MaxSymbols[i] = tempMax;

            }
            for (int i = 0; i < Deep; i++)
            {
                for (int j = 0; j < ShowList.Count; j++)
                {
                    ShowList[j][i] = ShowList[j][i] + GetSpaces(MaxSymbols[i] - ShowList[j][i].Length);
                }

            }
            if (TableHeader != "")
            {
                int TotalWidth = MaxSymbols.Sum();
                TotalWidth += MaxSymbols.Length * 2 + 1;
                int AddLeft = 1;
                while (TableHeader.Length < TotalWidth)
                {
                    TableHeader = AddLeft == 1 ? " " + TableHeader : TableHeader + " ";
                    AddLeft = -AddLeft;
                }
                ShowTableHeader(TableHeader);
            }
            ShowHeader(ShowList[0]);
            for (int i = 1; i < ShowList.Count; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < Deep; j++)
			    {
			     Console.Write(ShowList[i][j] + " | ");
			    }
                Console.WriteLine(  ); 
            }
        }
    }

   
}
