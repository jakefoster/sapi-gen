using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using org.ncore.ServicedApi.CodeGenerator.Api.Model;
using org.ncore.Diagnostics;

// NOTE: Here are the compiler directives for warning and error respectively.  We're going to need to wire these into generated code (starters) some of the time
//  when there's something we just can't get right.
//  #warning The compiler will report me as a warning but continue
//  #error The compiler will report me as an error and compilation will stop

// TODO: Add "auto-generated" warning comment to the top of .generated. files explaining that
//  the file is generated and should not be manually edited.  JF
// TODO: Add "tool version" and include in template Warning header thingy.  JF
namespace org.ncore.ServicedApi.CodeGenerator
{
    public partial class MainForm : Form
    {
        #region Static Members
        private const string DEFAULT_WINDOW_TITLE = "ServicedAPI Code Generator";
        private const string NEW_PROJECT_NAME = "Untitled Project";

        private static void _cascadeCheckedState( TreeNode parentNode )
        {
            TreeNode CurrentNode;
            for( int i = 0; i < parentNode.Nodes.Count; i++ )
            {
                CurrentNode = parentNode.Nodes[ i ];
                CurrentNode.Checked = parentNode.Checked;
                if( CurrentNode.Nodes.Count > 0 )
                {
                    _cascadeCheckedState( CurrentNode );
                }
            }
        }

        private static void _cascadeCheckedStateToParent( TreeNode childNode )
        {
            TreeNode parentNode = childNode.Parent;
            if( parentNode != null )
            {
                parentNode.Checked = childNode.Checked;
                _cascadeCheckedStateToParent( parentNode );
            }
        }

        private static string _buildWindowTitle( string projectName = "" )
        {
            if( string.IsNullOrEmpty( projectName ) )
            {
                projectName = NEW_PROJECT_NAME;
            }
            return String.Format( "{0} - {1}", DEFAULT_WINDOW_TITLE, projectName );
        }
        #endregion Static Members

        public MainForm()
        {
            InitializeComponent();
        }
        
        private RichTextBoxTextWriter _richTextBoxTextWriter;
        private Dictionary<string, Type> _plugins = null;
        private Object _currentSettings = null;
        private IGeneratorPlugin _generator = null;
        private CancellationTokenSource _cancellationTokenSource;
        private string _currentProject = string.Empty;

        #region Event Handlers
        private void MainForm_Load( object sender, EventArgs e )
        {
            _richTextBoxTextWriter = new RichTextBoxTextWriter( _consoleDisplayRichTextBox );
            Console.SetOut( _richTextBoxTextWriter );

            _fillGeneratorPluginDropdown();

            _intializeFromSavedSettings();
            
            _refreshModelTreeView();

            _setWindowText();
        }

        private void newProjectToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _revertToNewProject();
        }

