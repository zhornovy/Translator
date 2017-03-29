using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{

    class SyntaxAnalyzer
    {
        public int Current = 0;
        List<Output> Table;
        Output CurrentLexem
        {
            get
            {
                if (Current < Table.Count) return Table[Current];
                else throw new Exception("Unexpected end of file");
            }
        }

        public SyntaxAnalyzer(List<Output> table)
        {
            this.Table = table;
        }

        public void Result()
        {
            var founded = Check();
            if (founded)
            {
                Console.WriteLine("SyntaxAnalyzer Ok!");
            }
            else
            {
                View.ShowError("Line " + CurrentLexem.Line + " Before " + CurrentLexem.Name + " Lexeme");
            }
        }
        public bool Check()
        {
            bool founded = false;
            if (CurrentLexem.Code == 1) // {
            {
                Current++;
                if (SpisOps())
                {
                    if (CurrentLexem.Code == 2) // }
                        founded = true;
                }
            }
            return founded;
        }

        private bool SpisOps()
        {
            var founded = false;
            if (CurrentLexem.Code == 27) // мітка
            {
                Current++;
                if (CurrentLexem.Code == 3) // :
                {
                    Current++;
                    if (Oper())
                        founded = true;
                }
            }
            else if (Oper())
            {

                founded = true;
            }
            while (founded && CurrentLexem.Code == 8)
            {
                Current++;
                if (CurrentLexem.Name != "next" && CurrentLexem.Name != "}")
                {

                    if (CurrentLexem.Code == 27) // мітка
                    {
                        Current++;
                        if (CurrentLexem.Code == 3)
                        { // :
                            Current++;
                            if (Oper())
                                founded = true;
                        }
                    }
                    else if (Oper())
                    {
                        founded = true;
                    }
                    else
                        founded = false;
                }
                else if (CurrentLexem.Name == "next" || CurrentLexem.Name == "}")
                {
                    return founded;
                }
                else
                {
                    founded = false;
                }
            }
            return founded;
        }

        private bool Oper()
        {
            var founded = false;
            /* Присвоение */
            if (CurrentLexem.Code == 25) // id
            {
                Current++;
                if (CurrentLexem.Code == 6) // =
                {
                    Current++;
                    if (Vyrazenie())
                    {


                        founded = true;

                    }
                }
            }
            /* Цикл */
            else if (CurrentLexem.Code == 4) // for
            {
                Current++;
                if (CurrentLexem.Code == 25) // id
                {
                    Current++;
                    if (CurrentLexem.Code == 6) // =
                    {
                        Current++;
                        if (Vyrazenie())
                        {
                            if (CurrentLexem.Code == 5) // to
                            {
                                Current++;
                                if (Vyrazenie())
                                {
                                    if (CurrentLexem.Code == 8) // \n
                                    {
                                        Current++;
                                        if (SpisOps())
                                        {
                                            if (CurrentLexem.Code == 7) // next
                                            {
                                                Current++;
                                                founded = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /* Логвир */
            else if (CurrentLexem.Code == 9) // if
            {
                Current++;
                if (LogVyr())
                {
                    if (CurrentLexem.Code == 10) // thengoto
                    {
                        Current++;
                        if (CurrentLexem.Code == 27) // мітка
                        {
                            Current++;
                            founded = true;
                        }
                    }
                }
            }
            // read() and write()
            else if ((CurrentLexem.Code == 31) || (CurrentLexem.Code == 32)) // read
            {
                Current++;
                if (CurrentLexem.Code == 29) // (
                {
                    Current++;
                    if (SpisId())
                    {
                        if (CurrentLexem.Code == 30) // )
                        {
                            Current++;
                            founded = true;
                        }
                    }
                }
            }
            return founded;
        }

        private bool Vyrazenie()
        {
            var founded = false;
            if (term())
            {
                founded = true;
            }
            while (founded && (CurrentLexem.Code == 11 || CurrentLexem.Code == 12)) // + or -
            {
                Current++;
                if (term())
                {
                    founded = true;
                }
                else
                {
                    founded = false;
                }
            }
            return founded;
        }

        private bool term()
        {
            var founded = false;
            if (mnoj())
            {
                founded = true;
            }
            while (founded && (CurrentLexem.Code == 13 || CurrentLexem.Code == 14)) // * or /
            {
                Current++;
                if (mnoj())
                {
                    founded = true;
                }
                else
                {
                    founded = false;
                }
            }
            return founded;
        }

        private bool mnoj()
        {
            var founded = false;
            if (CurrentLexem.Code == 25 || CurrentLexem.Code == 26) // id or const
            {
                founded = true;
            }
            else if (CurrentLexem.Code == 29) // (
            {
                Current++;
                if (Vyrazenie())
                {
                    if (CurrentLexem.Code == 30) // )
                    {
                        founded = true;
                    }
                }
            }
            Current++;
            return founded;
        }
        private bool LogVyr()
        {
            var founded = false;
            if (LogTerm())
            {
                founded = true;
            }
            while (CurrentLexem.Code == 15) // or
            {
                Current++;
                if (LogTerm())
                {
                    founded = true;
                }
                else
                {
                    founded = false;
                }
            }

            return founded;
        }
        private bool LogTerm()
        {
            var founded = false;
            if (LogMnoj())
            {
                founded = true;
            }
            while (CurrentLexem.Code == 16) // or
            {
                Current++;
                if (LogMnoj())
                {
                    founded = true;
                }
                else
                {
                    founded = false;
                }
            }

            return founded;
        }

        private bool LogMnoj()
        {
            var founded = false;
            if (Vydnos())
            {
                founded = true;
            }
            else {
                Current--;
            }
            if (CurrentLexem.Code == 33) // not 
            {
                Current++;
                if (LogMnoj())
                {
                    founded = true;
                }
                else
                {
                    founded = false;
                }
            }
            else if (CurrentLexem.Code == 17) // [
            {
                Current++;
                if (LogVyr())
                {
                    if (CurrentLexem.Code == 18) //]
                    {
                        Current++;
                        founded = true;
                    }
                }
            }


            return founded;
        }
        private bool Vydnos()
        {
            var founded = false;
            if (Vyrazenie())
            {
                if (CurrentLexem.Code >= 19 && CurrentLexem.Code <= 24)
                {
                    Current++;
                    if (Vyrazenie())
                    {
                        founded = true;
                    }
                }
            }
            return founded;
        }

        private bool SpisId()
        {
            var found = false;
            if (CurrentLexem.Code == 25)//id
            {
                found = true;
                Current++;
            }
            while (CurrentLexem.Code == 28) // ,
            {
                Current++;
                if (CurrentLexem.Code == 25)//id
                {
                    found = true;
                    Current++;
                }
                else
                {
                    found = false;
                }
            }
            return found;
        }
    }
}