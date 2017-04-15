using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Translator
{
    class View
    {

        public static void ShowLexTables(Lexemes lexemes)
        {
            Table.ShowTable(lexemes.ToOutputStringArray(), "OUTPUT");
            Table.ShowTable(Lexemes.ToLexemeStringArray(lexemes.IdList), "Identificators");
            Table.ShowTable(Lexemes.ToLexemeStringArray(lexemes.ConstList), "Constants");
            Table.ShowTable(lexemes.ToMetkaStringArray(), "Metkas");
        }

        public static void Yellow(string s)
        {
            var cCol = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s);
            Console.ForegroundColor = cCol;
        }

        public static void ShowError(string Text)
        {
            ConsoleColor Prew = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + Text);
            Console.ForegroundColor = Prew;
        }

        public static void ShowWarning(string Text) 
        {
            ConsoleColor Prew = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + Text);
            Console.ForegroundColor = Prew;
        }
        public static string GetText()
        {
            string text;
            text = File.ReadAllText(
                        @"../../myProg.txt",
                        Encoding.UTF8);
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            text = regex.Replace(text, " ");
            text = text.Replace("\n ", "\n");
            return text;
        }
    }
}
