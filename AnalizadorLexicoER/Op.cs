using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalizadorLexicoER
{
    public class Op
    {

        int i = 0;
        bool cont = true;
        string regex;
        List<orden> list = new List<orden>();
        public Op (string regexpch)
        {
            regex = regexpch+(char)0;
        }
        public void operar()
        {
            int index = 0;
            while (cont)
            {
                if (regex[i] == (char)0)
                {
                    cont = false;
                }
                else if (i==0 && (regex[i]=='1'|| regex[i] == '2'||regex[i] == '3'|| regex[i] == '4'||
                    regex[i] == '5'|| regex[i] == '6'|| regex[i] == '7'|| regex[i] == '8'||regex[i] == '-')|| regex[i] == '9'|| regex[i] == '(')
                {
                    if (regex[i] == '-')
                    {
                        index = GetLastIndex();
                        orden Or = new orden("A",regex.Substring(i, (GetLastIndex() - i)));
                        list.Add(Or);
                        i = index;
                    }
                    else if (regex[i] == '(')
                    {
                        index = GetLastParen();
                        orden or = new orden("B", regex.Substring(i, (GetLastParen() - i + 1)));
                        list.Add(or);
                        i = index + 1;
                    }
                    else 
                    {
                        index = GetLastIndex();
                        orden Or = new orden("G", regex.Substring(i, (GetLastIndex() - i)));
                        list.Add(Or);
                        i = index;
                    }
                }
                if (regex[i]=='+' || regex[i] == '-' || regex[i] == '/'|| regex[i] == '*')
                {
                    if (regex[i + 1] == '(')
                    {
                        index = GetLastParen();
                        orden or = new orden("B",regex.Substring(i, (GetLastParen() - i + 1)));
                        list.Add(or);
                        i = index+1;

                    }
                    else
                    {
                        if (regex[i]=='+')
                        {
                            index = GetLastIndex();
                            orden or = new orden("E", regex.Substring(i, GetLastIndex() - i));
                            list.Add(or);
                            i = index;
                        }
                        else if (regex[i] == '-')
                        {
                            index = GetLastIndex();
                            orden or = new orden("F", regex.Substring(i, (GetLastIndex() - i)));
                            list.Add(or);
                            i = index;
                        }
                       else if (regex[i] == '/')
                        {
                            index = GetLastIndex();
                            orden or = new orden("C", regex.Substring(i, (GetLastIndex() - i)));
                            list.Add(or);
                            i = index;
                        }
                        else if (regex[i] == '*')
                        {
                            index = GetLastIndex();
                            orden or = new orden("D", regex.Substring(i, (GetLastIndex() - i)));
                            list.Add(or);
                            i = index;
                        }
                    }


                }
            }

        }
        public double resultado()

        {
            
            foreach (var item in list) //parentesis
            {
                if (item.tipo == "B")
                {
                    item.value=parseParen(item.value);
                    if (item.value.Contains("+"))
                    {
                        item.tipo = "E";
                    }
                    else if (item.value.Contains("-"))
                    {
                        item.tipo = "F";
                    }
                    else if (item.value.Contains("*"))
                    {
                        item.tipo = "D";
                    }
                    else if (item.value.Contains("/"))
                    {
                        item.tipo = "C";
                    }
                }
            }
            foreach (var item in list) //division
            {
                if (item.tipo == "C")
                {
                    int pos = list.IndexOf(item);
                    int j = 1;
                    while (list[pos - j].value == "")
                    {
                        j++;
                    }
                    if (list[pos - j].tipo == "G" || list[pos - j].tipo == "A")
                    {
                        list[pos - j].value = Convert.ToString((Convert.ToDouble(list[pos -j].value))/(Convert.ToDouble(item.value.Substring(1,1))));
                        item.tipo = "Z";
                        item.value = "";
                    }
                    else
                    {
                        switch (list[pos - j].value.Substring(0, 1))
                        {
                            case "+":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos - j].tipo = "E";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "-":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos - j].tipo = "F";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "/":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "/" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "/" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos -j].tipo = "C";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "*":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "*" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "*" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) / (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos - j].tipo = "D";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            foreach (var item in list) //mult
            {
                if (item.tipo == "D")
                {
                    int pos = list.IndexOf(item);
                    int j = 1;
                    while (list[pos - j].value == "")
                    {
                        j++;
                    }
                    if (list[pos -j].tipo == "G" || list[pos -j].tipo == "A")
                    {
                        list[pos -j].value = Convert.ToString((Convert.ToDouble(list[pos -j].value)) * (Convert.ToDouble(item.value.Substring(1))));
                        item.tipo = "Z";
                        item.value = "";
                    }
                    else
                    {
                        switch (list[pos -j].value.Substring(0, 1))
                        {
                            case "+":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) * (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) * (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos -j].tipo = "E";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "-":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) * (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) * (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos -j].tipo = "F";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "*":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "*" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) * (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "*" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) * (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos -j].tipo = "D";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            foreach (var item in list) //suma
            {
                if (item.tipo == "E")
                {
                    int pos = list.IndexOf(item);
                    int j = 1;
                    while (list[pos - j].value == "")
                    {
                        j++;
                    }
                    if (list[pos -j].tipo == "G"|| list[pos -j].tipo == "A")
                    {
                        list[pos -j].value = Convert.ToString((Convert.ToDouble(list[pos -j].value)) + (Convert.ToDouble(item.value.Substring(1)))); //aqui
                        item.tipo = "Z";
                        item.value = "";
                    }
                    else
                    {
                        switch (list[pos -j].value.Substring(0, 1))
                        {
                            case "+":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) + (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) + (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos -j].tipo = "E";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "-":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) + (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) + (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos -j].tipo = "F";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            foreach (var item in list) //RESTA
            {
                if (item.tipo == "F")
                {
                    int pos = list.IndexOf(item);
                    int j = 1;
                    while (list[pos - j].value == "")
                    {
                        j++;
                    }
                    if (list[pos -j].tipo == "G" || list[pos -j].tipo == "A")
                    {
                        list[pos -j].value = Convert.ToString((Convert.ToDouble(list[pos -j].value)) - (Convert.ToDouble(item.value.Substring(1))));
                        item.tipo = "Z";
                        item.value = "";
                    }
                    else
                    {
                        switch (list[pos - j].value.Substring(0, 1))
                        {
                            case "+":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) - (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "+" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) - (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos - j].tipo = "E";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            case "-":
                                if (list[pos - j].value.Substring(1, 1) == "-")
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(2, 1))) - (Convert.ToDouble(item.value.Substring(1))));
                                }
                                else
                                {
                                    list[pos - j].value = "-" + Convert.ToString((Convert.ToDouble(list[pos - j].value.Substring(1, 1))) - (Convert.ToDouble(item.value.Substring(1))));
                                }
                                list[pos - j].tipo = "F";
                                item.tipo = "Z";
                                item.value = "";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return Convert.ToDouble(list[0].value);
        }
        private int GetLastIndex()
        {
            // char[] regex = _scanner._regexp.ToCharArray();
            int j = i+1;
            while (regex[j] != '+' && regex[j] != '/' && regex[j] != '*'
                && regex[j] != '-' && regex[j] != '('&& j<regex.Length-1)
            {
                j++;
                if (regex[j] == (char)0)
                {
                    cont = false;
                }
            }
            return j;
        }

        private int GetLastParen()
        {
            int j = i;
            while (regex[j] != ')')
            {
                j++;
            }
            return j;
        }
        private string parseParen(string text)
        {
            string final = "";
            double num1 = 0;
            double num2 = 0;
            string op = "";
            string extra = "";
            if (text.Length == 5)
            {
                 num1 = Convert.ToDouble(text.Substring(1, 1));
                num2 = Convert.ToDouble(text.Substring(3, 1));
                op = text.Substring(2, 1);
            }
            else
            {
                num1 = Convert.ToDouble(text.Substring(2, 1));
                num2 = Convert.ToDouble(text.Substring(4, 1));
                op = text.Substring(3, 1);
                extra = text.Substring(0, 1);
            }
            switch (op)
            {
                case "+":
                    final = extra + Convert.ToString(num1 + num2);
                    break;
                case "-":
                    final = extra + Convert.ToString(num1 - num2);
                    break;
                case "/":
                    final = extra + Convert.ToString(num1 *num2);
                    break;
                case "*":
                    final = extra + Convert.ToString(num1 / num2);
                    break;
                default:
                    break;

            }
            return final;
        }

    }
}
