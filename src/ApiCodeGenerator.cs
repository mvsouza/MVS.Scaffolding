using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using MVS.Scaffolding.Models;

namespace MVS.Scaffolding
{
    [Alias("api")]
    public class ApiCodeGenerator : CustomCodeGenerator
    {
        public ApiCodeGenerator(
            IProjectContext projectContext,
            IApplicationInfo applicationInfo,
            ICodeGeneratorActionsService codeGeneratorActionsService,
            IServiceProvider serviceProvider,
            ILogger logger) : base(projectContext, applicationInfo, codeGeneratorActionsService, 
            serviceProvider, logger) { }
        
        public override string[] GetBaseTemplateFolders() => new []{ "Api" };
        
        public override IEnumerable<FileBoilerPlaitModel> GetFiles(string bundleName)
        {
            yield return new FileBoilerPlaitModel(InfrastructurePath, "Api", bundleName, "Controller", GeneratedFileExtension);
            yield return new FileBoilerPlaitModel(UnitTestPath, "Api", bundleName, "ControllerTests", GeneratedFileExtension);
        }

        public async Task GenerateCode(BaseModel model)
        {
            await base.GenerateCode(model);
        }
    }
}
