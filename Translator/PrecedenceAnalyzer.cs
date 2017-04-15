using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    class PrecedenceAnalyzer
    {
        List<Relation> rels = Rels.relations;
        List<Output> lexTable;
        Stack<string> stack = new Stack<string>();
        static Grammar g = new Grammar();
        Dictionary<string, string> gramm = g.grammar;
        //Dictionary<string, string> gramm;
        public PrecedenceAnalyzer(List<Output> table)
        {
            lexTable = table;
        }

        private static string ToString(object o)
        {
            if (o is string)
            {
                return o as string;
            }
            else
            {
                var type = (o as Output).Code;
                if (type == 25)
                {
                    return "ід";
                }
                else if (type == 26)
                {
                    return "константа";
                }
                else if (type == 27)
                {
                    return "мітка";
                }
                else
                {
                    return (o as Output).Name;
                }
            }
        }

        private string Compare(object f, object s)
        {
            string firstElem, secondElem;
            firstElem = ToString(f);
            secondElem = ToString(s);
            if (firstElem == "#")
            {
                return "<";
            }
            if (secondElem == "#")
            {
                return ">";
            }
            return rels.First(a => a.firstS == firstElem && a.secondS == secondElem)
                       .symbol;
        }

        public void Do()
        {
            var i = 0;
            stack.Push("#");
            while ( i < lexTable.Count)
            {
                string relation;
                if (i == 0)
                {
                    stack.Push(ToString(lexTable[i]));
                    i++;
                    continue;
                }
                if (i == lexTable.Count - 1)
                {
                    relation = ">";
                }
                else
                {
                    relation = Compare(stack.Peek(), lexTable[i]);
                }

                var tempList = stack.ToList();
                tempList.Reverse();
                tempList.ForEach(Console.Write);
                Console.Write( "\t");
                View.Yellow(relation);
                Console.WriteLine("\t{0} {1} {2}", lexTable[i].Name, i + 1 < lexTable.Count ? lexTable[i + 1].Name : "", i + 2 < lexTable.Count ? lexTable[i + 2].Name : "", relation);


                if (relation == "<" || relation == "=")
                {
                    stack.Push(ToString(lexTable[i]));
                    i++;
                    continue;
                }
                if (relation == ">")
                {
                    List<string> subText = new List<string>();
                    subText.Add(stack.Pop());
                    while (Compare(stack.Peek(), subText[subText.Count - 1]) != "<" && stack.Peek()!= "#")
                    {
                       subText.Add(stack.Pop()); 
                    }
                    subText.Reverse();
                    string subTestS = subText[0];
                    for (var j = 1; j < subText.Count; j++)
                    {
                        subTestS += " " + subText[j];
                    }
                    if (i == lexTable.Count - 1 && subText[subText.Count - 1] == "<сп.оп1>")
                    {
                         subTestS += " " + lexTable[i].Name;
                    }
                    stack.Push(gramm[subTestS]);
                    if (stack.Peek() == "<прог>")
                    {
                        Console.WriteLine("OK OK OK");
                        break;
                    }
                } 
            }
        }


    }
}
