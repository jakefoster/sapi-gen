using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public class GenerationErrorException : ApplicationException
    {
        private const string MESSAGE = "A generation error occurred.";
        
        public GenerationErrorException()
            : base( MESSAGE )
        {
        }

        public GenerationErrorException( Exception innerException )
            : base( MESSAGE, innerException )
        {
        }
    }
}
