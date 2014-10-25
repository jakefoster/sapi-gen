namespace org.ncore.ServicedApi.CodeGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._generateButton = new System.Windows.Forms.Button();
            this._consoleDisplayRichTextBox = new System.Windows.Forms.RichTextBox();
            this._servicedApiXmlLabel = new System.Windows.Forms.Label();
            this._servicedApiXmlFileTextBox = new System.Windows.Forms.TextBox();
            this._openFileDialogButton = new System.Windows.Forms.Button();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._outputPathRootLabel = new System.Windows.Forms.Label();
            this._outputPathRootTextBox = new System.Windows.Forms.TextBox();
            this._openFolderBrowserButton = new System.Windows.Forms.Button();
            this._clearConsoleButton = new System.Windows.Forms.Button();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._refreshModelButton = new System.Windows.Forms.Button();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._templatesTabPage = new System.Windows.Forms.TabPage();
            this._templateSelectionTreeView = new System.Windows.Forms.TreeView();
            this._modelTabPage = new System.Windows.Forms.TabPage();
            this._modelTreeView = new System.Windows.Forms.TreeView();
            this._optionsTabPage = new System.Windows.Forms.TabPage();
            this._runGeneratorSynchronouslyCheckBox = new System.Windows.Forms.CheckBox();
            this._enableTemplateDebuggingCheckBox = new System.Windows.Forms.CheckBox();
            this._showGenerationResultsWindowCheckBox = new System.Windows.Forms.CheckBox();
            this._stopOnGenerationErrorsCheckBox = new System.Windows.Forms.CheckBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this._generatorPluginLabel = new System.Windows.Forms.Label();
            this._generatorPluginComboBox = new System.Windows.Forms.ComboBox();
            this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openProjectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveProjectFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._templatesTabPage.SuspendLayout();
            this._modelTabPage.SuspendLayout();
            this._optionsTabPage.SuspendLayout();
            this._mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _generateButton
            // 
            this._generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._generateButton.Enabled = false;
            this._generateButton.Location = new System.Drawing.Point(140, 381);
            this._generateButton.Name = "_generateButton";
            this._generateButton.Size = new System.Drawing.Size(93, 23);
            this._generateButton.TabIndex = 11;
            this._generateButton.Text = "Generate";
            this._generateButton.UseVisualStyleBackColor = true;
            this._generateButton.Click += new System.EventHandler(this._generateButton_Click);
            // 
            // _consoleDisplayRichTextBox
            // 
            this._consoleDisplayRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._consoleDisplayRichTextBox.Location = new System.Drawing.Point(3, 24);
            this._consoleDisplayRichTextBox.Name = "_consoleDisplayRichTextBox";
            this._consoleDisplayRichTextBox.Size = new System.Drawing.Size(592, 383);
            this._consoleDisplayRichTextBox.TabIndex = 2;
            this._consoleDisplayRichTextBox.Text = "";
            this._consoleDisplayRichTextBox.WordWrap = false;
            // 
            // _servicedApiXmlLabel
            // 
            this._servicedApiXmlLabel.AutoSize = true;
            this._servicedApiXmlLabel.Location = new System.Drawing.Point(12, 40);
            this._servicedApiXmlLabel.Name = "_servicedApiXmlLabel";
            this._servicedApiXmlLabel.Size = new System.Drawing.Size(94, 13);
            this._servicedApiXmlLabel.TabIndex = 3;
            this._servicedApiXmlLabel.Text = "ServicedAPI XML:";
            // 
            // _servicedApiXmlFileTextBox
            // 
            this._servicedApiXmlFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._servicedApiXmlFileTextBox.Location = new System.Drawing.Point(112, 37);
            this._servicedApiXmlFileTextBox.Name = "_servicedApiXmlFileTextBox";
            this._servicedApiXmlFileTextBox.Size = new System.Drawing.Size(716, 20);
            this._servicedApiXmlFileTextBox.TabIndex = 1;
            this._servicedApiXmlFileTextBox.TextChanged += new System.EventHandler(this._servicedApiXmlFileTextBox_TextChanged);
            this._servicedApiXmlFileTextBox.Leave += new System.EventHandler(this._servicedApiXmlFileTextBox_Leave);
            // 
            // _openFileDialogButton
            // 
            this._openFileDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._openFileDialogButton.Location = new System.Drawing.Point(835, 35);
            this._openFileDialogButton.Name = "_openFileDialogButton";
            this._openFileDialogButton.Size = new System.Drawing.Size(35, 23);
            this._openFileDialogButton.TabIndex = 2;
            this._openFileDialogButton.Text = "...";
            this._openFileDialogButton.UseVisualStyleBackColor = true;
            this._openFileDialogButton.Click += new System.EventHandler(this._openFileDialogButton_Click);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.FileName = "openFileDialog1";
            this._openFileDialog.Filter = "ServicedAPI XML Files|*.servicedapi.xml";
            this._openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this._openFileDialog_FileOk);
            // 
            // _outputPathRootLabel
            // 
            this._outputPathRootLabel.AutoSize = true;
            this._outputPathRootLabel.Location = new System.Drawing.Point(12, 66);
            this._outputPathRootLabel.Name = "_outputPathRootLabel";
            this._outputPathRootLabel.Size = new System.Drawing.Size(93, 13);
            this._outputPathRootLabel.TabIndex = 6;
            this._outputPathRootLabel.Text = "Output Path Root:";
            // 
            // _outputPathRootTextBox
            // 
            this._outputPathRootTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._outputPathRootTextBox.Location = new System.Drawing.Point(112, 64);
            this._outputPathRootTextBox.Name = "_outputPathRootTextBox";
            this._outputPathRootTextBox.Size = new System.Drawing.Size(716, 20);
            this._outputPathRootTextBox.TabIndex = 3;
            this._outputPathRootTextBox.Leave += new System.EventHandler(this._outputPathRootTextBox_Leave);
            // 
            // _openFolderBrowserButton
            // 
            this._openFolderBrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._openFolderBrowserButton.Location = new System.Drawing.Point(835, 62);
            this._openFolderBrowserButton.Name = "_openFolderBrowserButton";
            this._openFolderBrowserButton.Size = new System.Drawing.Size(35, 23);
            this._openFolderBrowserButton.TabIndex = 4;
            this._openFolderBrowserButton.Text = "...";
            this._openFolderBrowserButton.UseVisualStyleBackColor = true;
            this._openFolderBrowserButton.Click += new System.EventHandler(this._openFolderBrowserButton_Click);
            // 
            // _clearConsoleButton
            // 
            this._clearConsoleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._clearConsoleButton.Location = new System.Drawing.Point(493, 1);
            this._clearConsoleButton.Name = "_clearConsoleButton";
            this._clearConsoleButton.Size = new System.Drawing.Size(102, 23);
            this._clearConsoleButton.TabIndex = 9;
            this._clearConsoleButton.Text = "Clear Console";
            this._clearConsoleButton.UseVisualStyleBackColor = true;
            this._clearConsoleButton.Click += new System.EventHandler(this._clearConsoleButton_Click);
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._splitContainer.Location = new System.Drawing.Point(15, 130);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._refreshModelButton);
            this._splitContainer.Panel1.Controls.Add(this._generateButton);
            this._splitContainer.Panel1.Controls.Add(this._tabControl);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._consoleDisplayRichTextBox);
            this._splitContainer.Panel2.Controls.Add(this._clearConsoleButton);
            this._splitContainer.Size = new System.Drawing.Size(855, 410);
            this._splitContainer.SplitterDistance = 253;
            this._splitContainer.TabIndex = 15;
            // 
            // _refreshModelButton
            // 
            this._refreshModelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._refreshModelButton.Enabled = false;
            this._refreshModelButton.Location = new System.Drawing.Point(20, 381);
            this._refreshModelButton.Name = "_refreshModelButton";
            this._refreshModelButton.Size = new System.Drawing.Size(105, 23);
            this._refreshModelButton.TabIndex = 10;
            this._refreshModelButton.Text = "Refresh Model";
            this._refreshModelButton.UseVisualStyleBackColor = true;
            this._refreshModelButton.Click += new System.EventHandler(this._refreshModelButton_Click);
            // 
            // _tabControl
            // 
            this._tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tabControl.Controls.Add(this._templatesTabPage);
            this._tabControl.Controls.Add(this._modelTabPage);
            this._tabControl.Controls.Add(this._optionsTabPage);
            this._tabControl.Location = new System.Drawing.Point(3, 3);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(247, 373);
            this._tabControl.TabIndex = 7;
            // 
            // _templatesTabPage
            // 
            this._templatesTabPage.Controls.Add(this._templateSelectionTreeView);
            this._templatesTabPage.Location = new System.Drawing.Point(4, 22);
            this._templatesTabPage.Name = "_templatesTabPage";
            this._templatesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._templatesTabPage.Size = new System.Drawing.Size(239, 347);
            this._templatesTabPage.TabIndex = 0;
            this._templatesTabPage.Text = "Templates";
            this._templatesTabPage.UseVisualStyleBackColor = true;
            // 
            // _templateSelectionTreeView
            // 
            this._templateSelectionTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._templateSelectionTreeView.CheckBoxes = true;
            this._templateSelectionTreeView.Location = new System.Drawing.Point(6, 6);
            this._templateSelectionTreeView.Name = "_templateSelectionTreeView";
            this._templateSelectionTreeView.Size = new System.Drawing.Size(227, 335);
            this._templateSelectionTreeView.TabIndex = 7;
            this._templateSelectionTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this._templateSelectionTreeView_MouseDown);
            this._templateSelectionTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this._templateSelectionTreeView_MouseUp);
            // 
            // _modelTabPage
            // 
            this._modelTabPage.Controls.Add(this._modelTreeView);
            this._modelTabPage.Location = new System.Drawing.Point(4, 22);
            this._modelTabPage.Name = "_modelTabPage";
            this._modelTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._modelTabPage.Size = new System.Drawing.Size(239, 347);
            this._modelTabPage.TabIndex = 1;
            this._modelTabPage.Text = "Model";
            this._modelTabPage.UseVisualStyleBackColor = true;
            // 
            // _modelTreeView
            // 
            this._modelTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._modelTreeView.CheckBoxes = true;
            this._modelTreeView.Location = new System.Drawing.Point(6, 6);
            this._modelTreeView.Name = "_modelTreeView";
            this._modelTreeView.Size = new System.Drawing.Size(227, 336);
            this._modelTreeView.TabIndex = 0;
            this._modelTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this._modelTreeView_MouseDown);
            // 
            // _optionsTabPage
            // 
            this._optionsTabPage.Controls.Add(this._runGeneratorSynchronouslyCheckBox);
            this._optionsTabPage.Controls.Add(this._enableTemplateDebuggingCheckBox);
            this._optionsTabPage.Controls.Add(this._showGenerationResultsWindowCheckBox);
            this._optionsTabPage.Controls.Add(this._stopOnGenerationErrorsCheckBox);
            this._optionsTabPage.Location = new System.Drawing.Point(4, 22);
            this._optionsTabPage.Name = "_optionsTabPage";
            this._optionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._optionsTabPage.Size = new System.Drawing.Size(239, 347);
            this._optionsTabPage.TabIndex = 2;
            this._optionsTabPage.Text = "Options";
            this._optionsTabPage.UseVisualStyleBackColor = true;
            // 
            // _runGeneratorSynchronouslyCheckBox
            // 
            this._runGeneratorSynchronouslyCheckBox.AutoSize = true;
            this._runGeneratorSynchronouslyCheckBox.Location = new System.Drawing.Point(6, 83);
            this._runGeneratorSynchronouslyCheckBox.Name = "_runGeneratorSynchronouslyCheckBox";
            this._runGeneratorSynchronouslyCheckBox.Size = new System.Drawing.Size(164, 17);
            this._runGeneratorSynchronouslyCheckBox.TabIndex = 12;
            this._runGeneratorSynchronouslyCheckBox.Text = "Run generator synchronously";
            this._runGeneratorSynchronouslyCheckBox.UseVisualStyleBackColor = true;
            this._runGeneratorSynchronouslyCheckBox.CheckedChanged += new System.EventHandler(this._runGeneratorSynchronouslyCheckBox_CheckedChanged);
            // 
            // _enableTemplateDebuggingCheckBox
            // 
            this._enableTemplateDebuggingCheckBox.AutoSize = true;
            this._enableTemplateDebuggingCheckBox.Location = new System.Drawing.Point(6, 59);
            this._enableTemplateDebuggingCheckBox.Name = "_enableTemplateDebuggingCheckBox";
            this._enableTemplateDebuggingCheckBox.Size = new System.Drawing.Size(155, 17);
            this._enableTemplateDebuggingCheckBox.TabIndex = 11;
            this._enableTemplateDebuggingCheckBox.Text = "Enable template debugging";
            this._enableTemplateDebuggingCheckBox.UseVisualStyleBackColor = true;
            this._enableTemplateDebuggingCheckBox.CheckedChanged += new System.EventHandler(this._enableTemplateDebuggingCheckBox_CheckedChanged);
            // 
            // _showGenerationResultsWindowCheckBox
            // 
            this._showGenerationResultsWindowCheckBox.AutoSize = true;
            this._showGenerationResultsWindowCheckBox.Checked = true;
            this._showGenerationResultsWindowCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showGenerationResultsWindowCheckBox.Location = new System.Drawing.Point(6, 35);
            this._showGenerationResultsWindowCheckBox.Name = "_showGenerationResultsWindowCheckBox";
            this._showGenerationResultsWindowCheckBox.Size = new System.Drawing.Size(178, 17);
            this._showGenerationResultsWindowCheckBox.TabIndex = 10;
            this._showGenerationResultsWindowCheckBox.Text = "Show generation results window";
            this._showGenerationResultsWindowCheckBox.UseVisualStyleBackColor = true;
            this._showGenerationResultsWindowCheckBox.CheckedChanged += new System.EventHandler(this._showGenerationResultsWindowCheckBox_CheckedChanged);
            // 
            // _stopOnGenerationErrorsCheckBox
            // 
            this._stopOnGenerationErrorsCheckBox.AutoSize = true;
            this._stopOnGenerationErrorsCheckBox.Checked = true;
            this._stopOnGenerationErrorsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._stopOnGenerationErrorsCheckBox.Location = new System.Drawing.Point(6, 12);
            this._stopOnGenerationErrorsCheckBox.Name = "_stopOnGenerationErrorsCheckBox";
            this._stopOnGenerationErrorsCheckBox.Size = new System.Drawing.Size(145, 17);
            this._stopOnGenerationErrorsCheckBox.TabIndex = 8;
            this._stopOnGenerationErrorsCheckBox.Text = "Stop on generation errors";
            this._stopOnGenerationErrorsCheckBox.UseVisualStyleBackColor = true;
            this._stopOnGenerationErrorsCheckBox.CheckedChanged += new System.EventHandler(this._stopOnGenerationErrorsCheckBox_CheckedChanged);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Location = new System.Drawing.Point(798, 90);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(73, 23);
            this.settingsButton.TabIndex = 6;
            this.settingsButton.Text = "Settings...";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this._settingsButton_Click);
            // 
            // _generatorPluginLabel
            // 
            this._generatorPluginLabel.AutoSize = true;
            this._generatorPluginLabel.Location = new System.Drawing.Point(12, 94);
            this._generatorPluginLabel.Name = "_generatorPluginLabel";
            this._generatorPluginLabel.Size = new System.Drawing.Size(89, 13);
            this._generatorPluginLabel.TabIndex = 22;
            this._generatorPluginLabel.Text = "Generator Plugin:";
            // 
            // _generatorPluginComboBox
            // 
            this._generatorPluginComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._generatorPluginComboBox.FormattingEnabled = true;
            this._generatorPluginComboBox.Location = new System.Drawing.Point(112, 91);
            this._generatorPluginComboBox.Name = "_generatorPluginComboBox";
            this._generatorPluginComboBox.Size = new System.Drawing.Size(680, 21);
            this._generatorPluginComboBox.TabIndex = 5;
            this._generatorPluginComboBox.SelectedIndexChanged += new System.EventHandler(this._generatorPluginComboBox_SelectedIndexChanged);
            // 
            // _mainMenuStrip
            // 
            this._mainMenuStrip.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._mainMenuStrip.Name = "_mainMenuStrip";
            this._mainMenuStrip.Size = new System.Drawing.Size(882, 24);
            this._mainMenuStrip.TabIndex = 24;
            this._mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project...";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As...";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // _openProjectFileDialog
            // 
            this._openProjectFileDialog.DefaultExt = "sgp.xml";
            this._openProjectFileDialog.FileName = "Project.sgp.xml";
            this._openProjectFileDialog.Filter = "SGP files (*.sgp.xml)|*.sgp.xml|All XML files (*.xml)|*.xml|All files (*.*)|*.*";
            this._openProjectFileDialog.Title = "Open org.ncore.ServicedApi.CodeGenerator Project";
            // 
            // _saveProjectFileDialog
            // 
            this._saveProjectFileDialog.DefaultExt = "sgp.xml";
            this._saveProjectFileDialog.Filter = "SGP files (*.sgp.xml)|*.sgp.xml|All XML files (*.xml)|*.xml|All files (*.*)|*.*";
            this._saveProjectFileDialog.Title = "Save Project";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 552);
            this.Controls.Add(this._generatorPluginComboBox);
            this.Controls.Add(this._generatorPluginLabel);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this._openFolderBrowserButton);
            this.Controls.Add(this._outputPathRootTextBox);
            this.Controls.Add(this._outputPathRootLabel);
            this.Controls.Add(this._openFileDialogButton);
            this.Controls.Add(this._servicedApiXmlFileTextBox);
            this.Controls.Add(this._servicedApiXmlLabel);
            this.Controls.Add(this._mainMenuStrip);
            this.MainMenuStrip = this._mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "ServicedAPI Code Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this._templatesTabPage.ResumeLayout(false);
            this._modelTabPage.ResumeLayout(false);
            this._optionsTabPage.ResumeLayout(false);
            this._optionsTabPage.PerformLayout();
            this._mainMenuStrip.ResumeLayout(false);
            this._mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button _generateButton;
        private System.Windows.Forms.RichTextBox _consoleDisplayRichTextBox;
        private System.Windows.Forms.Label _servicedApiXmlLabel;
        private System.Windows.Forms.TextBox _servicedApiXmlFileTextBox;
        private System.Windows.Forms.Button _openFileDialogButton;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.Label _outputPathRootLabel;
        private System.Windows.Forms.TextBox _outputPathRootTextBox;
        private System.Windows.Forms.Button _openFolderBrowserButton;
        private System.Windows.Forms.Button _clearConsoleButton;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Button _refreshModelButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _templatesTabPage;
        private System.Windows.Forms.TreeView _templateSelectionTreeView;
        private System.Windows.Forms.TabPage _modelTabPage;
        private System.Windows.Forms.TreeView _modelTreeView;
        private System.Windows.Forms.Label _generatorPluginLabel;
        private System.Windows.Forms.ComboBox _generatorPluginComboBox;
        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog _openProjectFileDialog;
        private System.Windows.Forms.SaveFileDialog _saveProjectFileDialog;
        private System.Windows.Forms.CheckBox _stopOnGenerationErrorsCheckBox;
        private System.Windows.Forms.CheckBox _showGenerationResultsWindowCheckBox;
        private System.Windows.Forms.TabPage _optionsTabPage;
        private System.Windows.Forms.CheckBox _runGeneratorSynchronouslyCheckBox;
        private System.Windows.Forms.CheckBox _enableTemplateDebuggingCheckBox;

    }
}

