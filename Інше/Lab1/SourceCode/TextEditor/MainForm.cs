using System.Text;

namespace TextEditor
{
    public partial class MainForm : Form
    {
        private readonly EncodingInfo[] _encodings;
        private ToolStripMenuItem[] _encodingMenuItems;
        private Encoding selectedEncoding;
        private string? filePath;

        public MainForm()
        {
            _encodings = Encoding.GetEncodings();
            _encodingMenuItems = _encodings
                .Select(q => new ToolStripMenuItem(q.DisplayName, null, SwitchEncoding, q.Name))
                .ToArray();
            selectedEncoding = _encodings.First().GetEncoding();
            _encodingMenuItems.First().Checked = true;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            encodingToolStripMenuItem.DropDownItems.AddRange(_encodingMenuItems);
        }

        private void SwitchEncoding(object? sender, EventArgs e)
        {
            if (sender is null)
                return;

            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;
            _encodingMenuItems.ToList().ForEach(q => q.Checked = false);
            clicked.Checked = true;
            selectedEncoding = _encodings.First(q => q.Name == clicked.Name).GetEncoding();
            
            if (filePath is not null)
            {
                var text = File.ReadAllText(filePath, selectedEncoding);
                mainTextBox.Text = text;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "txt";
            dialog.Title = "Create new file:";
            dialog.FileName = "Amogus";
            dialog.RestoreDirectory = true;
            dialog.AddExtension = true;
            dialog.Filter = "Text files (*.txt)|*.txt";
            var result = dialog.ShowDialog();
            
            if(result == DialogResult.OK)
            {
                filePath = dialog.FileName;
                mainTextBox.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                File.WriteAllText(filePath, mainTextBox.Text, selectedEncoding);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.Filter = "Text files (*.txt)|*.txt";
            var result = dialog.ShowDialog();
            
            if(result == DialogResult.OK)
            {
                filePath = dialog.FileName;
                var text = File.ReadAllText(filePath, selectedEncoding);
                mainTextBox.Text = text;
                mainTextBox.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath is not null)
            {
                File.WriteAllText(filePath, mainTextBox.Text, selectedEncoding);
            }
        }
    }
}