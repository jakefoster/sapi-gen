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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, ParentType = {this.ParentType}" )]
    public class ParentReferencePropertyModel : PropertyModel
    {
        public System.String ParentType { get; set; }
        public System.String ParentKey { get; set; }
        public System.String ChildKey { get; set; }

        public ParentReferencePropertyModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            this.ParentType = definition.Attribute( "ParentType" ).Value;
            this.ParentKey = definition.Attribute( "ParentKey" ).Value;
            this.ChildKey = definition.Attribute( "ChildKey" ).Value;
        }
    }
}
