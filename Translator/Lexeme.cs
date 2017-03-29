using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    class Lexemes
    {
        public List<Lexeme> LexList;
        public List<Lexeme> IdList;
        public List<Lexeme> ConstList;
        public List<Metka> MetkaList;
        public List<Output> OutputList;
        public Lexemes()
        {
            LexList    = new List<Lexeme>();
            IdList     = new List<Lexeme>();
            ConstList  = new List<Lexeme>();
            MetkaList  = new List<Metka>();
            OutputList = new List<Output>();

            LexList.Add(new Lexeme("{"   , 1));
            LexList.Add(new Lexeme("}"   , 2));
            LexList.Add(new Lexeme(":"   , 3));
            LexList.Add(new Lexeme("for" , 4));
            LexList.Add(new Lexeme("to"  , 5));
            LexList.Add(new Lexeme("="   , 6));
            LexList.Add(new Lexeme("next", 7));
            LexList.Add(new Lexeme("\n"  , 8));
            LexList.Add(new Lexeme("if"  , 9));
            LexList.Add(new Lexeme("thengoto", 10));
            LexList.Add(new Lexeme("+"   , 11));
            LexList.Add(new Lexeme("-"   , 12));
            LexList.Add(new Lexeme("*"   , 13));
            LexList.Add(new Lexeme("/"   , 14));
            LexList.Add(new Lexeme("or"  , 15));
            LexList.Add(new Lexeme("and" , 16));
            LexList.Add(new Lexeme("["   , 17));
            LexList.Add(new Lexeme("]"   , 18));
            LexList.Add(new Lexeme("<"   , 19));
            LexList.Add(new Lexeme("<="  , 20));
            LexList.Add(new Lexeme(">"   , 21));
            LexList.Add(new Lexeme(">="  , 22));
            LexList.Add(new Lexeme("=="  , 23));
            LexList.Add(new Lexeme("!="  , 24));
            LexList.Add(new Lexeme("id"  , 25));
            LexList.Add(new Lexeme("con" , 26));
            LexList.Add(new Lexeme("do", 34));
            // 27 - мітки
            LexList.Add(new Lexeme(",", 28));
            LexList.Add(new Lexeme("(", 29));
            LexList.Add(new Lexeme(")", 30));
            LexList.Add(new Lexeme("read", 31));
            LexList.Add(new Lexeme("write", 32));
            LexList.Add(new Lexeme("not", 33));
        }
        public Lexeme inList(string st){
            for (int i = 0; i < LexList.Count; i++){
                if (st==LexList[i].Name) {
                    return LexList[i];
                }
            }
            return new Lexeme("NULL", -1);
        }
        public static List<string[]> ToLexemeStringArray(List<Lexeme> T)
        {
            List<string[]> Show = new List<string[]>();
            Show.Add(Lexeme.HeaderToArray());
            for (int i = 0; i < T.Count; i++)
            {
                Show.Add(Lexeme.ToArray(T[i]));
            }
            return Show;
        }
        public List<string[]> ToOutputStringArray()
        {
            List<string[]> Show = new List<string[]>();
            Show.Add(Output.HeaderToArray());
            for (int i = 0; i < OutputList.Count; i++)
            {
                Show.Add(Output.ToArray(OutputList[i]));
            }
            return Show;
        }
        public List<string[]> ToMetkaStringArray()
        {
            List<string[]> Show = new List<string[]>();
            Show.Add(Metka.HeaderToArray());
            for (int i = 0; i < MetkaList.Count; i++)
            {
                Show.Add(Metka.ToArray(MetkaList[i]));
            }
            return Show;
        }
    }

    class Lexeme{
        public string Name { get; set; }
        public int Kod { get; set; }

        public Lexeme(string Name, int Id)
        {
            this.Name = Name;
            this.Kod = Id;
        }
        public static string[] HeaderToArray()
        {
            return new string[] { "Name", "Code"};
        }
        public static string[] ToArray(Lexeme Ob)
        {
            return new string[] { Ob.Name, Ob.Kod.ToString() };
        }
    }

    class Metka{
        public string Name { get; set; }
        public int Kod { get; set; }
        public int Defined { get; set; }
        public int Used { get; set; }

        public Metka(string Name, int Kod)
        {
            this.Name = Name;
            this.Kod = Kod;
        }

        public void SetDefined(){
            Defined = 1;
        }

        public void SetUsed(){
            Used = 1;
        }
        
        public static string[] HeaderToArray()
        {
             return new string[] { "Name", "Code", "Defined", "Used" };
        }

        public static string[] ToArray(Metka Ob)
        {
            return new string[] { Ob.Name, Ob.Kod.ToString(), Ob.Defined.ToString(), Ob.Used.ToString() };
        }
    }

    class Output{
        public int Line { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int KodInTable { get; set; }

        public Output(int Line, string Name, int code, int KodIn)
        {
            this.Line = Line;
            this.Name = Name;
            this.Code = code;
            this.KodInTable = KodIn;
        }
        public static string[] ToArray(Output Ob){
            return new string[] { Ob.Line.ToString(), Ob.Name, Ob.Code.ToString(), Ob.KodInTable!=0?Ob.KodInTable.ToString():" " };
        }
        public static string[] HeaderToArray()
        {
            return new string[] { "Line", "Name", "Code", "KodInTable" };
        }
    }
}