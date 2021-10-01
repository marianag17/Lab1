using System;
using System.Collections.Generic;
using System.Text;

namespace AnalizadorLexicoER
{
    public enum TokenType
    {
        Plus = '+',
        Minus = '-',
        Mult = '*',
        Div = '/',
        LParen = '(',
        RParen = ')',
        EOF = (char)0,
        Empty = (char)1,
        Null = (char)2,
        One = '1',
        Two = '2',
        Three = '3',
        Four = '4',
        Five = '5',
        Six = '6',
        Seven = '7',
        Eight = '8',
        Nine = '9',
        Zero = '0'
    }
}
