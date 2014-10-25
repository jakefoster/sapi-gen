namespace org.ncore.ServicedApi.CodeGenerator
{
    partial class ExceptionTreeForm
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
            this._exceptionGraphRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _exceptionGraphRichTextBox
            // 
            this._exceptionGraphRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._exceptionGraphRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._exceptionGraphRichTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._exceptionGraphRichTextBox.Location = new System.Drawing.Point(-3, 1);
            this._exceptionGraphRichTextBox.Name = "_exceptionGraphRichTextBox";
            this._exceptionGraphRichTextBox.Size = new System.Drawing.Size(898, 477);
            this._exceptionGraphRichTextBox.TabIndex = 0;
            this._exceptionGraphRichTextBox.Text = "";
            this._exceptionGraphRichTextBox.WordWrap = false;
            // 
            // ExceptionGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 476);
            this.Controls.Add(this._exceptionGraphRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ExceptionGraphForm";
            this.Text = "Exception Graph";
            this.Load += new System.EventHandler(this.ExceptionGraphForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox _exceptionGraphRichTextBox;
    }
}