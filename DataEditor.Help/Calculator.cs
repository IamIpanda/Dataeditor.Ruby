using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Calculator
    {
        static List<char> Operators = new List<char>() { '+', '-', '*', '/', '%', '^', '(', ')' };
        static List<string> Functions = new List<string>() { "sin", "cos", "ln", "lg", "exp", "sqrt", "trun" };
        public enum NodeType
        {
            Num,
            Operator,
            Variable,
            Function
        }
        public class CalcNode : ICloneable
        {
            public NodeType Type;
            public double Num;
            public char Operater;
            public string Value;
            public CalcNode(NodeType Type)
            {
                this.Type = Type;
                this.Num = 0.0;
                this.Operater = '#';
                this.Value = "";
            }
            public override string ToString()
            {
                if (Type == NodeType.Num)
                    return "CalcNode[Num]:" + Num.ToString();
                else if (Type == NodeType.Operator)
                    return "CalcNode[Ope]:" + Operater.ToString();
                else if (Type == NodeType.Variable)
                    return "CalcNode[Var]:" + Value.ToString();
                else if (Type == NodeType.Function)
                    return "CalcNode[Fun]" + Value.ToString();
                else
                    return "CalcNode";
            }
            static public CalcNode FromChar(char c)
            {
                CalcNode cn = new CalcNode(NodeType.Operator);
                cn.Operater = c;
                return cn;
            }
            static public CalcNode FromDouble(double d)
            {
                CalcNode cn = new CalcNode(NodeType.Num);
                cn.Num = d;
                return cn;
            }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
        public class MiddleExpression : List<CalcNode> { }
        public class PolishExpression : Stack<CalcNode> { }
        static int Rank(char c)
        {
            if (c == '+' || c == '-')
                return 1;
            else if (c == '*' || c == '/' || c == '%')
                return 2;
            else if (c == '^')
                return 3;
            else
                return 0;
        }
        static int Rank(CalcNode node)
        {
            if (node.Type == NodeType.Operator)
                return Rank(node.Operater);
            else if (node.Type == NodeType.Function)
                return 2;
            else
                return -1;
        }
        static double Calc(double x, double y, char c)
        {
            if (c == '+')
                return x + y;
            else if (c == '-')
                return y - x;
            else if (c == '*')
                return x * y;
            else if (c == '/')
            {
                if (x == 0)
                    return 0;
                return y / x;
            }
            else if (c == '%')
                return x % y;
            else if (c == '^')
                return Math.Pow(y, x);
            else
                return 0;
        }
        static double Calc(double x, string f)
        {
            if (f == "sin")
                return Math.Sin(x);
            else if (f == "cos")
                return Math.Cos(x);
            else if (f == "ln")
                if (x < 0) return 0;
                else return Math.Log(x);
            else if (f == "lg")
                if (x < 0) return 0;
                else return Math.Log10(x);
            else if (f == "exp")
                if (x > 700) return 0;
                else return Math.Exp(x);
            else if (f == "sqrt")
                if (x < 0) return 0;
                else return Math.Sqrt(x);
            else if (f == "trun")
                return Math.Truncate(x);
            else
                return 0;
        }
        /// <summary>
        /// 将字符串解析为中序表达式
        /// </summary>
        /// <param name="str">要解析的字符串</param>
        /// <returns>一个中序表达式结构体</returns>
        static public MiddleExpression TurnToMiddle(string str)
        {
            string s = str.Replace(" ", "");
            MiddleExpression m = new MiddleExpression();
            CalcNode LastNode = new CalcNode(NodeType.Num);
            char[] cs = s.ToCharArray();
            LastNode = null;
            foreach (char c in cs)
            {
                if (c >= 48 && c <= 57)
                {
                    if (LastNode != null && LastNode.Type == NodeType.Num)
                        LastNode.Num = LastNode.Num * 10 + c - 48;
                    else
                    {
                        LastNode = CalcNode.FromDouble(c - 48.0);
                        m.Add(LastNode);
                    }
                }
                else if (Operators.Contains(c))
                {
                    LastNode = CalcNode.FromChar(c);
                    m.Add(LastNode);
                }
                else
                {
                    if (LastNode != null && LastNode.Type == NodeType.Variable)
                        LastNode.Value += c;
                    else
                    {
                        LastNode = new CalcNode(NodeType.Variable);
                        LastNode.Value = c.ToString();
                        m.Add(LastNode);
                    }
                }
            }
            // 检查是否有计算节点是函数
            foreach (CalcNode node in m)
                if (node.Type == NodeType.Variable)
                    if (Functions.Contains(node.Value))
                        node.Type = NodeType.Function;
            return m;
        }
        static public PolishExpression TurnToPolish(MiddleExpression f)
        {
            Stack<CalcNode> S1 = new Stack<CalcNode>();
            Stack<CalcNode> S2 = new Stack<CalcNode>();
            S1.Push(new CalcNode(NodeType.Operator));
            PolishExpression e = new PolishExpression();
            foreach (CalcNode node in f)
            {
                if (node.Type == NodeType.Operator || node.Type == NodeType.Function)
                {
                    if (node.Operater == '(')
                        S1.Push(node);
                    else if (node.Operater == ')')
                    {
                        CalcNode LastNode = S1.Pop();
                        while (LastNode.Operater != '(')
                        {
                            S2.Push(LastNode);
                            LastNode = S1.Pop();
                        }
                    }
                    else
                    {
                        if (S1.Peek().Operater == '(')
                            S1.Push(node);
                        else
                        {
                            int rank = Rank(node);
                            int Toprank = Rank(S1.Peek());
                            if (rank <= Toprank)
                                while (!(S2.Peek().Operater == '(' || Rank(S1.Peek()) < rank))
                                    S2.Push(S1.Pop());
                            S1.Push(node);
                        }
                    }
                }
                else
                    S2.Push(node);
            }
            if (S1.Count != 0)
                while (S1.Count > 1)
                    S2.Push(S1.Pop());
            PolishExpression p = new PolishExpression();
            while (S2.Count > 0)
                p.Push(S2.Pop());
            return p;
        }
        static public QuickCalculator TurnToMachine(PolishExpression e)
        {
            Stack<double> ns = new Stack<double>();
            CalcNode c;
            double n1, n2;
            while (e.Count > 0)
            {
                c = e.Pop();
                if (c.Type == NodeType.Num)
                    ns.Push(c.Num);
                else if (c.Type == NodeType.Variable)
                {
                    e.Push(c);
                    return new QuickCalculator(ns, e);
                }
                else if (c.Type == NodeType.Function)
                {
                    n1 = ns.Pop();
                    ns.Push(Calc(n1, c.Value));
                }
                else
                {
                    n1 = ns.Pop(); n2 = ns.Pop();
                    ns.Push(Calc(n1, n2, c.Operater));
                }
            }
            return new QuickCalculator(ns, e);
        }
        public class QuickCalculator
        {
            Stack<double> results;
            Stack<CalcNode> nodes;
            Dictionary<string, double> Dictionary = new Dictionary<string, double>();
            public QuickCalculator(Stack<double> r, Stack<CalcNode> n)
            {
                results = r;
                nodes = n;
            }
            public QuickCalculator(string s)
            {

            }
            public double this[params double[] variables]
            {
                get
                {
                    int count = 0;
                    Stack<CalcNode> RunNodes = Clone(nodes);
                    Stack<double> ns = Clone(results);
                    Dictionary<string, double> Dic = new Dictionary<string, double>();
                    foreach (string cc in Dictionary.Keys)
                        Dic.Add(cc, Dictionary[cc]);
                    CalcNode c;
                    double n1, n2;
                    foreach (CalcNode cn in RunNodes)
                        if (cn.Type == NodeType.Variable)
                        {
                            double num = 0.0;
                            if (Dic.ContainsKey(cn.Value))
                                num = Dic[cn.Value];
                            else
                            {
                                num = variables[count];
                                Dic.Add(cn.Value, num);
                                count++;
                            }
                            cn.Num = num;
                            cn.Type = NodeType.Num;
                        }
                    while (RunNodes.Count > 0)
                    {
                        c = RunNodes.Pop();
                        if (c.Type == NodeType.Num)
                            ns.Push(c.Num);
                        else if (c.Type == NodeType.Function)
                        {
                            n1 = ns.Pop();
                            ns.Push(Calc(n1, c.Value));
                        }
                        else
                        {
                            n1 = ns.Pop(); n2 = ns.Pop();
                            ns.Push(Calc(n1, n2, c.Operater));
                        }
                    }
                    return ns.Pop();
                }
            }
            Stack<CalcNode> Clone(Stack<CalcNode> s)
            {
                Stack<CalcNode> k = new Stack<CalcNode>(s);
                Stack<CalcNode> p = new Stack<CalcNode>();
                while (k.Count > 0)
                    p.Push(k.Pop().Clone() as CalcNode);
                return p;
            }
            Stack<double> Clone(Stack<double> s)
            {
                Stack<double> k = new Stack<double>(s);
                Stack<double> p = new Stack<double>();
                while (k.Count > 0)
                    p.Push(k.Pop());
                return p;
            }
            static public QuickCalculator FromString(String s)
            {
                try
                {
                    MiddleExpression m = TurnToMiddle(s);
                    PolishExpression p = TurnToPolish(m);
                    QuickCalculator q = TurnToMachine(p);
                    return q;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
