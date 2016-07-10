using Cake.Core.IO;

namespace Cake.ServiceFabric.Extensions
{
    internal static class FilePathExtensions
    {
        internal static string FullPathQouted(this FilePath path)
        {
            return "\"" + path.FullPath.Replace("/", "\\") + "\"";
        }
    }
}
