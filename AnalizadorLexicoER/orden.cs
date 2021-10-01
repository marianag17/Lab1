using System;
using System.Collections.Generic;
using System.Text;

namespace AnalizadorLexicoER
{
    public class orden
    {
        public string tipo;
        public string value;

        public orden(string t,string v)
        {
            tipo = t;
            value = v;
        }
    }
}
