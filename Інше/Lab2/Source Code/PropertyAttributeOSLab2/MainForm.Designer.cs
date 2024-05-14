namespace PropertyAttributeOSLab2
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.fileList = new System.Windows.Forms.ListBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.propertyPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.extensionLabel = new System.Windows.Forms.Label();
            this.createdLabel = new System.Windows.Forms.Label();
            this.changedLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.openedLabel = new System.Windows.Forms.Label();
            this.attributeLabel = new System.Windows.Forms.Label();
            this.contentPage = new System.Windows.Forms.TabPage();
            this.contentBox = new System.Windows.Forms.RichTextBox();
            this.calculatePage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.afterReplacement = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.calculationLabel = new System.Windows.Forms.Label();
            this.replacementUpDown = new System.Windows.Forms.NumericUpDown();
            this.calculateButton = new System.Windows.Forms.Button();
            this.beforeReplacement = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.propertyPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contentPage.SuspendLayout();
            this.calculatePage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.replacementUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.fileList);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(1147, 594);
            this.splitContainer.SplitterDistance = 382;
            this.splitContainer.TabIndex = 0;
            // 
            // fileList
            // 
            this.fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileList.FormattingEnabled = true;
            this.fileList.HorizontalScrollbar = true;
            this.fileList.ItemHeight = 20;
            this.fileList.Location = new System.Drawing.Point(0, 0);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(382, 594);
            this.fileList.TabIndex = 1;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.propertyPage);
            this.tabControl.Controls.Add(this.contentPage);
            this.tabControl.Controls.Add(this.calculatePage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(761, 594);
            this.tabControl.TabIndex = 0;
            this.tabControl.Visible = false;
            // 
            // propertyPage
            // 
            this.propertyPage.Controls.Add(this.tableLayoutPanel1);
            this.propertyPage.Location = new System.Drawing.Point(4, 29);
            this.propertyPage.Margin = new System.Windows.Forms.Padding(5);
            this.propertyPage.Name = "propertyPage";
            this.propertyPage.Padding = new System.Windows.Forms.Padding(3);
            this.propertyPage.Size = new System.Drawing.Size(753, 561);
            this.propertyPage.TabIndex = 0;
            this.propertyPage.Text = "Properties";
            this.propertyPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.sizeLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pathLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.extensionLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.createdLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.changedLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.openedLabel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.attributeLabel, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(747, 555);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(5, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(216, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "Attributes";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(5, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Opened";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(5, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Extension";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeLabel.Location = new System.Drawing.Point(229, 24);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(513, 20);
            this.sizeLabel.TabIndex = 3;
            this.sizeLabel.Text = "size label";
            this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Full path";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(5, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "File size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathLabel.Location = new System.Drawing.Point(229, 2);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(513, 20);
            this.pathLabel.TabIndex = 4;
            this.pathLabel.Text = "path label";
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // extensionLabel
            // 
            this.extensionLabel.AutoSize = true;
            this.extensionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extensionLabel.Location = new System.Drawing.Point(229, 46);
            this.extensionLabel.Name = "extensionLabel";
            this.extensionLabel.Size = new System.Drawing.Size(513, 20);
            this.extensionLabel.TabIndex = 6;
            this.extensionLabel.Text = "extension label";
            this.extensionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // createdLabel
            // 
            this.createdLabel.AutoSize = true;
            this.createdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createdLabel.Location = new System.Drawing.Point(229, 68);
            this.createdLabel.Name = "createdLabel";
            this.createdLabel.Size = new System.Drawing.Size(513, 20);
            this.createdLabel.TabIndex = 8;
            this.createdLabel.Text = "created label";
            this.createdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // changedLabel
            // 
            this.changedLabel.AutoSize = true;
            this.changedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changedLabel.Location = new System.Drawing.Point(229, 90);
            this.changedLabel.Name = "changedLabel";
            this.changedLabel.Size = new System.Drawing.Size(513, 20);
            this.changedLabel.TabIndex = 9;
            this.changedLabel.Text = "changed label";
            this.changedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(5, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(216, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Created";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(5, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(216, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Changed";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openedLabel
            // 
            this.openedLabel.AutoSize = true;
            this.openedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openedLabel.Location = new System.Drawing.Point(229, 112);
            this.openedLabel.Name = "openedLabel";
            this.openedLabel.Size = new System.Drawing.Size(513, 20);
            this.openedLabel.TabIndex = 7;
            this.openedLabel.Text = "opened label";
            this.openedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // attributeLabel
            // 
            this.attributeLabel.AutoSize = true;
            this.attributeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attributeLabel.Location = new System.Drawing.Point(229, 134);
            this.attributeLabel.Name = "attributeLabel";
            this.attributeLabel.Size = new System.Drawing.Size(513, 20);
            this.attributeLabel.TabIndex = 14;
            this.attributeLabel.Text = "attribute label";
            this.attributeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contentPage
            // 
            this.contentPage.Controls.Add(this.contentBox);
            this.contentPage.Location = new System.Drawing.Point(4, 29);
            this.contentPage.Name = "contentPage";
            this.contentPage.Padding = new System.Windows.Forms.Padding(3);
            this.contentPage.Size = new System.Drawing.Size(753, 561);
            this.contentPage.TabIndex = 1;
            this.contentPage.Text = "Content";
            this.contentPage.UseVisualStyleBackColor = true;
            // 
            // contentBox
            // 
            this.contentBox.BackColor = System.Drawing.Color.Black;
            this.contentBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentBox.ForeColor = System.Drawing.Color.White;
            this.contentBox.Location = new System.Drawing.Point(3, 3);
            this.contentBox.Name = "contentBox";
            this.contentBox.ReadOnly = true;
            this.contentBox.Size = new System.Drawing.Size(747, 555);
            this.contentBox.TabIndex = 0;
            this.contentBox.Text = "";
            // 
            // calculatePage
            // 
            this.calculatePage.Controls.Add(this.tableLayoutPanel2);
            this.calculatePage.Location = new System.Drawing.Point(4, 29);
            this.calculatePage.Name = "calculatePage";
            this.calculatePage.Padding = new System.Windows.Forms.Padding(3);
            this.calculatePage.Size = new System.Drawing.Size(753, 561);
            this.calculatePage.TabIndex = 2;
            this.calculatePage.Text = "Calculate";
            this.calculatePage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.afterReplacement, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.calculationLabel, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.replacementUpDown, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.calculateButton, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.beforeReplacement, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(747, 555);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // afterReplacement
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.afterReplacement, 2);
            this.afterReplacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afterReplacement.Location = new System.Drawing.Point(375, 3);
            this.afterReplacement.Name = "afterReplacement";
            this.afterReplacement.ReadOnly = true;
            this.afterReplacement.Size = new System.Drawing.Size(369, 438);
            this.afterReplacement.TabIndex = 6;
            this.afterReplacement.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 515);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 40);
            this.label4.TabIndex = 0;
            this.label4.Text = "Replacement for invalid values:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calculationLabel
            // 
            this.calculationLabel.AutoSize = true;
            this.calculationLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.calculationLabel.Location = new System.Drawing.Point(561, 535);
            this.calculationLabel.Name = "calculationLabel";
            this.calculationLabel.Size = new System.Drawing.Size(183, 20);
            this.calculationLabel.TabIndex = 1;
            this.calculationLabel.Text = "Calculation Result:";
            this.calculationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // replacementUpDown
            // 
            this.replacementUpDown.DecimalPlaces = 8;
            this.replacementUpDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.replacementUpDown.Location = new System.Drawing.Point(189, 525);
            this.replacementUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.replacementUpDown.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.replacementUpDown.Name = "replacementUpDown";
            this.replacementUpDown.Size = new System.Drawing.Size(180, 27);
            this.replacementUpDown.TabIndex = 3;
            // 
            // calculateButton
            // 
            this.calculateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.calculateButton.Location = new System.Drawing.Point(375, 523);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(180, 29);
            this.calculateButton.TabIndex = 4;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // beforeReplacement
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.beforeReplacement, 2);
            this.beforeReplacement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.beforeReplacement.Location = new System.Drawing.Point(3, 3);
            this.beforeReplacement.Name = "beforeReplacement";
            this.beforeReplacement.ReadOnly = true;
            this.beforeReplacement.Size = new System.Drawing.Size(366, 438);
            this.beforeReplacement.TabIndex = 5;
            this.beforeReplacement.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 594);
            this.Controls.Add(this.splitContainer);
            this.Name = "MainForm";
            this.Text = "Property & Attribute Viewer";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.propertyPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contentPage.ResumeLayout(false);
            this.calculatePage.ResumeLayout(false);
            this.calculatePage.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.replacementUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer;
        private TabControl tabControl;
        private TabPage propertyPage;
        private TabPage contentPage;
        private TabPage calculatePage;
        private ListBox fileList;
        private RichTextBox contentBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label sizeLabel;
        private Label pathLabel;
        private Label label11;
        private Label label10;
        private Label label3;
        private Label extensionLabel;
        private Label createdLabel;
        private Label changedLabel;
        private Label label8;
        private Label label9;
        private Label openedLabel;
        private Label attributeLabel;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private Label calculationLabel;
        private NumericUpDown replacementUpDown;
        private Button calculateButton;
        private RichTextBox afterReplacement;
        private RichTextBox beforeReplacement;
    }
}