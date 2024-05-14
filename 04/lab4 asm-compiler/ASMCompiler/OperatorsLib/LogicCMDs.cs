using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine.OperatorsLib
{
    public class ANDOperator : Operator
    {
        public override string Name() { return "AND"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Register, "0010 00dw mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory, "0010 001w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.Register, "0000 000w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1000 00sw mod 100 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate8, "0010 010w im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1000 00sw mod 100 r/m im16"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate16, "0010 010w im16"));
        }
        public ANDOperator() : base() { }
        public ANDOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class XOROperator : Operator
    {
        public override string Name() { return "XOR"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Register, "0011 00dw mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory, "0011 001w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.Register, "0011 000w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1000 00sw mod 110 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate8, "0011 010w im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1000 00sw mod 110 r/m im16"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate16, "0011 010w im16"));
        }
        public XOROperator() : base() { }
        public XOROperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class OROperator : Operator
    {
        public override string Name() { return "OR"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Register, "0000 00dw mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory, "0000 001w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.Register, "0000 000w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1000 00sw mod 001 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate8, "0000 110w im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1000 00sw mod 001 r/m im16"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate16, "0000 110w im16"));
        }
        public OROperator() : base() { }
        public OROperator(Operand Op1, Operand Op2, int Line)
        {
            if (Op1 is Register && Op2 is Constant && !Op1.types.Contains(OperandType.RegisterAX))
            {
                Op1.rm = Op1.reg;
                Op1.reg = "reg";
                Op1.mod = "11";
                Op1.types.Add(OperandType.RegisterOrMemory);
            }
            this.Op1 = Op1;
            this.Op2 = Op2;
            RegistredFormats = new List<Format>();
            initFormats();
            foreach (Format f in RegistredFormats)
            {
                CodeLine = f.TryToFormat(Op1, Op2);
                CodeLine = CodeLine.Replace(" ", "");
                if (CodeLine != "")
                    break;
            }
            if (CodeLine == "")
                throw CompileError.WrongOperatorFormat(Name(), Line);
            length = CodeLine.Length / 8;
            if (Op1 is Address)
            {
                if (Op1.mod == "01")
                    length++;
                if (Op1.mod == "10")
                    length += 2;
                if (Op1.mod == "00" && Op1.rm == "110")
                    length += 2;
                hasAddress = (Op1 as Address).var != "";
            }
            if (Op2 is Address)
            {
                if (Op2.mod == "01")
                    length++;
                if (Op2.mod == "10")
                    length += 2;
                if (Op2.mod == "00" && Op2.rm == "110")
                    length += 2;
                hasAddress = (Op2 as Address).var != "";
            }
        }
    }
    public class SHLOperator : Operator
    {
        public override string Name() { return "SHL"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1101 000w mod 100 r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.RegisterCL, "1101 001w mod 100 r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1100 000w mod 100 r/m im8"));
        }
        public SHLOperator() : base() { }
        public SHLOperator(Operand Op1, Operand Op2, int Line)
        {
            if (Op1 is Register)
            {
                Op1.rm = Op1.reg;
                Op1.reg = "reg";
                Op1.mod = "11";
                Op1.types.Add(OperandType.RegisterOrMemory);
            }
            if (Op2 is Constant)
            {
                if ((Op2 as Constant).im8 == "00000001")
                    Op2 = new NullOperand("");
            }
            this.Op1 = Op1;
            this.Op2 = Op2;
            RegistredFormats = new List<Format>();
            initFormats();
            foreach (Format f in RegistredFormats)
            {
                CodeLine = f.TryToFormat(Op1, Op2);
                CodeLine = CodeLine.Replace(" ", "");
                if (CodeLine != "")
                    break;
            }
            if (CodeLine == "")
                throw CompileError.WrongOperatorFormat(Name(), Line);
            length = CodeLine.Length / 8;
            if (Op1 is Address)
            {
                if (Op1.mod == "01")
                    length++;
                if (Op1.mod == "10")
                    length += 2;
                if (Op1.mod == "00" && Op1.rm == "110")
                    length += 2;
                hasAddress = (Op1 as Address).var != "";
            }
            if (Op2 is Address)
            {
                if (Op2.mod == "01")
                    length++;
                if (Op2.mod == "10")
                    length += 2;
                if (Op2.mod == "00" && Op2.rm == "110")
                    length += 2;
                hasAddress = (Op2 as Address).var != "";
            }
        }
    }
    public class SAROperator : Operator
    {
        public override string Name() { return "SAR"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1101 000w mod 111 r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.RegisterCL, "1101 001w mod 111 r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1100 000w mod 111 r/m im8"));
        }
        public SAROperator() : base() { }
        public SAROperator(Operand Op1, Operand Op2, int Line)
        {
            if (Op1 is Register)
            {
                Op1.rm = Op1.reg;
                Op1.reg = "reg";
                Op1.mod = "11";
                Op1.types.Add(OperandType.RegisterOrMemory);

            }
            this.Op1 = Op1;
            this.Op2 = Op2;
            RegistredFormats = new List<Format>();
            initFormats();
            foreach (Format f in RegistredFormats)
            {
                CodeLine = f.TryToFormat(Op1, Op2);
                CodeLine = CodeLine.Replace(" ", "");
                if (CodeLine != "")
                    break;
            }
            if (CodeLine == "")
                throw CompileError.WrongOperatorFormat(Name(), Line);
            length = CodeLine.Length / 8;
            if (Op1 is Address)
            {
                if (Op1.mod == "01")
                    length++;
                if (Op1.mod == "10")
                    length += 2;
                if (Op1.mod == "00" && Op1.rm == "110")
                    length += 2;
                hasAddress = (Op1 as Address).var != "";
            }
            if (Op2 is Address)
            {
                if (Op2.mod == "01")
                    length++;
                if (Op2.mod == "10")
                    length += 2;
                if (Op2.mod == "00" && Op2.rm == "110")
                    length += 2;
                hasAddress = (Op2 as Address).var != "";
            }
        }
    }
}
