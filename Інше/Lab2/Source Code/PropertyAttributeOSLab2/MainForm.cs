using System.Globalization;
using System.Text;

namespace PropertyAttributeOSLab2
{
    public partial class MainForm : Form
    {
        private readonly SimpleLogger _logger;
        private readonly string[] _paths;
        private FileInfo _selected;

        /// <summary>
        /// Default constructor of MainForm.
        /// Here we create a logger, and locate the data files for the application.
        /// Found files are displayed on UI and are available to be selected.
        /// </summary>
        public MainForm()
        {
            _logger = new SimpleLogger();
            InitializeComponent();
            DirectoryInfo root = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo problemFolder = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "problem"));
            FileInfo[] filesRoot = root.GetFiles("*.dat");

            if (problemFolder.Exists)
            {
                FileInfo[] filesProblem = problemFolder.GetFiles("*.dat");
                _paths = filesRoot.Union(filesProblem).Select(q => q.FullName).ToArray();
            }
            else
            {
                _paths = filesRoot.Select(q => q.FullName).ToArray();
            }
            fileList.Items.AddRange(_paths.Select(q => Path.GetFileName(q)).ToArray());

            _logger.Log($"Launched the program, detected {_paths.Length} data files.");
        }

        /// <summary>
        /// Reloads fileInfo for a selected file via its filepath.
        /// </summary>
        /// <param name="path"></param>
        private void GetFileInfo(string path)
        {
            _selected = new FileInfo(path);
        }

        /// <summary>
        /// Is called automatically on changing the selected file, the data of the selected file will be displayed on UI.
        /// If file is selected for the first time this will also make the action menu for the file to be visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFileInfo(_paths[fileList.SelectedIndex]);
            tabControl.Enabled = true;
            tabControl.Visible = true;
            DisplayProps();
            DisplayContent();
            ClearCalculations();

            _logger.Log($"Selected {_selected.Name}, read its content and properties.");
        }

        /// <summary>
        /// Displays the properties of the selected file on the "Properties" page. 
        /// </summary>
        private void DisplayProps()
        {
            pathLabel.Text = _selected.FullName;
            sizeLabel.Text = $"{_selected.Length} bytes";
            extensionLabel.Text = _selected.Extension;
            openedLabel.Text = _selected.LastAccessTime.ToString();
            createdLabel.Text = _selected.CreationTime.ToString();
            changedLabel.Text = _selected.LastWriteTime.ToString();
            
            StringBuilder sb = new(' ');
            var enumValues = Enum.GetValues<FileAttributes>();

            foreach(var enumValue in enumValues)
            {
                //selected.Attributes contains a bitwise OR of all attributes of the file
                //By using bitwise AND (&) we get 0 in case this attribute is absent or the 
                //enumeration value if it is present (hence the comparison)
                if((_selected.Attributes & enumValue) == enumValue)
                { 
                    sb.Append(enumValue.ToString());
                    sb.Append(' ');
                }
            }
            attributeLabel.Text = sb.ToString().TrimEnd();

            Enum.GetValues(typeof(FileAttributes));
        }

        /// <summary>
        /// Reads text from the selected file and sets it in the relative textbox.
        /// </summary>
        private void DisplayContent()
        {
            string text = File.ReadAllText(_selected.FullName);
            contentBox.Text = text;
        }

        /// <summary>
        /// Calculates the sum of numbers in data file. Is called on "Calculate" button click.
        /// Replaces the invalid values with a number in replacementUpDown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            string text = File.ReadAllText(_selected.FullName);
            var separated = text.Split(' ');
            beforeReplacement.Text = String.Join(" + ", separated);

            var replacement = replacementUpDown.Value;
            var replaced = separated.Select(q 
                => decimal.TryParse(q, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result) 
                ? result 
                : replacement);
            afterReplacement.Text = String.Join(" + ", replaced);
            var sum = replaced.Sum();
            calculationLabel.Text = $"Calculation result = {sum}";


            _logger.Log($"The sum of values from {_selected.Name} replacing invalid values with {replacement} equals {sum}.");
        }

        /// <summary>
        /// Clears the tab with calculations on changing the file since the results are not longer relevant.
        /// </summary>
        private void ClearCalculations()
        {
            beforeReplacement.Text = "";
            afterReplacement.Text = "";
            calculationLabel.Text = "Calculation result:";
        }
    }
}