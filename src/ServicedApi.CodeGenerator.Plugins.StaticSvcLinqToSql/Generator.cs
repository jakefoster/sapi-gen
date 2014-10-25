using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.ncore.ServicedApi.CodeGenerator;
using org.ncore.ServicedApi.CodeGenerator.Api.Model;
using org.ncore.ServicedApi.CodeGenerator.T4Templating;
using Microsoft.VisualStudio.TextTemplating;
using org.ncore.Common;
using org.ncore.ServicedApi.CodeGenerator.Api;
using org.ncore.Diagnostics;
using System.Xml.Serialization;

namespace org.ncore.ServicedApi.CodeGenerator.Plugins.StaticSvcLinqToSql
{
    // TODO: Need unit tests of some sort for this.  JF
    public class Generator : IGeneratorPlugin
    {
        #region Statics
        private static readonly string _binPath = Path.GetDirectoryName( Assembly.GetExecutingAssembly().GetName().CodeBase ).Remove( 0, 6 );
        #endregion Statics

        private CancellationToken _cancellationToken;

        private GeneratorConfiguration _configuration;

        private Settings _settings
        {
            get
            {
                return (Settings)_configuration.PluginSettings;
            }
        }

        private SelectionTree _modelSelection;
        private SelectionTree _templateSelection;

        public object GetDefaultSettings()
        {
            return new Settings();
        }

        public object PromptForSettings( object currentSettings )
        {
            return SettingsForm.PromptForSettings( currentSettings );
        }