        private void openProjectToolStripMenuItem_Click( object sender, EventArgs e )
        {
            string projectFileLoaction = string.Empty;
            if( _openProjectFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                _currentProject = _openProjectFileDialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                XDocument document = XDocument.Load(_currentProject);

                string pluginName = document.Root.Element("Configuration").Element("GeneratorPlugin").Element("Name").Value;
                XElement pluginSettings = document.Root.Element("Configuration").Element("GeneratorPlugin").Element("PluginSettings").Element("Settings");

                this._servicedApiXmlFileTextBox.Text =
                    document.Root.Element("Configuration").Element("ServicedApiXmlFileLocation").Value;

                this._outputPathRootTextBox.Text =
                    document.Root.Element("Configuration").Element("OutputRootPath").Value;

                Type pluginType = _plugins[pluginName];
                _loadGenerator(pluginType);

                int index = _generatorPluginComboBox.FindString(pluginName);
                _generatorPluginComboBox.SelectedIndex = index;

                object settings = _generator.GetDefaultSettings();

                XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());

                _currentSettings = xmlSerializer.Deserialize(pluginSettings.CreateReader());

                _setWindowText();
            }
            catch (Exception ex)
            {
                StringBuilder data = new StringBuilder();
                foreach( object key in ex.Data.Keys )
                {
                    data.Append( "Key: " );
                    data.Append( key );
                    data.Append( " Value: " );
                    data.Append( ex.Data[key] );
                    data.Append( Environment.NewLine );
                }
                
                MessageBox.Show(
                        "An error occurred while trying to open the project file." + Environment.NewLine +
                        Environment.NewLine + Environment.NewLine +
                        "Exception" + Environment.NewLine + 
                        Environment.NewLine + 
                        "Exception.Type: " + ex.GetType().FullName + Environment.NewLine + 
                        "Exception.Message: " + ex.Message + Environment.NewLine +
                        "Exception.Source: " + ex.Source + Environment.NewLine +
                        "Exception.StackTrace: " + ex.StackTrace + Environment.NewLine +
                        Environment.NewLine + Environment.NewLine + 
                        "Exception.Data: " + Environment.NewLine + data

                    );
            }
        }

        private void saveProjectAsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _saveProject( true );
        }

        private void saveProjectToolStripMenuItem_Click( object sender, EventArgs e )
        {
            _saveProject( false );
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void _generateButton_Click( object sender, EventArgs e )
        {
            if( _cancellationTokenSource == null )
            {
                Spy.Mark();
                Console.WriteLine( "Starting Generation" );

                GeneratorConfiguration config = new GeneratorConfiguration( _servicedApiXmlFileTextBox.Text,
                                                                            _outputPathRootTextBox.Text )
                                                    {
                                                        PluginSettings = _currentSettings,
                                                        EnableTemplateDebugging = _enableTemplateDebuggingCheckBox.Checked,
                                                        StopOnGenerationError = _stopOnGenerationErrorsCheckBox.Checked
                                                    };

                try
                {
                    Console.WriteLine( String.Format( "Processing schema file {0}", _servicedApiXmlFileTextBox.Text ) );
                    Console.WriteLine( String.Format( "Root output path {0}", _outputPathRootTextBox.Text ) );

                    if( _generator == null )
                    {
                        return; // TODO: Or throw? JF                 
                    }

                    if( _runGeneratorSynchronouslyCheckBox.Checked )
                    {
                        Exception generatorException = null;
                        try
                        {
                            _generator.Generate( config, new SelectionTree( _templateSelectionTreeView ), new SelectionTree( _modelTreeView ), new CancellationToken() );
                        }
                        catch( Exception ex )
                        {
                            generatorException = ex;
                        }
                        finally
                        {
                            this.ShowGenerationResults( config, generatorException );
                        }
                    }
                    else
                    {
                        _cancellationTokenSource = new CancellationTokenSource();
                        Task task =
                            new Task(
                                () =>
                                _generator.Generate( config, new SelectionTree( _templateSelectionTreeView ),
                                                     new SelectionTree( _modelTreeView ), _cancellationTokenSource.Token ) );
                        Task continuation = task.ContinueWith( ( antecedent ) =>
                        {
                            Exception ex = null;
                            // TODO: What to do with this exception?  
                            //   We can't just ignore it - in fact, somethimes
                            //   it's the cancellation expetion.  Ugh.  JF
                            if( task.Exception != null )
                            {
                                ex = task.Exception;
                            }

                            if( this.InvokeRequired )
                            {
                                this.Invoke(
                                    new ShowGenerationResultsDelegate(
                                        ShowGenerationResults ),
                                    new object[] { config, ex } );
                            }
                            else
                            {
                                this.ShowGenerationResults( config, ex );
                            }
                        } );
                        task.Start();

                        _generateButton.Text = "Cancel";
                    }
                }
                catch( Exception ex )
                {
                    Spy.Trace( ex );
                    MessageBox.Show( ex.ToString(), "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                }
                
            }
            else
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
            }
        }

        private delegate void ShowGenerationResultsDelegate( GeneratorConfiguration generatorConfiguration, Exception generatorException );
        public void ShowGenerationResults( GeneratorConfiguration generatorConfiguration, Exception generatorException )
        {
            GenerationResults results = new GenerationResults( generatorConfiguration, generatorException );

            Console.WriteLine();
            Console.WriteLine( results.BuildSummaryMessage( "\n" ) );
            Spy.Trace( results.BuildSummaryMessage( null ) );

            if( _showGenerationResultsWindowCheckBox.Checked )
            {
                GenerationSummaryForm form = new GenerationSummaryForm( results );
                form.ShowDialog();                
            }
            
            _cancellationTokenSource = null;

            _generateButton.Text = "Generate";
        }

        private void _openFileDialogButton_Click( object sender, EventArgs e )
        {
            _openFileDialog.ShowDialog( this );
        }

        private void _openFileDialog_FileOk( object sender, CancelEventArgs e )
        {
            string xmlFileName = ( (OpenFileDialog)sender ).FileName;
            _servicedApiXmlFileTextBox.Text = xmlFileName;

            Properties.Settings.Default._servicedApiXmlFileTextBox_Text = _servicedApiXmlFileTextBox.Text;

            if( _outputPathRootTextBox.Text == string.Empty )
            {
                // NOTE: Just a default, but assumes that the target root output path is the directory ABOVE the input file's directory (i.e. the /src/ directory).  JF
                string rootFilePath = Path.GetDirectoryName( Path.GetDirectoryName( xmlFileName ) );
                _outputPathRootTextBox.Text = rootFilePath;

                Properties.Settings.Default._outputPathRootTextBox_Text = _outputPathRootTextBox.Text;
            }

            Properties.Settings.Default.Save();

            _refreshModelTreeView();
        }

        private void _openFolderBrowserButton_Click( object sender, EventArgs e )
        {
            if( _outputPathRootTextBox.Text != string.Empty )
            {
                _folderBrowserDialog.SelectedPath = _outputPathRootTextBox.Text;
            }

            if( _folderBrowserDialog.ShowDialog( this ) == DialogResult.OK )
            {
                _outputPathRootTextBox.Text = _folderBrowserDialog.SelectedPath;
                Properties.Settings.Default._outputPathRootTextBox_Text = _outputPathRootTextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void _clearConsoleButton_Click( object sender, EventArgs e )
        {
            _consoleDisplayRichTextBox.Clear();
        }

        private void _refreshModelButton_Click( object sender, EventArgs e )
        {
            _modelTreeView.Nodes.Clear();
            _refreshModelTreeView();
        }

        private void _settingsButton_Click( object sender, EventArgs e )
        {
            if( _generator == null )
            {
                return; // TODO: Or throw? JF
            }

            object newSettings = this._generator.PromptForSettings( _currentSettings );

            if( newSettings != null )
            {
                _currentSettings = newSettings;
            }
        }

        private void _generatorPluginComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            ComboBox comboBox = ( (ComboBox)sender );
            if( comboBox.SelectedItem != null )
            {
                Type generatorType = _plugins[ (string)comboBox.SelectedItem ];
                _loadGenerator( generatorType );

                Properties.Settings.Default._generatorPluginComboBox_Selected = (string)comboBox.SelectedItem;
                Properties.Settings.Default.Save();

                _generateButton.Enabled = true;
            }
            else
            {
                _generateButton.Enabled = false;
            }
        }

        private void _templateSelectionTreeView_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Right )
            {
                TreeNode node = _templateSelectionTreeView.GetNodeAt( e.X, e.Y );
                if( node != null )
                {
                    node.Checked = !node.Checked;
                    _cascadeCheckedState( node );                    
                }
            }
        }

        private void _templateSelectionTreeView_MouseUp( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                TreeNode node = _templateSelectionTreeView.GetNodeAt( e.X, e.Y );
                if( node != null && node.Checked )
                {
                    _cascadeCheckedStateToParent( node );
                }
            }
        }

        // TODO: Really corny way of doing this.  Better to use .Modified or something.  JF
        private void _servicedApiXmlFileTextBox_Leave( object sender, System.EventArgs e )
        {
            Properties.Settings.Default._servicedApiXmlFileTextBox_Text = _servicedApiXmlFileTextBox.Text;
            Properties.Settings.Default.Save();
        }

        // TODO: Really corny way of doing this.  Better to use .Modified or something.  JF
        private void _outputPathRootTextBox_Leave( object sender, System.EventArgs e )
        {
            Properties.Settings.Default._outputPathRootTextBox_Text = _outputPathRootTextBox.Text;
            Properties.Settings.Default.Save();
        }


        private void _modelTreeView_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            if( e.Button != MouseButtons.Right )
            {
                return;
            }
            TreeNode node = _modelTreeView.GetNodeAt( e.X, e.Y );
            node.Checked = !node.Checked;
            _cascadeCheckedState( node );
        }

        private void _stopOnGenerationErrorsCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            Properties.Settings.Default._stopOnGenerationErrorsCheckBox_Checked = ( (CheckBox)sender ).Checked;
            Properties.Settings.Default.Save();
        }

        private void _servicedApiXmlFileTextBox_TextChanged( object sender, EventArgs e )
        {
            if( _servicedApiXmlFileTextBox.Text != string.Empty )
            {
                _refreshModelButton.Enabled = true;
            }
            else
            {
                _refreshModelButton.Enabled = false;
            }
        }

        private void _showGenerationResultsWindowCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            Properties.Settings.Default._showGenerationResultsWindowCheckBox_Checked = ( (CheckBox)sender ).Checked;
            Properties.Settings.Default.Save();
        }

        private void _enableTemplateDebuggingCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            Properties.Settings.Default._enableTemplateDebuggingCheckBox_Checked = ( (CheckBox)sender ).Checked;
            Properties.Settings.Default.Save();
        }

        private void _runGeneratorSynchronouslyCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            Properties.Settings.Default._runGeneratorSynchronouslyCheckBox_Checked = ( (CheckBox)sender ).Checked;
            Properties.Settings.Default.Save();
        }
        #endregion Event Handlers

        private void _intializeFromSavedSettings()
        {
            _outputPathRootTextBox.Text = Properties.Settings.Default._outputPathRootTextBox_Text;
            _servicedApiXmlFileTextBox.Text = Properties.Settings.Default._servicedApiXmlFileTextBox_Text;

            string selectedPlugin = Properties.Settings.Default._generatorPluginComboBox_Selected;
            if( !string.IsNullOrEmpty( selectedPlugin ) )
            {
                int index = _generatorPluginComboBox.FindString( selectedPlugin );
                _generatorPluginComboBox.SelectedIndex = index;
            }

            _stopOnGenerationErrorsCheckBox.Checked = Properties.Settings.Default._stopOnGenerationErrorsCheckBox_Checked;
            _showGenerationResultsWindowCheckBox.Checked = Properties.Settings.Default._showGenerationResultsWindowCheckBox_Checked;
            _enableTemplateDebuggingCheckBox.Checked = Properties.Settings.Default._enableTemplateDebuggingCheckBox_Checked;
            _runGeneratorSynchronouslyCheckBox.Checked = Properties.Settings.Default._runGeneratorSynchronouslyCheckBox_Checked;
        }

        private void _fillGeneratorPluginDropdown()
        {
            string binPath = Path.GetDirectoryName( Assembly.GetExecutingAssembly().GetName().CodeBase ).Remove( 0, 6 );
            _plugins = _findGeneratorPlugins( Path.Combine( binPath, @"plugins\" ) );

            foreach( string key in _plugins.Keys )
            {
                _generatorPluginComboBox.Items.Add( key );                
            }
        }

        private static Dictionary<string, Type> _findGeneratorPlugins( string path )
        {
            Dictionary<string, Type> plugins = new Dictionary<string, Type>();

            DirectoryInfo di = new DirectoryInfo( path );

            FileInfo[] files = di.GetFiles( "*.dll" );
            foreach( FileInfo fileInfo in files )
            {
                Assembly assembly = Assembly.LoadFile( fileInfo.FullName );
                Type[] types = assembly.GetTypes();
                foreach( Type type in types )
                {
                    if( typeof( IGeneratorPlugin ).IsAssignableFrom( type ) )
                    {
                        plugins.Add( type.FullName, type );
                    }
                }
            }

            DirectoryInfo[] directories = di.GetDirectories();
            foreach( DirectoryInfo directoryInfo in directories )
            {
                Dictionary<string, Type> childPlugins = _findGeneratorPlugins( directoryInfo.FullName );
                // TODO: This seems hokey.  How to efficiently copy one dictionary into another?  JF
                foreach( string key in childPlugins.Keys )
                {
                    // TODO: Don't know if we really want to obscure duplicate plugin instance?!  JF
                    if( !plugins.ContainsKey( key ) )
                    {
                        plugins.Add( key, childPlugins[ key ] );
                    }
                }
            }

            return plugins;
        }
        
        private void _refreshModelTreeView()
        {
            if( _servicedApiXmlFileTextBox.Text == string.Empty )
            {
                return;
            }

            XDocument modelXml = null;

            try
            {
                modelXml = XDocument.Load( _servicedApiXmlFileTextBox.Text );
            }
            catch( IOException ex )
            {
                if( ex is FileNotFoundException || ex is DirectoryNotFoundException )
                {
                    Spy.Trace( ex );
                    MessageBox.Show( String.Format( "Error loading Model XML.  File not found.\r\n{0}", _servicedApiXmlFileTextBox.Text ), "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Stop );
                    _servicedApiXmlFileTextBox.Text = string.Empty;
                    _outputPathRootTextBox.Text = string.Empty;
                }
                else
                {
                    Spy.Trace( ex );
                    MessageBox.Show( String.Format( "{0} ({1})", ex.Message, ex.GetType().FullName ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
                }
            }
            catch( Exception ex )
            {
                Spy.Trace( ex );
                MessageBox.Show( String.Format( "{0} ({1})", ex.Message, ex.GetType().FullName ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk );
            }

            if( modelXml == null )
            {
                return;
            }

            ServicedApiModel model = new ServicedApiModel( modelXml );

            _modelTreeView.Nodes.Clear();

            TreeNode serviceObjectNodes = new TreeNode( "ServiceObjects" ) { Name = "ServiceObjects", Checked = true };
            foreach( string key in model.Services.Keys )
            {
                ServiceModel service = model.Services[ key ];
                TreeNode serviceNode = new TreeNode( service.Name ) { Name = service.Name, Checked = true };
                serviceObjectNodes.Nodes.Add( serviceNode );
            }
            _modelTreeView.Nodes.Add( serviceObjectNodes );

            TreeNode factypeNodes = new TreeNode( "Factypes" ) { Name = "Factypes", Checked = true };
            foreach( string key in model.Factypes.Keys )
            {
                FactypeModel factype = model.Factypes[ key ];
                TreeNode factypeNode = new TreeNode( factype.Name ) { Name = factype.Name, Checked = true };
                factypeNodes.Nodes.Add( factypeNode );
            }
            _modelTreeView.Nodes.Add( factypeNodes );

            TreeNode viewListNodes = new TreeNode( "ViewLists" ) { Name = "ViewLists", Checked = true };
            foreach( string key in model.ViewLists.Keys )
            {
                ViewListModel viewList = model.ViewLists[ key ];
                TreeNode viewListNode = new TreeNode( viewList.Name ) { Name = viewList.Name, Checked = true };
                viewListNodes.Nodes.Add( viewListNode );
            }
            _modelTreeView.Nodes.Add( viewListNodes );
        }

        private void _buildGenerationOptions()
        {
            if( _generator == null )
            {
                return; // TODO: or throw?  JF
            }
            SelectionTree templateSelection = this._generator.GetTemplateSelection();

            templateSelection.PopulateTreeView( _templateSelectionTreeView );
        }

        private void _loadGenerator( Type type )
        {
            _generator = (IGeneratorPlugin)Activator.CreateInstance( type );

            _currentSettings = _generator.GetDefaultSettings();

            _buildGenerationOptions();
        }

        private void _saveProject( bool prompt )
        {
            XElement settings = null;
            string selectedGenerator = string.Empty;
            if( _generator != null )
            {
                XmlSerializer xmlSerializer = new XmlSerializer( _currentSettings.GetType() );
                StringBuilder builder = new StringBuilder();
                TextWriter textWriter = new StringWriter( builder );
                xmlSerializer.Serialize( textWriter, _currentSettings );
                textWriter.Close();
                settings = XElement.Parse( builder.ToString() );

                selectedGenerator = _generatorPluginComboBox.SelectedItem.ToString();
            }



            XDocument document =
                new XDocument(
                    new XElement( "Project",
                                  new XElement( "Configuration",
                                                new XElement( "ServicedApiXmlFileLocation",
                                                              _servicedApiXmlFileTextBox.Text ),
                                                new XElement( "OutputRootPath", _outputPathRootTextBox.Text ),
                                                new XElement( "GeneratorPlugin",
                                                              new XElement( "Name", selectedGenerator ),
                                                              new XElement( "PluginSettings", settings )

                                                    )
                                      )
                        )
                    );

            string projectFileLoction = string.Empty;
            if( prompt == true || string.IsNullOrEmpty( _currentProject ) )
            {
                if( _saveProjectFileDialog.ShowDialog( this ) == DialogResult.OK )
                {
                    projectFileLoction = _saveProjectFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            else
            {
                projectFileLoction = _currentProject;
            }
            document.Save( projectFileLoction );

            _currentProject = projectFileLoction;
            _setWindowText();
        }

        private void _revertToNewProject()
        {
            _currentProject = null;
            _generator = null;
            _currentSettings = null;
            
            
            _servicedApiXmlFileTextBox.Clear();
            _outputPathRootTextBox.Clear();
            _generatorPluginComboBox.SelectedItem = null;
            _templateSelectionTreeView.Nodes.Clear();
            _modelTreeView.Nodes.Clear();

            _setWindowText();
        }

        private void _setWindowText()
        {
            this.Text = _buildWindowTitle( _currentProject );
        }
    }
}
