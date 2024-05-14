using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine.OperatorsLib
{
    public class LOOPOperator : Operator
    {
        public override string Name() { return "LOOP"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None, "1110 0010"));
        }
        public LOOPOperator() : base() { }
        public LOOPOperator(Operand op1, Operand op2, int Line)
            : base(op1, op2, Line)
        {
        }
    }
    public class JxxOperator : Operator
    {
        private static string cond = "xx";
        private static Dictionary<string, string> conditions = initConditions();
        private static Dictionary<string, string> initConditions()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("E", "0100");         d.Add("NE", "0101");
            d.Add("Z", "0100");         d.Add("NZ", "0101");

            d.Add("L", "1100");         d.Add("NL", "1101");
            d.Add("NGE", "1100");       d.Add("GE", "1101");

            d.Add("LE", "1110");        d.Add("NLE", "1111");
            d.Add("NG", "1110");        d.Add("G", "1111");
            return d;         
        }
        public override string Name() { return "J"+cond; }
        public override bool TryToCreate(string line)
        {
            if (line == "")
                return false;
            line = line.ToUpper();
            bool res = line[0] == 'J' && conditions.ContainsKey(line.Substring(1));
            if (res)
                cond = line.Substring(1);
            return res;
        }

        protected override void initFormats()
        {
            if (Convertors.angryOne)
                RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None, "0111 cond"));
            else
                RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None, "0000 1111 1000 cond"));
            
        }
        public JxxOperator() : base() { }
        public JxxOperator(Operand op1, Operand op2, int Line)
            :base(op1, op2, Line)
        {
            if (Convertors.angryOne)
                op1.mod = "01";
            CodeLine = CodeLine.Replace("cond", conditions[cond]);
        }
    }

}
