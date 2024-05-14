using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using ASMEngine;
using System.Windows.Forms;

namespace SimASM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\source";
            saveFileDialog1.InitialDirectory = Application.StartupPath + "\\source";
            sourceFilename = Application.StartupPath + "\\source\\try.asm";
            folderBrowserDialog1.SelectedPath = Application.StartupPath + "\\bin";
            textBox3.Text = Settings.WorkDir;
            foreach (Operator x in Operators.DataBase)
            {
                List<TreeNode> y = new List<TreeNode>();
                foreach (Format z in x.RegistredFormats)
                {
                    y.Add(new TreeNode(z.FormatLine));
                }
		        treeView1.Nodes.Add(new TreeNode(x.Name(), y.ToArray()));
            }

            sourceFilename = Application.StartupPath + "\\source\\ex02_com.asm";
            StreamReader file = new StreamReader(sourceFilename);
            CodeSource.Text = file.ReadToEnd();
            file.Close();
            tabPage3.Text = sourceFilename.Substring(sourceFilename.LastIndexOf("\\") + 1);
        }

        private void process1_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            richTextBox1.AppendText(e.Data);
        }

        private void run()
        {
            richTextBox1.Text = "";
            if (binFilename == null)
                build();

            // Create cmd.exe, turning off the window and redirecting I/O to us
            //ProcessStartInfo info = new ProcessStartInfo("C:\\Projects\\Asembler\\ConsoleApplication1\\bin\\Debug\\out.exe");
            if (cmdProcess != null)
                cmdProcess.Close();
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.Arguments = "/C" + binFilename;
            info.CreateNoWindow = true;
            info.ErrorDialog = false;
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            cmdProcess = new Process();
            cmdProcess.StartInfo = info;
            cmdProcess.Start();
            //richTextBox1.AppendText(cmdProcess.Start().ToString());
            //cmdProcess = Process.Start(info);

            // Capture I/O
            errorReader = cmdProcess.StandardError;
            outputReader = cmdProcess.StandardOutput;
            inputWriter = cmdProcess.StandardInput;
            inputWriter.AutoFlush = false; // mcw: was true; see OnKeyDown

            //inputWriter.WriteLine(@"C:\\Projects\\Asembler\\ConsoleApplication1\\bin\\Debug\\out.exe");
            //inputWriter.Flush();

            // Begin async read on standard error and output
            ErrorBeginRead();
            OutputBeginRead();
            tabControl1.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            run();            
        }

        void ErrorBeginRead()
        {
            errorReader.BaseStream.BeginRead(errorBuffer, 0, errorBuffer.Length, new AsyncCallback(this.OnErrorInput), null);
        }

        void OutputBeginRead()
        {
            outputReader.BaseStream.BeginRead(outputBuffer, 0, outputBuffer.Length, new AsyncCallback(this.OnOutputInput), null);
        }

        void OnErrorInput(IAsyncResult result)
        {
            // Dump the characters to the text box
            int cb = errorReader.BaseStream.EndRead(result);
            if (cb == 0) return;
            richTextBox1.AppendText("RunTime Error!\n");
            richTextBox1.AppendText(System.Text.Encoding.ASCII.GetString(errorBuffer, 0, cb));

            // Start another read
            ErrorBeginRead();
        }

        void OnOutputInput(IAsyncResult result)
        {
            // Dump the characters to the text box
            int cb = outputReader.BaseStream.EndRead(result);
            if (cb == 0) return;

            string s = System.Text.Encoding.ASCII.GetString(outputBuffer, 0, cb);
            RichTextBox.CheckForIllegalCrossThreadCalls = false;
            richTextBox1.AppendText(s);
            if (s == ">")
            {
                //this.ScrollToCaret

            }
            Debug.WriteLine(string.Format("OnOutputInput({0})", s));

            // Start another read
            OutputBeginRead();
        }

        Process cmdProcess = null;
        StreamReader errorReader = null;
        StreamReader outputReader = null;
        StreamWriter inputWriter = null;
        byte[] errorBuffer = new byte[1];
        byte[] outputBuffer = new byte[1];
        StringBuilder outputString = new StringBuilder();

        private string sourceFilename;
        private string binFilename;

        private void build()
        {
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            try
            {
                if (checkBox2.Checked)
                    save();
                ASMFile file = new ASMFile(new List<string>(CodeSource.Lines));
                //file.MakeComFile = UserSettings.Default.MakeCom;
                file.MakeComFile = Settings.IfMakeCom;
                List<Line> res = file.OutCodes();
                CompileResults.Rows.Clear();
                foreach (Line x in res)
                {
                    CompileResults.Rows.Add(x.LineNumber, x.Address, x.Code, x.Source);
                }
                if (binFilename == null)
                    //    binFilename = UserSettings.Default.WorkingDirectory + UserSettings.Default.DefaultFilename;
                    binFilename = Settings.WorkDir + sourceFilename.Substring(sourceFilename.LastIndexOf("\\") + 1);
                binFilename = binFilename.Remove(binFilename.IndexOf('.'));
                if (Settings.IfMakeCom)
                    binFilename += ".com";
                else
                    binFilename += ".exe";
                file.BuildToFile(binFilename);
                richTextBox3.Text = file.ShowFile();
            }
            catch (CompileError e1)
            {
                richTextBox2.Text = "Compile error!\n";
                richTextBox2.AppendText(e1.LineNumber.ToString("0000") + " : " + e1.Message);
                if (e1.LineNumber != -1)
                CodeSource.Select(CodeSource.Text.IndexOf(CodeSource.Lines[e1.LineNumber]), CodeSource.Lines[e1.LineNumber].Length);
                binFilename = null;
                tabControl1.SelectedIndex = 1;
            }
            catch (System.Reflection.TargetInvocationException e1)
            {
                richTextBox2.Text = "Compile error!\n";
                richTextBox2.AppendText((e1.InnerException as CompileError).LineNumber.ToString("0000") + " : " + (e1.InnerException as CompileError).Message);
                if ((e1.InnerException as CompileError).LineNumber != -1)
                      CodeSource.Select(CodeSource.Text.IndexOf(CodeSource.Lines[(e1.InnerException as CompileError).LineNumber]), CodeSource.Lines[(e1.InnerException as CompileError).LineNumber].Length);
                binFilename = null;
                tabControl1.SelectedIndex = 1;

            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            build();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            binFilename = null;
            run();            
        }
        private void save()
        {
            saveToFile(sourceFilename);
            tabPage3.Text = sourceFilename.Substring(sourceFilename.LastIndexOf("\\") + 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            save();
            button6.Enabled = false;
        }

        private void CodeSource_TextChanged(object sender, EventArgs e)
        {
            if (!tabPage3.Text.EndsWith("*"))
            {
                button6.Enabled = true;
                tabPage3.Text += "*";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFilename = saveFileDialog1.FileName;
                saveToFile(sourceFilename);
                tabPage3.Text = sourceFilename.Substring(sourceFilename.LastIndexOf("\\") + 1);
            }
        }

        private void saveToFile(string filename)
        {
            FileStream f = new FileStream(filename, FileMode.Create);
            StreamWriter file = new StreamWriter(f);
            file.Write(CodeSource.Text);
            file.Flush();
            file.Close();
            f.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFilename = openFileDialog1.FileName;
                StreamReader file = new StreamReader(sourceFilename);
                CodeSource.Text = file.ReadToEnd();
                file.Close();                
                tabPage3.Text = sourceFilename.Substring(sourceFilename.LastIndexOf("\\") + 1);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.IfMakeCom = checkBox1.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Settings.WorkDir = folderBrowserDialog1.SelectedPath;
                textBox3.Text = Settings.WorkDir;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                inputWriter.WriteLine(textBox1.Text);
                inputWriter.Flush();
                textBox1.Text = "";
            }
        }

    }
}




