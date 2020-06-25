using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

namespace MVS.Scaffolding.Models
{
    /// <summary>
    /// This class defines the command line options to be used with the 'customaspnetcodegenerator'.
    /// </summary>
    public class BaseModel
    {
        [Option(Name="bundleName", ShortName="n", Description="Name of the bundle to be generated.")]
        public string ClassName { get; set; }

        [Option(Name="force", ShortName="f", Description="Specify this option to overwrite existing files.")]
        public bool Force { get; internal set; }
        public string Namespace { get; internal set; }
    }
}
