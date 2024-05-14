using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    public enum OperandType : byte
    {
        RegisterOrMemory = 0, // R/M
        Register = 1,  // R
        Immediate8 = 2, // im8
        Immediate16 = 6, // im16
        SegmentRegister,
        RegisterCL,
        Memory = 3,  // M
        RegisterAX = 4,   // A
        None = 5
    }

    public abstract class Operand
    {
        internal string im8 = "im8";
        internal string im16 = "im16";
        internal string reg = "reg";
        internal string w = "w";
        internal string rm = "r/m";
        internal string mod = "mod";
        internal string s = "s";
        internal string sreg = "sreg";
        internal List<OperandType> types;

        internal abstract bool TryToCreate(string line);
    }

    public class Constant : Operand
    {
        private int data;

        internal override bool TryToCreate(string line)
        {
            return Convertors.IsConstant(line.ToUpper()) || (line.Length == 3 && line.StartsWith("\"") && line.EndsWith("\""));
        }

        public Constant() : base() { }
        public Constant(string line, int bytes)
        {
            line = line.ToUpper();
            if (line.StartsWith("\""))
                data = (int)line[1];
            else
                data = Convertors.ToNumber(line);
            im8 = Convertors.ExpandTo(Convertors.DecimalToBase(data, 2), 1);
            im16 = Convertors.ExpandTo(Convertors.DecimalToBase(data, 2), 2);
            types = new List<OperandType>();
            if (im8.Length <= 8 && bytes == 1)
            //if (im8.Length <= 8)
            {
                types.Add(OperandType.Immediate8);
            }
            else
                types.Add(OperandType.Immediate16);
        }
    }
    public class Register : Operand
    {
        private static string GetCode(string line, out string w)
        {
            if (line.Contains("L") || line.Contains("H"))
                w = "0";
            else
                w = "1";
            if (line == "AL" || line == "AX")
                return "000";
            if (line == "CL" || line == "CX")
                return "001";
            if (line == "DL" || line == "DX")
                return "010";
            if (line == "BL" || line == "BX")
                return "011";
            if (line == "AH" || line == "SP")
                return "100";
            if (line == "CH" || line == "BP")
                return "101";
            if (line == "DH" || line == "SI")
                return "110";
            if (line == "BH" || line == "DI")
                return "111";
            throw CompileError.WrongRegister(line);
        }
        internal override bool TryToCreate(string line)
        {
            line = line.ToUpper();
            return Convertors.OperationRegistres.Contains(line) || Convertors.OffsetRegisters.Contains(line) || Convertors.BaseRegisters.Contains(line);
        }

        public Register() : base() { }
        public Register(string line)
        {
            line = line.ToUpper();
            reg = GetCode(line, out w);
            types = new List<OperandType>();
            types.Add(OperandType.Register);
            if (line.StartsWith("A"))
                types.Add(OperandType.RegisterAX);
            if (line == "CL")
                types.Add(OperandType.RegisterCL);
        }
        public Register(string line, bool first)
        {
            line = line.ToUpper();
            types = new List<OperandType>();
            types.Add(OperandType.Register);
            if (line.StartsWith("A"))
                types.Add(OperandType.RegisterAX);
            if (line == "CL")
                types.Add(OperandType.RegisterCL);
            if (first)
                reg = GetCode(line, out w);
            else
            {
                reg = GetCode(line, out w);
                rm = GetCode(line, out w);
                mod = "11";
                types.Add(OperandType.RegisterOrMemory);
            }
        }
    }
    public class SegmentRegister : Operand
    {
        private static string GetCode(string line)
        {
            if (line == "ES")
                return "000";
            if (line == "CS")
                return "001";
            if (line == "SS")
                return "010";
            if (line == "DS")
                return "011";
            //if (line == "BH" || line == "DI")
            throw CompileError.WrongSegmentRegister(line);
        }

        internal override bool TryToCreate(string line)
        {
            line = line.ToUpper();
            return Convertors.SegmentRegisters.Contains(line);
        }

        public SegmentRegister() : base() { }
        public SegmentRegister(string line)
        {
            line = line.ToUpper();
            sreg = GetCode(line);
            types = new List<OperandType>();
            types.Add(OperandType.SegmentRegister);
        }
    }

    internal class Address : Operand
    {
        internal int offset;
        internal string var;
        internal override bool TryToCreate(string line)
        {
            return true;
        }
        private void Set(string mod, string rm)
        {
            this.rm = rm;
            this.mod = mod;
        }
        private bool DoDisp(string line)
        {
            mod = "10";
            var = "";
            bool res = true;
            if (line.StartsWith("+"))
                line = line.Substring(1);
            if (line.Contains("+"))
            {
                string[] s = line.Split('+');
                res = Convertors.IsConstant(s[1]);
                if (res)
                {
                    var = s[0];
                    offset = Convertors.ToNumber(s[1]);
                }
            }
            else
            {
                if (Convertors.IsConstant(line))
                    offset = Convertors.ToNumber(line);
                else
                    var = line;
            }
            return res;
        }
        private bool Contains(string[] items, string item)
        {
            bool res = false;
            foreach (string x in items)
            {
                if (x == item)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public Address() : base() { }
        public Address(string line)
        {
            var = "";
            line = line.ToUpper();
            while (line.Contains("]["))
                line = line.Replace("][", "]");
            string[] parts = line.Split(new char[] { ' ', '[', ']' }, 3, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length - 1; i++)
			{
                if (!Convertors.AddressRegisters.Contains(parts[i]))
                {
                    string x1 = parts[i];
                    parts[i] = parts[parts.Length - 1];
                    parts[parts.Length - 1] = x1;
                    break;
                }
			}

            if (parts.Length == 1)
            {
                switch (parts[0])
                {
                    case "SI":
                        Set("00", "100"); break;
                    case "DI":
                        Set("00", "101"); break;
                    case "BX":
                        Set("00", "111"); break;
                    default:
                        if (DoDisp(parts[0]))
                            Set("00", "110");
                        break;
                }
            }
            if (parts.Length == 2)
            {
                if (Contains(parts, "BX") && Contains(parts, "SI"))
                    Set("00", "000");
                if (Contains(parts, "BX") && Contains(parts, "DI"))
                    Set("00", "001");
                if (Contains(parts, "BP") && Contains(parts, "SI"))
                    Set("00", "010");
                if (Contains(parts, "BP") && Contains(parts, "DI"))
                    Set("00", "011");
                if (parts[0] == "SI" && DoDisp(parts[1]))
                    rm = "100";
                if (parts[0] == "DI" && DoDisp(parts[1]))
                    rm = "101";
                if (parts[0] == "BP" && DoDisp(parts[1]))
                    rm = "110";
                if (parts[0] == "BX" && DoDisp(parts[1]))
                    rm = "111";

            }
            if (parts.Length == 3)
            {
                if (Contains(parts, "BX") && Contains(parts, "SI") && DoDisp(parts[2]))
                    Set("10", "000");
                if (Contains(parts, "BX") && Contains(parts, "DI") && DoDisp(parts[2]))
                    Set("10", "001");
                if (Contains(parts, "BP") && Contains(parts, "SI") && DoDisp(parts[2]))
                    Set("10", "010");
                if (Contains(parts, "BP") && Contains(parts, "DI") && DoDisp(parts[2]))
                    Set("10", "011");
            }
            types = new List<OperandType>();
            types.Add(OperandType.RegisterOrMemory);
            types.Add(OperandType.Memory);
        }
    }

    internal class NullOperand : Operand
    {
        internal override bool TryToCreate(string line)
        {
            return true;
        }
        public NullOperand(string line)
        {
            types = new List<OperandType>();
            types.Add(OperandType.None);
        }
    }

}
