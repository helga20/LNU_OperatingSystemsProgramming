using System;
using System.Collections.Generic;
using System.Text;
using ASMEngine.OperatorsLib;

namespace ASMEngine
{
    public class Operators
    {
        public static List<Operator> DataBase = initDataBase();
        private static List<Operator> initDataBase()
        {
            List<Operator> res = new List<Operator>();
            res.Add(new ADDOperator());
            res.Add(new SUBOperator());
            res.Add(new INCOperator());
            res.Add(new DECOperator());
            res.Add(new CMPOperator());
            res.Add(new NEGOperator());
            res.Add(new MULOperator());
            res.Add(new IMULOperator());
            res.Add(new DIVOperator());
            res.Add(new IDIVOperator());
            res.Add(new CBWOperator());
            res.Add(new INTOperator());
            res.Add(new MOVOperator());
            res.Add(new LEAOperator());
            res.Add(new PUSHOperator());
            res.Add(new POPOperator());
            res.Add(new XCHGOperator());
            res.Add(new LOOPOperator());
            res.Add(new JMPOperator());
            res.Add(new RETOperator());
            res.Add(new JxxOperator());
            res.Add(new ANDOperator());
            res.Add(new SHLOperator());
            res.Add(new SAROperator());
            res.Add(new XOROperator());
            res.Add(new OROperator());
            res.Add(new CLDOperator());
            res.Add(new STOSBOperator());
            return res;
        }

        internal static bool IsOperator(string line)
        {
            line = line.ToUpper();
            bool res = false;
            foreach (Operator x in DataBase)
            {
                if (x.TryToCreate(line))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        internal static Operator ParseOperator(string line, AddressTable table, int LineNumber)
        {
            string[] parts = line.Split(new char[2] { ' ', ',' }, 3);
            //if (parts.Length > 3)
              //  throw new CompileError(LineNumber, "Bad operator string, too many parts : " + line);
            List<Operand> RegistredOperands = new List<Operand>();
            RegistredOperands.Add(new Register());
            RegistredOperands.Add(new Constant());
            RegistredOperands.Add(new SegmentRegister());
            RegistredOperands.Add(new Address());

            
            foreach (Operator x in DataBase)
            {
                if (x.TryToCreate(parts[0].ToUpper()))
                {
                    if (parts.Length == 1)
                    {
                        return (Operator)Activator.CreateInstance(x.GetType(), new NullOperand(""), new NullOperand(""), LineNumber);
                    }
                    if (parts.Length == 2)
                    {
                        Operand first = null;
                        foreach(Operand y in RegistredOperands)
                        {
                            if (y.TryToCreate(parts[1]))
                            {
                                if (y is Constant)
                                    first = (Operand)Activator.CreateInstance(y.GetType(), parts[1], 1);
                                else
                                    first = (Operand)Activator.CreateInstance(y.GetType(), parts[1]);
                                break;
                            }
                        }
                        if (first == null)
                            throw CompileError.WrongOperatorFormat("First operand of one-operand operator is invalid", LineNumber);
                        return (Operator)Activator.CreateInstance(x.GetType(), first, new NullOperand(""), LineNumber);
                    }
                    if (parts.Length == 3)
                    {
                        Operand first = null;
                        Operand second = null;
                        foreach (Operand y in RegistredOperands)
                        {
                            if (y.TryToCreate(parts[1]))
                            {
                                if (y is Constant)
                                    first = (Operand)Activator.CreateInstance(y.GetType(), parts[1].ToUpper(), 1);
                                else
                                    first = (Operand)Activator.CreateInstance(y.GetType(), parts[1]);
                                break;
                            }
                        }
                        foreach (Operand y in RegistredOperands)
                        {
                            if (y.TryToCreate(parts[2]))
                            {
                                if (y is Constant)
                                {
                                    if (first.w == "w")
                                        first.w = "1";
                                    second = (Operand)Activator.CreateInstance(y.GetType(), parts[2], 1 + int.Parse(first.w));
                                }
                                else
                                {
                                    if (y is Register)
                                    {
                                        //if (parts[1].ToUpper().StartsWith("A"))
                                      //      second = new Register(parts[2], true);
                                      //  else
                                            second = new Register(parts[2], !(first is Register || first is SegmentRegister));
                                                //second = (Operand)Activator.CreateInstance(y.GetType(), parts[2], (first is Register || first is SegmentRegister) && parts[1].ToUpper() != "AX");
                                    }
                                    else
                                        second = (Operand)Activator.CreateInstance(y.GetType(), parts[2]);
                                }
                                break;
                            }
                        }
                        if (first == null)
                            throw CompileError.WrongOperatorFormat("First operand of two-operand operator is invalid", LineNumber);
                        if (second == null)
                            throw CompileError.WrongOperatorFormat("Second operand of two-operand operator is invalid", LineNumber);
                        if (first is Address)
                            table.Add((first as Address).var);
                        if (second is Address)
                            table.Add((second as Address).var);
                        return (Operator)Activator.CreateInstance(x.GetType(), first, second, LineNumber);
                    }
                }
            }
            throw new CompileError(LineNumber, "No acceptable operator for this line");
        }
    }
}
