using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    // TODO: Figure out how to serialize the model and pass it directly into the templates (across app domains) 
    //  instead of passing in the XML as a string and having to re-hydrate the model for each template run.
    //  I believe this guy has to inherit MarshalByRef and also has to be marked as serializable.  JF
    [Serializable]
    [DebuggerDisplay( "Name = {this.Name}" )]
    public abstract class BaseModel : IParent
    {
        public System.String Name { get; set; }
        public System.String PluralName { get; set; }
        public IParent Parent { get; set; }
        public ServicedApiModel ServicedApi { get; set; }

        public BaseModel( XElement definition, IParent parent )
        {
            this.Parent = parent;
            if( this.Parent is ServicedApiModel )
            {
                ServicedApi = (ServicedApiModel)this.Parent;
            }
            else if( ( (BaseModel)this.Parent ).Parent is ServicedApiModel )
            {
                ServicedApi = (ServicedApiModel)( (BaseModel)this.Parent ).Parent;
            }
            this.Hydrate( definition );
        }

        public virtual void Hydrate( XElement definition )
        {
            if( definition.Attribute( "Name" ) != null )
            {
                this.Name = definition.Attribute( "Name" ).Value;
            }
            else
            {
                this.Name = this.GetDefaultName();
            }

            if( definition.Attribute( "PluralName" ) != null )
            {
                this.PluralName = definition.Attribute( "PluralName" ).Value;
            }
            else
            {
                this.PluralName = TextFormatter.Pluralize( this.Name );
            }
        }

        public virtual string GetDefaultName()
        {
            throw new ApplicationException( "No default name specified for this model type." );
        }
    }
}
