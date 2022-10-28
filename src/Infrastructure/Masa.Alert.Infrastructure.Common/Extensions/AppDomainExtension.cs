namespace Masa.Alert.Infrastructure.Common.Extensions;

public static class AppDomainExtension
{
    public static Assembly[] GetAllAssemblies(this AppDomain domain)
    {
        var folderPath = domain.BaseDirectory;

        var assemblyFiles = AssemblyUtils.GetAssemblyFiles(folderPath, SearchOption.TopDirectoryOnly);

        return assemblyFiles.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToArray();
    }
}
