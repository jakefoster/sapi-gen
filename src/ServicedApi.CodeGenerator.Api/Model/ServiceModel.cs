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
    public class ServiceModel : BaseModel
    {
        public ServiceAttributeModel IdentifierProperty { get; set; }
        public ServiceAttributeModel VersionProperty { get; set; }
        public Dictionary<System.String, ServiceAttributeModel> Attributes { get; set; }
        public Dictionary<System.String, ParentReferencePropertyModel> ParentReferences { get; set; }
        public Dictionary<System.String, ChildReferencePropertyModel> ChildReferences { get; set; }
        public CreateOperationModel CreateOperation { get; set; }
        public ReadOperationModel ReadOperation { get; set; }
        public UpdateOperationModel UpdateOperation { get; set; }
        public DeleteOperationModel DeleteOperation { get; set; }
        public Dictionary<System.String, ReadByParentOperationModel> ReadByParentOperations { get; set; }
        public Dictionary<System.String, ReadParentOperationModel> ReadParentOperations { get; set; }
        public Dictionary<System.String, ReadChildrenOperationModel> ReadChildrenOperations { get; set; }
        public Dictionary<System.String, GetByParentOperationModel> GetByParentOperations { get; set; }

        public ServiceModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override void Hydrate( XElement definition )
        {
            base.Hydrate( definition );

            List<XElement> attributeNodes = definition.Element( "Properties" ).Elements( "Attribute" ).ToList();
            this.Attributes = new Dictionary<string, ServiceAttributeModel>( attributeNodes.Count );
            foreach( XElement attributeNode in attributeNodes )
            {
                ServiceAttributeModel property = new ServiceAttributeModel( attributeNode, this );
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

            XElement createOpNode = definition.Element( "Operations" ).Element( "Create" );
            if( createOpNode != null )
            {
                this.CreateOperation = new CreateOperationModel( createOpNode, this );
            }

            XElement readOpNode = definition.Element( "Operations" ).Element( "Read" );
            if( readOpNode != null )
            {
                this.ReadOperation = new ReadOperationModel( readOpNode, this );
            }

            XElement updateOpNode = definition.Element( "Operations" ).Element( "Update" );
            if( updateOpNode != null )
            {
                this.UpdateOperation = new UpdateOperationModel( updateOpNode, this );
            }

            XElement deleteOpNode = definition.Element( "Operations" ).Element( "Delete" );
            if( deleteOpNode != null )
            {
                this.DeleteOperation = new DeleteOperationModel( deleteOpNode, this );
            }

            List<XElement> readByParentOpNodes = definition.Element( "Operations" ).Elements( "ReadByParent" ).ToList();
            this.ReadByParentOperations = new Dictionary<string, ReadByParentOperationModel>( readByParentOpNodes.Count );
            foreach( XElement operationNode in readByParentOpNodes )
            {
                ReadByParentOperationModel operationModel = new ReadByParentOperationModel( operationNode, this );
                this.ReadByParentOperations.Add( operationModel.Name, operationModel );
            }

            List<XElement> readParentOpNodes = definition.Element( "Operations" ).Elements( "ReadParent" ).ToList();
            this.ReadParentOperations = new Dictionary<string, ReadParentOperationModel>( readParentOpNodes.Count );
            foreach( XElement operationNode in readParentOpNodes )
            {
                ReadParentOperationModel operationModel = new ReadParentOperationModel( operationNode, this );
                this.ReadParentOperations.Add( operationModel.Name, operationModel );
            }

            List<XElement> readChildrenOpNodes = definition.Element( "Operations" ).Elements( "ReadChildren" ).ToList();
            this.ReadChildrenOperations = new Dictionary<string, ReadChildrenOperationModel>( readChildrenOpNodes.Count );
            foreach( XElement operationNode in readChildrenOpNodes )
            {
                ReadChildrenOperationModel operationModel = new ReadChildrenOperationModel( operationNode, this );
                this.ReadChildrenOperations.Add( operationModel.Name, operationModel );
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
