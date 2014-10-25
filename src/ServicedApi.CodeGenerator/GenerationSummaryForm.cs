using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.ncore.Extensions;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public partial class GenerationSummaryForm : Form
    {
        private GenerationResults _generationResults;

        public GenerationSummaryForm( GenerationResults generationResults )
        {
            _generationResults = generationResults;

            InitializeComponent();

            _showExceptionTreeLinkLabel.Hide();
        }

        private void GenerationSummaryForm_Load( object sender, EventArgs e )
        {
            if( _generationResults.FinalStatus != GenerationResults.FinalStatusEnum.CompletedSuccessfully )
            {
                _successFailureLabel.Text = "Generation failed!";
                _successFailureLabel.ForeColor = Color.Crimson;
                _successFailurePictureBox.Image = _successFailurePictureBox.ErrorImage;
            }
            else
            {
                _successFailureLabel.Text = "Generation succeeded.";
                _successFailureLabel.ForeColor = Color.Green;
            }

            TreeNode statusNode = _detailsTreeView.Nodes.Add( "Summary: " );
            statusNode.Nodes.Add( "Final Status: " + _generationResults.FinalStatus.ToString() );
            statusNode.Nodes.Add( "Duration (seconds): " + _generationResults.GeneratorConfiguration.GenerationDuration.ToString() );
            statusNode.Nodes.Add( "Files generated: " + _generationResults.GeneratorConfiguration.FileGenerationCount.ToString() );
            statusNode.Nodes.Add( "Files not generated due to errors: " + _generationResults.GeneratorConfiguration.FailedFileGenerationCount.ToString() );

            if( _generationResults.HadException )
            {
                _showExceptionTreeLinkLabel.Show();

                // TODO: Again, some kind of recursive call to populate the entire exception heirarchy would be nice.  JF
                Exception targetException = _generationResults.GenerationException;
                if( targetException is AggregateException )
                {
                    // TODO: Cheesy.  Let's fix this.  JF
                    targetException = targetException.InnerException;
                }

                TreeNode exceptionNode = _detailsTreeView.Nodes.Add( "Exception: " + targetException.GetType().FullName );
                exceptionNode.Nodes.Add( "Type: " + targetException.GetType().FullName );
                exceptionNode.Nodes.Add( "Message: " + targetException.Message );
                exceptionNode.Nodes.Add( "Source: " + targetException.Source );
                exceptionNode.Nodes.Add( "TargetSite: " + targetException.TargetSite );
                if( targetException.InnerException != null )
                {
                    exceptionNode.Nodes.Add( "InnerException: " + targetException.InnerException.GetType().FullName );
                }
            }

            if( _generationResults.HadGenerationErrors )
            {
                TreeNode generationErrorsNode = _detailsTreeView.Nodes.Add( "Generation Errors" );

                foreach( CompilerError error in _generationResults.GeneratorConfiguration.CompilerErrors )
                {
                    TreeNode errorNode = generationErrorsNode.Nodes.Add( "Generation Error: " + error.ErrorText );
                    errorNode.Nodes.Add( "ErrorText: " + error.ErrorText );
                    errorNode.Nodes.Add( "FileName: " + error.FileName );
                    errorNode.Nodes.Add( "ErrorNumber: " + error.ErrorNumber );
                    errorNode.Nodes.Add( "Line: " + error.Line );
                    errorNode.Nodes.Add( "Column: " + error.Column );
                    errorNode.Nodes.Add( "IsWarning: " + error.IsWarning );
                }
            }

            _detailsTreeView.SelectedNode = null;

            _detailsTreeView.ExpandAll();

            _successFailureLabel.Focus();
        }

        private void _showExceptionTreeLinkLabel_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            ExceptionTreeForm form = new ExceptionTreeForm( _generationResults.GenerationException.ToXDocument() );
            form.ShowDialog();
        }
    }
}
