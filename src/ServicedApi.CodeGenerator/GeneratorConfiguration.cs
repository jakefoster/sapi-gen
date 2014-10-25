using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Text;
using org.ncore.ServicedApi.CodeGenerator.Api;
using org.ncore.ServicedApi.CodeGenerator.Api.Model;

namespace org.ncore.ServicedApi.CodeGenerator
{

    public class GeneratorConfiguration
    {
        // TODO: Should we check to make sure this isn't empty?
        public string RootOutputPath { get; set; }

        public bool HadErrors { get; set; }
        public CompilerErrorCollection CompilerErrors { get; set; }
        public double GenerationDuration { get; set; }
        public int FileGenerationCount { get; set; }
        public int FailedFileGenerationCount { get; set; }

        public bool StopOnGenerationError { get; set; }

        public Object PluginSettings { get; set; }

        public bool EnableTemplateDebugging { get; set; }

        private ServicedApiModel _model = null;
        public ServicedApiModel Model 
        { 
            get
            {
                return _model;
            }
        }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        private string _schemaFileName = string.Empty;
        public string SchemaFileName
        { 
            get
            {
                return _schemaFileName;
            }
        }

        public XDocument Schema
        {
            get
            {
                return Model.Schema;
            }
        }

        public String SchemaXml
        {
            get
            {
                return Model.Schema.ToString();
            }
        }

        public GeneratorConfiguration( string schemaFileName, string rootOutputPath )
        {
            _schemaFileName = schemaFileName;
            RootOutputPath = rootOutputPath;
            XDocument modelXml = XDocument.Load( _schemaFileName );
            _model = new ServicedApiModel( modelXml );
            CompilerErrors = new CompilerErrorCollection();
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        public string BuildOutputPath( string projectNamePattern, string subPathFragment )
        {
            string projectName = projectNamePattern.Replace( "{?}", _model.Namespace );
            string outputPathFragment = String.Format( @"{0}\{1}", projectName, subPathFragment );
            string outputPath = Path.Combine( this.RootOutputPath, outputPathFragment );
            return outputPath;
        }

    }

}
