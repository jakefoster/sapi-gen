using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace org.ncore.ServicedApi.CodeGenerator.Plugins.StaticSvcLinqToSql
{
    [Serializable]
    public class Settings
    {
        // TODO: This should be a Factype considering how we're using it.  JF
        public enum TargetFrameworkVersionEnum
        {
            dotNet_3_5,
            dotNet_4_0
        }

        private bool _doNotGenerateStarters = false;
        public bool DoNotGenerateStarters
        {
            get { return _doNotGenerateStarters; }
            set { _doNotGenerateStarters = value; }
        }

        private string _nameOfGeneratedFolder = "_generated";
        public string NameOfGeneratedFolder
        {
            get { return _nameOfGeneratedFolder; }
            set { _nameOfGeneratedFolder = value; }
        }

        private string _nameOfStartersFolder = "_starters";
        public string NameOfStartersFolder
        {
            get { return _nameOfStartersFolder; }
            set { _nameOfStartersFolder = value; }
        }

        private TargetFrameworkVersionEnum _targetFrameworkVersion = TargetFrameworkVersionEnum.dotNet_4_0;
        public TargetFrameworkVersionEnum TargetFrameworkVersion
        {
            get { return _targetFrameworkVersion; }
            set { _targetFrameworkVersion = value; }
        }

        [XmlIgnore]
        public string TargetFrameworkVersionText
        {
            set
            {
                if( value == "3.5" )
                {
                    _targetFrameworkVersion = TargetFrameworkVersionEnum.dotNet_3_5;
                }
                else if( value == "4.0" )
                {
                    _targetFrameworkVersion = TargetFrameworkVersionEnum.dotNet_4_0;
                }
                else
                {
                    throw new ApplicationException( "Unknown TargetFrameworkVersion" );
                }
            }
            get
            {
                if( _targetFrameworkVersion == TargetFrameworkVersionEnum.dotNet_3_5 )
                {
                    return "3.5";
                }
                else if( _targetFrameworkVersion == TargetFrameworkVersionEnum.dotNet_4_0 )
                {
                    return "4.0";
                }
                else
                {
                    // NOTE: Well this should never happen... JF
                    throw new ApplicationException( "Unknown TargetFrameworkVersion" );
                }
            }
        }
    }
}
