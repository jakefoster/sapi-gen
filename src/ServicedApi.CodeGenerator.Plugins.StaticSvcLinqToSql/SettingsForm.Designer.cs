namespace org.ncore.ServicedApi.CodeGenerator.Plugins.StaticSvcLinqToSql
{
    partial class SettingsForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this._startersFolderNameLabel = new System.Windows.Forms.Label();
            this._startersFolderNameTextBox = new System.Windows.Forms.TextBox();
            this._generatedFolderNameLabel = new System.Windows.Forms.Label();
            this._generatedFolderNameTextBox = new System.Windows.Forms.TextBox();
            this._targetFrameworkVersionLabel = new System.Windows.Forms.Label();
            this._targetFrameworkVersionComboBox = new System.Windows.Forms.ComboBox();
            this._generateStartersLabel = new System.Windows.Forms.Label();
            this._doNotGenerateStartersCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(44, 117);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(139, 117);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // _startersFolderNameLabel
            // 
            this._startersFolderNameLabel.AutoSize = true;
            this._startersFolderNameLabel.Location = new System.Drawing.Point(13, 86);
            this._startersFolderNameLabel.Name = "_startersFolderNameLabel";
            this._startersFolderNameLabel.Size = new System.Drawing.Size(116, 13);
            this._startersFolderNameLabel.TabIndex = 16;
            this._startersFolderNameLabel.Text = "Name of starters folder:";
            // 
            // _startersFolderNameTextBox
            // 
            this._startersFolderNameTextBox.Location = new System.Drawing.Point(149, 83);
            this._startersFolderNameTextBox.Name = "_startersFolderNameTextBox";
            this._startersFolderNameTextBox.Size = new System.Drawing.Size(77, 20);
            this._startersFolderNameTextBox.TabIndex = 15;
            this._startersFolderNameTextBox.Text = "_starters";
            // 
            // _generatedFolderNameLabel
            // 
            this._generatedFolderNameLabel.AutoSize = true;
            this._generatedFolderNameLabel.Location = new System.Drawing.Point(13, 60);
            this._generatedFolderNameLabel.Name = "_generatedFolderNameLabel";
            this._generatedFolderNameLabel.Size = new System.Drawing.Size(130, 13);
            this._generatedFolderNameLabel.TabIndex = 14;
            this._generatedFolderNameLabel.Text = "Name of generated folder:";
            // 
            // _generatedFolderNameTextBox
            // 
            this._generatedFolderNameTextBox.Location = new System.Drawing.Point(149, 57);
            this._generatedFolderNameTextBox.Name = "_generatedFolderNameTextBox";
            this._generatedFolderNameTextBox.Size = new System.Drawing.Size(77, 20);
            this._generatedFolderNameTextBox.TabIndex = 13;
            this._generatedFolderNameTextBox.Text = "_generated";
            // 
            // _targetFrameworkVersionLabel
            // 
            this._targetFrameworkVersionLabel.AutoSize = true;
            this._targetFrameworkVersionLabel.Location = new System.Drawing.Point(12, 34);
            this._targetFrameworkVersionLabel.Name = "_targetFrameworkVersionLabel";
            this._targetFrameworkVersionLabel.Size = new System.Drawing.Size(107, 13);
            this._targetFrameworkVersionLabel.TabIndex = 12;
            this._targetFrameworkVersionLabel.Text = "Target .NET Version:";
            // 
            // _targetFrameworkVersionComboBox
            // 
            this._targetFrameworkVersionComboBox.FormattingEnabled = true;
            this._targetFrameworkVersionComboBox.Items.AddRange(new object[] {
            "3.5",
            "4.0"});
            this._targetFrameworkVersionComboBox.Location = new System.Drawing.Point(126, 29);
            this._targetFrameworkVersionComboBox.Name = "_targetFrameworkVersionComboBox";
            this._targetFrameworkVersionComboBox.Size = new System.Drawing.Size(62, 21);
            this._targetFrameworkVersionComboBox.TabIndex = 11;
            this._targetFrameworkVersionComboBox.Text = "4.0";
            // 
            // _generateStartersLabel
            // 
            this._generateStartersLabel.AutoSize = true;
            this._generateStartersLabel.Location = new System.Drawing.Point(12, 9);
            this._generateStartersLabel.Name = "_generateStartersLabel";
            this._generateStartersLabel.Size = new System.Drawing.Size(149, 13);
            this._generateStartersLabel.TabIndex = 9;
            this._generateStartersLabel.Text = "Do Not Generate Starter Files:";
            // 
            // _doNotGenerateStartersCheckBox
            // 
            this._doNotGenerateStartersCheckBox.AutoSize = true;
            this._doNotGenerateStartersCheckBox.Location = new System.Drawing.Point(167, 9);
            this._doNotGenerateStartersCheckBox.Name = "_doNotGenerateStartersCheckBox";
            this._doNotGenerateStartersCheckBox.Size = new System.Drawing.Size(15, 14);
            this._doNotGenerateStartersCheckBox.TabIndex = 10;
            this._doNotGenerateStartersCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 154);
            this.Controls.Add(this._startersFolderNameLabel);
            this.Controls.Add(this._startersFolderNameTextBox);
            this.Controls.Add(this._generatedFolderNameLabel);
            this.Controls.Add(this._generatedFolderNameTextBox);
            this.Controls.Add(this._targetFrameworkVersionLabel);
            this.Controls.Add(this._targetFrameworkVersionComboBox);
            this.Controls.Add(this._generateStartersLabel);
            this.Controls.Add(this._doNotGenerateStartersCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label _startersFolderNameLabel;
        private System.Windows.Forms.TextBox _startersFolderNameTextBox;
        private System.Windows.Forms.Label _generatedFolderNameLabel;
        private System.Windows.Forms.TextBox _generatedFolderNameTextBox;
        private System.Windows.Forms.Label _targetFrameworkVersionLabel;
        private System.Windows.Forms.ComboBox _targetFrameworkVersionComboBox;
        private System.Windows.Forms.Label _generateStartersLabel;
        private System.Windows.Forms.CheckBox _doNotGenerateStartersCheckBox;
    }
}