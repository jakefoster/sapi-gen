using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace org.ncore.ServicedApi.CodeGenerator
{
    public interface IGeneratorPlugin
    {
        object GetDefaultSettings();
        object PromptForSettings( object currentSettings );
        void Generate( GeneratorConfiguration configuration, SelectionTree templateSelection,
                              SelectionTree modelSelection, CancellationToken cancellationToken );
        SelectionTree GetTemplateSelection();
    }
}