        public SelectionTree GetTemplateSelection()
        {
            SelectionTree selectionTree = new SelectionTree()
                                         {
                                             new SelectionTreeNode( "ContextObjects", "Context Objects" )
                                                 {
                                                     new SelectionTreeNode( "ContextObjects_ApiExecutionContext",
                                                                            "API ExecutionContext" ),
                                                     new SelectionTreeNode( "ContextObjects_FakeOrmDataContext",
                                                                            "Fake ORM DataContext" ),
                                                     new SelectionTreeNode( "ContextObjects_LinqToSqlOrmDataContext",
                                                                            "Linq-to-SQL ORM DataContext" )
                                                 },
                                             new SelectionTreeNode( "DomainObjects", "Domain Objects" )
                                                 {
                                                     new SelectionTreeNode( "DomainObjects_SystemTests", "System Tests" )
                                                         {
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_SystemTests_ServiceObjects",
                                                                 "Service Objects" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_SystemTests_Factypes",
                                                                 "Factypes" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_SystemTests_ViewLists",
                                                                 "ViewLists" )
                                                         },
                                                     new SelectionTreeNode( "DomainObjects_UnitTests", "Unit Tests" )
                                                         {
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_UnitTests_ServiceObjects",
                                                                 "Service Objects" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_UnitTests_Factypes",
                                                                 "Factypes" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_UnitTests_ViewLists",
                                                                 "ViewLists" )
                                                         },
                                                     new SelectionTreeNode( "DomainObjects_Api", "API" )
                                                         {
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_Api_ServiceObjects",
                                                                 "Service Objects" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_Api_Factypes",
                                                                 "Factypes" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_Api_ViewLists",
                                                                 "ViewLists" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_Api_SiloInterfaces",
                                                                 "Silo Interfaces" )
                                                         },
                                                     new SelectionTreeNode( "DomainObjects_FakeOrm", "Fake ORM" )
                                                         {
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_FakeOrm_StoreTypes",
                                                                 "StoreTypes" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_FakeOrm_Silos",
                                                                 "Silos" ),
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_FakeOrm_Stores",
                                                                 "Stores" )
                                                         },
                                                     new SelectionTreeNode( "DomainObjects_LinqToSqlOrm",
                                                                            "Linq-To-SQL ORM" )
                                                         {
                                                             new SelectionTreeNode(
                                                                 "DomainObjects_LinqToSqlOrm_Silos",
                                                                 "Silos" )
                                                         }

                                                 },
                                         };
            
            return selectionTree;
        }

        public void Generate( GeneratorConfiguration configuration, SelectionTree templateSelection, SelectionTree modelSelection, CancellationToken cancellationToken )
        {
            Console.WriteLine( "Generating..." );
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {

                // TODO: Verify _configuration.PluginSettings is typeof(Settings).  JF
                _configuration = configuration;
                _cancellationToken = cancellationToken;
                _modelSelection = modelSelection;
                _templateSelection = templateSelection;

                if( _templateSelection.FindItem( "ContextObjects" ).IsPathChecked() )
                {
                    Console.WriteLine( "Generating ContextObjects" );
                    _generate_ContextObjects();
                }
                else
                {
                    Console.WriteLine( "Skipping ContextClass generation" );
                }

                if( _templateSelection.FindItem( "DomainObjects" ).IsPathChecked() )
                {
                    Console.WriteLine( "Generating DomainObjects" );
                    foreach( ServiceModel service in _configuration.Model.Services.Values )
                    {
                        if( _modelSelection.FindItem( service.Name ).IsPathChecked() )
                        {
                            Console.WriteLine( String.Format( "Generating {0} ServiceObject", service.Name ) );
                            _generate_DomainObjects_ServiceObject( service );
                        }
                        else
                        {
                            Console.WriteLine( String.Format( "Skipping {0} ServiceObject generation", service.Name ) );
                        }
                    }
                    foreach( FactypeModel factype in _configuration.Model.Factypes.Values )
                    {
                        if( _modelSelection.FindItem( factype.Name ).IsPathChecked() )
                        {
                            Console.WriteLine( String.Format( "Generating {0} Factype", factype.Name ) );
                            _generate_DomainObjects_Factype( factype );
                        }
                        else
                        {
                            Console.WriteLine( String.Format( "Skipping {0} Factype generation", factype.Name ) );
                        }
                    }
                    foreach( ViewListModel viewList in _configuration.Model.ViewLists.Values )
                    {
                        if( _modelSelection.FindItem( viewList.Name ).IsPathChecked() )
                        {
                            Console.WriteLine( String.Format( "Generating {0} ViewList", viewList.Name ) );
                            _generate_DomainObjects_ViewList( viewList );
                        }
                        else
                        {
                            Console.WriteLine( String.Format( "Skipping {0} ViewList generation", viewList.Name ) );
                        }
                    }
                }
                else
                {
                    Console.WriteLine( "Skipping DomainObject generation" );
                }

            }
            catch( Exception ex )
            {
                Spy.Trace( ex );
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _configuration.GenerationDuration = TimeSpan.FromMilliseconds( stopwatch.ElapsedMilliseconds ).TotalSeconds;
                //Console.WriteLine( "Completed in {0} seconds.", TimeSpan.FromMilliseconds( stopwatch.ElapsedMilliseconds ).TotalSeconds );
                //Spy.Trap( () => stopwatch.ElapsedMilliseconds );
            }
        }

        private void _generate_ContextObjects()
        {
            if( _templateSelection.FindItem( "ContextObjects_ApiExecutionContext" ).IsPathChecked() )
            {
                _generate_ContextObjects_Api_ExecutionContext();
            }

            if( _templateSelection.FindItem( "ContextObjects_FakeOrmDataContext" ).IsPathChecked() )
            {
                _generate_ContextObjects_FakeOrm_DataContext();
            }

            if( _templateSelection.FindItem( "ContextObjects_LinqToSqlOrmDataContext" ).IsPathChecked() )
            {
                _generate_ContextObjects_LinqToSqlOrm_DataContext();
            }
        }

        private void _generate_ContextObjects_Api_ExecutionContext()
        {
            Console.WriteLine( "-> Generating API ExecutionContext 'generated' class file" );

            _processTemplate(
                @"DomainLayer\ContextObjects\ExecutionContext.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", _settings.NameOfGeneratedFolder ),
                "ExecutionContext.generated",
                ".cs" );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_ContextObjects_Api_ExecutionContext_starter();
            }
        }

        private void _generate_ContextObjects_Api_ExecutionContext_starter()
        {
            Console.WriteLine( "-> Generating API ExecutionContext 'starter' class file" );

            _processTemplate(
                @"DomainLayer\ContextObjects\ExecutionContext.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", _settings.NameOfStartersFolder ),
                "ExecutionContext",
                ".cs" );
        }

        private void _generate_ContextObjects_FakeOrm_DataContext()
        {
            Console.WriteLine( "-> Generating Fake ORM DataContext 'generated' class file" );

            _processTemplate(
                @"OrmLayer\FakeOrm\ContextObjects\FakeDataContext.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", _settings.NameOfGeneratedFolder ),
                "FakeDataContext.generated",
                ".cs" );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_ContextObjects_FakeOrm_DataContext_starter();
            }
        }

        private void _generate_ContextObjects_FakeOrm_DataContext_starter()
        {
            Console.WriteLine( "-> Generating Fake ORM DataContext 'starter' class file" );

            _processTemplate(
                @"OrmLayer\FakeOrm\ContextObjects\FakeDataContext.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", _settings.NameOfStartersFolder ),
                "FakeDataContext",
                ".cs" );
        }

        private void _generate_ContextObjects_LinqToSqlOrm_DataContext()
        {
            Console.WriteLine( "-> Generating Linq-to-SQL ORM DataContext 'generated' class file" );

            _processTemplate(
                @"OrmLayer\LinqToSqlOrm\ContextObjects\OrmDataContext.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.LinqToSql", _settings.NameOfGeneratedFolder ),
                "OrmDataContext.generated",
                ".cs" );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_ContextObjects_LinqToSqlOrm_DataContext_starter();
            }
        }

        private void _generate_ContextObjects_LinqToSqlOrm_DataContext_starter()
        {
            Console.WriteLine( "-> Generating Linq-to-SQL ORM DataContext 'starter' class file" );

            _processTemplate(
                @"OrmLayer\LinqToSqlOrm\ContextObjects\OrmDataContext.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.LinqToSql", _settings.NameOfStartersFolder ),
                "OrmDataContext",
                ".cs" );
        }

        private void _generate_DomainObjects_ServiceObject( ServiceModel service )
        {
            if( _templateSelection.FindItem( "DomainObjects_SystemTests_ServiceObjects" ).IsPathChecked() )
            {
                _generate_DomainObjects_SystemTest_ServiceObject( service );
            }

            if( _templateSelection.FindItem( "DomainObjects_UnitTests_ServiceObjects" ).IsPathChecked() )
            {
                _generate_DomainObjects_UnitTest_ServiceObject( service );
            }

            if( _templateSelection.FindItem( "DomainObjects_Api_ServiceObjects" ).IsPathChecked() )
            {
                _generate_DomainObjects_Api_ServiceObject( service );
            }

            if( _templateSelection.FindItem( "DomainObjects_Api_SiloInterfaces" ).IsPathChecked() )
            {
                _generate_DomainObjects_Api_ServiceObject_SiloInterface( service );
            }
            
            if( _templateSelection.FindItem( "DomainObjects_FakeOrm_StoreTypes" ).IsPathChecked() )
            {
                _generate_DomainObjects_FakeOrm_ServiceObject_StoreType( service );
            }
            if( _templateSelection.FindItem( "DomainObjects_FakeOrm_Silos" ).IsPathChecked() )
            {
                _generate_DomainObjects_FakeOrm_ServiceObjectSilo( service );
            }

            if( _templateSelection.FindItem( "DomainObjects_FakeOrm_Stores" ).IsPathChecked() )
            {
                _generate_DomainObjects_FakeOrm_ServiceObjectStore( service );
            }

            if( _templateSelection.FindItem( "DomainObjects_LinqToSqlOrm_Silos" ).IsPathChecked() )
            {
                _generate_DomainObjects_LinqToSqlOrm_ServiceObjectSilo( service );
            }
        }

        private void _generate_DomainObjects_SystemTest_ServiceObject( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ServiceObject system tests 'starter' class file",
                                            service.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\SystemTests\ServiceObjects\Tests.starter.cs.t4",
                _configuration.BuildOutputPath( "_systemtests.{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                service.Name + "Tests",
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_UnitTest_ServiceObject( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ServiceObject unit tests 'starter' class file",
                                            service.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\UnitTests\ServiceObjects\Tests.starter.cs.t4",
                _configuration.BuildOutputPath( "_unittests.{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                service.Name + "Tests",
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_Api_ServiceObject( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ServiceObject 'generated' class file", service.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ServiceObjects\ServiceObject.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Services\" + _settings.NameOfGeneratedFolder ),
                service.Name + ".generated",
                ".cs",
                service.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_Api_ServiceObject_starter( service );
            }
        }

        private void _generate_DomainObjects_Api_ServiceObject_starter( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ServiceObject 'starter' class file", service.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ServiceObjects\ServiceObject.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                service.Name,
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_Api_ServiceObject_SiloInterface( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} IServiceObjectSilo 'generated' class file", service.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ServiceObjects\ISilo.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Silos\" + _settings.NameOfGeneratedFolder ),
                "I" + service.Name + "Silo.generated",
                ".cs",
                service.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_Api_ServiceObject_SiloInterface_starter( service );
            }
        }

        private void _generate_DomainObjects_Api_ServiceObject_SiloInterface_starter( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} IServiceObjectSilo 'starter' class file", service.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ServiceObjects\ISilo.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Silos\" + _settings.NameOfStartersFolder ),
                "I" + service.Name + "Silo",
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_FakeOrm_ServiceObject_StoreType( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake StoreType 'generated' class file", service.Name ) );

                _processTemplate(
                    @"OrmLayer\FakeOrm\StoreTypes\ServiceObject.generated.cs.t4",
                    _configuration.BuildOutputPath( "{?}.Orm.Fake", @"StoreTypes\" + _settings.NameOfGeneratedFolder ),
                    service.Name + ".generated",
                    ".cs",
                    service.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_FakeOrm_ServiceObject_StoreType_starter( service );
            }
        }

        private void _generate_DomainObjects_FakeOrm_ServiceObject_StoreType_starter( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake StoreType 'starter' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\StoreTypes\ServiceObject.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"StoreTypes\" + _settings.NameOfStartersFolder ),
                service.Name,
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_FakeOrm_ServiceObjectSilo( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake ServiceObjectSilo 'generated' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Silos\ServiceObjectSilo.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Silos\" + _settings.NameOfGeneratedFolder ),
                service.Name + "Silo.generated",
                ".cs",
                service.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_FakeOrm_ServiceObjectSilo_starter( service );
            }
        }

        private void _generate_DomainObjects_FakeOrm_ServiceObjectSilo_starter( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake ServiceObjectSilo 'starter' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Silos\ServiceObjectSilo.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Silos\" + _settings.NameOfStartersFolder ),
                service.Name + "Silo",
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_FakeOrm_ServiceObjectStore( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake Store 'generated' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Stores\ServiceObject.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Stores\" + _settings.NameOfGeneratedFolder ),
                service.Name + "Store.generated",
                ".cs",
                service.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_FakeOrm_ServiceObjectStore_starter( service );
            }
        }

        private void _generate_DomainObjects_FakeOrm_ServiceObjectStore_starter( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake Store 'starter' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Stores\ServiceObject.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Stores\" + _settings.NameOfStartersFolder ),
                service.Name + "Store",
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_LinqToSqlOrm_ServiceObjectSilo( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} LinqToSql ServiceObjectSilo 'generated' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\LinqToSqlOrm\Silos\ServiceObjectSilo.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.LinqToSql", @"Silos\" + _settings.NameOfGeneratedFolder ),
                service.Name + "Silo.generated",
                ".cs",
                service.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_LinqToSqlOrm_ServiceObject_ServiceObjectSilo_starter( service );
            }
        }

        private void _generate_DomainObjects_LinqToSqlOrm_ServiceObject_ServiceObjectSilo_starter( ServiceModel service )
        {
            Console.WriteLine( String.Format( "-> Generating {0} LinqToSql ServiceObjectSilo 'starter' class file", service.Name ) );

            _processTemplate(
                @"OrmLayer\LinqToSqlOrm\Silos\ServiceObjectSilo.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.LinqToSql", @"Silos\" + _settings.NameOfStartersFolder ),
                service.Name + "Silo",
                ".cs",
                service.Name );
        }

        private void _generate_DomainObjects_ViewList( ViewListModel viewList )
        {
            if( _templateSelection.FindItem( "DomainObjects_SystemTests_ViewLists" ).IsPathChecked() )
            {
                _generate_DomainObjects_SystemTest_ViewList( viewList );
            }

            if( _templateSelection.FindItem( "DomainObjects_UnitTests_ViewLists" ).IsPathChecked() )
            {
                _generate_DomainObjects_UnitTest_ViewList( viewList );
            }

            if( _templateSelection.FindItem( "DomainObjects_Api_ViewLists" ).IsPathChecked() )
            {
                _generate_DomainObjects_Api_ViewList( viewList );
            }
            
            if( _templateSelection.FindItem( "DomainObjects_Api_SiloInterfaces" ).IsPathChecked() )
            {
                _generate_DomainObjects_Api_ViewList_SiloInterface( viewList );
            }

            if( _templateSelection.FindItem( "DomainObjects_FakeOrm_Silos" ).IsPathChecked() )
            {
                _generate_DomainObjects_FakeOrm_ViewListSilo( viewList );
            }

            if( _templateSelection.FindItem( "DomainObjects_LinqToSqlOrm_Silos" ).IsPathChecked() )
            {
                _generate_DomainObjects_LinqToSqlOrm_ViewListSilo( viewList );
            }
        }

        private void _generate_DomainObjects_SystemTest_ViewList( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ViewList system tests 'starter' class file",
                                            viewList.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\SystemTests\ViewLists\Tests.starter.cs.t4",
                _configuration.BuildOutputPath( "_systemtests.{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                viewList.Name + "Tests",
                ".cs",
                viewList.Name );
        }

        private void _generate_DomainObjects_UnitTest_ViewList( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ViewList unit tests 'starter' class file",
                                            viewList.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\UnitTests\ViewLists\Tests.starter.cs.t4",
                _configuration.BuildOutputPath( "_unittests.{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                viewList.Name + "Tests",
                ".cs",
                viewList.Name );
        }

        private void _generate_DomainObjects_Api_ViewList( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ViewList 'generated' class file", viewList.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ViewLists\ViewList.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Services\" + _settings.NameOfGeneratedFolder ),
                viewList.Name + ".generated",
                ".cs",
                viewList.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_Api_ViewList_starter( viewList );
            }
        }

        private void _generate_DomainObjects_Api_ViewList_starter( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} ViewList 'starter' class file", viewList.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ViewLists\ViewList.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                viewList.Name,
                ".cs",
                viewList.Name );
        }

        private void _generate_DomainObjects_Api_ViewList_SiloInterface( ViewListModel viewList )
        {
            // TODO: Determine if we actually need this.  I don't think we do.  I don't think 
            //  there would be any code in it.  I guess we can add it later if needed.  JF
            /*
            Console.WriteLine( String.Format( "-> Generating {0} IViewListSilo 'generated' class file", viewList.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ViewLists\ISilo.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Silos\" + _settings.NameOfGeneratedFolder ),
                "I" + viewList.Name + "Silo.generated",
                ".cs",
                viewList.Name );
            */

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_Api_ViewList_SiloInterface_starter( viewList );
            }
        }

        private void _generate_DomainObjects_Api_ViewList_SiloInterface_starter( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} IViewListSilo 'starter' class file", viewList.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\ViewLists\ISilo.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Silos\" + _settings.NameOfStartersFolder ),
                "I" + viewList.Name + "Silo",
                ".cs",
                viewList.Name );
        }

        private void _generate_DomainObjects_FakeOrm_ViewListSilo( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake ViewListSilo 'generated' class file", viewList.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Silos\ViewListSilo.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Silos\" + _settings.NameOfGeneratedFolder ),
                viewList.Name + "Silo.generated",
                ".cs",
                viewList.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_FakeOrm_ViewListSilo_starter( viewList );
            }
        }

        private void _generate_DomainObjects_FakeOrm_ViewListSilo_starter( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake ViewListSilo 'starter' class file", viewList.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Silos\ViewListSilo.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Silos\" + _settings.NameOfStartersFolder ),
                viewList.Name + "Silo",
                ".cs",
                viewList.Name );
        }

        private void _generate_DomainObjects_LinqToSqlOrm_ViewListSilo( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} LinqToSql ViewListSilo 'generated' class file", viewList.Name ) );

            _processTemplate(
                @"OrmLayer\LinqToSqlOrm\Silos\ViewListSilo.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.LinqToSql", @"Silos\" + _settings.NameOfGeneratedFolder ),
                viewList.Name + "Silo.generated",
                ".cs",
                viewList.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_LinqToSqlOrm_ViewListSilo_starter( viewList );
            }
        }

        private void _generate_DomainObjects_LinqToSqlOrm_ViewListSilo_starter( ViewListModel viewList )
        {
            Console.WriteLine( String.Format( "-> Generating {0} LinqToSql ViewListSilo 'starter' class file", viewList.Name ) );

            _processTemplate(
                @"OrmLayer\LinqToSqlOrm\Silos\ViewListSilo.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.LinqToSql", @"Silos\" + _settings.NameOfStartersFolder ),
                viewList.Name + "Silo",
                ".cs",
                viewList.Name );
        }

        private void _generate_DomainObjects_Factype( FactypeModel factype )
        {
            if( _templateSelection.FindItem( "DomainObjects_SystemTests_Factypes" ).IsPathChecked() )
            {
                _generate_DomainObjects_SystemTest_Factype( factype );
            }

            if( _templateSelection.FindItem( "DomainObjects_UnitTests_Factypes" ).IsPathChecked() )
            {
                _generate_DomainObjects_UnitTest_Factype( factype );
            }

            if( _templateSelection.FindItem( "DomainObjects_Api_Factypes" ).IsPathChecked() )
            {
                _generate_DomainObjects_Api_Factype( factype );
            }

            if( _templateSelection.FindItem( "DomainObjects_FakeOrm_StoreTypes" ).IsPathChecked() )
            {
                _generate_DomainObjects_FakeOrm_Factype_StoreType( factype );
            }

            if( _templateSelection.FindItem( "DomainObjects_FakeOrm_Stores" ).IsPathChecked() )
            {
                _generate_DomainObjects_FakeOrm_Factype_Store( factype );
            }
        }

        private void _generate_DomainObjects_SystemTest_Factype( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Factype system tests 'starter' class file", factype.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\SystemTests\Factypes\Tests.starter.cs.t4",
                _configuration.BuildOutputPath( "_systemtests.{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                factype.Name + "Tests",
                ".cs",
                factype.Name );
        }

        private void _generate_DomainObjects_UnitTest_Factype( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Factype unit tests 'starter' class file", factype.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\UnitTests\Factypes\Tests.starter.cs.t4",
                _configuration.BuildOutputPath( "_unittests.{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                factype.Name + "Tests",
                ".cs",
                factype.Name );
        }

        private void _generate_DomainObjects_Api_Factype( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Factype 'generated' class file", factype.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\Factypes\Factype.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Services\" + _settings.NameOfGeneratedFolder ),
                factype.Name + ".generated",
                ".cs",
                factype.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_Api_Factype_starter( factype );
            }
        }

        private void _generate_DomainObjects_Api_Factype_starter( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Factype 'starter' class file", factype.Name ) );

            _processTemplate(
                @"DomainLayer\DomainObjects\Api\Factypes\Factype.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Api", @"Services\" + _settings.NameOfStartersFolder ),
                factype.Name,
                ".cs",
                factype.Name );
        }

        private void _generate_DomainObjects_FakeOrm_Factype_StoreType( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake StoreType 'generated' class file", factype.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\StoreTypes\Factype.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"StoreTypes\" + _settings.NameOfGeneratedFolder ),
                factype.Name + ".generated",
                ".cs",
                factype.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_FakeOrm_Factype_StoreType_starter( factype );
            }
        }

        private void _generate_DomainObjects_FakeOrm_Factype_StoreType_starter( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake StoreType 'starter' class file", factype.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\StoreTypes\Factype.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"StoreTypes\" + _settings.NameOfStartersFolder ),
                factype.Name,
                ".cs",
                factype.Name );
        }

        private void _generate_DomainObjects_FakeOrm_Factype_Store( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake Store 'generated' class file", factype.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Stores\Factype.generated.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Stores\" + _settings.NameOfGeneratedFolder ),
                factype.Name + "Store.generated",
                ".cs",
                factype.Name );

            if( !_settings.DoNotGenerateStarters )
            {
                _generate_DomainObjects_FakeOrm_Factype_Store_starter( factype );
            }
        }

        private void _generate_DomainObjects_FakeOrm_Factype_Store_starter( FactypeModel factype )
        {
            Console.WriteLine( String.Format( "-> Generating {0} Fake Store 'starter' class file", factype.Name ) );

            _processTemplate(
                @"OrmLayer\FakeOrm\Stores\Factype.starter.cs.t4",
                _configuration.BuildOutputPath( "{?}.Orm.Fake", @"Stores\" + _settings.NameOfStartersFolder ),
                factype.Name + "Store",
                ".cs",
                factype.Name );
        }

        private string _buildTemplateTag()
        {
            string templateLanguage = "C#";
            if( _settings.TargetFrameworkVersion == Settings.TargetFrameworkVersionEnum.dotNet_3_5 )
            {
                templateLanguage = "C#v3.5";
            }

            string templateDebug = "false";
            if( _configuration.EnableTemplateDebugging )
            {
                templateDebug = "true";
            }

            return String.Format( "<#@ template hostspecific=\"true\" language=\"{0}\" debug=\"{1}\" #>", templateLanguage, templateDebug );
        }

        private void _processTemplate( string templateFileName, string outputPath, string outputFileNameRoot, string outputFileExtension, string targetObjectName = null )
        {
            if( _cancellationToken.IsCancellationRequested )
            {
                Console.WriteLine( "Cancellation request receieved." );
                throw new TaskCanceledException();
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add( "TargetObjectName", targetObjectName );

            TemplatingHost host = new TemplatingHost( outputFileExtension, Encoding.Unicode );
            host.DefaultParameters = parameters;
            host.Model = _configuration.Model;
            host.StandardAssemblyReferences.Add( "org.ncore.ServicedApi.CodeGenerator.Api.dll" );
            host.StandardAssemblyReferences.Add( "org.ncore.ServicedApi.CodeGenerator.T4Templating.dll" );
            host.StandardAssemblyReferences.Add( "org.ncore.dll" );
            string templateFilePath = Path.Combine( _binPath, @"Templates\" );
            string includeFilePath = Path.Combine( templateFilePath, @"_includes\" );
            host.IncludeFileSearchPaths.Add( includeFilePath );
            // NOTE: Could also specify the /_includes/ directory in the following way
            //  though this is really less elegant since it means we have to know about
            //  where our templates are located within the bin directory.  Of course
            //  this is required for other things to work, but the above is just neater.  JF
            //host.IncludeFileSearchPaths.Add( @"plugins\ServicedApi.CodeGenerator.Plugins.StaticSvcLinqToSql\Templates\_includes\" );
            host.TemplateFile = Path.Combine( templateFilePath, templateFileName );

            // TODO: Maybe someday we want these as embedded resources but right now they're changing too much
            //  so instead we'll just copy the templates to the bin directory.  Hmm, prevents template debugging 
            //  if they're embedded resources.  Big reason to NOT do this.  JF
            //string input = EmbeddedResource.LoadAsString( "Resources.Templates." + templateFileName );
            string input = _buildTemplateTag() + File.ReadAllText( host.TemplateFile );

            Engine engine = new Engine();
            string output = engine.ProcessTemplate( input, host );

            string outputFileName = Path.Combine( outputPath, outputFileNameRoot + host.FileExtension );

            DirectoryInfo directory = new DirectoryInfo( outputPath );
            if( !directory.Exists )
            {
                directory.Create();
            }

            File.WriteAllText( outputFileName, output, host.FileEncoding );

            if( host.Errors.HasErrors )
            {
                _configuration.FailedFileGenerationCount++;
                _configuration.HadErrors = true;
                foreach( CompilerError error in host.Errors )
                {
                    try
                    {
                        _configuration.CompilerErrors.Add( error );

                    }
                    catch( Exception ex )
                    {
                        Console.WriteLine( "-> EXCEPTION: {0}", ex.ToString() );
                    }
                }
                _printErrors( host.Errors );

                if( _configuration.StopOnGenerationError )
                {
                    throw new GenerationErrorException();
                }
            }
            else
            {
                _configuration.FileGenerationCount++;
            }
        }

        private static void _printErrors( CompilerErrorCollection errors )
        {
            Spy.Trace( EventClass.Error, "The following errors occurred while processing." );
            foreach( CompilerError error in errors )
            {
                Console.WriteLine( "-> ERROR: {0}", error.ToString() );
            }
        }
    }
}
