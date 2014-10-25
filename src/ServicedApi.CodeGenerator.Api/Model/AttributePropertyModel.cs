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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, Type = {this.Type}" )]
    public abstract class AttributePropertyModel : PropertyModel
    {
        public System.String Type { get; set; }
        
        public AttributePropertyModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            this.Type = definition.Attribute( "Type" ).Value;
        }
    }
}
