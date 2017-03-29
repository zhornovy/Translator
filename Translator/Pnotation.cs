using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    //Reverse Polish notation

    enum PolizType
    {
        id, con, symbol , metka
    }
    class PolizElem
    {
        public readonly string Name;
        public readonly PolizType Type;
        public readonly int Prior;

        public PolizElem(string n, PolizType t)
        {
            Name = n;
            Type = t;
        }
    }

    class Pnotation
    {
        private List<Output> lexemesOutput;
        private static Grammar g = new Grammar();
        private Dictionary<string, string> gramm = g.grammar;
        private List<PolizElem> POLIZ = new List<PolizElem>();
        private Stack<Output> stack = new Stack<Output>();
        public Pnotation(List<Output> LO )
        {
            lexemesOutput = LO;
            DO();
        }

        public int GetPriory(string s)
        {
            switch (s)
            {
                case "(":
                    return 0;
                case ")":
                case "=":
                    return 1;
                case "or":
                    return 2;
                case "and":
                    return 3;
                case "not":
                    return 4;
                case "<":
                case ">":
                case "<=":
                case ">=":
                case "==":
                case "!=":
                    return 5;
                case "+":
                case "-":
                    return 6;
                case "*":
                case "/":
                    return 7;
            }
            return -1;
        }

        public void DO()
        {
            var i = 0;
            while (!(i >= lexemesOutput.Count))
            {
                var curLex = lexemesOutput[i];
                if (curLex.Code == 25 || curLex.Code == 26 || curLex.Code == 27)
                {
                    if (curLex.Code == 25)
                    {
                        POLIZ.Add(new PolizElem(curLex.Name, PolizType.id));
                    }
                    if (curLex.Code == 26)
                    {
                        POLIZ.Add(new PolizElem(curLex.Name, PolizType.con));
                    }
                    if (curLex.Code == 27)
                    {
                        POLIZ.Add(new PolizElem(curLex.Name, PolizType.metka));
                    }
                    i++;
                    continue;
                }
                if (curLex.Code >= 11 && curLex.Code <= 24 || curLex.Code == 33 || curLex.Name == "=")
                {
                    while (true)
                    {
                        if (stack.Count == 0 || GetPriory(stack.Peek().Name) < GetPriory(curLex.Name))
                        {
                            stack.Push(curLex);
                            break;
                        }
                        if (GetPriory(stack.Peek().Name) >= GetPriory(curLex.Name))
                        {
                            POLIZ.Add(new PolizElem(stack.Pop().Name, PolizType.symbol));
                        }
                    }

                    i++;
                    continue;
                }
                if (curLex.Name == "(")
                {
                    stack.Push(curLex);
                    i++;
                    continue;
                }
                if (curLex.Name == ")")
                {
                    while (stack.Peek().Name != "(")
                    {
                        POLIZ.Add(new PolizElem(stack.Pop().Name, PolizType.symbol));
                    }
                    stack.Pop(); // Deleting '(' symbol from the stack
                    i++;
                    continue;
                }
                if (curLex.Name == "if")
                {
                    stack.Push(curLex);
                    i++;
                    continue;
                }
                if (curLex.Name == "thengoto")
                {
                    while (stack.Peek().Name != "if")
                    {
                        POLIZ.Add(new PolizElem(stack.Pop().Name, PolizType.symbol));
                    }
                    POLIZ.Add(new PolizElem("mi UPL",PolizType.symbol));
                    stack.Push(new Output(0, "mi", 0, 0));
                    i++;
                    continue;
                }
                if (curLex.Name == "\\n")
                {
                    while (stack.Peek().Name != "if")
                    {
                        POLIZ.Add(new PolizElem(stack.Pop().Name, PolizType.symbol));
                    }
                    stack.Pop(); // Deleting 'if' symbol from the stack
                    i++;
                    continue;
                }
            }
            while (stack.Count != 0)
            {
                POLIZ.Add(new PolizElem(stack.Pop().Name, PolizType.symbol));
            }
            POLIZ.ForEach(x => Console.Write(x.Name + " "));
            Console.WriteLine();
        }
    }
}
