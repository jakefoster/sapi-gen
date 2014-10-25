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
    [DebuggerDisplay( "Namespace = {this.Namespace}" )]
    public class ServicedApiModel : IParent
    {
        [NonSerialized]
        // TODO: Convert to a string so it can be serialized?  JF
        private XDocument _schema;

        public System.String Namespace { get; set; }
        public Dictionary<System.String, ServiceModel> Services { get; set; }
        public Dictionary<System.String, FactypeModel> Factypes { get; set; }
        public Dictionary<System.String, ViewListModel> ViewLists { get; set; }
        public XDocument Schema
        {
            get
            {
                return _schema;
            }
        }

        public ServicedApiModel( XDocument schema )
        {
            _schema = schema;
            this.Hydrate( _schema );
        }

        public ServicedApiModel( System.String definitionXml )
        {
            XDocument definition = XDocument.Parse( definitionXml );
            this.Hydrate( definition );
        }

        public void Hydrate( XDocument definition )
        {
            this.Namespace = definition.Root.Attribute( "Namespace" ).Value;

            List<XElement> serviceNodes = definition.Root.Elements( "Services" ).Elements( "Service" ).ToList();
            this.Services = new Dictionary<string, ServiceModel>( serviceNodes.Count );
            foreach( XElement serviceNode in serviceNodes )
            {
                ServiceModel service = new ServiceModel( serviceNode, this );
                this.Services.Add( service.Name, service );
            }

            List<XElement> factypeNodes = definition.Root.Elements( "Factypes" ).Elements( "Factype" ).ToList();
            this.Factypes = new Dictionary<string, FactypeModel>( factypeNodes.Count );
            foreach( XElement factypeNode in factypeNodes )
            {
                FactypeModel factype = new FactypeModel( factypeNode, this );
                this.Factypes.Add( factype.Name, factype );
            }

            List<XElement> viewListNodes = definition.Root.Elements( "ViewLists" ).Elements( "ViewList" ).ToList();
            this.ViewLists = new Dictionary<string, ViewListModel>( viewListNodes.Count );
            foreach( XElement viewListNode in viewListNodes )
            {
                ViewListModel viewList = new ViewListModel( viewListNode, this );
                this.ViewLists.Add( viewList.Name, viewList );
            }

            // TODO: Pocos
            //List<XElement> factypeNodes = definition.Root.Elements( "Factypes" ).Elements( "Factype" ).ToList();
            //this.Factypes = new Dictionary<string, FactypeModel>( factypeNodes.Count );
            //foreach( XElement factypeNode in factypeNodes )
            //{
            //    FactypeModel factype = new FactypeModel( factypeNode, this );
            //    this.Factypes.Add( factype.Name, factype );
            //}
        }
    }
}
