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
    [Alias("command")]
    public class CommandCodeGenerator : CustomCodeGenerator
    {
        public CommandCodeGenerator(
            IProjectContext projectContext,
            IApplicationInfo applicationInfo,
            ICodeGeneratorActionsService codeGeneratorActionsService,
            IServiceProvider serviceProvider,
            ILogger logger) : base(projectContext, applicationInfo, codeGeneratorActionsService, 
            serviceProvider, logger) { }
        
        public override string[] GetBaseTemplateFolders() => new []{ "Command" };
        
        public override IEnumerable<FileBoilerPlaitModel> GetFiles(string bundleName)
        {
            string appPath = ApplicationPath,
                testPath = $"{UnitTestPath}/Application";
            yield return new FileBoilerPlaitModel(appPath, "Command", bundleName, "Command", GeneratedFileExtension);
            yield return new FileBoilerPlaitModel(appPath, "Command", bundleName, "CommandHandler", GeneratedFileExtension);
            yield return new FileBoilerPlaitModel(appPath, "Validation", bundleName, "CommandValidation", GeneratedFileExtension);
            yield return new FileBoilerPlaitModel(testPath, "Validation", bundleName, "CommandValidationTests", GeneratedFileExtension);
            yield return new FileBoilerPlaitModel(testPath, "Command", bundleName, "CommandHandlerTests", GeneratedFileExtension);
        }

        public async Task GenerateCode(CommandModel model)
        {
            await base.GenerateCode(model);
        }
    }
}
