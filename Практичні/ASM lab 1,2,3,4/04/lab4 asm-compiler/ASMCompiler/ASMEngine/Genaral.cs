using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    public class Convertors
    {
        public static bool angryOne = false;
        public static int DBDWParse(string s)
        {
            s = s.ToUpper();
            if (s == "DB")
                return 1;
            else
                return 2;
        }
        public static string OperationRegisters8 = "AL,BL,CL,DL,AH,BH,CH,DH";
        public static string OperationRegisters16 = "AX,BX,CX,DX";
        public static string OperationRegisters32 = "EAX,EBX,ECX,EDX";
        public static string OffsetRegisters = "SI,DI";
        public static string BaseRegisters = "BX,BP";
        public static string AddressRegisters = OffsetRegisters + "," + BaseRegisters;
        public static string SegmentRegisters = "ES,SS,DS,CS";
      
        private static int base10 = 10;
        private static char[] cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        private static int[] iHexaNumeric = new int[] { 10, 11, 12, 13, 14, 15 };
        private static int[] iHexaIndices = new int[] { 0, 1, 2, 3, 4, 5 };
        private static int asciiDiff = 48;
        public static string OperationRegistres = OperationRegisters16 + OperationRegisters8;

        public static byte[] ConvertWord(uint number)
        {
            byte[] bytes = new byte[2];
            bytes = BitConverter.GetBytes((UInt16)number);
            return bytes;
        }
        public static uint Divide(int number, uint divider)
        {
            if (number % divider == 0)
                return (uint)(number / divider);
            else
                return (uint)(number / divider + 1);
        }
        public static int TryNegative(int number, int bytes)
        {
            if (number < 0)
                return (int)Math.Round(Math.Pow(256, bytes)) + number;
            else
                return number;
        }
        public static string DecimalToBase(int iDec, int numbase)
        {
            string strBin = "";
            int[] result = new int[32];
            int MaxBit = 32;
            for (; iDec > 0; iDec /= numbase)
            {
                int rem = iDec % numbase;
                result[--MaxBit] = rem;
            }
            for (int i = 0; i < result.Length; i++)
                if ((int)result.GetValue(i) >= base10)
                    strBin += cHexa[(int)result.GetValue(i) % base10];
                else
                    strBin += result.GetValue(i);
            strBin = strBin.TrimStart(new char[] { '0' });
            return strBin;
        }
        public static int BaseToDecimal(string sBase, int numbase)
        {
            int dec = 0;
            int b;
            int iProduct = 1;
            string sHexa = "";
            if (numbase > base10)
                for (int i = 0; i < cHexa.Length; i++)
                    sHexa += cHexa.GetValue(i).ToString();
            for (int i = sBase.Length - 1; i >= 0; i--, iProduct *= numbase)
            {
                string sValue = sBase[i].ToString();
                if (sValue.IndexOfAny(cHexa) >= 0)
                    b = iHexaNumeric[sHexa.IndexOf(sBase[i])];
                else
                    b = (int)sBase[i] - asciiDiff;
                dec += (b * iProduct);
            }
            return dec;
        }
        public static bool IsConstant(string line)
        {
            line = line.ToUpper();
            bool res = false;
            try
            {
                if (line.EndsWith("H"))
                {
                    int x = Int32.Parse(line.Substring(0, line.Length - 1), System.Globalization.NumberStyles.HexNumber);
                    res = true;
                }
                if (line.EndsWith("B"))
                {
                    int x = BaseToDecimal(line.Substring(0, line.Length - 1), 2);
                    res = true;
                }
                if (!res)
                {
                    int x = Int32.Parse(line);
                    res = true;
                }
            }
            catch
            {
                res = false;
            }
            return res;
        }
        public static int ToNumber(string line)
        {
            int res = 0;
            try
            {
                line = line.ToUpper();
                if (line.EndsWith("H"))
                    res = Convertors.BaseToDecimal(line.Substring(0, line.Length - 1), 16);
                else
                {
                    if (line.EndsWith("B"))
                        res = Convertors.BaseToDecimal(line.Substring(0, line.Length - 1), 2);
                    else
                        res = Int32.Parse(line);
                }
            }
            catch (Exception)
            {
                if (line == "?")
                    res = 0;
                else
                    throw CompileError.WrongNumericInput(line);
                
            }
            return res;
        }
        public static string ExpandTo(string line, int bytes)
        {
            while (line.Length < bytes * 8)
            {
                line = "0" + line;
            }
            if (bytes == 2)
            {
                string hi = line.Substring(0, 8);
                string lo = line.Substring(8, 8);
                line = lo + hi;
            }
            return line;
        }
        public static string HexToByte(string v, int bytes)
        {
            string res = "";
            switch (bytes)
            {
                case 1:
                    {
                        while (v.Length < 8)
                            v = "0" + v;
                        string hi = v.Substring(0, 4);
                        string lo = v.Substring(4, 4);
                        res = lo + hi;
                        break;
                    }
                case 2:
                    {
                        while (v.Length < 16)
                            v = "0" + v;
                        string[] r = new string[4];
                        for (int i = 0; i < 4; i++)
                            r[i] = v.Substring(i * 4, 4);
                        res = r[1] + r[0] + r[3] + r[2];
                        break;
                    }
            }
            return res;
        }
    }
}
