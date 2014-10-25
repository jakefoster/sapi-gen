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
    [DebuggerDisplay( "Name = {this.Name}, Visibility = {this.Visibility}" )]
    public abstract class OperationModel : BaseModel
    {
        public VisibilityFactype Visibility { get; set; }

        public OperationModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            if( definition.Attribute( "Visibility" ) != null )
            {
                this.Visibility = definition.Attribute( "Visibility" ).Value;
            }
            else
            {
                this.Visibility = this.GetDefaultVisibility();
            }
        }

        public virtual string GetDefaultVisibility()
        {
            return VisibilityFactype.Public;
        }
    }
}
