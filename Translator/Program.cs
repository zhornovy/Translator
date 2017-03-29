using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    static class Program
    {
        static void Main(string[] args)
        {
            var Errors = 0;
            string text = View.GetText();
            Lexemes lexemes = new Lexemes();
            Automat automat = new Automat(lexemes);
            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    automat.ChangeState(text[i]);
                }
            }
            catch (Exception e)
            {
                Errors = 1;
                View.ShowError(e.Message);
            }
            View.ShowLexTables(lexemes);
            Errors += automat.CheckMetka();
            //if (Errors == 0)
            //{
            //    try
            //    {
            //        var syn = new SyntaxAnalyzer(lexemes.OutputList);
            //        syn.Result();
            //    }
            //    catch (Exception ex)
            //    {

            //        View.ShowError(ex.Message);
            //    }
            //}
            //if (Errors == 0)
            //{
            //    var automatTable = new AutomatTable();
            //    var synAuto = new AutomatWithStack(automatTable, lexemes.OutputList);
            //    synAuto.Initializer();
            //}
            //if (Errors == 0)
            //{
            //    var pa = new PrecedenceAnalyzer(lexemes.OutputList);
            //    pa.Do();
            //}
            var pa = new Pnotation(lexemes.OutputList);
            if (Errors == 0)
            {
                //var pa = new Pnotation(lexemes.OutputList);
                //pa.Do();
            }
            Console.Read();
        }
    }
}
