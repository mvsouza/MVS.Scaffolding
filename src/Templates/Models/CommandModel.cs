using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

namespace MVS.Scaffolding.Models
{
    /// <summary>
    /// This class defines the command line options to be used with the 'customaspnetcodegenerator'.
    /// </summary>
    public class CommandModel : BaseModel
    {
        [Option(Name="ReturnType", ShortName="r", Description="Command return type.")]
        public string ReturnType { get; set; }
    }
}
