using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    
    class Automat
    {
        public int Stan = 1;
        string futureLex = "";
        public int Line = 1;
        private Lexemes lexemes;
        public Automat(Lexemes getLexemes)
        {
            this.lexemes = getLexemes; 
        }

        public int CheckMetka()
        {
            var Error = 0;
            for (int i = 0; i < lexemes.MetkaList.Count; i++)
            {
                if ((lexemes.MetkaList[i].Used == 1) && (lexemes.MetkaList[i].Defined == 0))
                {
                    View.ShowError(lexemes.MetkaList[i].Name + " Used but not Defined");
                    Error = 1;
                }
                else if ((lexemes.MetkaList[i].Used == 0) && (lexemes.MetkaList[i].Defined == 1))
                {
                    View.ShowWarning(lexemes.MetkaList[i].Name + " Defined but not Used");

                }
            }
            return Error;
        }

        private void AddToOutput(int Line, string Name, int Kod,int KodIn)
        {
            if (Name=="\n")
            {
                Name = "\\n";
            }
            lexemes.OutputList.Add(new Output(Line, Name, Kod, KodIn));
            Stan = 1;
        }

        private void endMeIdentif(int Line, string Name)
        {
            Lexeme iSHere = null;
            for (int i = 0; i < lexemes.IdList.Count; i++){
                if (lexemes.IdList[i].Name == Name)
                {
                    iSHere = lexemes.IdList[i];
                    break;
                }
            }
            if (iSHere==null){
                int maxKod = 0;

                for (int i = 0; i < lexemes.IdList.Count; i++)
                {
                    if (maxKod < lexemes.IdList[i].Kod)
                    {
                        maxKod = lexemes.IdList[i].Kod;
                    }
                }
                maxKod = maxKod + 1;
    
                iSHere = new Lexeme(Name, maxKod);
                lexemes.IdList.Add(iSHere);
            }
            AddToOutput(Line, iSHere.Name, 25 ,iSHere.Kod);
            Stan = 1;
        }
        private Metka endMetka(int Line, string Name)
        {
            Metka iSHere = null;
            for (int i = 0; i < lexemes.MetkaList.Count; i++)
            {
                if (lexemes.MetkaList[i].Name == Name)
                {
                    iSHere = lexemes.MetkaList[i];
                    break;
                }
            }
            if (iSHere == null)
            {
                var maxKod = 0;
                for (int i = 0; i < lexemes.MetkaList.Count; i++)
                {
                    if (maxKod < lexemes.MetkaList[i].Kod)
                    {
                        maxKod = lexemes.MetkaList[i].Kod;
                    }
                }
                maxKod = maxKod + 1;

                iSHere = new Metka(Name, maxKod);
                lexemes.MetkaList.Add(iSHere);
            }
            AddToOutput(Line, iSHere.Name, 27, iSHere.Kod);
            Stan = 1;
            return iSHere;
        }

        private void endMeConst(int Line, string Name)
        {
            Lexeme iSHere = null;
            for (int i = 0; i < lexemes.ConstList.Count; i++)
            {
                if (lexemes.ConstList[i].Name == Name)
                {
                    iSHere = lexemes.ConstList[i];
                    break;
                }
            }
            if (iSHere == null)
            {
                var maxKod = 0;
                for (int i = 0; i < lexemes.ConstList.Count; i++)
                {
                    if (maxKod < lexemes.ConstList[i].Kod)
                    {
                        maxKod = lexemes.ConstList[i].Kod;
                    }
                }
                maxKod = maxKod + 1;
                iSHere = new Lexeme(Name, maxKod);
                lexemes.ConstList.Add(iSHere);
            }
            AddToOutput(Line, iSHere.Name, 26, iSHere.Kod);
            Stan = 1;
        }
        public int ChangeState(char s){
            switch (Stan)
            {
                case 1:
                    if ((s>='a') && (s<='z')){
                        futureLex = s.ToString();
                        Stan = 2;
                    }else if (char.IsDigit(s) == true){
                        futureLex = s.ToString();
                        Stan = 3;
                    }else switch (s)
                    {
                        case '.':
                            futureLex = s.ToString();
                            Stan = 4;
                            break;
                        case '<':
                        case '>':
                        case '=':
                        case '!':
                            futureLex = s.ToString();
                            Stan = 9;
                            break;
                        case '\n':
                        {
                            Lexeme temp = lexemes.inList(s.ToString());
                            AddToOutput(Line, temp.Name, temp.Kod, 0);
                            Line++;
                            Stan = 1;
                        }
                            break;
                        case '\r':
                        case '\t':
                        case ' ':
                            Stan = 1;
                            break;
                        case '+':
                        case '-':
                        case '*':
                        case '/':
                        case ':':
                        case '{':
                        case '}':
                        case '[':
                        case ']':
                        case '(':
                        case ')':
                        case ',':
                        {
                            var temp = lexemes.inList(s.ToString());
                            AddToOutput(Line, temp.Name, temp.Kod, 0);
                        }
                            break;
                        default:
                            throw new Exception(Line + " Line: Unknown symbol - " + s.ToString() + " LexemAnalyzer");
                    }

                    break;
                case 2:
                    if (((s >= 'a') && (s <= 'z')) || ((s >= '0') && (s <= '9')))
                    {
                        futureLex += s.ToString();
                    }else if(s==':'){
                        var myMetka = endMetka(Line, futureLex);
                        myMetka.SetDefined();
                        ChangeState(s);
                    }
                    else
                    {
                        Lexeme temp = lexemes.inList(futureLex);
                        
                        if ( temp.Name != "NULL" ) {
                            AddToOutput(Line, temp.Name, temp.Kod, 0);
                        } else if (lexemes.OutputList.Count>0 && (lexemes.OutputList[lexemes.OutputList.Count - 1].Name == "thengoto")){
                            var myMetka = endMetka(Line, futureLex);
                            myMetka.SetUsed();
                            ChangeState(s);
                        }
                        else {
                            endMeIdentif(Line, futureLex);
                        }
                        ChangeState(s);
                    }
                    break;
                case 3:
                    if (Char.IsDigit(s) == true){
                        futureLex += s.ToString();
                    }else if(s=='.'){
                        futureLex += s.ToString();
                        Stan = 5;
                    }else if(s=='E'){
                        futureLex += s.ToString();
                        Stan = 6;
                    }else {
                        endMeConst(Line, futureLex);
                        ChangeState(s);
                    }
                    break;
                case 4:
                    if (Char.IsDigit(s) == true)
                    {
                        futureLex += s.ToString();
                        Stan = 5;
                    }else {
                        throw new Exception(Line+ " Line: Number Expected");
                    }
                    break;
                case 5:
                    if (Char.IsDigit(s) == true){
                        futureLex += s.ToString();
                    }else if (s == 'E'){
                        futureLex += s.ToString();
                        Stan = 6;
                    }else{
                        endMeConst(Line, futureLex);
                        ChangeState(s);
                    }
                    break;
                case 6:
                    if ((s=='+') || (s=='-'))
                    {
                         futureLex += s.ToString();
                         Stan = 7;
                    }
                    else if(Char.IsDigit(s))
                    {
                         futureLex += s.ToString();
                         Stan = 8;
                    }
                    else
                    {
                        throw new Exception(Line + " Line: Need a number or SIGN after 'E'");
                    }
                    break;
                case 7:
                    if (Char.IsDigit(s)){
                        futureLex += s.ToString();
                        Stan = 8;
                    }else{
                        throw new Exception(Line + " Line: Need a number after SIGN");
                    }
                    break;
                case 8:
                    if (Char.IsDigit(s))
                    {
                        futureLex += s.ToString();
                    }
                    else
                    {
                        endMeConst(Line, futureLex);
                        ChangeState(s);
                    }
                    break;
                case 9:
                    if (s=='=')
                    {
                        futureLex += s.ToString();
                        Lexeme temp = lexemes.inList(futureLex);
                        AddToOutput(Line, temp.Name, temp.Kod, 0);
                    }
                    else
                    {
                        Lexeme temp = lexemes.inList(futureLex);
                        AddToOutput(Line, temp.Name, temp.Kod, 0);
                        ChangeState(s);
                    }
                    break;
                default:
                    //throw new Exception(Line + " ERRROR Need Number in this line State = def");
                    break;
            }
            return -1;
        }
    }
}
