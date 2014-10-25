using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TextTemplating;
using org.ncore.Common;
using org.ncore.Extensions;

namespace org.ncore.ServicedApi.CodeGenerator.Api
{
    public class TextFormatter
    {
        // TODO: To nc?
        public static string FirstToUpper( string value )
        {
            if( value == null )
            {
                throw new ArgumentException( "Argument cannot be null.", "value" );
            }

            if( value.Equals( string.Empty ) )
            {
                return string.Empty;
            }

            if( value.Length == 1 )
            {
                return value.ToUpper();
            }

            return value.Substring( 0, 1 ).ToUpper() + value.Substring( 1 );
        }

        // TODO: To nc?
        public static string FirstToLower( string value )
        {
            if( value == null )
            {
                throw new ArgumentException( "Argument cannot be null.", "value" );
            }

            if( value.Equals( string.Empty ) )
            {
                return string.Empty;
            }

            if( value.Length == 1 )
            {
                return value.ToLower();
            }

            return value.Substring( 0, 1 ).ToLower() + value.Substring( 1 );
        }

        // NOTE: Shamelessly borrowed from: http://mattgrande.wordpress.com/2009/10/28/pluralization-helper-for-c/
        // TODO: Re-write & expand.  Honestly, this code is missing a LOT of pluralizations.  JF
        private static readonly IList<string> Unpluralizables = new List<string> { "equipment", "information", "rice", "money", "species", "series", "fish", "sheep", "deer" };
        private static readonly IDictionary<string, string> Pluralizations = new Dictionary<string, string>
        {
            // Start with the rarest cases, and move to the most common
            { "person", "people" },
            { "ox", "oxen" },
            { "child", "children" },
            { "foot", "feet" },
            { "tooth", "teeth" },
            { "goose", "geese" },
            { "criterion", "criteria" }, // NOTE: JF Added
            // And now the more standard rules.
            { "(.*)fe$", "$1ves" },         // ie, wolf, wife // NOTE: JF fixed - expression was originally (.*)fe? which was incorrectly matching stuff like "TaskDefinition"
            { "(.*)man$", "$1men" },
            { "(.+[aeiou]y)$", "$1s" },
            { "(.+[^aeiou])y$", "$1ies" },
            { "(.+z)$", "$1zes" },
            { "([m|l])ouse$", "$1ice" },
            { "(.+)(e|i)x$", @"$1ices"},    // ie, Matrix, Index
            { "(octop|vir)us$", "$1i"},
            { "(.+(s|x|sh|ch))$", @"$1es"},
            { "(.+)", @"$1s" }
        };
        
        // TODO: Would be nice to have an extension method on String as well.  E.g. "Foo".Pluralize().  JF
        public static string Pluralize( string singular )
        {
            return Pluralize( 2, singular );
        }

        public static string Pluralize( int count, string singular )
        {
            if( count == 1 )
                return singular;

            if( Unpluralizables.Contains( singular.ToLowerInvariant() ) )
                return singular;

            var plural = "";

            foreach( var pluralization in Pluralizations )
            {
                if( Regex.IsMatch( singular, pluralization.Key ) )
                {
                    plural = Regex.Replace( singular, pluralization.Key, pluralization.Value );
                    break;
                }
            }

            return plural;
        }

        // NOTE: Shamelessly borrowed from: http://lotsacode.wordpress.com/2010/03/05/singularization-pluralization-in-c/
        // TODO: Re-write
        private static readonly IDictionary<string, string> Singularizations = new Dictionary<string, string>
        {
            // Start with the rarest cases, and move to the most common
            {"people", "person"},
            {"oxen", "ox"},
            {"children", "child"},
            {"feet", "foot"},
            {"teeth", "tooth"},
            {"geese", "goose"},
            { "criteria", "criterion" }, // NOTE: JF Added
            // And now the more standard rules.
            {"(.*)ives$", "$1ife"}, // wives -> wife, lives -> life // NOTE: JF fixed, was (.*)ives? which is wrong
            {"(.*)ves$", "$1f"}, // wolves -> wolf // NOTE: JF fixed, was (.*)ves? which is wrong
            {"(.*)men$", "$1man"},
            {"(.+[aeiou])ys$", "$1y"},
            {"(.+[^aeiou])ies$", "$1y"},
            {"(.+)zes$", "$1"},
            {"([m|l])ice$", "$1ouse"},
            {"matrices", @"matrix"},
            {"indices", @"index"},
            {"(.*)ices", @"$1ex"}, // Matrix, Index
            {"(octop|vir)i$", "$1us"},
            {"(.+(s|x|sh|ch))es$", @"$1"},
            {"(.+)s", @"$1"}
        };

        public static string Singularize( string word )
        {
            if( Unpluralizables.Contains( word.ToLowerInvariant() ) )
            {
                return word;
            }

            foreach( var singularization in Singularizations )
            {
                if( Regex.IsMatch( word, singularization.Key ) )
                {
                    return Regex.Replace( word, singularization.Key, singularization.Value );
                }
            }

            return word;
        }

        public static bool IsPlural( string word )
        {
            if( Unpluralizables.Contains( word.ToLowerInvariant() ) )
            {
                return true;
            }

            foreach( var singularization in Singularizations )
            {
                if( Regex.IsMatch( word, singularization.Key ) )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
