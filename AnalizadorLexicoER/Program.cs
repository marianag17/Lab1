using System;

namespace AnalizadorLexicoER
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                    BIENVENIDO                               ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("                                INGRESE SU EXPRESIÓN                          ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                 IMPORTANTE: SOLAMENTE NÚMEROS DE UN DÍGITO POR FAVOR                 ");
            string regexp = Console.ReadLine();
            Parser parser = new Parser();
            parser.Parse(regexp);
            Op op = new Op(regexp);
            op.operar();
            Console.WriteLine("Respuesta: "+ Convert.ToString( op.resultado()));
            Console.ReadLine();
        }
    }
}
