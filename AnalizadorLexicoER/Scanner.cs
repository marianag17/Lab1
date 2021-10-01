using System;
using System.Collections.Generic;
using System.Text;

namespace AnalizadorLexicoER
{
    public class Scanner
    {
        public string _regexp=""; //string a analizar
        public int _index = 0; //posicion actual en el string
        private const char EOF = (char)0; //centinela para marcar el ffinal de la cadena
        private int _state = 0;
        public Scanner (string regexp)
        {
            _regexp = regexp+(char)TokenType.EOF;
            _index = 0;
            _state = 0;
        }
        public Token GetToken()
        {
            Token result = new Token() { Value = (char)0 };	

            bool tokenFound = false;
            while (!tokenFound)
            {
                char peek = _regexp[_index];
                switch (_state)
                {
                    case 0:
                        while (char.IsWhiteSpace(peek))
                        {
                            _index++;
                            peek = _regexp[_index];
                        }
                        {
                            switch (peek)
                            {
                                case '\\':
                                    _state = 1;
                                    break;
                                case (char)TokenType.LParen:
                                case (char)TokenType.RParen:
                                case (char)TokenType.Minus:
                                case (char)TokenType.Plus:
                                case (char)TokenType.Mult:
                                case (char)TokenType.Div:
                                case (char)TokenType.EOF:
                                case (char)TokenType.Zero:
                                case (char)TokenType.One:
                                case (char)TokenType.Two:
                                case (char)TokenType.Three:
                                case (char)TokenType.Four:
                                case (char)TokenType.Five:
                                case (char)TokenType.Six:
                                case (char)TokenType.Seven:
                                case (char)TokenType.Eight:
                                case (char)TokenType.Nine:

                                    tokenFound = true;
                                    result.Tag = (TokenType)peek;
                                    break;
                                default:
                                    throw new Exception("Error de sintaxis");
                            }
                        }
                        break;
                    case 1:
                        switch (peek)
                        {
                            case (char)TokenType.LParen:
                            case (char)TokenType.RParen:
                            case (char)TokenType.Minus:
                            case (char)TokenType.Plus:
                            case (char)TokenType.Mult:
                            case (char)TokenType.Div:
                            case '\\':
                            case ' ': 
                                throw new Exception("Error de sintaxis");
                            case 'E':
                                tokenFound = true;
                                result.Tag = TokenType.Null;
                                break;
                            case '0':
                                tokenFound = true;
                                result.Tag = TokenType.Empty;
                                break;
                            default:
                                throw new Exception("Lex Error");
                                
                        }
                        break;
                    default:
                        break;
                }
                _index++;
            } //mientras no haya encontrado el token
            _state = 0;
            return result;
        } //GetToken
    }
}
