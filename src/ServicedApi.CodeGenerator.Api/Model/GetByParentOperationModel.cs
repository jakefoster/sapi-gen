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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}, ParentType = {this.ParentType}, ChildKey = {this.ChildKey}, ParentKey = {this.ParentKey}, KeyType = {this.KeyType}" )]
    public class GetByParentOperationModel : OperationModel
    {
        public System.String ParentType { get; set; }
        public System.String ChildKey { get; set; }
        public System.String KeyType
        { 
            get
            {
                // TODO: Figure out something more elegant for this.  JF
                if( this.Parent is ServiceModel )
                {
                    return ( (ServiceModel)this.Parent ).Attributes[ this.ChildKey ].Type;
                }
                else if( this.Parent is FactypeModel )
                {
                    return ( (FactypeModel)this.Parent ).Attributes[ this.ChildKey ].Type;
                }
                throw new ApplicationException( "Parent is not a valid type for this operation." );
            }
        }

        public GetByParentOperationModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );
            this.ParentType = definition.Attribute( "ParentType" ).Value;
            this.ChildKey = definition.Attribute( "ChildKey" ).Value;
        }
    }
}
