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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, Type = {this.Type}, IsKey = {this.IsKey}" )]
    public class FactypeAttributeModel : AttributePropertyModel
    {
        public System.Boolean IsKey { get; set; }

        public FactypeAttributeModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );
            this.IsKey = definition.Attribute( "IsKey" ) != null ? bool.Parse( definition.Attribute( "IsKey" ).Value ) : false;
            if( this.IsKey )
            {
                ( (FactypeModel)this.Parent ).KeyProperty = this;
            }
        }
    }
}
