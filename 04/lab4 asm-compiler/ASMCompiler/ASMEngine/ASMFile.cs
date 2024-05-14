using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    public class Line
    {
        public string LineNumber;
        public string Address;
        public string Code;
        public string Source;

        public Line(string LineNumber, string Address, string Code, string Source)
        {
            this.LineNumber = LineNumber;
            this.Address = Address;
            this.Code = Code;
            this.Source = Source;
        }
    }

    public class ASMFile
    {
        private List<string> data;
        private List<Operator> parsed;

        private List<string> segments;
        private List<int> reloctable;
        private int codeLength;
        private int stackSize;

        public bool MakeComFile = false;
        private string ReplaceAll(string s, string s1, string s2)
        {
            while (s.Contains(s1))
            {
                s = s.Replace(s1, s2);
            }
            return s;
        }
        private void clean()
        {
            for (int i = 0; i < data.Count; i++)
            {
                //data[i] = data[i].ToUpper();
                if (data[i].Contains(";"))
                    data[i] = data[i].Remove(data[i].IndexOf(";"));
                data[i] = data[i].Trim();
                data[i] = ReplaceAll(data[i], "\t", " ");
                data[i] = ReplaceAll(data[i], " ;", ";");
                data[i] = ReplaceAll(data[i], ";", "");
               // data[i] = ReplaceAll(data[i], "  ", " ");
                if (data[i].Contains("\""))
                {
                    string[] c = data[i].Split(new char[1] { '\"' });
                    switch (c.Length)
                    {
                        case 2:
                            c[0] = ReplaceAll(c[0], "  ", " ");
                            c[0] = ReplaceAll(c[0], ", ", ",");
                            c[0] = ReplaceAll(c[0], ": ", ":");
                            data[i] = c[0] + "\"" + c[1] + "\"";
                            break;
                        case 3:
                            c[0] = ReplaceAll(c[0], "  ", " ");
                            c[0] = ReplaceAll(c[0], ", ", ",");
                            c[0] = ReplaceAll(c[0], ": ", ":");
                            c[2] = ReplaceAll(c[2], "  ", " ");
                            c[2] = ReplaceAll(c[2], ", ", ",");
                            c[2] = ReplaceAll(c[2], ": ", ":");
                            data[i] = c[0] + "\"" + c[1] + "\"" + c[2];
                            break;
                    }
                }
                else
                {
                    data[i] = ReplaceAll(data[i], "  ", " ");
                    data[i] = ReplaceAll(data[i], ", ", ",");
                    data[i] = ReplaceAll(data[i], ": ", ":");
                }
                if (data[i] == " ")
                    data[i] = "";
                //if (data[i].EndsWith(":"))
                //{
                //    if (i + 1 < data.Count)
                //        data[i + 1] = data[i] + data[i + 1];
                //    data.RemoveAt(i);
                //    i--;
                //    continue;
                //}
            }
        }

        public List<byte> MakeEXEHeader()
        {
            List<byte> res = new List<byte>();
            res.Add(0x4d);
            res.Add(0x5a);
            res.AddRange(Convertors.ConvertWord((uint)(codeLength + Convertors.Divide(reloctable.Count * 4 + 28, 16)*16) % 512));
            res.AddRange(Convertors.ConvertWord(Convertors.Divide(codeLength + reloctable.Count * 4 + 28,512)));
            res.AddRange(Convertors.ConvertWord((uint)reloctable.Count));
            res.AddRange(Convertors.ConvertWord(Convertors.Divide(reloctable.Count * 4 + 28, 16)));
            res.AddRange(Convertors.ConvertWord(0x0100));
            res.AddRange(Convertors.ConvertWord(0xFFFF));
            // Stack segment - SS
            res.AddRange(Convertors.ConvertWord(Convertors.Divide(codeLength, 16)));
            // Stack size - SP 100h
            res.AddRange(Convertors.ConvertWord((uint)stackSize));
            res.AddRange(Convertors.ConvertWord(0x0000));
            res.AddRange(Convertors.ConvertWord(0x0000));   // IP
            res.AddRange(Convertors.ConvertWord(0x0000));   // CS
            res.AddRange(Convertors.ConvertWord(0x001C));
            res.AddRange(Convertors.ConvertWord(0x0000));
            foreach (int x in reloctable)
            {
                res.AddRange(Convertors.ConvertWord((uint)x));
                res.Add(0x00);
                res.Add(0x00);
            }
            while (res.Count % 16 != 0)
            {
                res.Add(0x00);
            }
            return res;
        }
        public void OutCodesToFile(string filename)
        {
            FileStream output = new FileStream(filename, FileMode.Create);
            StreamWriter file = new StreamWriter(output);
            foreach (Operator x in parsed)
            {
                file.WriteLine(x.CodeLine.Length / 8 + " : " + x.CodeLine);
            }
            file.Flush();
            file.Close();
            output.Close();
        }
        public List<Line> OutCodes()
        {
            List<Line> res = new List<Line>();
            int lineID = 0;
            foreach (Operator x in parsed)
            {
                string s1 = lineID.ToString("0000") + ":";
                string s2 = x.address.ToString("x").ToUpper();
                while (s2.Length != 4)
                    s2 = "0" + s2;
                List<byte> bytes = new List<byte>();
                for (int i = 0; i < x.CodeLine.Length; i += 4)
                    {
                        try
                        {
                            bytes.Add((byte)Convert.ToByte(x.CodeLine.Substring(i, 4), 2));
                        }
                        catch (Exception)
                        {
                            throw CompileError.ErrorInByte(i / 8);
                        }
                }
                string s3 = "";
                for (int i = 0; i < bytes.Count; i+=2)
                {
                    s3 += bytes[i].ToString("x").ToUpper() + bytes[i+1].ToString("x").ToUpper() + " ";
                }
                s3 = s3.TrimEnd(' ');
                string s4 = data[parsed.IndexOf(x)];
                lineID++;
                if (s3 == "")
                    s2 = "";
                res.Add(new Line(s1, s2, s3, s4));
            }
            return res;
        }
        public void BuildToFile(string filename)
        {
            FileStream output = new FileStream(filename, FileMode.Create);
            BinaryWriter file = new BinaryWriter(output);
            string all = "";
            foreach (Operator x in parsed)
            {
                all += x.CodeLine;
            }
            codeLength = all.Length / 8;
            List<byte> bytes = new List<byte>();
            for(int i = 0; i < all.Length; i+=8)
            {
                bytes.Add((byte)Convert.ToByte(all.Substring(i, 8), 2));
            }
            if (!MakeComFile)
                bytes.InsertRange(0, MakeEXEHeader());
            int div = 0;
            foreach (byte b in bytes.ToArray())
            {
                if (b > 16)
                    hex += b.ToString("x") + " ";
                else
                    hex += "0" + b.ToString("x") + " ";
                if ((div + 1) % 16 == 0)
                    hex += "\n";
                div++;
                file.Write(b);
            }
            file.Flush();
            file.Close();
            output.Close();
        }
        private string hex = "";
        public string ShowFile()
        {
            return hex.ToUpper();
        }

        public List<string> Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        private void compile()
        {
            AddressTable table = new AddressTable();
            int curAddress = 0;
            foreach (string s in data)
            {
                if (s == "")
                {
                    parsed.Add(new EmptyOperator());
                    continue;
                }
                string[] parts = s.Split(' ', ',', ':');
                if (Operators.IsOperator(parts[0]))
                    parsed.Add(Operators.ParseOperator(s, table, data.IndexOf(s)));
                else
                {
                    if (parts.Length > 1 && Operators.IsOperator(parts[1]))
                    {
                        table.Add(parts[0]);
                        parsed.Add(Operators.ParseOperator(s.Substring(s.IndexOf(parts[1])), table, data.IndexOf(s)));
                        table.Set(parts[0], curAddress);
                    }
                    else
                    {
                        //instructions or labels
                        switch (parts[0].ToUpper())
                        {
                            case "STACK":
                                {
                                    parsed.Add(new AddressShiftOperator(0));
                                    stackSize = Convertors.ToNumber(parts[1]);
                                    break;
                                }
                            case "ORG":
                                {
                                    parsed.Add(new AddressShiftOperator(Convertors.ToNumber(parts[1])));
                                    break;
                                }
                            case "SEGMENT":
                                {
                                    segments.Add(parts[1].ToUpper());
                                    table.Add(parts[1]);
                                    if (curAddress % 16 != 0)
                                    {
                                        parsed.Add(new AddressOperator('"' + new string((char)0, 16 - curAddress % 16) + '"', 1));
                                     //   parsed.Add(new EmptyOperator());
                                        table.Set(parts[1], curAddress / 16 + 1);
                                    }
                                    else
                                    {
                                        parsed.Add(new EmptyOperator());
                                        table.Set(parts[1], curAddress / 16);
                                    }
                                    curAddress = 0;
                                    continue;
                                    //break;
                                }
                            case "DB":
                            case "DW":
                                {
                                    parts = s.Split(new char[1] { ' ' }, 2);
                                    parsed.Add(new AddressOperator(parts[1], Convertors.DBDWParse(parts[0])));
                                    break;
                                }
                            default:
                                {
                                    parts = s.Split(new char[1] { ' ' }, 3);
                                    if (parts.Length > 1 && (parts[1].ToUpper() == "DB" || parts[1].ToUpper() == "DW"))
                                    {
                                        table.Add(parts[0]);
                                        parsed.Add(new AddressOperator(parts[2], Convertors.DBDWParse(parts[1])));
                                        table.Set(parts[0], curAddress);
                                    }
                                    else
                                    {
                                        if (parts.Length > 1)
                                            throw CompileError.WrongLineFormat(data.IndexOf(s));
                                        parts[0] = parts[0].Replace(":", "");
                                        table.Add(parts[0]);
                                        table.Set(parts[0], curAddress);
                                        parsed.Add(new EmptyOperator());
                                        //throw new CompileError(data.IndexOf(s), "Unknown line format in : " + s);
                                    }
                                    break;
                                }
                        }
                    }
                }
                //if (parsed[parsed.Count - 1].hasAddress)

                parsed[parsed.Count - 1].address = curAddress;
                curAddress += parsed[parsed.Count - 1].length;
            }
            foreach (string x in table.Data.Keys)
            {
                if (table.Data[x] == "")
                    throw CompileError.IdentifierUndeclared(x, -1);
            }
            if (segments.Count > 1 && MakeComFile)
                throw CompileError.ComMultisegment();
            foreach (Operator y in parsed)
            {
                if (y.hasAddress)
                {
                    int bytes = 1;
                    int offset = 0;
                    string name = "";
                    if (y.Op1 is Address)
                    {
                        name = (y.Op1 as Address).var;
                        offset = (y.Op1 as Address).offset;
                        if (y.Op1.mod == "10")
                            bytes = 2;
                        if (y.Op1.mod == "00" && y.Op1.rm == "110")
                            bytes = 2;
                    }
                    if (y.Op2 is Address)
                    {
                        name = (y.Op2 as Address).var;
                        offset = (y.Op2 as Address).offset;
                        if (y.Op2.mod == "10")
                            bytes = 2;
                        if (y.Op2.mod == "00" && y.Op2.rm == "110")
                            bytes = 2;
                    }
                    if (segments.Contains(name))
                    {
                        reloctable.Add(y.address + (y.length - 2));
                    }
                    if (y is OperatorsLib.JMPOperator || y is OperatorsLib.JxxOperator)
                    {
                        if (Convertors.angryOne)
                            y.CodeLine += Convertors.ExpandTo(table.GetOffset(name, offset - y.address - y.length, 1), 1);
                        else
                            y.CodeLine += Convertors.ExpandTo(table.GetOffset(name, offset - y.address - y.length, 2), 2);
                    }
                    else
                    {
                        if (y is OperatorsLib.LOOPOperator)
                            y.CodeLine += Convertors.ExpandTo(table.GetOffset(name, offset - y.address - y.length, 1), 1);
                        else
                        {
                            if (y.Op2 is Constant)
                            {
                                if (!y.Op2.types.Contains(OperandType.Immediate8))
                                    y.CodeLine = y.CodeLine.Insert(y.CodeLine.Length - 2 * 8, Convertors.ExpandTo(table.GetOffset(name, offset, bytes), bytes));
                                else
                                    y.CodeLine = y.CodeLine.Insert(y.CodeLine.Length - 1 * 8, Convertors.ExpandTo(table.GetOffset(name, offset, bytes), bytes));
                            }
                            else
                                y.CodeLine += Convertors.ExpandTo(table.GetOffset(name, offset, bytes), bytes);
                        }
                    }
                }
            }
        }
        public ASMFile(List<string> data)
        {
            segments = new List<string>();
            reloctable = new List<int>();
            this.data = data;
            clean();
            parsed = new List<Operator>();
            compile();
            OutCodesToFile("log.txt");
        }
    }
}
