using System;
using System.Collections.Generic;
using System.Text;

namespace AnalizadorLexicoER
{
    class Parser
    {
        Scanner _scanner;
        Token _token;
        double res = 0;
        List<Op> order = new List<Op>();
        int pasada = 0;
        private void E()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    T();
                    EP();
                    break;
                case TokenType.One:
                case TokenType.Two:
                case TokenType.Three:
                case TokenType.Four:
                case TokenType.Five:
                case TokenType.Six:
                case TokenType.Seven:
                case TokenType.Eight:
                case TokenType.Nine:
                case TokenType.Zero:
                case TokenType.LParen:
                    T();
                    EP();
                    break;
                default:
                    break;
            }
        }
        private void EP()
        {
            switch (_token.Tag)
            {
                case TokenType.Plus:
                    Match(TokenType.Plus);
                    T();
                    EP();
                    break;
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    T();
                    EP();
                    break;
                default:
                    break;
            }
        }
        private void F()
        {
            switch (_token.Tag)
            {
                case TokenType.Zero:
                    Match(TokenType.Zero);
                    break;
                case TokenType.One:
                    
                    Match(TokenType.One);
                    break;
                case TokenType.Two:
                    Match(TokenType.Two);
                    break;
                case TokenType.Three:
                    Match(TokenType.Three);
                    break;
                case TokenType.Four:
                    Match(TokenType.Four);
                    break;
                case TokenType.Five:
                    Match(TokenType.Five);
                    break;
                case TokenType.Six:
                    Match(TokenType.Six);
                    break;
                case TokenType.Seven:
                    Match(TokenType.Seven);
                    break;
                case TokenType.Eight:
                    Match(TokenType.Eight);
                    break;
                case TokenType.Nine:
                    Match(TokenType.Nine);
                    break;
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    E();
                    Match(TokenType.RParen);
                    break;

                default:
                    break;
            }
        }
        private void T() //ya
        {
            switch (_token.Tag)
            {
                case TokenType.LParen:
                case TokenType.One:
                case TokenType.Two:
                case TokenType.Three:
                case TokenType.Four:
                case TokenType.Five:
                case TokenType.Six:
                case TokenType.Seven:
                case TokenType.Eight:
                case TokenType.Nine:
                case TokenType.Zero:
                    F();
                    TP();
                    break;
                default:
                    break;
            }
        }
        private void TP() //ya
        {
            switch (_token.Tag)
            {
                case TokenType.Mult:
                    Match(TokenType.Mult);
                    F();
                    TP();
                    break;
                case TokenType.Div:
                    Match(TokenType.Div);
                    F();
                    TP();
                    break;
                default:
                    break;
            }
        }
        private void Match(TokenType tag)
        {
            if (_token.Tag==tag)
            {
                _token = _scanner.GetToken();
            }
            else
            {
                throw new Exception("Error de sintaxis");
            }
        } 
        public void Parse (string regexp)
        {
            _scanner = new Scanner(regexp + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            switch (_token.Tag)
            {
                case TokenType.LParen:
                case TokenType.Empty:
                case TokenType.Null:
                case TokenType.One:
                case TokenType.Two:
                case TokenType.Three:
                case TokenType.Four:
                case TokenType.Five:
                case TokenType.Six:
                case TokenType.Seven:
                case TokenType.Eight:
                case TokenType.Nine:
                case TokenType.Zero:
                case TokenType.Minus:
                    E();
                    break;
                default:
                    break;
            }
            Match(TokenType.EOF);
            
        }
        //private int GetLastIndex()
        //{
        //    char[] regexpch = _scanner._regexp.ToCharArray();
        //    int i = _scanner._index-1;
        //    while (regexpch[i]!='+'&& regexpch[i] != '/'&&regexpch[i] != '*' 
        //        && regexpch[i] != '-' && regexpch[i] != '(' && regexpch[i] != (char)0)
        //    {
        //        i++;
        //    }
        //    return i;
        //}
        //private int GetFirstIndex()
        //{
        //    char[] regexpch = _scanner._regexp.ToCharArray();
        //    int i = _scanner._index-1;
        //    while (regexpch[i] != '+' && regexpch[i] != '/' && regexpch[i] != '*'
        //        && regexpch[i] != '-' && regexpch[i] != ')'&& regexpch[i]!= (char)0 )
        //    {
        //        i--;
        //    }
        //    return i;
        //}
        //private int GetLenght()
        //{
        //    return GetLastIndex() - GetFirstIndex() ;
           
        //}

        //private int GetParen()
        //{
        //    char[] regexpch = _scanner._regexp.ToCharArray();
        //    int i = _scanner._index - 1;
        //    while ( regexpch[i] != ')')
        //    {
        //        i++;
        //    }
        //    return i+1;
        //}

        //private int GetLParen()
        //{
        //    return GetParen() - GetFirstIndex();
        //}
    }
}
