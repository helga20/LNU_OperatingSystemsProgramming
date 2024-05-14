using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SimASM
{
    public static class Settings
    {
        public static bool IfMakeCom = true;
        public static string WorkDir = initDir();
        private static string initDir()
        {
            return Application.StartupPath + "\\bin\\";
        }
    }
}
    