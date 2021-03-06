﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    [Serializable]
    public class DeleteOperationModel : OperationModel
    {
        public DeleteOperationModel( XElement definition, IParent parent )
            : base( definition, parent )
        {
        }

        public override string GetDefaultName()
        {
            return "Delete";
        }
    }
}
