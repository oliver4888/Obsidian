using java.lang;
using java.net;

namespace org.bukkit.plugin.java
{
    public class PluginClassLoader : URLClassLoader
    {
        public PluginClassLoader(URL[] urls, ClassLoader parent) : base(urls, parent)
        {
        }

        public PluginClassLoader(URL[] urls) : base(urls)
        {
        }

        public PluginClassLoader(URL[] urls, ClassLoader parent, URLStreamHandlerFactory factory) : base(urls, parent, factory)
        {
        }
        
        
    }
}