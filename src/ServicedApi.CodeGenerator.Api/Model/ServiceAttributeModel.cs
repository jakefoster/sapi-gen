using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    [Serializable]
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, Type = {this.Type}, IsIdentity = {this.IsIdentity}, IsVersion = {this.IsVersion}" )]
    public class ServiceAttributeModel : AttributePropertyModel
    {
        public System.Boolean IsIdentity { get; set; }
        public System.Boolean IsVersion { get; set; }

        public ServiceAttributeModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );
            this.IsIdentity = definition.Attribute( "IsIdentifier" ) != null ? bool.Parse( definition.Attribute( "IsIdentifier" ).Value ) : false;
            if( this.IsIdentity )
            {
                ( (ServiceModel)this.Parent ).IdentifierProperty = this;
            }
            this.IsVersion = definition.Attribute( "IsVersion" ) != null ? bool.Parse( definition.Attribute( "IsVersion" ).Value ) : false;
            if( this.IsVersion )
            {
                ( (ServiceModel)this.Parent ).VersionProperty = this;
            }
        }
    }
}
