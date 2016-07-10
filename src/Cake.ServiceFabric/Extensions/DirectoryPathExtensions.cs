using Cake.Core.IO;

namespace Cake.ServiceFabric.Extensions
{
    internal static class DirectoryPathExtensions
    {
        internal static string FullPathEscaped(this DirectoryPath path)
        {
            return path.FullPath.Replace("/", "\\");
        }
        
        internal static string FullPathQouted(this DirectoryPath path)
        {
            return "\"" + FullPathEscaped(path) + "\"";
        }
    }
}
