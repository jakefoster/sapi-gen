using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.ncore.Common;

namespace org.ncore.ServicedApi.CodeGenerator.Api.Model
{
    [Serializable]
    public class VisibilityFactype : Factype
    {
        public static readonly VisibilityFactype Private = new VisibilityFactype( "private", 0 );
        public static readonly VisibilityFactype Protected = new VisibilityFactype( "protected", 1 );
        public static readonly VisibilityFactype Internal = new VisibilityFactype( "internal", 2 );
        public static readonly VisibilityFactype Public = new VisibilityFactype( "public", 3 );

        private VisibilityFactype( string keyword, System.Int32 precedence )
            : base()
        {
            this.Keyword = keyword;
            this.Precedence = precedence;
        }

        // NOTE: Optional additions to the class to add functionality.  These are all just wrapper methods.  JF
        public static implicit operator VisibilityFactype( string moniker )
        {
            return VisibilityFactype.Parse<VisibilityFactype>( moniker );
        }

        public static implicit operator VisibilityFactype( Int32 precedence )
        {
            return VisibilityFactype.Parse( precedence );
        }

        public static List<VisibilityFactype> GetAll()
        {
            return VisibilityFactype.GetAll<VisibilityFactype>();
        }

        public static string[] GetMonikers()
        {
            return VisibilityFactype.GetMonikers<VisibilityFactype>();
        }

        public static string[] GetKeywords()
        {
            List<VisibilityFactype> list = VisibilityFactype.GetAll();
            string[] keywords = new string[ list.Count ];
            for( int i = 0; i < keywords.Length; ++i )
            {
                keywords[ i ] = list[ i ].Keyword;
            }
            return keywords;
        }

        public static VisibilityFactype Parse( Int32 precedence )
        {
            List<VisibilityFactype> list = VisibilityFactype.GetAll();
            foreach( VisibilityFactype item in list )
            {
                if( item.Precedence.Equals( precedence ) )
                {
                    return (VisibilityFactype)item;
                }
            }

            throw new ArgumentException(
                string.Format( "Value {0} is not a member of {1}", precedence, typeof( VisibilityFactype ).ToString() )
                );
        }

        public string Keyword { get; protected set; }
        public System.Int32 Precedence { get; protected set; }
    }
}
