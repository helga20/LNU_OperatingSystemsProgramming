using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    
    public class Format
    {
        public OperandType Op1Type;
        public OperandType Op2Type;
        public string FormatLine;
        public string TryToFormat(Operand Op1, Operand Op2)
        {
            string res = "";
            if (Op1.types.Contains(Op1Type) && Op2.types.Contains(Op2Type))
            {
                res = FormatLine;
                res = res.Replace("w", Op1.w);
                res = res.Replace("w", Op2.w);
                res = res.Replace("reg", Op1.reg);
                res = res.Replace("reg", Op2.reg);
                res = res.Replace("r/m", Op1.rm);
                res = res.Replace("r/m", Op2.rm);
                res = res.Replace("mod", Op1.mod);
                res = res.Replace("mod", Op2.mod);
                res = res.Replace("im8", Op1.im8);
                res = res.Replace("im8", Op2.im8);
                res = res.Replace("im16", Op1.im16);
                res = res.Replace("im16", Op2.im16);
                res = res.Replace("xreg", Op1.sreg);
                res = res.Replace("xreg", Op2.sreg);
                res = res.Replace("cond", "y").Replace("d", "1").Replace("y", "cond");
            }
            return res;
        }
        public Format(OperandType Op1Type, OperandType Op2Type, string FormatLine)
        {
            this.Op1Type = Op1Type;
            this.Op2Type = Op2Type;
            this.FormatLine = FormatLine.Trim().Replace("s", "0");
        }
    }
}
