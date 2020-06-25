namespace MVS.Scaffolding
{
    public class FileBoilerPlaitModel
    {
        private string _bundleName;
        private string _class;
        private string _projectPath;
        private string _namespaceMember;
        private string _ext;
        public FileBoilerPlaitModel(string projectPath, string namespaceMember, string bundleName, string @class, string ext)
        {
            _projectPath = projectPath;
            _namespaceMember = namespaceMember;
            _class = @class;
            _ext = ext;
            _bundleName = bundleName;
        }
        public string @File => $"{_class}.cshtml";

        public string DestinationPath => $"{_projectPath}/{_namespaceMember}/{_bundleName}{_class}{_ext}";

    }
}