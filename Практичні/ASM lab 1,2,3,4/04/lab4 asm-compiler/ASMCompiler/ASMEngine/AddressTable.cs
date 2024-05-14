using System;
using System.Collections.Generic;
using System.Text;

namespace ASMEngine
{
    public class AddressTable
    {
        private Dictionary<string, string> data;

        public Dictionary<string, string> Data
        {
            get { return data; }
        }

        public void Add(string name)
        {
            if (name != null)
            {
                name = name.ToUpper();
                if (name != "")
                {
                    if (!data.ContainsKey(name))
                        data.Add(name, "");
                }
            }
        }
        public void Set(string name, int offset)
        {
            name = name.ToUpper();
            if (!data.ContainsKey(name))
                throw CompileError.VariableNotDefined(name, -1);
            data[name] = offset.ToString();
        }
        public string GetOffset(string name, int offset, int bytes)
        {
            if (!data.ContainsKey(name))
                throw CompileError.VariableNotDefined(name, -1);
            string s = data[name.ToUpper()];
            return Convertors.DecimalToBase(Convertors.TryNegative(Convertors.ToNumber(s) + offset, bytes), 2);
        }

        public AddressTable()
        {
            data = new Dictionary<string, string>();
        }
    }
     
}
