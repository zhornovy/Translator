using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    class Exit{
        public static int Ex = 1;
        public static int Er = -1;
        public static int? No = null;
    }
    class TableElem
    {
        public int St;
        public int Symbol;
        public int? Stack;
        public int? Beta;
        public int? Exit = null;
        public TableElem()
        {

        }
        public TableElem(int Stat, int Sym, int? Bet,int? St,int? Ex)
        {
            this.St = Stat;
            this.Symbol = Sym;
            this.Beta = Bet;
            this.Stack = St;
            this.Exit = Ex;
        }
    }
    
    class AutomatTable
    {
        int? n = null;
        public List<TableElem> table = new List<TableElem>();
        public AutomatTable()
        {

            table.Add(new TableElem(1, 1,  3,   2, Exit.No));      //St: 1, Sym: "{", B= 3, St= 2, ExitNeRavno = -1 });
            table.Add(new TableElem(1, 0,  n,   n, Exit.Er));
            table.Add(new TableElem(2, 2,  n,   n, Exit.Ex));      //St: 2, Sym: "}", ExRno = 1 });
            table.Add(new TableElem(2, 8,  3,   2, Exit.No));      //St:2, Sym: "\\n", B= 3, St= 2, ExitNeRavno = -1 });
            table.Add(new TableElem(2, 0,  n,   n, Exit.Er));
            table.Add(new TableElem(3, 27, 4,   n, Exit.No));      //St: 3, Sym: "mitka", B= 4 });
            table.Add(new TableElem(3, 0,  6,   5, Exit.No));      //St: 3, Sym: "Empty", B= 6, St= 5 });
            table.Add(new TableElem(4, 3,  6,   5, Exit.No));      //St: 4, Sym: ":", B= 6, St= 5, ExitNeRavno = -1 });
            table.Add(new TableElem(4, 0,  n,   n, Exit.Er));
            table.Add(new TableElem(5, 0,  n,   n, Exit.Ex));      //St: 5, Sym: "Empty", ExitNeRavno = 1 });
            table.Add(new TableElem(6, 25, 7,   n, Exit.No));      //St: 6, Sym: "id", B = 7});
            table.Add(new TableElem(6, 9,  18,  9, Exit.No));      //St: 6, Sym: "if", B= 18, St=9 });
            table.Add(new TableElem(6, 4,  11,  n, Exit.No));      //St: 6, Sym: "for", B= 11 });
            table.Add(new TableElem(6, 31, 16,  n, Exit.No));      //St: 6, Sym: "read", B= 16 });
            table.Add(new TableElem(6, 32, 16,  n, Exit.No));      //St: 6, Sym: "write", B= 16,ExitNeRavno=-1 });
            table.Add(new TableElem(6, 0,  n,   n, Exit.Er));
            table.Add(new TableElem(7, 6,  22,  5, Exit.No));      //St: 7, Sym: "=", B= 22, St= 5 });
            table.Add(new TableElem(7, 0,  n,   n, Exit.Er));
            table.Add(new TableElem(9, 10, 10,  n, Exit.No));      //St: 9, Sym: "thengoto", B= 10,ExitNeRavno=-1 });
            table.Add(new TableElem(9, 0,  n,   n, Exit.Er));
            table.Add(new TableElem(10,27, n,   n, Exit.Ex));      //St: 10, Sym: "mikta", ExRno = 1, ExitNeRavno = -1 });
            table.Add(new TableElem(10,0,  n,   n, Exit.Er));
            table.Add(new TableElem(11,25, 12,  n, Exit.No));      //St: 11, Sym: "id", B= 12,ExitNeRavno=-1 });
            table.Add(new TableElem(11,0,  n,   n, Exit.Er));
            table.Add(new TableElem(12,6,  22,  13,Exit.No));      //St: 12, Sym: "=", B= 22 , St=13,ExitNeRavno=-1});
            table.Add(new TableElem(12,0,  n,   n, Exit.Er));
            table.Add(new TableElem(13,5,  22,  14,Exit.No));      //St: 13, Sym: "to", B= 22, St=14,ExitNeRavno=-1 });
            table.Add(new TableElem(13,0,  n,   n, Exit.Er));
            table.Add(new TableElem(14,8,  3,   15,Exit.No));      //St: 14, Sym: "\n", B= 3, St=15,ExitNeRavno=-1 });
            table.Add(new TableElem(14,0,  n,   n, Exit.Er));
            table.Add(new TableElem(15,7,  n,   n, Exit.Ex));      //St: 15, Sym: "next", ExRno=1 });
            table.Add(new TableElem(15,8,  3,   15,Exit.No));      //St: 15, Sym: "\\n", B= 3, St=15, ExitNeRavno=-1 });
            table.Add(new TableElem(15,0,  n,   n, Exit.Er));
            table.Add(new TableElem(16,29, 26,  17,Exit.No));      //St: 16, Sym: "(", B= 26, St=17, ExitNeRavno=-1 });
            table.Add(new TableElem(16,0,  n,   n, Exit.Er));
            table.Add(new TableElem(17,30, n,   n, Exit.No));      //St: 17, Sym: ")", ExRno=1, ExitNeRavno=-1});
            table.Add(new TableElem(17,0,  n,   n, Exit.Ex));
            table.Add(new TableElem(18,33, 18,  n, Exit.No));      //St: 18, Sym: "not", Beta=18 });
            table.Add(new TableElem(18,17, 18,  21,Exit.No));      //St: 18, Sym: "[", B= 18, St=21});
            table.Add(new TableElem(18,0,  22,  19,Exit.No));      //St: 18, Sym: "Empty", B= 22, St=19});
            table.Add(new TableElem(19,19, 22,  20,Exit.No));      //St: 19, Sym: "<", B= 22, St=20 });
            table.Add(new TableElem(19,21, 22,  20,Exit.No));      //St: 19, Sym: ">", B= 22, St= 20 });
            table.Add(new TableElem(19,20, 22,  20,Exit.No));      //St: 19, Sym: "<=", B= 22, St= 20 });
            table.Add(new TableElem(19,22, 22,  20,Exit.No));      //St: 19, Sym: ">=", B= 22, St= 20,ExitNeRavno=-1 });
            table.Add(new TableElem(19,0,  n,   n, Exit.Er));
            table.Add(new TableElem(20,16, 18,  n, Exit.No));      //St: 20, Sym: "and", B= 18});
            table.Add(new TableElem(20,15, 18,  n, Exit.No));      //St: 20, Sym: "or", B= 18, ExitNeRavno=1});
            table.Add(new TableElem(20,0,  n,   n, Exit.Ex));
            table.Add(new TableElem(21,18, 20,  n, Exit.No));      //St: 21, Sym: "]", B= 20, ExitNeRavno = -1 });
            table.Add(new TableElem(21,0,  n,   n, Exit.Er));
            table.Add(new TableElem(22,0,  24,  23,Exit.No));      //St: 22, Sym: "Empty", B= 24, St= 23 });
            table.Add(new TableElem(23,12, 24,  23,Exit.No));      //St: 23, Sym: "-", B= 24, St= 23});
            table.Add(new TableElem(23,11, 24,  23,Exit.No));      //St: 23, Sym: "+", B= 24, St= 23, ExitNeRavno = 1 });
            table.Add(new TableElem(23,0,  n,   n, Exit.Ex));
            table.Add(new TableElem(24,0,  28,  25,Exit.No));      //St: 24, Sym: "Empty", B= 28, St= 25 });
            table.Add(new TableElem(25,13, 28,  25,Exit.No));      //St: 25, Sym: "*", B= 28, St= 25});
            table.Add(new TableElem(25,14, 28,  25,Exit.No));      //St: 25, Sym: "/", B= 28, St= 25, ExitNeRavno = 1 });
            table.Add(new TableElem(25,0,  n,   n, Exit.Ex));
            table.Add(new TableElem(26,25, 27,  n, Exit.No));      //St: 26, Sym: "id", B= 27,  ExitNeRavno = -1 });
            table.Add(new TableElem(26,0,  n,   n, Exit.Er));
            table.Add(new TableElem(27,28, 26,  n, Exit.No));      //St: 27, Sym: ",", B= 26, ExitNeRavno = 1 });
            table.Add(new TableElem(27,0,  n,   n, Exit.Ex));
            table.Add(new TableElem(28,25, n,   n, Exit.Ex));      //St: 28, Sym: "id", ExRno = 1 });
            table.Add(new TableElem(28,26, n,   n, Exit.Ex));      //St: 28, Sym: "con", ExRno = 1 });
            table.Add(new TableElem(28,29, 22,  29,Exit.No));      //St: 28, Sym: "(", B= 22, St=29, ExitNeRavno = -1 });
            table.Add(new TableElem(28,0,  n,   n, Exit.Er));
            table.Add(new TableElem(29,30, n,   n, Exit.Ex));      //St: 29, Sym: ")", ExRno = 1, ExitNeRavno = -1 });
            table.Add(new TableElem(29,0,  n,   n, Exit.Er));
        }
    }
    class AutomatWithStack
    {
        List<TableElem> table;
        public int? state = 1;
        int Line = 1;
        Stack<int?> stack = new Stack<int?>();
        List<Output> lexemes;
        public AutomatWithStack(AutomatTable t, List<Output> lex)
        {
            table = t.table;
            lexemes = lex;
        }
        public List<TableElem> getCurrentStates(int? state)
        {
            List<TableElem> currentStates = new List<TableElem>();
            for (int j = 0; j < table.Count; j++)
            {
                if (table[j].St == state)
                {
                    currentStates.Add(table[j]);
                }
            }
            return currentStates;
        }
        public void Initializer()
        {
            for (int i = 0; i < lexemes.Count; i++)
            {
                try
                {
                     Check(lexemes[i]);
                }
                catch (Exception ex)
                { 
                    View.ShowError(ex.Message);
                    break;
                }
            }
            if (lexemes[lexemes.Count-1].Kod!=2)
            {
                View.ShowError("Unexpected end of file");
            }
        }
        public void Check(Output lex){
            List<TableElem> currentStates = getCurrentStates(state);
            bool noincrement = false;
            for (int j = 0; j < currentStates.Count; j++)
            {
                if (currentStates[j].Symbol == lex.Kod)
                {
                    if (lex.Kod == 8)
                    {
                        Line++;
                    }
                    GetBetaStack(currentStates, j);
                    if (currentStates[j].Exit==Exit.Ex)
                    {
                        
                        if (stack.Count>0)
                        {
                            state = stack.Pop();
                        }
                        else
                        {
                            Console.WriteLine("OK OK OK");
                        }
                    }
                    if (currentStates[j].Exit == Exit.Er)
                    {
                        throw new Exception("Line: "+Line+" near lexeme :"+ lex.Name);
                    }
                    break;
                }
                else if (currentStates[j].Symbol == 0)
                {
                    noincrement = GetBeta(currentStates, noincrement, j);
                    GetStack(currentStates, j);
                    if (currentStates[j].Exit == Exit.Ex)
                    {
                        if (stack.Count > 0)
                        {
                            state = stack.Pop();
                            noincrement = true;
                        }
                        else
                        {
                            Console.WriteLine("OK OK OK");
                        }
                    }
                    if (currentStates[j].Exit == Exit.Er)
                    {
                        throw new Exception("Line: " + Line + " near lexeme :" + lex.Name);
                    }
                }
            }
            if (noincrement)
            {
                Check(lex);
            }
        }

        private void GetBetaStack(List<TableElem> currentStates, int j)
        {
            if (currentStates[j].Beta != null)
            {
                state = currentStates[j].Beta;
            }
            if (currentStates[j].Stack != null)
            {
                stack.Push(currentStates[j].Stack);
            }
        }

        private void GetStack(List<TableElem> currentStates, int j)
        {
            if (currentStates[j].Stack != null)
            {
                stack.Push(currentStates[j].Stack);
            }
        }

        private bool GetBeta(List<TableElem> currentStates, bool noincrement, int j)
        {
            if (currentStates[j].Beta != null)
            {
                state = currentStates[j].Beta;
                noincrement = true;
            }
            return noincrement;
        }
    }
}
