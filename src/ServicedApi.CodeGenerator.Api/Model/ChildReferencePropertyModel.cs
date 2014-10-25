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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, ChildType = {this.ChildType}" )]
    public class ChildReferencePropertyModel : PropertyModel
    {
        public System.String ChildType { get; set; }
        public System.String ChildKey { get; set; }
        public System.String ParentKey { get; set; }

        public ChildReferencePropertyModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            this.ChildType = definition.Attribute( "ChildType" ).Value;
            this.ChildKey = definition.Attribute( "ChildKey" ).Value;
            this.ParentKey = definition.Attribute( "ParentKey" ).Value;
        }
    }
}
