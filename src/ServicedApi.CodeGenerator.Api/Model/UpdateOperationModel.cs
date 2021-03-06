﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    [Serializable]
    public class UpdateOperationModel : OperationModel
    {
        public UpdateOperationModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override string GetDefaultName()
        {
            return "Update";
        }
    }
}
