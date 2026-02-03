using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Data
{
    public abstract class FileData
    {
        protected string _path;
        public FileMetadata Metadata;
        public string Name;
        public readonly string Type;

        public string Path => _path;

        protected FileData(string type)
        {
            Type = type;
        }

        protected FileData(string type, string path)
        {
            Type = type;
            _path = path;
        }

        public string GetFileName(bool includeExtension = true) => FileUtils.GetFileName(Path, includeExtension);

        public abstract void SetAsActive();

        public abstract void MoveToCloud();

        public abstract void MoveToLocal();
    }
}
