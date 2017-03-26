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
            if (CurrentLexem.Kod == 1) // {
            {
                Current++;
                if (SpisOps())
                {
                    if (CurrentLexem.Kod == 2) // }
                        founded = true;
                }
            }
            return founded;
        }

        private bool SpisOps()
        {
            var founded = false;
            if (CurrentLexem.Kod == 27) // мітка
            {
                Current++;
                if (CurrentLexem.Kod == 3) // :
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
            while (founded && CurrentLexem.Kod == 8)
            {
                Current++;
                if (CurrentLexem.Name != "next" && CurrentLexem.Name != "}")
                {

                    if (CurrentLexem.Kod == 27) // мітка
                    {
                        Current++;
                        if (CurrentLexem.Kod == 3)
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
            if (CurrentLexem.Kod == 25) // id
            {
                Current++;
                if (CurrentLexem.Kod == 6) // =
                {
                    Current++;
                    if (Vyrazenie())
                    {


                        founded = true;

                    }
                }
            }
            /* Цикл */
            else if (CurrentLexem.Kod == 4) // for
            {
                Current++;
                if (CurrentLexem.Kod == 25) // id
                {
                    Current++;
                    if (CurrentLexem.Kod == 6) // =
                    {
                        Current++;
                        if (Vyrazenie())
                        {
                            if (CurrentLexem.Kod == 5) // to
                            {
                                Current++;
                                if (Vyrazenie())
                                {
                                    if (CurrentLexem.Kod == 8) // \n
                                    {
                                        Current++;
                                        if (SpisOps())
                                        {
                                            if (CurrentLexem.Kod == 7) // next
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
            else if (CurrentLexem.Kod == 9) // if
            {
                Current++;
                if (LogVyr())
                {
                    if (CurrentLexem.Kod == 10) // thengoto
                    {
                        Current++;
                        if (CurrentLexem.Kod == 27) // мітка
                        {
                            Current++;
                            founded = true;
                        }
                    }
                }
            }
            // read() and write()
            else if ((CurrentLexem.Kod == 31) || (CurrentLexem.Kod == 32)) // read
            {
                Current++;
                if (CurrentLexem.Kod == 29) // (
                {
                    Current++;
                    if (SpisId())
                    {
                        if (CurrentLexem.Kod == 30) // )
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
            while (founded && (CurrentLexem.Kod == 11 || CurrentLexem.Kod == 12)) // + or -
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
            while (founded && (CurrentLexem.Kod == 13 || CurrentLexem.Kod == 14)) // * or /
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
            if (CurrentLexem.Kod == 25 || CurrentLexem.Kod == 26) // id or const
            {
                founded = true;
            }
            else if (CurrentLexem.Kod == 29) // (
            {
                Current++;
                if (Vyrazenie())
                {
                    if (CurrentLexem.Kod == 30) // )
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
            while (CurrentLexem.Kod == 15) // or
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
            while (CurrentLexem.Kod == 16) // or
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
            if (CurrentLexem.Kod == 33) // not 
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
            else if (CurrentLexem.Kod == 17) // [
            {
                Current++;
                if (LogVyr())
                {
                    if (CurrentLexem.Kod == 18) //]
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
                if (CurrentLexem.Kod >= 19 && CurrentLexem.Kod <= 24)
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
            if (CurrentLexem.Kod == 25)//id
            {
                found = true;
                Current++;
            }
            while (CurrentLexem.Kod == 28) // ,
            {
                Current++;
                if (CurrentLexem.Kod == 25)//id
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