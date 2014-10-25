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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, ChildType = {this.ChildType}, ChildKey = {this.ChildKey}, ParentKey = {this.ParentKey}, KeyType = {this.KeyType}" )]
    public class ReadChildrenOperationModel : OperationModel
    {
        public System.String ChildType { get; set; }
        public System.String ChildKey { get; set; }
        public System.String ParentKey { get; set; }
        public System.String ChildOperation { get; set; }
        public System.String KeyType
        {
            get
            {
                // TODO: Figure out something more elegant for this.  JF
                if( this.Parent is ServiceModel )
                {
                    return ( (ServiceModel)this.Parent ).Attributes[ this.ParentKey ].Type;
                }
                else if( this.Parent is FactypeModel )
                {
                    return ( (FactypeModel)this.Parent ).Attributes[ this.ParentKey ].Type;
                }
                throw new ApplicationException( "Parent is not a valid type for this operation." );
            }
        }

        public ReadChildrenOperationModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );
            this.ChildType = definition.Attribute( "ChildType" ).Value;
            this.ChildKey = definition.Attribute( "ChildKey" ).Value;
            this.ParentKey = definition.Attribute( "ParentKey" ).Value;
            this.ChildOperation = definition.Attribute( "ChildOperation" ).Value;
        }
    }
}
