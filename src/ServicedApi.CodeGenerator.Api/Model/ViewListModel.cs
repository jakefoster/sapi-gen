using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    [Serializable]
    public class ViewListModel : BaseModel
    {
        public Dictionary<System.String, ViewListAttributeModel> Attributes { get; set; }

        public ViewListModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            List<XElement> attributeNodes = definition.Element( "Properties" ).Elements( "Attribute" ).ToList();
            this.Attributes = new Dictionary<string, ViewListAttributeModel>( attributeNodes.Count );
            foreach( XElement attributeNode in attributeNodes )
            {
                ViewListAttributeModel property = new ViewListAttributeModel( attributeNode, this );
                this.Attributes.Add( property.Name, property );
            }
        }
    }
}
