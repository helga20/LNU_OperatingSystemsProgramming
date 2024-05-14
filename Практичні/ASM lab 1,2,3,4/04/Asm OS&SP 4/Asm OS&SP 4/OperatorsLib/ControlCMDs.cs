using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine.OperatorsLib
{
    public class JMPOperator : Operator
    {
        public override string Name() { return "JMP"; }

        protected override void initFormats()
        {
            if (Convertors.angryOne)
                RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None, "1110 1011"));
            else
                RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None, "1110 1001"));
            //RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 1111 mod 100 r/m"));
        }
        public JMPOperator() : base() { }
        public JMPOperator(Operand op1, Operand op2, int Line)
            : base(op1, op2, Line)
        {
            if (Convertors.angryOne)
                op1.mod = "01";
        }
    }
    public class RETOperator : Operator
    {
        public override string Name(){return  "RET";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.None, OperandType.None, "1100 0011"));
        }
        public RETOperator() : base() { }
        public RETOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class STOSBOperator : Operator
    {
        public override string Name() { return "STOSB"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.None, OperandType.None, "1010 1010"));
        }
        public STOSBOperator() : base() { }
        public STOSBOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
}
