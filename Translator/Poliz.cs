using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
   class PolizCalc
    {
        private static double ParseNum(string s)
        {
            double res = 0;
            if (s[0]=='.')
            {
                s = "0" + s;
            }
            if (s[s.Length-1] == '.')
            {
                s = s + "0" ;
            }
            s = s.Replace('.', ',');
            int indexOfE = s.IndexOf("E");
            if (indexOfE > 0)
            {
                if (s[indexOfE - 1] == ',')
                {
                    s = s.Insert(indexOfE, "0");
                    indexOfE++;
                }
                string td = s.Substring(0, indexOfE);
                res = double.Parse(td);
                string poryadok = s.Substring(indexOfE + 1);
                int por = int.Parse(poryadok);
                if (por < 0)
                {
                    for (int i = 0; i < Math.Abs(por); i++)
                    {
                        res /= 10;
                    }
                }
                if (por >= 0)
                {
                    for (int i = 0; i < Math.Abs(por); i++)
                    {
                        res *= 10;
                    }
                }
            }
            else
            {
                res = double.Parse(s);
            }
            return res;
        }
        public static double Calc(List<PolizElem> poliz)
        {
            double res = 0;
            Stack<PolizElem> stack = new Stack<PolizElem>();
            int i = 0;
            while (i!=poliz.Count)
            {
                if (poliz[i].Type==PolizType.con || poliz[i].Type == PolizType.id)
                {
                    stack.Push(poliz[i]);
                }
                if (poliz[i].Type == PolizType.symbol)
                {
                    var elem1 = ParseNum(stack.Pop().Name);
                    var elem2 = ParseNum(stack.Pop().Name);
                    double tRes = 0;
                    if (poliz[i].Name == "+")
                    {
                        tRes = elem1 + elem2;
                    }
                    if (poliz[i].Name == "-")
                    {
                        tRes = elem1 - elem2;
                    }
                    if (poliz[i].Name == "*")
                    {
                        tRes = elem1 * elem2;
                    }
                    if (poliz[i].Name == "/")
                    {
                        tRes = elem1 - elem2;
                    }
                    stack.Push(new PolizElem(tRes + "", PolizType.con));
                }
                i++;
            }
            res = double.Parse(stack.Pop().Name);
            return res;
        }
    }
}

