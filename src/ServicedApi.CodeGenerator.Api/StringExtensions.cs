using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.ncore.ServicedApi.CodeGenerator.Api
{
    public static class StringExtensions
    {
        public static string ToPublicName( this String instance )
        {
            return TextFormatter.FirstToUpper( instance );
        }

        public static string ToProtectedName( this String instance )
        {
            return "_" + TextFormatter.FirstToUpper( instance );
        }

        public static string ToInternalName( this String instance )
        {
            return TextFormatter.FirstToLower( instance );
        }

        public static string ToPrivateName( this String instance )
        {
            return "_" + TextFormatter.FirstToLower( instance );
        }
    }
}
