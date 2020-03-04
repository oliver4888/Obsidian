using java.lang;
using java.net;

namespace org.bukkit.plugin.java
{
    public class PluginClassLoader : URLClassLoader
    {
        private final JavaPluginLoader loader;
        private final Set<String> seenIllegalAccess = Collections.newSetFromMap(new ConcurrentHashMap<>());
        
        public PluginClassLoader(URL[] urls, ClassLoader parent) : base(urls, parent)
        {
        }

        public PluginClassLoader(URL[] urls) : base(urls)
        {
        }

        public PluginClassLoader(URL[] urls, ClassLoader parent, URLStreamHandlerFactory factory) : base(urls, parent, factory)
        {
            
        }

        public void initialize(JavaPlugin javaPlugin)
        {
            javaPlugin.init()
        }
        
        
    }
}