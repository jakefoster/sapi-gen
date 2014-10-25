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
    public class CreateOperationModel : OperationModel
    {
        public CreateOperationModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override string GetDefaultName()
        {
            return "Create";
        }
    }
}
