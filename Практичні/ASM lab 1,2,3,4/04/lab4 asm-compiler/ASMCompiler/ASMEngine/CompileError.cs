using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    public class CompileError : Exception
    {
        public int LineNumber;
        public new string Message;

        public static CompileError WrongOperatorFormat(string Name, int Line)
        {
            return new CompileError(Line, "No acceptable format found for operator : " + Name);
        }
        public static CompileError VariableNotDefined(string Variable, int Line)
        {
            return new CompileError(Line, "Variable not found : " + Variable);
        }
        public static CompileError IdentifierUndeclared(string Variable, int Line)
        {
            return new CompileError(Line, "Identifier undeclared : " + Variable);
        }
        public static CompileError ComMultisegment()
        {
            return new CompileError(-1, " Can't create multisegment com file");
        }
        public static CompileError WrongNumericInput(string line)
        {
            return new CompileError(-1, "Wrong numeric input : " + line);
        }
        public static CompileError WrongRegister(string Line)
        {
            return new CompileError(-1, "Wrong register : " + Line);
        }
        public static CompileError WrongSegmentRegister(string Line)
        {
            return new CompileError(-1, "Wrong segment register : " + Line);
        }
        public static CompileError WrongOperand(string Description, int Line)
        {
            return new CompileError(Line, Description);
        }
        public static CompileError NoOpertor(int Line)
        {
            return new CompileError(Line, "No operator found for this line");
        }
        public static CompileError WrongLineFormat(int Line)
        {
            return new CompileError(Line, "Wrong formatted line");
        }
        public static CompileError NoOpeatorFormat(string Name, int Line)
        {
            return new CompileError(Line, "No acceptable format found for operator : " + Name);
        }

       

        public CompileError(int LineNumber, string Message)
        {
            this.Message = Message;
            this.LineNumber = LineNumber;
        }

        internal static CompileError ErrorInByte(int p)
        {
            throw new CompileError(-1, "Error in byte : " + p);
        }
    }
}
