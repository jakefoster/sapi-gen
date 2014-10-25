using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using org.ncore.Extensions;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public class GenerationResults
    {
        // TODO: Should be a factype so we have user-friendly descriptors.  JF
        public enum FinalStatusEnum
        {
            FailedWithGenerationErrors,
            StoppedOnGenerationError,
            FailedWithException,
            FailedWithExceptionAndGenerationErrors,
            CancelledByUser,
            CancelledByUserWithGenerationErrors,
            CompletedSuccessfully
        }

        public Exception GenerationException { get; private set; }
        public GeneratorConfiguration GeneratorConfiguration { get; private set; }

        public bool HadGenerationErrors { get; private set; }
        public bool StoppedOnGenerationError { get; private set; }
        public bool HadException { get; private set; }
        public bool CancelledByUser { get; private set; }
        public FinalStatusEnum FinalStatus { get; private set; }

        public GenerationResults( GeneratorConfiguration generatorConfiguration, Exception generatorException )
        {
            this.GenerationException = generatorException;
            this.GeneratorConfiguration = generatorConfiguration;

            if( generatorException != null )
            {
                if( generatorException is AggregateException && ( generatorException.InnerException is TaskCanceledException || generatorException.InnerException is GenerationErrorException ) )
                {
                    if( generatorException.InnerException is TaskCanceledException )
                    {
                        CancelledByUser = true;
                    }
                    else if( generatorException.InnerException is GenerationErrorException )
                    {
                        StoppedOnGenerationError = true;
                    }
                }
                else
                {
                    HadException = true;
                }
            }

            HadGenerationErrors = generatorConfiguration.HadErrors;

            FinalStatus = FinalStatusEnum.CompletedSuccessfully;
            if( HadException )
            {
                if( HadGenerationErrors)
                {
                    FinalStatus = FinalStatusEnum.FailedWithExceptionAndGenerationErrors;
                }
                else
                {
                    FinalStatus = FinalStatusEnum.FailedWithException;                    
                }
            }
            else if( StoppedOnGenerationError )
            {
                FinalStatus = FinalStatusEnum.StoppedOnGenerationError;
            }
            else if( CancelledByUser )
            {
                if( HadGenerationErrors )
                {
                    FinalStatus = FinalStatusEnum.CancelledByUserWithGenerationErrors;
                }
                else
                {
                    FinalStatus = FinalStatusEnum.CancelledByUser;
                }
            }
            else if( HadGenerationErrors )
            {
                FinalStatus = FinalStatusEnum.FailedWithGenerationErrors;
            }
        }

        public string BuildSummaryMessage( string newline )
        {
            if( newline == null )
            {
                newline = Environment.NewLine;
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat( "____GENERATION RESULTS____{0}", newline );
            builder.AppendFormat( "{0}", newline );
            builder.AppendFormat( "SUMMARY:{0}", newline );
            builder.AppendFormat( "   Final status: {0}{1}", this.FinalStatus.ToString(), newline );
            builder.AppendFormat( "   Duration (seconds): {0}{1}", this.GeneratorConfiguration.GenerationDuration.ToString(), newline );
            builder.AppendFormat( "   Files generated: {0}{1}", this.GeneratorConfiguration.FileGenerationCount.ToString(), newline );
            builder.AppendFormat( "   Files not generated due to errors: {0}{1}", this.GeneratorConfiguration.FailedFileGenerationCount.ToString(), newline );

            // TODO: Some sort of recursive thingy to show the full exception tree.  Need to better
            //  understand the AggregateException structure though.  JF
            if( HadException )
            {
                Exception targetException = this.GenerationException;
                if( targetException is AggregateException )
                {
                    // TODO: Cheesy.  Let's fix this.  JF
                    targetException = targetException.InnerException;
                }

                builder.AppendFormat( "{0}", newline );
                builder.AppendFormat( "EXCEPTION: {0}{1}", targetException.GetType().FullName, newline );
                builder.AppendFormat( "   Type: {0}{1}", targetException.GetType().FullName, newline );
                builder.AppendFormat( "   Message: {0}{1}", targetException.Message, newline );
                builder.AppendFormat( "   Source: {0}{1}", targetException.Source, newline );
                builder.AppendFormat( "   TargetSite: {0}{1}", targetException.TargetSite, newline );
                if( targetException.StackTrace != null )
                {
                    builder.AppendFormat( "   StackTrace: {0}{1}", targetException.StackTrace.Replace( Environment.NewLine, " " ), newline );
                }
                if( targetException.InnerException != null )
                {
                    builder.AppendFormat( "   InnerException: {0}{1}", targetException.InnerException.GetType().FullName, newline );
                }
            }

            if( HadGenerationErrors )
            {
                builder.AppendFormat( "{0}", newline );
                builder.AppendFormat( "GENERATION ERRORS:{0}", newline );
                foreach( CompilerError error in GeneratorConfiguration.CompilerErrors )
                {
                    builder.AppendFormat( "   ERROR: {0}{1}", error.ErrorText, newline );
                    builder.AppendFormat( "      ErrorText: {0}{1}", error.ErrorText, newline ); 
                    builder.AppendFormat( "      FileName: {0}{1}", error.FileName, newline );
                    builder.AppendFormat( "      ErrorNumber: {0}{1}", error.ErrorNumber, newline );
                    builder.AppendFormat( "      Line: {0}{1}", error.Line, newline );
                    builder.AppendFormat( "      Column: {0}{1}", error.Column, newline );
                    builder.AppendFormat( "      IsWarning: {0}{1}", error.IsWarning, newline );
                }
            }

            return builder.ToString();
        }
    }
}
