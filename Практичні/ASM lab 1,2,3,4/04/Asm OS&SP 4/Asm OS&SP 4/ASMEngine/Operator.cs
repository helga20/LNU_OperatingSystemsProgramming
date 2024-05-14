using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    public abstract class Operator
    {
        public List<Format> RegistredFormats;
        internal int length;
        internal int address;
        internal bool hasAddress = false;
        protected abstract void initFormats();
        public virtual string Name()
        {
            return "";
        }
        public virtual bool TryToCreate(string line)
        {
            return line.ToUpper() == Name();
        }

        internal Operand Op1;
        internal Operand Op2;
        internal string CodeLine;

        public Operator()
        {
            RegistredFormats = new List<Format>();
            initFormats();
        }

        public Operator(Operand Op1, Operand Op2, int Line)
        {
            if (this is OperatorsLib.LOOPOperator)
                Op1.mod = "01";
            if (Convertors.angryOne &&(this is OperatorsLib.JMPOperator || this is OperatorsLib.JxxOperator))
                Op1.mod = "01";
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
    public class AddressShiftOperator : Operator
    {
        protected override void initFormats() { }
        public AddressShiftOperator(int length)
        {
            Op1 = new NullOperand("");
            Op2 = new NullOperand("");
            hasAddress = false;
            CodeLine = "";
            this.length = length;
        }
    }
    public class EmptyOperator : AddressShiftOperator
    {
        public EmptyOperator() : base(0) { }
    }
    public class AddressOperator : Operator
    {
        protected override void initFormats() { }
        public AddressOperator(string line, int bytes)
        {
            Op1 = new NullOperand("");
            Op2 = new NullOperand("");
            hasAddress = false;
            if (line.Contains(new string(',', 1)))
            {
                string[] lines = line.Split(',');
                CodeLine = "";
                foreach (string s in lines)
                {
                    CodeLine += Convertors.ExpandTo(Convertors.DecimalToBase(Convertors.ToNumber(s), 2), bytes);
                }
            }
            else
            {
                if (line.Contains(new string('"', 1)))
                {
                    CodeLine = "";
                    line = line.Trim('"');
                    foreach (char c in line)
                    {
                        CodeLine += Convertors.ExpandTo(Convert.ToString(Convert.ToUInt16(c), 2), 1);
                    }
                }
                else
                {
                    CodeLine = Convertors.ExpandTo(Convertors.DecimalToBase(Convertors.ToNumber(line), 2), bytes);
                }
            }
            length = CodeLine.Length / 8;
        }
    }
}
