using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class Grammar
    {
        public Dictionary<string, string> grammar = new Dictionary<string, string>();
        public Grammar()
        {
            grammar.Add("{ <сп.оп1> }", "<прог>");
            grammar.Add("<сп.оп>", "<сп.оп1>");
            grammar.Add("<опер>", "<сп.оп>");
            grammar.Add("<сп.оп> \\n <опер>", "<сп.оп>");
            grammar.Add("<н.опер>", "<опер>");
            grammar.Add("мітка : <н.опер>", "<опер>");
            grammar.Add("read ( <сп.ідент1> )", "<н.опер>");
            grammar.Add("write ( <сп.ідент1> )", "<н.опер>");
            grammar.Add("for ід = <вир1> to <вир1> do <сп.оп1> next", "<н.опер>");
            grammar.Add("if <лог.вир1> thengoto мітка", "<н.опер>");
            grammar.Add("ід = <вир1>", "<н.опер>");
            grammar.Add("<сп.ідент>", "<сп.ідент1>");
            grammar.Add(", ід", "<сп.ідент>");
            grammar.Add("<сп.ідент> , ід", "<сп.ідент>");
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
            grammar.Add("<лог.вир>", "<лог.вир1>");
            grammar.Add("<лог.терм1>", "<лог.вир>");
            grammar.Add("<лог.вир> or <лог.терм1>", "<лог.вир>");
            grammar.Add("<лог.терм>", "<лог.терм1>");
            grammar.Add("<лог.множ>", "<лог.терм>");
            grammar.Add("<лог.терм> and <лог.множ>", "<лог.терм>");
            grammar.Add("<відношення>", "<лог.множ>");
            grammar.Add("[ <лог.вир1> ]", "<лог.множ>");
            grammar.Add("not <лог.множ>", "<лог.множ>");
            grammar.Add("<вир1> < <вир1>", "<відношення>");
            grammar.Add("<вир1> > <вир1>", "<відношення>");
            grammar.Add("<вир1> <= <вир1>", "<відношення>");
            grammar.Add("<вир1> >= <вир1>", "<відношення>");
            grammar.Add("<вир1> != <вир1>", "<відношення>");
            grammar.Add("<вир1> == <вир1>", "<відношення>");
        }
    }
}
