using System;
using System.Linq;
using System.Reflection;

namespace Obsidian.Persistence
{
    public static class PersistenceProviderFactory
    {
        public static BasePersistenceProvider GetProvider(string name, PersistenceInfo info)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Type providerType = assembly.GetTypes().FirstOrDefault(t => t.Name == $"{name}PersistenceProvider") ?? typeof(BasePersistenceProvider);
            return (BasePersistenceProvider)Activator.CreateInstance(providerType, info);
        }
    }
}
