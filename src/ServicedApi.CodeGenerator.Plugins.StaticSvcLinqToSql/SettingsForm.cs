using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.ncore.Diagnostics;

namespace org.ncore.ServicedApi.CodeGenerator.Plugins.StaticSvcLinqToSql
{
    public partial class SettingsForm : Form
    {
        public static object PromptForSettings( object currentSettings )
        {
            SettingsForm form = new SettingsForm( currentSettings );
            if( form.ShowDialog() == DialogResult.OK )
            {
                Settings settings = new Settings();
                settings.DoNotGenerateStarters = form._doNotGenerateStartersCheckBox.Checked;
                settings.TargetFrameworkVersionText = form._targetFrameworkVersionComboBox.Text;
                settings.NameOfGeneratedFolder = form._generatedFolderNameTextBox.Text;
                settings.NameOfStartersFolder = form._startersFolderNameTextBox.Text;
                return settings;
            }
            else
            {
                return null;
            }
        }

        public SettingsForm( object currentSettings )
        {
            if( currentSettings != null && currentSettings.GetType() != typeof( Settings ) )
            {
                throw new ApplicationException( "Settings object is not of type SettingsForm.Settings." );
            }

            InitializeComponent();

            if( currentSettings != null )
            {
                this._doNotGenerateStartersCheckBox.Checked = ( (Settings)currentSettings ).DoNotGenerateStarters;
                this._targetFrameworkVersionComboBox.Text = ( (Settings)currentSettings ).TargetFrameworkVersionText;
                this._generatedFolderNameTextBox.Text = ( (Settings)currentSettings ).NameOfGeneratedFolder;
                this._startersFolderNameTextBox.Text = ( (Settings)currentSettings ).NameOfStartersFolder;
            }
        }

        private void SettingsForm_Load( object sender, EventArgs e )
        {

        }

        private void okButton_Click( object sender, EventArgs e )
        {

        }

        private void _refreshTemplatesButton_Click( object sender, EventArgs e )
        {
            // TODO: Needed?  JF
            /*
            DirectoryInfo projectDirectory = Directory.GetParent( _binPath ).Parent;
            DirectoryInfo templateDirectory = new DirectoryInfo( Path.Combine( projectDirectory.FullName, @"Templates\" ) );

            _copyDirectory( templateDirectory.FullName, Path.Combine( _binPath, @"Templates\" ), false );
            */
        }

        // TODO: I can't believe this doesn't existing somewhere in the .NET BCL?!  Move to NCore main? JF
        private static void _copyDirectory( String source, String destination, bool copyHidden )
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo( source );
            DirectoryInfo destinationDirectory = new DirectoryInfo( destination );

            // NOTE: Don't copy hidden directories.
            if( !copyHidden && ( ( sourceDirectory.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden ) )
            {
                Spy.Trace( "Skipping hidden directory. {0}", sourceDirectory.Name );
                return;
            }

            if( !destinationDirectory.Exists )
            {
                Directory.CreateDirectory( destinationDirectory.FullName );
            }

            FileInfo[] files = sourceDirectory.GetFiles( "*" );
            foreach( FileInfo file in files )
            {
                if( !copyHidden && ( ( file.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden ) )
                {
                    Spy.Trace( "Skipping hidden file. {0}", file.Name );
                }
                else
                {
                    file.CopyTo( Path.Combine( destination, file.Name ), true );
                }

            }

            DirectoryInfo[] children = sourceDirectory.GetDirectories();
            foreach( DirectoryInfo directory in children )
            {
                _copyDirectory( Path.Combine( source, directory.Name ), Path.Combine( destination, directory.Name ), copyHidden );
            }
        }
    }
}
