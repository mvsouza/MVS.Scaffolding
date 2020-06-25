using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using MVS.Scaffolding.Models;

namespace MVS.Scaffolding
{
    public abstract class CustomCodeGenerator : ICodeGenerator
    {
        private string _bundleName;
        private IProjectContext _projectContext;
        private IApplicationInfo _applicationInfo;
        private ICodeGeneratorActionsService _codeGeneratorActionsService;
        private IServiceProvider _serviceProvider;
        private ILogger _logger;

        public CustomCodeGenerator(
            IProjectContext projectContext,
            IApplicationInfo applicationInfo,
            ICodeGeneratorActionsService codeGeneratorActionsService,
            IServiceProvider serviceProvider,
            ILogger logger)
        {
            _projectContext = projectContext ?? throw new ArgumentNullException(nameof(projectContext));
            _applicationInfo = applicationInfo ?? throw new ArgumentNullException(nameof(applicationInfo));
            _codeGeneratorActionsService = codeGeneratorActionsService ?? throw new ArgumentNullException(nameof(codeGeneratorActionsService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task GenerateCode(BaseModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (string.IsNullOrEmpty(model.ClassName))
                throw new ArgumentException("Please specify the name of the command using --bundleName");
            
            var files = GetFiles(model.ClassName);
            foreach (var code in files)
                if (File.Exists(code.DestinationPath) && !model.Force)
                    throw new InvalidOperationException($"File already exists '{code.DestinationPath}' use -f to force over write.");
            
            model.Namespace = _applicationInfo.ApplicationName.ReplaceLast(".Infrastructure", "");
            foreach (var code in files)
            {
                await _codeGeneratorActionsService.AddFileFromTemplateAsync(code.DestinationPath, code.File, TemplateFolders, model);
                _logger.LogMessage($"Added: {code.DestinationPath.Substring(code.DestinationPath.LastIndexOf('/'))}");
            }
        }

        public abstract IEnumerable<FileBoilerPlaitModel> GetFiles(string bundleName);
        public abstract string[] GetBaseTemplateFolders();
        public string GeneratedFileExtension => ".cs";
        public string InfrastructurePath => _applicationInfo.ApplicationBasePath;
        public string ApplicationPath => InfrastructurePath.ReplaceLast("Infrastructure", "Application");
        public string UnitTestPath => InfrastructurePath.Replace("src","test").ReplaceLast("Infrastructure", "UnitTest");
        protected IEnumerable<string> TemplateFolders => 
            TemplateFoldersUtilities.GetTemplateFolders(
                    containingProject: this.GetType().GetTypeInfo().Assembly.GetName().Name,
                    applicationBasePath: InfrastructurePath,
                    baseFolders: GetBaseTemplateFolders(),
                    projectContext: _projectContext);
    }
}
