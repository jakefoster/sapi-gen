namespace org.ncore.ServicedApi.CodeGenerator
{
    partial class GenerationSummaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerationSummaryForm));
            this._successFailureLabel = new System.Windows.Forms.Label();
            this._detailsTreeView = new System.Windows.Forms.TreeView();
            this._showExceptionTreeLinkLabel = new System.Windows.Forms.LinkLabel();
            this._successFailurePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._successFailurePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _successFailureLabel
            // 
            this._successFailureLabel.AutoSize = true;
            this._successFailureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._successFailureLabel.Location = new System.Drawing.Point(47, 18);
            this._successFailureLabel.Name = "_successFailureLabel";
            this._successFailureLabel.Size = new System.Drawing.Size(137, 26);
            this._successFailureLabel.TabIndex = 0;
            this._successFailureLabel.Text = "Generation...";
            // 
            // _detailsTreeView
            // 
            this._detailsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._detailsTreeView.Location = new System.Drawing.Point(12, 67);
            this._detailsTreeView.Name = "_detailsTreeView";
            this._detailsTreeView.Size = new System.Drawing.Size(608, 264);
            this._detailsTreeView.TabIndex = 1;
            // 
            // _showExceptionTreeLinkLabel
            // 
            this._showExceptionTreeLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._showExceptionTreeLinkLabel.AutoSize = true;
            this._showExceptionTreeLinkLabel.Location = new System.Drawing.Point(500, 51);
            this._showExceptionTreeLinkLabel.Name = "_showExceptionTreeLinkLabel";
            this._showExceptionTreeLinkLabel.Size = new System.Drawing.Size(120, 13);
            this._showExceptionTreeLinkLabel.TabIndex = 2;
            this._showExceptionTreeLinkLabel.TabStop = true;
            this._showExceptionTreeLinkLabel.Text = "Show full exception tree";
            this._showExceptionTreeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._showExceptionTreeLinkLabel_LinkClicked);
            // 
            // _successFailurePictureBox
            // 
            this._successFailurePictureBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("_successFailurePictureBox.ErrorImage")));
            this._successFailurePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("_successFailurePictureBox.Image")));
            this._successFailurePictureBox.Location = new System.Drawing.Point(12, 14);
            this._successFailurePictureBox.Name = "_successFailurePictureBox";
            this._successFailurePictureBox.Size = new System.Drawing.Size(32, 32);
            this._successFailurePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._successFailurePictureBox.TabIndex = 3;
            this._successFailurePictureBox.TabStop = false;
            // 
            // GenerationSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 343);
            this.Controls.Add(this._successFailurePictureBox);
            this.Controls.Add(this._showExceptionTreeLinkLabel);
            this.Controls.Add(this._detailsTreeView);
            this.Controls.Add(this._successFailureLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GenerationSummaryForm";
            this.Text = "Generation Summary";
            this.Load += new System.EventHandler(this.GenerationSummaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._successFailurePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _successFailureLabel;
        private System.Windows.Forms.TreeView _detailsTreeView;
        private System.Windows.Forms.LinkLabel _showExceptionTreeLinkLabel;
        private System.Windows.Forms.PictureBox _successFailurePictureBox;
    }
}