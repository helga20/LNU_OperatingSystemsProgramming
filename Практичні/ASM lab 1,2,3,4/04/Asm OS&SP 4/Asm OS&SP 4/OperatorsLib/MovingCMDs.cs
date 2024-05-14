using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine.OperatorsLib
{
    public class MOVOperator : Operator
    {
        public override string Name(){return  "MOV";}

        protected override void initFormats()
        {
            //RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Memory, "1010 000w"));
            //RegistredFormats.Add(new Format(OperandType.Memory, OperandType.RegisterAX, "1010 001w"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Register,  "1000 100w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.RegisterOrMemory,  "1000 101w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1100 011w mod 000 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1100 011w mod 000 r/m im16"));
            
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Immediate8, "1011 w reg im8"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Immediate16, "1011 w reg im16"));

            RegistredFormats.Add(new Format(OperandType.SegmentRegister, OperandType.RegisterOrMemory, "1000 1110 mod xreg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.SegmentRegister, "1000 1100 mod xreg r/m"));
        }
        public MOVOperator():base(){}
        public MOVOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class PUSHOperator : Operator
    {
        public override string Name(){return  "PUSH";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.None, "01010 reg"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None,   "1111 1111 mod 110 r/m"));
            RegistredFormats.Add(new Format(OperandType.Immediate8, OperandType.None, "0110 10s0 im8"));
            RegistredFormats.Add(new Format(OperandType.Immediate16, OperandType.None, "0110 10s0 im16"));
        }
        public PUSHOperator() : base() { }
        public PUSHOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class POPOperator : Operator
    {
        public override string Name(){return  "POP";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.None, "1000 1111 mod 000 r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.None, "0101 1 reg"));
        }
        public POPOperator() : base() { }
        public POPOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class XCHGOperator : Operator
    {
        public override string Name(){return  "XCHG";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Register, "1001 0 reg"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Register, "1000 011w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.RegisterOrMemory, "1000 011w mod reg r/m"));
            
        }
        public XCHGOperator() : base() { }
        public XCHGOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class LEAOperator : Operator
    {
        public override string Name(){ return "LEA"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory, "1000 1101 mod reg r/m"));
        }
        public LEAOperator() : base() { }
        public LEAOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
}
