using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    [Serializable]
    public class FactypeModel : BaseModel
    {
        public System.String MonikerName{ get; set; }
        public FactypeAttributeModel KeyProperty { get; set; }
        public Dictionary<System.String, FactypeAttributeModel> Attributes { get; set; }
        public Dictionary<System.String, ParentReferencePropertyModel> ParentReferences { get; set; }
        public Dictionary<System.String, ChildReferencePropertyModel> ChildReferences { get; set; }
        public Dictionary<System.String, GetByParentOperationModel> GetByParentOperations { get; set; }
        public System.Boolean IsKeyed
        {
            get
            {
                return this.KeyProperty != null;
            }
        }

        public FactypeModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            this.MonikerName = definition.Attribute( "MonikerName" ).Value;

            List<XElement> attributeNodes = definition.Element( "Properties" ).Elements( "Attribute" ).ToList();
            this.Attributes = new Dictionary<string, FactypeAttributeModel>( attributeNodes.Count );
            foreach( XElement attributeNode in attributeNodes )
            {
                FactypeAttributeModel property = new FactypeAttributeModel( attributeNode, this );
                this.Attributes.Add( property.Name, property );
            }

            List<XElement> parentReferenceNodes = definition.Element( "Properties" ).Elements( "ParentReference" ).ToList();
            this.ParentReferences = new Dictionary<string, ParentReferencePropertyModel>( parentReferenceNodes.Count );
            foreach( XElement parentReferenceNode in parentReferenceNodes )
            {
                ParentReferencePropertyModel property = new ParentReferencePropertyModel( parentReferenceNode, this );
                this.ParentReferences.Add( property.Name, property );
            }

            List<XElement> childReferenceNodes = definition.Element( "Properties" ).Elements( "ChildReference" ).ToList();
            this.ChildReferences = new Dictionary<string, ChildReferencePropertyModel>( childReferenceNodes.Count );
            foreach( XElement childReferenceNode in childReferenceNodes )
            {
                ChildReferencePropertyModel property = new ChildReferencePropertyModel( childReferenceNode, this );
                this.ChildReferences.Add( property.Name, property );
            }

            List<XElement> getByParentOpNodes = definition.Element( "Operations" ).Elements( "GetByParent" ).ToList();
            this.GetByParentOperations = new Dictionary<string, GetByParentOperationModel>( getByParentOpNodes.Count );
            foreach( XElement operationNode in getByParentOpNodes )
            {
                GetByParentOperationModel operationModel = new GetByParentOperationModel( operationNode, this );
                this.GetByParentOperations.Add( operationModel.Name, operationModel );
            }
        }
    }
}
