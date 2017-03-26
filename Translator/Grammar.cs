using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class Grammar
    {
        public  Dictionary<string, string> grammar = new Dictionary<string, string>();
        public Grammar()
        {
             grammar.Add("<вир>", "<вир1>");
             grammar.Add("<терм1>", "<вир>");
             grammar.Add("<вир> + <терм1>", "<вир>");
             grammar.Add("<вир> - <терм1>", "<вир>");
             grammar.Add("<терм>", "<терм1>");
             grammar.Add("<множ>", "<терм>");
             grammar.Add("<терм> * <множ>", "<терм>");
             grammar.Add("<терм> / <множ>", "<терм>");
             grammar.Add("( <вир1> )", "<множ>");
             grammar.Add("константа", "<множ>");
             grammar.Add("ід", "<множ>");
        }
    }
}
