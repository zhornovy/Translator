using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Translator
{
    internal class PrecedenceAnalyzerPolizArithmatic
    {
        private static Grammar g = new Grammar();
        private Dictionary<string, string> gramm = g.grammar;
        private List<Output> lexTable;
        private List<PolizElem> POLIZ = new List<PolizElem>();
        private List<Relation> rels = Rels.relations;
        private Stack<Output> stack = new Stack<Output>();

        public PrecedenceAnalyzerPolizArithmatic(List<Output> table)
        {
            lexTable = table;
        }

        private static string ToString(object o)
        {
            if (o is string)
                return o as string;
            var type = (o as Output).Code;
            if (type == 25)
                return "ід";
            if (type == 26)
                return "константа";
            if (type == 27)
                return "мітка";
            return (o as Output).Name;
        }

        private string Compare(object f, object s)
        {
            string firstElem, secondElem;
            firstElem = ToString(f);
            secondElem = ToString(s);
            if (firstElem == "#")
                return "<";
            if (secondElem == "#")
                return ">";
            return rels.First(a => a.firstS == firstElem && a.secondS == secondElem)
                .symbol;
        }

        public void Do()
        {
            var i = 0;
            stack.Push(new Output(0, "#",0,0));
            var endSymb = 0;
            while (!(i >= lexTable.Count-1 && stack.Count<=2))
            {
                var relation = "";
                if (i == 0)
                {
                    stack.Push(lexTable[i]);
                    if (i >= lexTable.Count - 1)
                        endSymb = 1;
                    else
                        i++;
                    continue;
                }

                if (endSymb == 0)
                    relation = Compare(stack.Peek(), lexTable[i]);
                else
                    relation = Compare(stack.Peek(), "#");

                var tempList = stack.ToList();
                tempList.Reverse();
                tempList.ForEach(x => Console.Write(x.Name));
                Console.Write("\t");
                View.Yellow(relation);
                if (endSymb == 0)
                {
                    Console.WriteLine("\t{0} {1} {2}", lexTable[i].Name,
                        i + 1 < lexTable.Count ? lexTable[i + 1].Name : "",
                        i + 2 < lexTable.Count ? lexTable[i + 2].Name : "");
                }
                else
                {
                    Console.WriteLine("\t#");
                }
                


                if (relation == "<" || relation == "=")
                {
                    stack.Push(lexTable[i]);
                    if (i >= lexTable.Count - 1)
                        endSymb = 1;
                    else
                        i++;
                    continue;
                }
                if (relation == ">")
                {
                    
                    var subText = new List<string>();
                    var adder = stack.Pop();
                    if (adder.Code == 25)
                        subText.Add("ід");
                    if (adder.Code == 26)
                        subText.Add("константа");
                    else
                        subText.Add(adder.Name);

                    while (Compare(stack.Peek(), subText[subText.Count - 1]) != "<" && stack.Peek().Name != "#")
                    {
                        adder = stack.Pop();
                        if (adder.Code == 25)
                            subText.Add("ід");
                        if (adder.Code == 26)
                            subText.Add("константа");
                        else
                            subText.Add(adder.Name);
                    }
                    subText.Reverse();
                    var subTestS = subText[0];
                    for (var j = 1; j < subText.Count; j++)
                        subTestS += " " + subText[j];
                    if (i == lexTable.Count - 1 && subText[subText.Count - 1] == "<сп.оп1>")
                        subTestS += " " + lexTable[i].Name;

                    if (adder.Code == 25)
                        POLIZ.Add(new PolizElem(adder.Name,PolizType.id));
                    if (adder.Code == 26)
                        POLIZ.Add(new PolizElem(adder.Name, PolizType.con));
                    else
                    {
                        if (subTestS.IndexOf("+")>0)
                        {
                            POLIZ.Add(new PolizElem("+", PolizType.symbol));
                        }
                        if (subTestS.IndexOf("-") > 0)
                        {
                            POLIZ.Add(new PolizElem("-", PolizType.symbol));
                        }
                        if (subTestS.IndexOf("*") > 0)
                        {
                            POLIZ.Add(new PolizElem("*", PolizType.symbol));
                        }
                        if (subTestS.IndexOf("/") > 0)
                        {
                            POLIZ.Add(new PolizElem("/", PolizType.symbol));
                        }
                    }
                    stack.Push(new Output(0, gramm[subTestS],0,0));
                    
                }
            }
            Console.WriteLine("Ok Ok Ok");
            POLIZ.ForEach(x => Console.Write(x.Name + " "));
            Console.WriteLine();
            View.Yellow("Res = ");
            Console.WriteLine(PolizCalc.Calc(POLIZ).ToString().Replace(",","."));
        }
    }
}