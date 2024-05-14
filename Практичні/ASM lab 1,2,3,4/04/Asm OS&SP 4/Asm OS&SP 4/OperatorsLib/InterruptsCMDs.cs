using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine.OperatorsLib
{
    public  class INTOperator : Operator
    {
        public override string Name(){return  "INT";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Immediate8, OperandType.None, "1100 1101 im8"));
        }
        public INTOperator() : base() { }
        public INTOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class CLDOperator : Operator
    {
        public override string Name() { return "CLD"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.None, OperandType.None, "1111 1100"));
        }
        public CLDOperator() : base() { }
        public CLDOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }

}

