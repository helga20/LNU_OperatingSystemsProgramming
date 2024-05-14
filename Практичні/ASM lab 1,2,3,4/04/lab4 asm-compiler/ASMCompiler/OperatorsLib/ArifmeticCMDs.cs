using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine.OperatorsLib
{
    public class ADDOperator : Operator
    {
        public override string Name()
        {
            return "ADD";
         }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Register,    "0000 00dw mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory,      "0000 001w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.Register,      "0000 000w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1000 00sw mod 000 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate8, "0000 010w im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1000 00sw mod 000 r/m im16"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate16, "0000 010w im16"));
        }
        public ADDOperator() : base() { }
        public ADDOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public  class SUBOperator : Operator
    {
        public override string Name(){return  "SUB";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Register, "0010 10dw mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory, "0010 101w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.Register, "0010 100w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1000 00sw mod 001 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate8, "0010 010w im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1000 00sw mod 001 r/m im16"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate16, "0010 010w im16"));
        }
        public SUBOperator() : base() { }
        public SUBOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class INCOperator : Operator
    {
        public override string Name(){return  "INC";}

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.None, "0100 0 reg"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 1111 mod 000 r/m"));            
        }
        public INCOperator() : base() { }
        public INCOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class DECOperator : Operator
    {
        public override string Name() { return "DEC"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.None, "0100 1 reg"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 111w mod 001 r/m"));
        }
        public DECOperator() : base() { }
        public DECOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class CMPOperator : Operator
    {
        public override string Name() { return "CMP"; }

        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Register, "0011 10dw mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.Memory, "0011 100w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.Memory, OperandType.Register, "0011 101w mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate8, "0011 110w im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "1000 00sw mod 111 r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "1000 00sw mod 111 r/m im16"));
            RegistredFormats.Add(new Format(OperandType.RegisterAX, OperandType.Immediate16, "0011 110w im16"));
        }
        public CMPOperator() : base() { }
        public CMPOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class NEGOperator : Operator
    {
        public override string Name() { return "NEG"; }
        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 011w mod 011 r/m"));
        }
        public NEGOperator() : base() { }
        public NEGOperator(Operand Op1, Operand Op2, int Line)
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

    public class MULOperator : Operator
    {
        public override string Name(){return  "MUL";}
        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 011w mod 100 r/m"));
        }
        public MULOperator() : base() { }
        public MULOperator(Operand Op1, Operand Op2, int Line)
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
    public class IMULOperator : Operator
    {
        public override string Name() { return "IMUL"; }
        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 0111 mod 101 r/m"));
            RegistredFormats.Add(new Format(OperandType.Register, OperandType.RegisterOrMemory, "0000 1111 1010 1111 mod reg r/m"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate8, "0110 10s1 mod reg r/m im8"));
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.Immediate16, "0110 10s1 mod reg r/m im16"));
        }
        public IMULOperator() : base() { }
        public IMULOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class DIVOperator : Operator
    {
        public override string Name() { return "DIV"; }
        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 011w mod 110 r/m"));
        }
        public DIVOperator() : base() { }
        public DIVOperator(Operand Op1, Operand Op2, int Line)
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
    public class IDIVOperator : Operator
    {
        public override string Name() { return "IDIV"; }
        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.RegisterOrMemory, OperandType.None, "1111 011w mod 111 r/m"));
        }
        public IDIVOperator() : base() { }
        public IDIVOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
    public class CBWOperator : Operator
    {
        public override string Name() { return "CBW"; }
        protected override void initFormats()
        {
            RegistredFormats.Add(new Format(OperandType.None, OperandType.None, "1001 1000"));
        }
        public CBWOperator() : base() { }
        public CBWOperator(Operand op1, Operand op2, int Line) : base(op1, op2, Line) { }
    }
}
